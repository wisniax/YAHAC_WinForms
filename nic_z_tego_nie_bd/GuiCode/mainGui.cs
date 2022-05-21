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
using System.Configuration;
using System.Security.Cryptography;

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
		static public IAuctionHouse AHInstance { get; private set; }
		static public int timedif { get; set; }

		public MainGui()
		{
			timedif = 8000;
			//Update Settings file on update (so actually never)
			if (Properties.Settings.Default.UpgradeRequired)
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.UpgradeRequired = false;
				Properties.Settings.Default.Save();
			}
			if (MainGui.Encode(Properties.Settings.Default.easterEggs) == "6582df3932a187c34d14e9dd9d47317732e675030f4663c043aa3692983609b9") { AHInstance = new AuctionHouseAlpha(); timedif = 0; }
			else if (MainGui.Encode(Properties.Settings.Default.easterEggs) == "34e8a6a096eb19b29620a92d2b1c72c9df8b3f6e4ddfc9db8d52f4418965aae2") AHInstance = new AuctionHouseAlpha();
			else AHInstance = new AuctionHouseInstance();



			//Regular stuff
			InitializeComponent();
			loadDefaultForm(Properties.Settings.Default.Starting_Ui);
			//var cos = new GuiCode.nbtReader("CHUJ");
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
				case "Better AH":
					loadForm(new BetterAH());
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

		public static async Task awaitTasks()
		{
			await Task.Delay(Properties.Settings.Default.tasks);
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
				timerBZ.Stop();
				await Task.Run(() => BazaarCheckup.refresh());
				timerBZ.Start();
		}
		private async void timerAH_Tick(object sender, EventArgs e)
		{
			timerAH.Stop();
			var cos = await AHInstance.refresh();
			timerAH.Start();
		}

		private void buttonAh_Click(object sender, EventArgs e)
		{
			loadForm(new AuctionHouse());
		}

		private void timerRefScreenTimer_Tick(object sender, EventArgs e)
		{
			decimal ahTime = (Decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - AHInstance.ahCache.lastUpdated) / 1000;
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

		private void buttonBetterAh_Click(object sender, EventArgs e)
		{
			loadForm(new BetterAH());
		}

		private void ahAgeBox_DoubleClick(object sender, EventArgs e)
		{
			AHInstance.hardrefresh();
		}
		public static string Encode(string rawData)
		{
			// Create a SHA256   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}//END OF CLASS
}//END OF NAMESPACE

