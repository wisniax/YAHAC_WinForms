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
	public partial class Bazaar : Form
	{
		public long guilastUpdated;
		public Bazaar()
		{
			guilastUpdated = 0;
			InitializeComponent();
		}
		public void listWrite()
		{
			listBox1.BeginUpdate();
			listBox1.Items.Clear();
			foreach (var item in BazaarCheckup.bazaarObj.products)
			{
					listBox1.Items.Add(BazaarCheckup.bazaarObj.products[item.Key].product_id);
			}
			listBox1.EndUpdate();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (BazaarCheckup.bazaarObj.lastUpdated != guilastUpdated)
			{
				guilastUpdated = BazaarCheckup.bazaarObj.lastUpdated;
				listWrite();
			}

		}
		private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBox1.SelectedItem == null) return;
			listBox1.Hide();
			timer1.Stop();
			guilastUpdated = 0;
			timer2.Start();
			listViewSellPrice.Show();
			listViewBuyPrice.Show();
		}
		private void showExtraInfo()
		{
			listViewSellPrice.Items.Clear();
			listViewBuyPrice.Items.Clear();
			var sell_summary = BazaarCheckup.bazaarObj.products[listBox1.SelectedItem.ToString()].sell_summary;
			var buy_summary = BazaarCheckup.bazaarObj.products[listBox1.SelectedItem.ToString()].buy_summary;
			foreach (var bzitem in sell_summary)
			{
				var listitem = listViewSellPrice.Items.Add(bzitem.amount.ToString());
				listitem.SubItems.Add(bzitem.pricePerUnit.ToString());
			}
			foreach (var bzitem in buy_summary)
			{
				var listitem = listViewBuyPrice.Items.Add(bzitem.amount.ToString());
				listitem.SubItems.Add(bzitem.pricePerUnit.ToString());
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (BazaarCheckup.bazaarObj.lastUpdated != guilastUpdated)
			{
				guilastUpdated = BazaarCheckup.bazaarObj.lastUpdated;
				showExtraInfo();
			}
		}
	}

	public static class BazaarCheckup
	{
		static private HttpCliento httpCliento;
		static private string bzString;
		static private string bzUrl = "https://api.hypixel.net/skyblock/bazaar";
		static public BazaarObj bazaarObj;

		//constructor 
		static BazaarCheckup()
		{
			httpCliento = new HttpCliento();
			bazaarObj = new BazaarObj();
		}
		static public void refresh()
		{
			var bzTask = httpCliento.GetAsync(bzUrl);
			var cachedBz = bzTask.Result.Content.ReadAsStringAsync();
			bzString = cachedBz.Result;
			bazaarObj = deserializeBz(bzString);
		}
		static private BazaarObj deserializeBz(string toDes)
		{ //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-6-0
			BazaarObj bazaarObj = JsonSerializer.Deserialize<BazaarObj>(toDes);
			return bazaarObj;
		}

		public struct BazaarObj
		{
			public bool success { get; set; }
			public long lastUpdated { get; set; }
			public Dictionary<string, BazaarItemDef> products { get; set; }
		}
		public struct BazaarItemDef
		{
			public string product_name { get; set; } //Translation from prod_id to item name requ
			public string product_id { get; set; }
			public List<BzOrders> sell_summary { get; set; }
			public List<BzOrders> buy_summary { get; set; }
			public Quick_status quick_status { get; set; }
		}
		public struct BzOrders
		{
			public UInt32 amount { get; set; }
			public decimal pricePerUnit { get; set; }
			public UInt16 orders { get; set; }

		}
		public struct Quick_status
		{
			public string productId { get; set; }
			public double sellPrice { get; set; }
			public UInt32 sellVolume { get; set; }
			public UInt32 sellMovingWeek { get; set; }
			public UInt16 sellOrders { get; set; }
			public double buyPrice { get; set; }
			public UInt32 buyVolume { get; set; }
			public UInt32 buyMovingWeek { get; set; }
			public UInt16 buyOrders { get; set; }
		}
	}
}
