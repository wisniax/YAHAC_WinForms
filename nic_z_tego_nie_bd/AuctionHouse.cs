using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace nic_z_tego_nie_bd
{
	//AH FORM
	public partial class AuctionHouse : Form
	{
		public AuctionHouse()
		{
			InitializeComponent();
		}
	}




	//AH MAIN CLASS
	public static class AuctionHouseInstance
	{
		static auctionHouse ahCache;
		static AuctionHouseInstance()
		{
			ahCache = new auctionHouse();
			ahCache.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
		}

		//Main AH caching function
		public static async Task refresh()
		{
			var ahFetcher = new AuctionHouseFetcher();
			await Task.Run(() => ahFetcher.refresh());
			auctionHouse ahCacheTemp = new auctionHouse();
			ahCacheTemp.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCacheTemp.success = ahFetcher.AHpages[0].success;
			ahCacheTemp.totalAuctions = ahFetcher.AHpages[0].totalAuctions;
			ahCacheTemp.lastUpdated = ahFetcher.AHpages[0].lastUpdated;
			ahCacheTemp.totalPages = ahFetcher.AHpages[0].totalPages;


			//get rid of not bin auctions //For now:)
			var tasks = new List<Task>();
			foreach (var onePage in ahFetcher.AHpages)
			{
				//https://stackoverflow.com/questions/17119075/do-you-have-to-put-task-run-in-a-method-to-make-it-async
				var task = Task.Run(()=> onePage.auctions.RemoveAll(vari => vari.bin == false));
				tasks.Add(task);
			}
			await Task.WhenAll(tasks);

			//merge to 1 list
			List<AuctionHouseFetcher.itemData> wholeAH = new List<AuctionHouseFetcher.itemData>();
			foreach (var i in ahFetcher.AHpages)
			{
				wholeAH.AddRange(i.auctions);
			}
			wholeAH.Sort((x,y)=>x.item_name.CompareTo(y.item_name));

			//Split to multiple lists
			//Repo: https://stackoverflow.com/questions/2697253/using-linq-to-group-a-list-of-objects-into-a-new-grouped-list-of-list-of-objects
			//Nahh not req rn
		
			//move to dictionary
			List<AuctionHouseFetcher.itemData> existingItems;
			foreach (var item in wholeAH)
			{
				if (ahCacheTemp.items.TryGetValue(item.item_name, out existingItems)==true)
				{
					existingItems.Add(item);
				}
				else
				{
					ahCacheTemp.items.Add(item.item_name,new List<AuctionHouseFetcher.itemData> { item });
				}
			}



			//ahCacheTemp.items = Enumerable.ToDictionary(wholeAH, (a) => (a.item_name), (a) => (a));
			

		}
		static void prepPages(AuctionHouseFetcher.AuctionHousePage onePage) //testing for void type should be Task<AuctionHouseFetcher.AuctionHousePage> if does not work
		{
			onePage.auctions.RemoveAll(vari => vari.bin == false);
			foreach (var item in onePage.auctions)
			{
				//Regex pattern = new Regex("[;,\t\r ]|[\n]{2}");
				string itemName = item.item_name;
				if (itemName.Contains("[Lvl ")==true)
				{
					var index = itemName.IndexOf(']');
					item.dictKey = itemName.Remove(0, index + 1);
				}
				else if (new[] { "Gentle", "Odd", "Fast" }.Any(c => itemName.Contains(c)) == true)
				{

				}
			}
		}




		public struct auctionHouse
		{
			public bool success { get; set; }
			public UInt32 totalAuctions { get; set; }
			public UInt16 totalPages { get; set; }
			public long lastUpdated { get; set; }
			public Dictionary<string, List<AuctionHouseFetcher.itemData>> items { get; set; }
		}



		//public struct AHitemdef
		//{
		//	public string base_name { get; set; }
		//	public string item_name { get; set; }
		//	public string uuid { get; set; }
		//}

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
		public async Task refresh()
		{//https://stackoverflow.com/questions/25009437/running-multiple-async-tasks-and-waiting-for-them-all-to-complete
		 //https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
			AHpages.Clear();
			var firstPage = await getAhPageAsync(0);
			AHpages.Add(firstPage);
			page1TimeStamp = AHpages[0].lastUpdated;
			int page1age = (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - page1TimeStamp);
			if (page1age < 57000)
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
				catch { await refresh(); return; }
				//Parse collected data
				for (int i = 1; (i < AHpages[0].totalPages); i++)
				{
					AHpages.Add(tasks[i - 1].Result);
					if (AHpages[i - 1].lastUpdated != page1TimeStamp) { await refresh(); return; }
				}
			}
			else
			{//If the data was too old wait a bit and repeat the refresh Task then
				await Task.Delay(10000);
				long temppage1age = page1age;
				while (temppage1age == page1age)
				{
					var tempfirstPage = await getAhPageAsync(0);
					temppage1age = tempfirstPage.lastUpdated;
					await Task.Delay(2000);
				}
				await refresh();
				return;
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
			public string dictKey { get; set; }
			public UInt32 starting_bid { get; set; }
			public UInt32 highest_bid_amount { get; set; }
			public bool bin { get; set; }
		}



	}
}
