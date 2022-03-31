using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nic_z_tego_nie_bd
{
	public partial class ItemCrafts : Form
	{
		public List<GuiCode.itemToCraftUC> itemsUi;
		public long timestampAH;
		public long timestampBZ;
		public ItemCrafts()
		{
			timestampAH = 0;
			timestampBZ = 0;
			itemsUi = new();
			InitializeComponent();
			renderAllUis();
		}
		private void renderAllUis()
		{
			List<GuiCode.itemToCraftUC> tempitemsUi = new();
			if (GuiCode.ItemsToCraft.items == null) return;
			foreach (var item in GuiCode.ItemsToCraft.items)//Multicore UserControls render cooming soon
			{
				var itemUC = new GuiCode.itemToCraftUC();
				itemUC.initialize(item);
				tempitemsUi.Add(itemUC);
			}
			itemsUi = tempitemsUi;
			var tempLocation = flowLayoutPanel1.VerticalScroll.Value;
			itemsUi.Sort((a,b)=>b.profit.CompareTo(a.profit));
			flowLayoutPanel1.Controls.Clear();
			foreach (var item in itemsUi)
			{
				flowLayoutPanel1.Controls.Add(item);
			}
			//flowLayoutPanel1.VerticalScroll.Value = tempLocation;
			flowLayoutPanel1.AutoScrollPosition = new Point(0,Math.Abs(tempLocation));
			timestampAH = AuctionHouseInstance.ahCache.lastUpdated;
			timestampBZ = BazaarCheckup.bazaarObj.lastUpdated;
		}
		private void refreshAllUis()
		{
			foreach (var item in itemsUi)
			{
				item.refreshData();
			}
			timestampAH = AuctionHouseInstance.ahCache.lastUpdated;
			timestampBZ = BazaarCheckup.bazaarObj.lastUpdated;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (timestampAH!= AuctionHouseInstance.ahCache.lastUpdated) //Check only for ah bc update takes too long and does not remember current pos :(
			{
				timer1.Stop();
				refreshAllUis();
				timer1.Start();
			}
		}
	}



}