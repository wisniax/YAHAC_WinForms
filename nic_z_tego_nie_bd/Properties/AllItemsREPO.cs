using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.IO.Compression;
using System.IO;

namespace nic_z_tego_nie_bd.Properties
{

	public static class AllItemsREPO
	{
		const string hypixelRepoURL = ("https://api.hypixel.net/resources/skyblock/items");
		public static ItemRepo itemRepo { get; private set; }
		public static Dictionary<string, VanillaItem> vanillaItems { get; private set; }
		public static Dictionary<string, List<Item>> rarityItemRepo { get; private set; }
		static AllItemsREPO()
		{
			itemRepo = new();
			rarityItemRepo = new();
			vanillaItems = new();
			populateVanillaList();
			assignVanillaTextures();
			populateList();
			genRarityItemsRepo();
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


		public struct ItemRepo
		{
			public bool success { get; set; }
			public long lastUpdated { get; set; }
			public List<Item> items { get; set; }
		}
		public class Item
		{
			public string id { get; set; }
			public string name { get; set; }
			public int durability { get; set; }
			public string tier  { get; set; }
			public string material { get; set; }
			public bool glowing { get; set; }
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

	//public static class MinecraftTextures
	//{
	//	public static Dictionary<string, Image> VanillaTextures { get; }
	//	static MinecraftTextures()
	//	{
	//		VanillaTextures = assignVanillaTextures();
	//	}

	//}
}