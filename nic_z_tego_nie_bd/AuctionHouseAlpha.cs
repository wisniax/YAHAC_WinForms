using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nic_z_tego_nie_bd
{
	public interface IAuctionHouse
	{
		public AuctionHouseInstance.auctionHouse ahCache { get; set; }
		public Task<bool> refresh();
		public void hardrefresh();
		//public bool isInitialized { get; }
	}
	public class AuctionHouseAlpha : IAuctionHouse
	{
		private const string auctionsUrl = "https://api.hypixel.net/skyblock/auctions?page=";
		private const string auctions_endedUrl = "https://api.hypixel.net/skyblock/auctions_ended";
		public AuctionHouseInstance.auctionHouse ahCache { get; set; }
		public bool isInitialized { get; private set; }
		bool wholeAHGathered = false;
		HttpCliento httpCliento;
		const int tasksD = 5000;
		int floatingAge;
		public AuctionHouseAlpha()
		{
			floatingAge = 45000;
			httpCliento = new();
			isInitialized = false;
			ahCache = new();
			ahCache.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCache.lastUpdated = 0;
			ahCache.age = 0;
			//Task.Run(() => refresh());
		}
		public void hardrefresh()
		{
			wholeAHGathered = false;
		}
		public async Task<bool> fetchAllPages()
		{
			var ahFetcher = new AuctionHouseFetcher();
			var ahFetchTask = await ahFetcher.refresh();
			if (ahFetchTask == false) return false;
			AuctionHouseInstance.auctionHouse ahCacheTemp = new();
			ahCacheTemp.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCacheTemp.totalAuctions = ahFetcher.AHpages[0].totalAuctions;
			ahCacheTemp.lastUpdated = ahFetcher.AHpages[0].lastUpdated;
			ahCacheTemp.age = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			ahCacheTemp.totalPages = ahFetcher.AHpages[0].totalPages;


			//get rid of not bin auctions and assign dictionary key //For now:)
			var tasks = new List<Task>();
			foreach (var onePage in ahFetcher.AHpages)
			{
				//https://stackoverflow.com/questions/17119075/do-you-have-to-put-task-run-in-a-method-to-make-it-async
				var task = Task.Run(() => prepPageAlpha(onePage.auctions));
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
			return true;
		}
		public async Task<bool> refresh()
		{
			if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - ahCache.age <= floatingAge) && DateTimeOffset.Now.ToUnixTimeMilliseconds() - ahCache.lastUpdated <= 120000) return false;
			if (!wholeAHGathered) { while (!(await fetchAllPages())) ; wholeAHGathered = true; floatingAge = 45000; return false; } //If its first init or smth went rly wrong redownload whole ah and start anew
			var AHEndedPageTask = getAHEndedPageAsync();
			var AHEndedPage = await AHEndedPageTask;
			if ((AHEndedPage.lastUpdated == ahCache.lastUpdated) || !AHEndedPage.success) { floatingAge += 1000; return false; }
			floatingAge -= 250;
			var AHFirstPageTask = getAHPageAsync();
			if (AHEndedPage.lastUpdated - ahCache.lastUpdated >= 100000) { wholeAHGathered = false; return false; }
			var AHFirstPage = await AHFirstPageTask;
			if (!AHFirstPage.success) { await Task.Delay(900); return false; }
			if (AHEndedPage.lastUpdated != AHFirstPage.lastUpdated) { await Task.Delay(250); return false; }

			foreach (var auction in AHEndedPage.auctions)
			{
				var AHEnded_item_id = getIdFromNbtString(nbtReader(auction.item_bytes));
				int index = new();
				try
				{
					index = ahCache.items[AHEnded_item_id].FindIndex((a) => a.uuid == auction.auction_id);
				}
				catch (Exception)
				{
					continue;
				}
				if (index == -1) continue;
				ahCache.items[AHEnded_item_id].RemoveAt(index);
			}
			var newAuctionsList = AHFirstPage.auctions.FindAll((a) => a.start > ahCache.lastUpdated);
			prepPageAlpha(newAuctionsList);


			List<AuctionHouseFetcher.itemData> wholeAH = ahCache.items.SelectMany(d => d.Value).ToList();
			wholeAH.AddRange(newAuctionsList);
			AuctionHouseInstance.auctionHouse ahCacheTemp = new();
			ahCacheTemp.items = new Dictionary<string, List<AuctionHouseFetcher.itemData>>();
			ahCacheTemp.totalAuctions = AHFirstPage.totalAuctions;
			ahCacheTemp.lastUpdated = AHFirstPage.lastUpdated;
			ahCacheTemp.age = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			ahCacheTemp.totalPages = AHFirstPage.totalPages;
			//move to dictionary
			List<AuctionHouseFetcher.itemData> existingItems;
			int totalAuctions = wholeAH.Count();
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
			//never does bc non bin auctions are deleted sooo cant rly check whether i have good ah :( i hope so tho
			//bool doesMatch = totalAuctions == ahCacheTemp.totalAuctions;
			ahCacheTemp.success = AHFirstPage.success;
			//Waiting for async tasks to be completed
			await MainGui.awaitTasks();
			ahCache = ahCacheTemp;
			return true;
		}



		private async Task<AuctionHouseFetcher.AuctionHousePage> getAHPageAsync(int page = 0)
		{ //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-6-0
			var ahTask = await httpCliento.GetAsync(auctionsUrl + page.ToString());
			var cachedAhPage = ahTask.Content.ReadAsStringAsync();
			string toDes = cachedAhPage.Result;
			AuctionHouseFetcher.AuctionHousePage ahObj = JsonSerializer.Deserialize<AuctionHouseFetcher.AuctionHousePage>(toDes);
			return ahObj;
		}
		private async Task<Auctions_ended> getAHEndedPageAsync()
		{ //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-6-0
			var ahTask = await httpCliento.GetAsync(auctions_endedUrl);
			var cachedAhPage = ahTask.Content.ReadAsStringAsync();
			string toDes = cachedAhPage.Result;
			Auctions_ended ahObj = JsonSerializer.Deserialize<Auctions_ended>(toDes);
			return ahObj;
		}

		void prepPageAlpha(List<AuctionHouseFetcher.itemData> onePage)
		{
			onePage.RemoveAll(vari => vari.bin == false);
			foreach (var item in onePage)
			{
				if (item.item_name == "null") { item.dictKey = "null"; continue; }
				item.dictKey = getIdFromNbtString(nbtReader(item.item_bytes));
			}
		}
		//Function to prepare page for export to dictionary
		[Obsolete]
		void prepPage(AuctionHouseFetcher.AuctionHousePage onePage)
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
				item.dictKey = assignDictKey(item.item_name, item.tier);
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
							item.dictKey = item.dictKey.Remove(0, index + 1).Trim();
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
		public class Item
		{
			public string auction_id { get; set; }
			//public string seller { get; set; }
			//public string seller_profile { get; set; }
			//public string buyer { get; set; }
			public long timestamp { get; set; }
			//public int price { get; set; }
			public bool bin { get; set; }
			public string item_bytes { get; set; }
		}

		public class Auctions_ended
		{
			public bool success { get; set; }
			public long lastUpdated { get; set; }
			public List<Item> auctions { get; set; }
		}

		public string nbtReader(string strong)
		{
			//strong = @"H4sIAAAAAAAAAEWQ3U4bMRCFZxNKkwUVCXFZoeGn6g0pyfIT6F0IIJAaqCBVL6txPMlaWu9Gtpc2vEmfIO+RB0PMRmq58VjHZ75jnRigCZGJASCqQc3o6G8E7/pFmYcohnqgSR2aN0bzdUYTL66XGN5r46cZzZqw8q1w3BB1DdYWc3XhmJ8Zb2FvMe/+TDnHWVFiSk+cfw6oWARNlias0eSwK6aQMmbkAy7mdOLl7BYOrUAPqlUH26JYyglHhQ8eyTFqHjkmLww1g3V5l9XOJ5lf5B8fZfYLq0zO+NuEFE1g6yUNJQl2lm7VC8EZVQbG69KbIq9iLeclhgK25E7TaTZD+ufyFViCxot51r8fDO7vGrByR5ZhU8Q32GNKTkMMG1d/gqP/uo8hfmPVYVUtW6oKh0bVOGz0hsOH24sfw6tfjze9h0vBl6Xo+zwaj5LzRLXUqT5rHY8paVHntN1Sie50k3bC+kw1oBmMZR/ITuHDyeHxYZLg0dd2B78PAGqwerlsXOLgFXUUxnPsAQAA";
			strong = strong.Replace(@"\u003d", "="); //Must have bc HYPIXEL :)
			var byteArray = Convert.FromBase64String(strong);
			MemoryStream memoryStream = new(byteArray);
			GZipStream gZipStream = new(memoryStream, CompressionMode.Decompress, false);
			SharpNBT.TagReader tagReader = new(gZipStream, SharpNBT.FormatOptions.BigEndian);
			SharpNBT.TagContainer tag = tagReader.ReadTag() as SharpNBT.TagContainer;
			return tag.Stringify();
		}

		public string getIdFromNbtString(string nbtString)
		{
			string itemId = Regex.Match(nbtString, "\"?\\bid\\b\"?: ?\"([A-Z_:0-9]+)\"").Groups[1].Value;
			return itemId;
			//nbtString = nbtString.Replace("uuid", "uuha");
			//nbtString = nbtString.Replace("mob_id", "mob_ha");
			//var splitted = nbtString.Split("ExtraAttributes");
			//splitted = splitted[1].Split("id: ");
			//splitted = splitted[1].Split("\"");
			//return splitted[1];
		}
	}
}
