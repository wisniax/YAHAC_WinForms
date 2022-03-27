using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace nic_z_tego_nie_bd.Properties
{

	public static class AllItemsREPO
	{
		static string repoURL = ("https://api.hypixel.net/resources/skyblock/items");
		public static ItemRepo itemRepo;
		public static Dictionary<string, List<Item>> rarityItemRepo { get; private set; }
		static AllItemsREPO()
		{
			itemRepo = new();
			rarityItemRepo = new();
			populateList();
			genRarityItemsRepo();
		}

		public static void populateList()
		{
			var httpCl = new HttpCliento();
			var repoTask = httpCl.GetAsync(repoURL);
			var repoCache = repoTask.Result.Content.ReadAsStringAsync();
			var repoString = repoCache.Result;	
			itemRepo = JsonSerializer.Deserialize<ItemRepo>(repoString);
			if (itemRepo.success != true) { Task.Delay(5000); populateList(); }
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
			public string tier  { get; set; }
		}

	}
}