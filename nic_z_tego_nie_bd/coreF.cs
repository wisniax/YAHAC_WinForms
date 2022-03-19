using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace nic_z_tego_nie_bd
{
	public class coreF
	{
		//My new core funcion bcs i can
		public coreF()
		{
			//MainGui mainGui = new MainGui();
		//	var task1 = new Task(()=> MessageBox.Show("KOCHAM POP-UPY"));
		//	var task2 = new Task(() => MessageBox.Show("KOCHAM POP-UPY2"));
			var thread2 = new Thread(() => Application.Run(new MainGui()));
			thread2.Name = "MainThread";
			//var thread2 = new Thread(() => mainGui.Show());
		//	task1.Start();
		//	task2.Start();
			thread2.Start();
		//	task1.Wait();
			
		//	Task.Run(()=> MessageBox.Show("2.0"));
		}


		public static void showMessage(string strong)
		{
			MessageBox.Show(strong);
		}
	}

}
