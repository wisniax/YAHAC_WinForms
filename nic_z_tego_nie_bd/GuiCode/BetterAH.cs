using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class BetterAH : Form
	{
		private readonly object locker = new object();
		List<ItemToSearchFor> itemsToSearchFor;
		List<AuctionHouseFetcher.itemData> matchingItems;
		public List<GuiCode.itemUC> itemsUi;
		SoundPlayer soundPlayer;
		long lastCalculated;

		public BetterAH()
		{
			matchingItems = new();
			itemsUi = new();
			itemsToSearchFor = loadRecipes();
			if (itemsToSearchFor == null) itemsToSearchFor = new();
			if (Properties.Settings.Default.playSound)
			{
				soundPlayer = new(Properties.Resources.notify_sound);
			}
			InitializeComponent();
			timer1.Start();
		}

		static string Encode(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		void findMatchingItems()
		{   //THIS WAY FINDING ITEMS IS 12 times faster for me... Wonder whyy (Totally not 12 threads CPU)
			List<AuctionHouseFetcher.itemData> tempmatchingItems = new();
			if (itemsToSearchFor.Count == 0) { timer1.Stop(); return; }
			var tasks = new List<Task>();
			foreach (var item in itemsToSearchFor)
			{
				tasks.Add(Task.Run(() => checkIfItemsMatch(item, tempmatchingItems)));
			}
			Task.WaitAll(tasks.ToArray());
			tempmatchingItems.Sort((a, b) => a.starting_bid.CompareTo(b.starting_bid));
			matchingItems = tempmatchingItems;
			lastCalculated = AuctionHouseInstance.ahCache.lastUpdated;
		}

		void checkIfItemsMatch(ItemToSearchFor item, List<AuctionHouseFetcher.itemData> tempmatchingItems)
		{

			{
				//Get list of items on AH that match ID
				if (!AuctionHouseInstance.ahCache.items.ContainsKey(item.item_dictKey)) { return; }
				var itemsToSearchOn = AuctionHouseInstance.ahCache.items[item.item_dictKey];

				//Get the ones that match price and query
				foreach (var entry in itemsToSearchOn)
				{
					if (!entry.bin) continue;                           //Skip if not bin
					if (entry.starting_bid > item.maxPrice) continue;   //Skip if price's too high

					//Skip if lore doesn't contain text
					try
					{
						foreach (var text in item.searchQueries)
						{
							if (!entry.item_lore.Contains(text)) throw new Exception();
						}
					}
					catch (Exception)
					{
						continue;
					}

					//Finally add matching item to list
					lock (locker)
					{
						tempmatchingItems.Add(entry);
					}
				}
				//if (matchingItems.Count == 0) continue;					//test whether any items were added
			}
		}




		//Should be a seperate class not a copy paste but well gonna migrate to WPF soonTM anyway so why bother
		void renderAllItems()
		{
			List<GuiCode.itemUC> tempitemsUi = new();
			if (BazaarCheckup.bazaarObj.success != true) return;
			foreach (var item in matchingItems)
			{
				var itemUCC = new GuiCode.itemUC();
				itemUCC.initialize(item.dictKey, RenderItemName);
				itemUCC.Tag = item;
				tempitemsUi.Add(itemUCC);
			}
			itemsUi = tempitemsUi;
			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel1.Controls.AddRange(itemsUi.ToArray());
			//timestampBZ = BazaarCheckup.bazaarObj.lastUpdated;
			labelItemNameTip.BringToFront();
			if (Properties.Settings.Default.playSound) playSound();
			var cos = Properties.Settings.Default.easterEggs;
			if (Encode(Properties.Settings.Default.easterEggs) == "6582df3932a187c34d14e9dd9d47317732e675030f4663c043aa3692983609b9") JadeRald();
		}
		void playSound()
		{
			var lista = itemsToSearchFor.FindAll((a) => a.priority >= 1);
			foreach (var item in lista)
			{
				if (itemsUi.Exists((a) => a.item_id == item.item_dictKey))
				{
					soundPlayer.Play();
					return;
				}
			}
		}
		void JadeRald()
		{
			var lista = itemsToSearchFor.FindAll((a) => a.priority >= 5);
			lista.Sort((a, b) => b.priority.CompareTo(a.priority));
			foreach (var item in lista)
			{
				if (itemsUi.Exists((a) => a.item_id == item.item_dictKey))
				{
					var smth = itemsUi.Find((a) => a.item_id == item.item_dictKey);
					CopyToClipboard("/viewauction " + ((AuctionHouseFetcher.itemData)smth.Tag).uuid);
					return;
				}
			}
		}
		private Point CalcPointPosition(Control control)
		{
			Point OFFSET = new(24, 16);
			Point point = Cursor.Position;
			var relativePoint = this.PointToClient(point);
			relativePoint.Offset(OFFSET);
			var controlSize = this.Size;

			if (controlSize.Width < relativePoint.X + control.Size.Width) relativePoint.Offset((-2) * OFFSET.X - control.Width, 0);
			if (controlSize.Height < relativePoint.Y + control.Size.Height) relativePoint.Offset(0, (-2) * OFFSET.Y - control.Height);

			return relativePoint;
		}
		private void RenderItemName(itemUC sender, GuiCode.itemUC.MouseEvents mouseEvents)
		{
			switch (mouseEvents)
			{
				case GuiCode.itemUC.MouseEvents.Enter:
					labelItemNameTip.Enabled = true;
					labelItemNameTip.Text = Properties.AllItemsREPO.IDtoNAME(sender.item_id);
					labelItemNameTip.Location = CalcPointPosition(labelItemNameTip);
					labelItemNameTip.Refresh();
					labelItemNameTip.Visible = true;
					break;
				case GuiCode.itemUC.MouseEvents.LocationChanged:
					labelItemNameTip.Location = CalcPointPosition(labelItemNameTip);
					break;
				case GuiCode.itemUC.MouseEvents.Click:
					labelItemNameTip.Visible = false;
					labelItemNameTip.Enabled = false;
					CopyToClipboard("/viewauction " + ((AuctionHouseFetcher.itemData)sender.Tag).uuid);
					break;
				case GuiCode.itemUC.MouseEvents.Leave:
					labelItemNameTip.Visible = false;
					labelItemNameTip.Enabled = false;
					break;
				default:
					break;
			}
		}
		void CopyToClipboard(string str)
		{
			var thread = new Thread(() => Clipboard.SetText(str));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
		}


		public class ItemToSearchFor
		{
			public string item_dictKey { get; set; }
			public List<String> searchQueries { get; set; }
			public UInt32 maxPrice { get; set; }
			public UInt16 priority { get; set; }
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if ((lastCalculated != AuctionHouseInstance.ahCache.lastUpdated) && AuctionHouseInstance.ahCache.lastUpdated != 0)
			{
				findMatchingItems();
				renderAllItems();
			}
		}


		void saveRecipes()
		{
			var stronk = JsonSerializer.Serialize(itemsToSearchFor);
			Properties.Settings.Default.BetterAHQuery = stronk;
			Properties.Settings.Default.Save();
		}
		List<ItemToSearchFor> loadRecipes()
		{
			var stronk = JsonSerializer.Deserialize<List<ItemToSearchFor>>(Properties.Settings.Default.BetterAHQuery);
			return stronk;
		}

	}



	public class nbtReader
	{
		public nbtReader(string strong)
		{
			strong = @"H4sIAAAAAAAAAEWQ3U4bMRCFZxNKkwUVCXFZoeGn6g0pyfIT6F0IIJAaqCBVL6txPMlaWu9Gtpc2vEmfIO+RB0PMRmq58VjHZ75jnRigCZGJASCqQc3o6G8E7/pFmYcohnqgSR2aN0bzdUYTL66XGN5r46cZzZqw8q1w3BB1DdYWc3XhmJ8Zb2FvMe/+TDnHWVFiSk+cfw6oWARNlias0eSwK6aQMmbkAy7mdOLl7BYOrUAPqlUH26JYyglHhQ8eyTFqHjkmLww1g3V5l9XOJ5lf5B8fZfYLq0zO+NuEFE1g6yUNJQl2lm7VC8EZVQbG69KbIq9iLeclhgK25E7TaTZD+ufyFViCxot51r8fDO7vGrByR5ZhU8Q32GNKTkMMG1d/gqP/uo8hfmPVYVUtW6oKh0bVOGz0hsOH24sfw6tfjze9h0vBl6Xo+zwaj5LzRLXUqT5rHY8paVHntN1Sie50k3bC+kw1oBmMZR/ITuHDyeHxYZLg0dd2B78PAGqwerlsXOLgFXUUxnPsAQAA";
			strong = strong.Replace(@"\u003d", "="); //Must have bc HYPIXEL :)
			var byteArray = Convert.FromBase64String(strong);
			MemoryStream memoryStream = new(byteArray);
			GZipStream gZipStream = new(memoryStream, CompressionMode.Decompress, false);
			SharpNBT.TagReader tagReader = new(gZipStream, SharpNBT.FormatOptions.BigEndian);
			SharpNBT.TagContainer tag = tagReader.ReadTag() as SharpNBT.TagContainer;
			tag.ToJsonString();
		}
	}
}
