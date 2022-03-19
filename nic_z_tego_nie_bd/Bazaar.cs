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
		BazaarCheckup bazaarCheckup;
		public long guilastUpdated;
		public Bazaar(BazaarCheckup bazaarCheckup)
		{
			this.bazaarCheckup = bazaarCheckup;
			guilastUpdated = 0;
			InitializeComponent();
		}
		public void textBoxWrite()
		{
			decimal timeElapsed = (decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - bazaarCheckup.bazaarObj.lastUpdated) / 1000;
			textBox1.Clear();
			textBox1.Text = timeElapsed.ToString("F1")+" "+ HttpCliento.reqInLastMinute;
		}
		public void listWrite()
		{
			listBox1.BeginUpdate();
			listBox1.Items.Clear();
			foreach (var item in bazaarCheckup.bazaarObj.products)
			{
					listBox1.Items.Add(bazaarCheckup.bazaarObj.products[item.Key].product_id);
			}
			listBox1.EndUpdate();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			textBoxWrite();
			if (bazaarCheckup.bazaarObj.lastUpdated != guilastUpdated)
			{
				guilastUpdated = bazaarCheckup.bazaarObj.lastUpdated;
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
			listView1.Show();
		}
		private void showExtraInfo()
		{
			listView1.Items.Clear();
			for (int i = 0; i < bazaarCheckup.bazaarObj.products[listBox1.SelectedItem.ToString()].buy_summary.Count; i++)
			{
				var chuj = listView1.Items.Add(bazaarCheckup.bazaarObj.products[listBox1.SelectedItem.ToString()].buy_summary[i].amount.ToString());
				chuj.SubItems.Add(bazaarCheckup.bazaarObj.products[listBox1.SelectedItem.ToString()].buy_summary[i].pricePerUnit.ToString());
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			textBoxWrite();
			if (bazaarCheckup.bazaarObj.lastUpdated != guilastUpdated)
			{
				guilastUpdated = bazaarCheckup.bazaarObj.lastUpdated;
				showExtraInfo();
			}
		}
	}

	public class BazaarCheckup
	{
		HttpCliento httpCliento;
		string bzString;
		private string bzUrl = "https://api.hypixel.net/skyblock/bazaar";
		public BazaarObj bazaarObj = new BazaarObj();
		//constructor 
		public BazaarCheckup()
		{
			httpCliento = new HttpCliento();
			refresh();
		}
		public void refresh()
		{
			var bzTask = httpCliento.GetAsync(bzUrl);
			var cachedBz = bzTask.Result.Content.ReadAsStringAsync();
			bzString = cachedBz.Result;
			bazaarObj = deserializeBz(bzString);
		}
		private BazaarObj deserializeBz(string toDes)
		{ //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-6-0
			BazaarObj bazaarObj = JsonSerializer.Deserialize<BazaarObj>(toDes);
			return bazaarObj;
		}

		public struct BazaarObj
		{
			public bool success { get; set; }
			public long lastUpdated { get; set; }
			public Dictionary<string, BazaarItemDef> products { get; set; }
			//public object products { get; set; } []
		}
		public struct BazaarItemDef
		{
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
