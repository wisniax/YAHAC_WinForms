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
using System.Globalization;

namespace nic_z_tego_nie_bd
{
	public partial class Bazaar : Form
	{
		public List<GuiCode.itemUC> itemsUi;
		public long timestampBZ;
		private string selectedItem;
		bool success;
		public Bazaar()
		{
			timestampBZ = 0;
			itemsUi = new();
			InitializeComponent();
		}
		void renderAllItems()
		{
			List<GuiCode.itemUC> tempitemsUi = new();
			if (BazaarCheckup.bazaarObj.success != true) return;
			foreach (var item in BazaarCheckup.bazaarObj.products)
			{
				var itemUC = new GuiCode.itemUC();
				itemUC.initialize(item.Key, RenderItemName);
				tempitemsUi.Add(itemUC);
			}
			itemsUi = tempitemsUi;
			flowLayoutPanel1.Controls.Clear();
			foreach (var item in itemsUi)
			{
				flowLayoutPanel1.Controls.Add(item);

			}
			timestampBZ = BazaarCheckup.bazaarObj.lastUpdated;
			labelItemNameTip.BringToFront();
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
		private void RenderItemName(string sender_id, GuiCode.itemUC.MouseEvents mouseEvents)
		{
			switch (mouseEvents)
			{
				case GuiCode.itemUC.MouseEvents.Enter:
					labelItemNameTip.Enabled = true;
					labelItemNameTip.Text = Properties.AllItemsREPO.IDtoNAME(sender_id);
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
					OpenClickedItem(sender_id);
					break;
				case GuiCode.itemUC.MouseEvents.Leave:
					labelItemNameTip.Visible = false;
					labelItemNameTip.Enabled = false;
					break;
				default:
					break;
			}
		}
		private void OpenClickedItem(string item_id)
		{
			if (!BazaarCheckup.bazaarObj.products.ContainsKey(item_id)) return;
			selectedItem = item_id;
			flowLayoutPanel1.Hide();
			flowLayoutPanel1.Enabled = false;
			timer1.Stop();
			timestampBZ = 0;
			listView1.Enabled = true;
			listView1.Show();
			timer2.Start();
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			if (BazaarCheckup.bazaarObj.lastUpdated != timestampBZ)
			{
				if (timestampBZ == 0) renderAllItems();
			}
			GuiCode.itemUC.TickEnchBrush();
			foreach (var item in itemsUi.FindAll(a => a.isGlowing == true))
			{
				item.redrawImageWithBrush();
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (BazaarCheckup.bazaarObj.lastUpdated != timestampBZ)
			{
				timer2.Stop();
				showExtraInfo();
				timer2.Start();
			}
		}
		private void showExtraInfo()
		{
			if (!BazaarCheckup.bazaarObj.products.ContainsKey(selectedItem)) return;
			var bzItem = BazaarCheckup.bazaarObj.products[selectedItem];
			int offersAmount = bzItem.buy_summary.Count >= bzItem.sell_summary.Count ? bzItem.buy_summary.Count : bzItem.sell_summary.Count;
			listView1.Items.Clear();
			listView1.Groups.Clear();
			for (int i = 0; i < offersAmount; i++)
			{
				//Render buy offers
				ListViewItem listViewItem;
				if (i < bzItem.buy_summary.Count)
				{
					listViewItem = listView1.Items.Add("");
					listViewItem.SubItems.Add(bzItem.buy_summary[i].pricePerUnit.ToString("N1", CultureInfo.CreateSpecificCulture("fr-CA")));
					listViewItem.SubItems.Add(bzItem.buy_summary[i].amount.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA")));
				}
				else
				{
					listViewItem = listView1.Items.Add("");
					listViewItem.SubItems.Add("");
					listViewItem.SubItems.Add("");
				}

				//Render sell offers
				if (i < bzItem.sell_summary.Count)
				{
					listViewItem.SubItems.Add(bzItem.sell_summary[i].pricePerUnit.ToString("N1", CultureInfo.CreateSpecificCulture("fr-CA")));
					listViewItem.SubItems.Add(bzItem.sell_summary[i].amount.ToString("N0", CultureInfo.CreateSpecificCulture("fr-CA")));
				}
				else
				{
					listViewItem.SubItems.Add("");
					listViewItem.SubItems.Add("");
				}
			}
			timestampBZ = BazaarCheckup.bazaarObj.lastUpdated;
			listView1.BringToFront();
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
			var bazaarObjtemp = deserializeBz(bzString);
			if (Properties.AllItemsREPO.itemRepo.success != true) return;
			foreach (var item in bazaarObjtemp.products)
			{
				item.Value.product_name = Properties.AllItemsREPO.IDtoNAME(item.Value.product_id);
			}
			bazaarObj = bazaarObjtemp;
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
		public class BazaarItemDef
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
