using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using nic_z_tego_nie_bd.GuiCode;

namespace nic_z_tego_nie_bd
{
	//https://www.tutorialsteacher.com/csharp/csharp-delegates
	//https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
	//https://docs.microsoft.com/pl-pl/dotnet/api/system.componentmodel.backgroundworker?view=net-6.0
	//https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.invoke?redirectedfrom=MSDN&view=windowsdesktop-6.0#System_Windows_Forms_Control_Invoke_System_Delegate_
	public partial class MainGui : Form
	{
		public AuctionHouseFetcher auctionHouseFetcher;
		//public Settings settings;
		//public static ITR.ItemTextureResolver itemTextureResolver;
		public MainGui()
		{
			if (Properties.Settings.Default.UpgradeRequired)
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.UpgradeRequired = false;
				Properties.Settings.Default.Save();
			}
			InitializeComponent();
			loadDefaultForm(Properties.Settings.Default.Starting_Ui);
			//if (Directory.Exists(@".\ITR_Cache.zip"))
			//{
			//	itemTextureResolver = new();
			//	var task = itemTextureResolver.Init();
			//	task.ContinueWith((_) => (doopy = true));
			//}
		}


		private void loadDefaultForm(string name)
		{
			switch (name)
			{
				case "Bazaar":
					loadForm(new Bazaar());
					break;
				case "AuctionHouse":
					loadForm(new AuctionHouse());
					break;
				case "Item Crafts":
					loadForm(new ItemCrafts());
					break;
				default:
					loadForm(new SettingsUi());
					break;
			}
		}
		public void loadForm(object objForm)
		{
			var asd = Properties.Settings.Default.Starting_Ui;
			if (this.mainPanel.Controls.Count > 0) this.mainPanel.Controls[0].Dispose();
			Form form = objForm as Form;
			form.TopLevel = false;
			form.Dock = DockStyle.Fill;
			this.mainPanel.Controls.Add(form);
			this.mainPanel.Tag = form;
			form.Show();
		}
		private async void timer1_Tick(object sender, EventArgs e)
		{
			if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - BazaarCheckup.bazaarObj.lastUpdated > 12000)
			{
				timerBZ.Stop();
				await Task.Run(() => BazaarCheckup.refresh());
				timerBZ.Start();
			}
		}
		private async void timerAH_Tick(object sender, EventArgs e)
		{
			if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - AuctionHouseInstance.ahCache.lastUpdated > 65000)
			{
				timerAH.Stop();
				try
				{
					await Task.Run(() => AuctionHouseInstance.refresh());
				}
				catch { }
				timerAH.Start();
			}
		}

		private void buttonAh_Click(object sender, EventArgs e)
		{
			loadForm(new AuctionHouse());
		}

		private void timerRefScreenTimer_Tick(object sender, EventArgs e)
		{
			decimal ahTime = (Decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - AuctionHouseInstance.ahCache.lastUpdated) / 1000;
			decimal bzTime = (Decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - BazaarCheckup.bazaarObj.lastUpdated) / 1000;
			int reqInLastMin = HttpCliento.reqInLastMinute;
			ahAgeBox.Text = ahTime.ToString("F1");
			bzAgeBox.Text = bzTime.ToString("F1");
			apiReqBox.Text = reqInLastMin.ToString();
		}

		private void buttonBazaar_Click(object sender, EventArgs e)
		{
			loadForm(new Bazaar());
		}

		private void buttonSettings_Click(object sender, EventArgs e)
		{
			loadForm(new SettingsUi());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			loadForm(new ItemCrafts());
		}
	}//END OF CLASS
}//END OF NAMESPACE

