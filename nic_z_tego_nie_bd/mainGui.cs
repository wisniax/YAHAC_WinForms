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
		Task taskBz;
		Thread taskAh;
		public MainGui()
		{
			taskBz = new Task(() => bazaarCheckup = new BazaarCheckup());
			//taskAh = new Thread(() => auctionHouseFetcher = new AuctionHouseFetcher());
			taskAh = new Thread(() => AuctionHouseInstance.refresh());
			//taskAh = new Thread(()=>AuctionHouseFetcher.refresh());
			taskAh.Name = "threadAH";
			taskAh.Start();
			taskBz.Start();
			//	bazaarCheckup = new BazaarCheckup();
			//	AuctionHouseFetcher auctionHouseFetcher = new AuctionHouseFetcher();
			InitializeComponent();
			taskBz.Wait();
			loadForm(new Bazaar(bazaarCheckup));
		}
		public void loadForm(object objForm)
		{
			taskBz.Wait();
			if (this.mainPanel.Controls.Count > 0) this.mainPanel.Controls.RemoveAt(0);
			Form form = objForm as Form;
			form.TopLevel = false;
			form.Dock = DockStyle.Fill;
			this.mainPanel.Controls.Add(form);
			this.mainPanel.Tag = form;
			form.Show();
		}
		int timerVar = 0;
		private void timer1_Tick(object sender, EventArgs e)
		{
			if ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - bazaarCheckup.bazaarObj.lastUpdated > 12000) && (timerVar <= 0))
			{
				timerVar = 10;
				Task.Run(()=> bazaarCheckup.refresh());
			}
			else timerVar--;
		}


		private void buttonBazaar_Click(object sender, EventArgs e)
		{
			loadForm(new Bazaar(bazaarCheckup));
		}

	}//END OF CLASS
}//END OF NAMESPACE

