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
			var thread2 = new Thread(() => Application.Run(new MainGui()));
			thread2.Name = "MainThread";
			thread2.Start();
		}
	}

}
