using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

namespace nic_z_tego_nie_bd
{
	//AH FORM
	public partial class AuctionHouse : Form
	{
		public long guilastUpdated;
		public AuctionHouse()
		{
			guilastUpdated = 0;
			InitializeComponent();
			listBoxWrite();
		}
		public void listBoxWrite()
		{
			listViewItems.BeginUpdate();
			//listViewItems.Items.Clear();
			guilastUpdated = AuctionHouseInstance.ahCache.lastUpdated;
			if (Properties.AllItemsREPO.itemRepo.success != true) return;
			var sorted = AuctionHouseInstance.ahCache.items.OrderBy(x => Properties.AllItemsREPO.IDtoNAME(x.Key));
			foreach (var item in sorted)
			{
				var addedID = listViewItems.Items.Add(Properties.AllItemsREPO.IDtoNAME(item.Key));
				addedID.Tag = item.Key;
				switch (item.Value[0].tier)
				{
					case "SUPREME":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("55FFFF", 16)); //DIVINE? 55FFFF
						break;
					case "MYTHIC":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("FF55FF", 16)); //MYTHIC FF55FF
						break;
					case "LEGENDARY":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("FFAA00", 16)); //LEGENDARY FFAA00
						break;
					case "EPIC":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("AA00AA", 16)); //EPIC AA00AA
						break;
					case "RARE":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("5555FF", 16)); //RARE 5555FF
						break;
					case "UNCOMMON":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("55FF55", 16)); //UNCOMMON 55FF55
						break;
					case "VERY_SPECIAL":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("FF5555", 16)); //VERY SPECIAL FF5555
						break;
					case "SPECIAL":
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("FF5555", 16)); //SPECIAL FF5555
						break;
					default:
						addedID.ForeColor = Color.FromArgb(Convert.ToInt32("000000", 16)); //COMMON FFFFFF But its white so we use 000000
						break;
				}
			}
			listViewItems.EndUpdate();
			//listViewItems.Refresh();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if ((guilastUpdated != AuctionHouseInstance.ahCache.lastUpdated) && (AuctionHouseInstance.ahCache.success == true))
			{
				timer1.Stop();
				listBoxWrite();
				timer1.Start();
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			if (listViewItems.SelectedItems.Count == 0) return;
			listViewItems.Hide();
			timer1.Stop();
			guilastUpdated = 0;
			timer2.Start();
			listViewItemDetails.Show();
		}
		private void showExtraInfo()
		{
			listViewItemDetails.Items.Clear();
			//translate item name to id
			//wrong function bc im stupid... List must be switched to listView as it can store item tag (name--Id conversion is lossy) As such whole function have to be rebuilt
			var selectedItem = listViewItems.SelectedItems[0].Tag.ToString();

			//render item of given id to list
			try
			{
				var listedItems = AuctionHouseInstance.ahCache.items[selectedItem];
				listedItems.Sort((x, y) => x.starting_bid.CompareTo(y.starting_bid));
				foreach (var item in listedItems)
				{
					var ahitem = listViewItemDetails.Items.Add(item.item_name);
					ahitem.Tag = item.uuid;
					ahitem.SubItems.Add(item.starting_bid.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA")));
					ahitem.SubItems.Add(item.uuid);
				}
			}
			catch { return; }
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (AuctionHouseInstance.ahCache.lastUpdated != guilastUpdated)
			{
				guilastUpdated = AuctionHouseInstance.ahCache.lastUpdated;
				showExtraInfo();
			}
		}

		private void listViewItemDetails_DoubleClick(object sender, EventArgs e)
		{
			if (listViewItemDetails.SelectedItems == null) return;
			var selectedItems = listViewItemDetails.SelectedItems;
			if (selectedItems.Count != 1) return;
			string clipText = "/viewauction " + selectedItems[0].Tag;
			var thread = new Thread(() => Clipboard.SetText(clipText));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
		}


	}




	//AH MAIN CLASS
	public static class AuctionHouseInstance
	{
		public static auctionHouse ahCache;
		static AuctionHouseInstance()
		{
			ahCache = new auctionHouse();
			ahCache.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCache.lastUpdated = 0;
		}

		//Main AH caching function
		public static async Task refresh()
		{
			var ahFetcher = new AuctionHouseFetcher();
			var ahFetchTask = await ahFetcher.refresh();
			if (ahFetchTask == false) return;
			auctionHouse ahCacheTemp = new auctionHouse();
			ahCacheTemp.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCacheTemp.totalAuctions = ahFetcher.AHpages[0].totalAuctions;
			ahCacheTemp.lastUpdated = ahFetcher.AHpages[0].lastUpdated;
			ahCacheTemp.totalPages = ahFetcher.AHpages[0].totalPages;


			//get rid of not bin auctions and assign dictionary key //For now:)
			var tasks = new List<Task>();
			foreach (var onePage in ahFetcher.AHpages)
			{
				//https://stackoverflow.com/questions/17119075/do-you-have-to-put-task-run-in-a-method-to-make-it-async
				var task = Task.Run(() => prepPage(onePage));
				tasks.Add(task);
			}
			await Task.WhenAll(tasks);

			//merge to 1 list
			List<AuctionHouseFetcher.itemData> wholeAH = new List<AuctionHouseFetcher.itemData>();
			foreach (var i in ahFetcher.AHpages)
			{
				wholeAH.AddRange(i.auctions);
			}

			//move to dictionary
			List<AuctionHouseFetcher.itemData> existingItems;
			wholeAH.Sort((x, y) => x.dictKey.CompareTo(y.dictKey));
			foreach (var item in wholeAH)
			{
				if (ahCacheTemp.items.TryGetValue(item.dictKey, out existingItems) == true)
				{
					existingItems.Add(item);
				}
				else
				{
					ahCacheTemp.items.Add(item.dictKey, new List<AuctionHouseFetcher.itemData> { item });
				}
			}

			//Replace both objects
			ahCacheTemp.success = ahFetcher.AHpages[0].success;
			//var cos = from entry in ahCacheTemp.items orderby Properties.AllItemsREPO.IDtoNAME(entry.Key) ascending select entry; //ahCacheTemp.items.OrderByDescending(x => Properties.AllItemsREPO.IDtoNAME(x.Key));
			ahCache = ahCacheTemp;
		}

		//Function to prepare page for export to dictionary
		static void prepPage(AuctionHouseFetcher.AuctionHousePage onePage)
		{
			onePage.auctions.RemoveAll(vari => vari.bin == false);


			//All the reforges in game
			var swordReforges = new[] { "Gentle", "Odd", "Fast", "Fair", "Epic", "Sharp", "Sharp", "Spicy", 
				"Legendary", "Dirty", "Fabled", "Suspicious", "Gilded", "Warped", "Withered", "Bulky", 
				"Heroic", "Forceful"};

			var rodReforges = new[] { "Salty", "Treacherous", "Stiff", "Lucky" };

			var bowReforges = new[] { "Deadly", "Fine", "Grand", "Hasty", "Neat", "Rapid", "Unreal", "Awkward",
						"Rich", "Precise", "Spiritual", "Headstrong" };

			var armorReforges = new[] { "Clean", "Fierce", "Heavy", "Light", "Mythic", "Pure", "Smart", "Titanic",
						"Wise", "Perfect", "Necrotic", "Ancient", "Spiked", "Renowned", "Cubic", "Warped", "Reinforced",
						"Loving", "Ridiculous", "Empowered", "Giant", "Submerged", "Jaded", "Very", "Highly", "Extremely",
						"Thicc", "Absolutely", "Zealous", "Godly", "Candied", "Ancient", "Forceful" };

			var AccessoriesReforges = new[] { "Bizarre", "Itchy", "Ominous", "Pleasant", "Pretty", "Shiny", "Simple",
						"Strange", "Vivid", "Godly", "Demonic", "Forceful", "Hurtful", "Keen", "Strong", "Superior", "Unpleasant",
						"Zealous", "Silky", "Bloody", "Shaded", "Sweet" };

			var toolReforges = new[] { "Moil", "Toil", "Blessed", "Bountiful", "Magnetic", "Fruitful", "Refined", "Stellar",
						"Mithraic", "Auspicious", "Fleet", "Heated", "Ambered" };


			//Check each item in 1 page
			foreach (var item in onePage.auctions)
			{
				//set dictName for each item
				item.dictKey = item.item_name;

				//Set dict key for item with matching name of the same rarity
				item.dictKey = assignDictKey(item.item_name,item.tier);
				if (item.dictKey != item.item_name) continue;

				//Set dict key for item witch matching name of the 1 lower rarity
				switch (item.tier)
				{
					case "SUPREME":
						item.dictKey = assignDictKey(item.item_name, "MYTHIC");
						break;
					case "MYTHIC":
						item.dictKey = assignDictKey(item.item_name, "LEGENDARY");
						break;
					case "LEGENDARY":
						item.dictKey = assignDictKey(item.item_name, "EPIC");
						break;
					case "EPIC":
						item.dictKey = assignDictKey(item.item_name, "RARE");
						break;
					case "RARE":
						item.dictKey = assignDictKey(item.item_name, "UNCOMMON");
						break;
					case "UNCOMMON":
						item.dictKey = assignDictKey(item.item_name, "COMMON");
						break;
					case "VERY_SPECIAL":
						item.dictKey = assignDictKey(item.item_name, "SPECIAL");
						break;
					default:
						break;
				}
				if (item.dictKey != item.item_name) continue;

				//So the repo for this item does not exist? sadge just try anything then
				switch (item.category)
				{
					case "weapon":
						item.dictKey = Regex.Replace(item.dictKey, @"[^\u0020-\u007E]", string.Empty);
						item.dictKey = item.dictKey.Trim();
					
						if ((swordReforges.Any(s => item.dictKey.Contains(s)) == true) || (bowReforges.Any(s => item.dictKey.Contains(s)) == true))
						{
							int spcIndex = item.dictKey.IndexOf(' ');
							item.dictKey = item.dictKey.Remove(0, spcIndex + 1);
						}
					
						break;
					
					case "armor": //BUG: Wise Dragon Armor -> Dragon Armor --> Partialy solved (added pre item dict assign)
						item.dictKey = Regex.Replace(item.dictKey, @"[^\u0020-\u007E]", string.Empty);
						item.dictKey = item.dictKey.Trim();
					
						if (armorReforges.Any(s => item.dictKey.Contains(s)) == true)
						{
							int spcIndex = item.dictKey.IndexOf(' ');
							item.dictKey = item.dictKey.Remove(0, spcIndex + 1);
						}
						else if (item.dictKey.Contains("Not So") == true)
						{
							int spcIndex = item.dictKey.IndexOf(' ');
							spcIndex = item.dictKey.IndexOf(' ', spcIndex + 1);
							item.dictKey = item.dictKey.Remove(0, spcIndex + 1);
						}
					
						break;
					
					case "accessories":
						if (AccessoriesReforges.Any(s => item.dictKey.Contains(s)) == true)
						{
							int spcIndex = item.dictKey.IndexOf(' ');
							item.dictKey = item.dictKey.Remove(0, spcIndex + 1);
						}
					
						break;

					case "consumables":
						break;

					case "blocks":
						break;

					case "misc": //add tools reforge removal and "Even More" ref removal <-- not req anymore?
						item.dictKey = Regex.Replace(item.dictKey, @"[^\u0020-\u007E]", string.Empty);
						item.dictKey = item.dictKey.Trim();

						if (item.dictKey.Contains("[Lvl ") == true)
						{
							var index = item.dictKey.IndexOf(']');
							item.dictKey = item.dictKey.Remove(0, index + 1).TrimEnd();
						}

						break;
					default:
						item.dictKey = "Unsorted :(";
						break;
				}//ENDOF switch
				//var repoElem = Properties.AllItemsREPO.itemRepo.items.Find(matchID => matchID.name == item.dictKey);
				//if (repoElem != null) item.dictKey = repoElem.name;
			}//ENDOF foreach item
		}//ENDOF function

		private static string assignDictKey(string item_name, string rarity)
		{
			if (Properties.AllItemsREPO.rarityItemRepo.ContainsKey(rarity) == false) return item_name;
			foreach (var dictItem in Properties.AllItemsREPO.rarityItemRepo[rarity])
			{
				if (item_name.Contains(dictItem.name))
				{
					return dictItem.id;
				}
			}
			return item_name;
		}



		public struct auctionHouse
		{
			public bool success { get; set; }
			public UInt32 totalAuctions { get; set; }
			public UInt16 totalPages { get; set; }
			public long lastUpdated { get; set; }
			public Dictionary<string, List<AuctionHouseFetcher.itemData>> items { get; set; }
		}

	}




	public class AuctionHouseFetcher
	{
		private const string ahUrl = "https://api.hypixel.net/skyblock/auctions?page=";
		public List<AuctionHousePage> AHpages { get; set; }
		HttpCliento httpCliento;
		public long page1TimeStamp;
		public AuctionHouseFetcher()
		{
			httpCliento = new HttpCliento();
			page1TimeStamp = 0;
			AHpages = new List<AuctionHousePage>();
		}
		public async Task<bool> refresh()
		{//https://stackoverflow.com/questions/25009437/running-multiple-async-tasks-and-waiting-for-them-all-to-complete
		 //https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
			AHpages.Clear();
			var firstPage = await getAhPageAsync(0);
			if (firstPage.success == false) return false;

			AHpages.Add(firstPage);
			page1TimeStamp = AHpages[0].lastUpdated;
			int page1age = (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - page1TimeStamp);
			if (page1age < 55000)
			{

				//Crate list of TO DO tasks
				var tasks = new List<Task<AuctionHousePage>>();
				for (int i = 1; (i < AHpages[0].totalPages); i++)
				{
					var task = getAhPageAsync(i);
					tasks.Add(task);
				}

				//Handle them
				try { await Task.WhenAll(tasks); }
				catch { await Task.Delay(15000); return false; }

				//Parse collected data
				for (int i = 1; (i < AHpages[0].totalPages); i++)
				{
					AHpages.Add(tasks[i - 1].Result);
					if (AHpages[i].lastUpdated != page1TimeStamp) { await Task.Delay(10000); return false; }
				}
				return true;
			}
			else
			{//If the data was too old wait a bit and repeat the refresh Task then
				await Task.Delay(5000);
				long temppage1age = page1age;
				while (temppage1age == page1age)
				{
					var tempfirstPage = await getAhPageAsync(0);
					temppage1age = tempfirstPage.lastUpdated;
					await Task.Delay(5000);
				}
				await refresh();
				return true;
			}
		}

		private async Task<AuctionHousePage> getAhPageAsync(int page)
		{ //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-6-0
			var ahTask = await httpCliento.GetAsync(ahUrl + page.ToString());
			var cachedAhPage = ahTask.Content.ReadAsStringAsync();
			string toDes = cachedAhPage.Result;
			AuctionHousePage ahObj = JsonSerializer.Deserialize<AuctionHousePage>(toDes);
			return ahObj;
		}

		public struct AuctionHousePage
		{
			public bool success { get; set; }
			public UInt16 page { get; set; }
			public UInt16 totalPages { get; set; }
			public UInt32 totalAuctions { get; set; }
			public long lastUpdated { get; set; }
			public List<itemData> auctions { get; set; }
		}
		public class itemData
		{
			public string uuid { get; set; }
			public string auctioneer { get; set; }
			public UInt64 start { get; set; }
			public UInt64 end { get; set; }
			public string item_name { get; set; }
			public string tier { get; set; }
			public string category { get; set; }
			public string dictKey { get; set; }
			public UInt32 starting_bid { get; set; }
			public UInt32 highest_bid_amount { get; set; }
			public bool bin { get; set; }
		}
	}
}
