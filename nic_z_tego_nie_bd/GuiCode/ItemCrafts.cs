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

namespace nic_z_tego_nie_bd
{
	public partial class ItemCrafts : Form
	{
		public List<GuiCode.itemToCraftUC> itemsUi;
		public long timestamp;
		public ItemCrafts()
		{
			timestamp = 0;
			itemsUi = new();
			InitializeComponent();
			renderAllUis();
		}
		private void renderAllUis()
		{
			try
			{
				foreach (var item in ItemsToCraft.items)
				{
					var itemUC = new GuiCode.itemToCraftUC();
					itemUC.initialize(item);
					itemsUi.Add(itemUC);
				}
				flowLayoutPanel1.Controls.Clear();
				foreach (var item in itemsUi)
				{
					flowLayoutPanel1.Controls.Add(item);
				}
			}
			catch { return; }
		}
	}



	//Get item prices
	public static class ItemsToCraft
	{
		public static List<ItemRecipe> items;
		static ItemsToCraft()
		{
			items = loadRecipes();
		}




		public static void refresh()
		{

		}


		//Load/Save recipes to memory
		public static void saveRecipes(List<ItemRecipe> toSave)
		{
			var stronk = JsonSerializer.Serialize(toSave);
			Properties.Settings.Default.items = stronk;
			Properties.Settings.Default.Save();
		}
		public static List<ItemRecipe> loadRecipes()
		{
			var stronk = JsonSerializer.Deserialize<List<ItemRecipe>>(Properties.Settings.Default.items);
			return stronk;
		}



		//To store item recipes
		public struct ItemRecipe
		{
			public string item_name { get; set; }
			public string selldictKey { get; set; }
			public Source sellTo { get; set; }
			public List<reqItem> reqItems { get; set; }
		}
		public struct reqItem
		{
			public string item_name { get; set; }
			public string item_dictKey { get; set; }
			public Source source { get; set; }
			public UInt32 amount { get; set; }
		}
		public enum Source
		{
			Bazaar,
			AuctionHouse
		}
	}
}
