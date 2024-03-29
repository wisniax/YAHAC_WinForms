﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Drawing.Imaging;
using System.Configuration;

namespace nic_z_tego_nie_bd.Properties
{

	public static class AllItemsREPO
	{
		const string hypixelRepoURL = ("https://api.hypixel.net/resources/skyblock/items");
		public static ItemRepo itemRepo { get; private set; }
		public static Dictionary<string, VanillaItem> vanillaItems { get; private set; }
		public static Dictionary<string, List<Item>> rarityItemRepo { get; private set; }
		public static ITR.ItemTextureResolver itemTextureResolver { get; private set; }
		static AllItemsREPO()
		{

			//Some stuff for cute skins:)
			string PathToCacheFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
			PathToCacheFile = PathToCacheFile.Remove(PathToCacheFile.LastIndexOf('\\'));
			itemTextureResolver = new();
			itemTextureResolver.FastInit(PathToCacheFile + @"\ITR_Cache.zip");
			itemTextureResolver.LoadResourcepack(Properties.Resources.Wolds_and_Beyond_x16);

			itemRepo = new();
			rarityItemRepo = new();
			vanillaItems = new();
			populateVanillaList();
			//assignVanillaTextures();
			populateList();
			genRarityItemsRepo();
			assignCoolTextures();
		}

		private static void populateList()
		{
			var httpCl = new HttpCliento();
			var repoTask = httpCl.GetAsync(hypixelRepoURL);
			var repoCache = repoTask.Result.Content.ReadAsStringAsync();
			var repoString = repoCache.Result;	
			itemRepo = JsonSerializer.Deserialize<ItemRepo>(repoString);
			if (itemRepo.success != true) { Task.Delay(5000); populateList(); }
		}
		private static void populateVanillaList()
		{
			vanillaItems = new();
			var vanillaItemsList = JsonSerializer.Deserialize<List<VanillaItem>>(Properties.Resources.DetailedVanillaItemsInfo);
			foreach (var item in vanillaItemsList)
			{
				string itemId = item.text_type.ToString() + (item.meta != 0 ? (":" + item.meta.ToString()) : "");
				vanillaItems.Add(itemId, item);
			}
		}

		private static void genRarityItemsRepo()
		{
			foreach (var item in itemRepo.items)
			{
				List<Item> existingItems;
				if (item.tier == null) { item.tier = "COMMON"; }
				if (rarityItemRepo.TryGetValue(item.tier, out existingItems) == true)
				{
					existingItems.Add(item);
				}
				else
				{
					rarityItemRepo.Add(item.tier, new List<Item> { item });
				}
			}
		}
		private static void assignCoolTextures()
		{
			foreach (var item in AllItemsREPO.itemRepo.items)
			{
				item.Texture = itemTextureResolver.GetItemFromID(item.id).Texture;
			}
		}

		[Obsolete("There's a new one :)")]
		private static void assignVanillaTextures()
		{
			var memoryStream = new MemoryStream(Properties.Resources.VanillaItemTextures);
			var VanillaTexturesArch = new ZipArchive(memoryStream, ZipArchiveMode.Read);
			var images = VanillaTexturesArch.Entries.Where((a) => a.FullName.EndsWith(".png"));
			//foreach (ZipArchiveEntry item in images)
			//{
			//	var image = Image.FromStream(item.Open());
			//	//tempTextures.Add(item.Name.Split('.')[0], image);
			//}
			foreach (var item in AllItemsREPO.vanillaItems)
			{
				string textureName = item.Value.type.ToString() + "-" + item.Value.meta.ToString() + ".png";
				var entry = images.FirstOrDefault(a => a.Name == textureName);
				item.Value.Texture = Image.FromStream(entry.Open());
			}



			//var cosie = MinecraftVanillaTextures.GetEntry("assets/minecraft/mcpatcher/cit/armor/farm/icons/helm.png");
			//var wosie = cosie.Open();
			//var cos = Image.FromStream(wosie);
		}



		//item_dictKey --> item_NAME conversion
		public static string IDtoNAME(string itemID)
		{
			var repoElem = Properties.AllItemsREPO.itemRepo.items.Find(matchID => matchID.id == itemID);
			if (repoElem != null) return repoElem.name;
			else return itemID;
		}
		public static string IDtoMATERIAL(string itemID)
		{
			var repoElem = Properties.AllItemsREPO.itemRepo.items.Find(matchID => matchID.id == itemID);
			if (repoElem != null) 
			{
				if (repoElem.durability==0)	return repoElem.material; 
				else
				{
					string material = repoElem.material + ":" + repoElem.durability.ToString();
					return material;
				}
			}
			else return itemID;
		}

		//item_dictKey --> Item conversion
		public static Item IDtoITEM(string itemID)
		{
			Item item = itemRepo.items.Find(matchID => matchID.id == itemID);
			if (item != null)
			return item;
			else return new Item();
		}

		/// <summary>
		/// List of items
		/// </summary>
		public struct ItemRepo
		{
			public bool success { get; set; }
			public long lastUpdated { get; set; }
			public List<Item> items { get; set; }
		}

		/// <summary>
		/// Specific item in list.<br/>
		/// </summary>
		public class Item
		{
			public string id { get; set; }
			public string name { get; set; }
			public int durability { get; set; }
			public string tier  { get; set; }
			public string material { get; set; }
			public bool glowing { get; set; }
			public Image Texture { get; set; }
		}
		public class VanillaItem
		{
			public int type { get; set; }
			public int meta { get; set; }
			public string name { get; set; }
			public string text_type { get; set; }
			public Image Texture { get; set; }
		}

	}

	public static class UsefulConversions
	{
		private const int bytesPerPixel = 4;

		/// <summary>
		/// Change the opacity of an image
		/// </summary>
		/// <param name="originalImage">The original image</param>
		/// <param name="opacity">Opacity, where 1.0 is no opacity, 0.0 is full transparency</param>
		/// <returns>The changed image</returns>
		public static Image ChangeImageOpacity(Image originalImage, double opacity)
		{
			if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
			{
				// Cannot modify an image with indexed colors
				return originalImage;
			}

			Bitmap bmp = (Bitmap)originalImage.Clone();

			// Specify a pixel format.
			PixelFormat pxf = PixelFormat.Format32bppArgb;

			// Lock the bitmap's bits.
			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			// This code is specific to a bitmap with 32 bits per pixels 
			// (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
			int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
			byte[] argbValues = new byte[numBytes];

			// Copy the ARGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

			// Manipulate the bitmap, such as changing the
			// RGB values for all pixels in the the bitmap.
			for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
			{
				// argbValues is in format BGRA (Blue, Green, Red, Alpha)

				// If 100% transparent, skip pixel
				if (argbValues[counter + bytesPerPixel - 1] == 0)
					continue;

				int pos = 0;
				pos++; // B value
				pos++; // G value
				pos++; // R value

				argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
			}

			// Copy the ARGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

			return bmp;
		}

	}

	//public static class MinecraftTextures
	//{
	//	public static Dictionary<string, Image> VanillaTextures { get; }
	//	static MinecraftTextures()
	//	{
	//		VanillaTextures = assignVanillaTextures();
	//	}

	//}
}