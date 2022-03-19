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

namespace nic_z_tego_nie_bd
{
	//https://www.tutorialsteacher.com/csharp/csharp-delegates
	//https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
	//https://docs.microsoft.com/pl-pl/dotnet/api/system.componentmodel.backgroundworker?view=net-6.0
	//https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.invoke?redirectedfrom=MSDN&view=windowsdesktop-6.0#System_Windows_Forms_Control_Invoke_System_Delegate_
	public partial class MainGui : Form
	{
		public BazaarCheckup bazaarCheckup;
		public AuctionHouseFetcher auctionHouseFetcher;
		Task taskBz, taskAh;
		public MainGui()
		{
			taskBz = new Task(() => bazaarCheckup = new BazaarCheckup());
			//taskAh = new Task(() => AuctionHouseInstance.refresh());
			//taskAh.Start();
			taskBz.Start();
			InitializeComponent();
			taskBz.Wait();
			loadForm(new Bazaar(bazaarCheckup));
		}
		public void loadForm(object objForm)
		{
			if (this.mainPanel.Controls.Count > 0) this.mainPanel.Controls.RemoveAt(0);
			Form form = objForm as Form;
			form.TopLevel = false;
			form.Dock = DockStyle.Fill;
			this.mainPanel.Controls.Add(form);
			this.mainPanel.Tag = form;
			form.Show();
		}
		private async void timer1_Tick(object sender, EventArgs e)
		{
			if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - bazaarCheckup.bazaarObj.lastUpdated > 12000)
			{
				timerBZ.Stop();
				await Task.Run(()=> bazaarCheckup.refresh());
				timerBZ.Start();
			}
		}
		private async void timerAH_Tick(object sender, EventArgs e)
		{
			if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - AuctionHouseInstance.ahCache.lastUpdated > 65000) 
			{
				timerAH.Stop();
				await Task.Run(()=>AuctionHouseInstance.refresh());
				timerAH.Start();
			}
		}

		private void buttonAh_Click(object sender, EventArgs e)
		{
			//taskAh.Wait();
			loadForm(new AuctionHouse());
		}

		private void timerRefScreenTimer_Tick(object sender, EventArgs e)
		{
			decimal ahTime = (Decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - AuctionHouseInstance.ahCache.lastUpdated) / 1000;
			decimal bzTime = (Decimal)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - bazaarCheckup.bazaarObj.lastUpdated) / 1000;
			int reqInLastMin = HttpCliento.reqInLastMinute;
			ahAgeBox.Text = ahTime.ToString("F1");
			bzAgeBox.Text = bzTime.ToString("F1");
			apiReqBox.Text = reqInLastMin.ToString();
		}

		private void buttonBazaar_Click(object sender, EventArgs e)
		{
			taskBz.Wait();
			loadForm(new Bazaar(bazaarCheckup));
		}

	}//END OF CLASS
}//END OF NAMESPACE

