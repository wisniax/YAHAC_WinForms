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
			listBox1.BeginUpdate();
			listBox1.Items.Clear();
			guilastUpdated = AuctionHouseInstance.ahCache.lastUpdated;
			foreach (var item in AuctionHouseInstance.ahCache.items)
			{
				listBox1.Items.Add(item.Key);
			}
			listBox1.EndUpdate();
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

		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem == null) return;
			listBox1.Hide();
			timer1.Stop();
			guilastUpdated = 0;
			timer2.Start();
			listViewItemDetails.Show();
		}
		private void showExtraInfo()
		{
			listViewItemDetails.Items.Clear();
			var listedItems = AuctionHouseInstance.ahCache.items[listBox1.SelectedItem.ToString()];
			listedItems.Sort((x, y) => x.starting_bid.CompareTo(y.starting_bid));
			foreach (var item in listedItems)
			{
				var ahitem = listViewItemDetails.Items.Add(item.item_name);
				ahitem.Tag = item.uuid;
				ahitem.SubItems.Add(item.starting_bid.ToString());
				ahitem.SubItems.Add(item.uuid);
			}
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
			//await Task.Run(() => ahFetcher.refresh());
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
				//var task = Task.Run(() => onePage.auctions.RemoveAll(vari => vari.bin == false));
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

			//Split to multiple lists
			//Repo: https://stackoverflow.com/questions/2697253/using-linq-to-group-a-list-of-objects-into-a-new-grouped-list-of-list-of-objects
			//Nahh not req rn

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
			ahCacheTemp.success = ahFetcher.AHpages[0].success;
			ahCache = ahCacheTemp;


			//ahCacheTemp.items = Enumerable.ToDictionary(wholeAH, (a) => (a.item_name), (a) => (a));


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

					case "armor":
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

					case "misc": //add tools reforge removal and "Even More" ref removal
						if (item.dictKey.Contains("[Lvl ") == true)
						{
							var index = item.dictKey.IndexOf(']');
							item.dictKey = item.dictKey.Remove(0, index + 2);
						}

						break;
					default:
						item.dictKey = "Unsorted :(";
						break;
				}//ENDOF switch
			}//ENDOF foreach item
		}//ENDOF function




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
			AHpages = new List<AuctionHousePage>(); //new AuctionHousePage[100]
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
					tasks.Add(task);                                                //THOSE DO NOT WORK -->tasks.Add(new Task(()=>getAhPage(i)));	//(new Task(() => AHpages.Add(getAhPage(i))));
				}
				//Handle them
				try { await Task.WhenAll(tasks); }
				catch { await Task.Delay(5000); return false; }
				//Parse collected data
				for (int i = 1; (i < AHpages[0].totalPages); i++)
				{
					AHpages.Add(tasks[i - 1].Result);
					if (AHpages[i].lastUpdated != page1TimeStamp) { await Task.Delay(5000); return false; }
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
			//AHpages[page] = JsonSerializer.Deserialize<AuctionHousePage>(toDes);
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
			public string category { get; set; }
			public string dictKey { get; set; }
			public UInt32 starting_bid { get; set; }
			public UInt32 highest_bid_amount { get; set; }
			public bool bin { get; set; }
		}
	}
}
