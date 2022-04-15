using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.IO.Compression;
using System.IO;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class itemUC : UserControl
	{
		public delegate void HandleCalledEvent(string sender, MouseEvents whatsGoingOn);
		HandleCalledEvent handleCalledEvent;
		string item_id;
		Image image;
		public itemUC()
		{
		 	InitializeComponent();
		}

		public void initialize(string item_id, HandleCalledEvent handleCalledEvent)
		{
			this.item_id = item_id;
			renderImage();
			this.handleCalledEvent = handleCalledEvent;
		}
		void renderImage()
		{
			//pictureBox1.Image = Properties.Resources.iconof;
			string materialid = Properties.AllItemsREPO.IDtoMATERIAL(item_id).ToLower();
			if (Properties.AllItemsREPO.vanillaItems.ContainsKey(materialid)) pictureBox1.Image = (Image)(Properties.AllItemsREPO.vanillaItems[materialid].Texture.Clone());
			image = (Image)pictureBox1.Image.Clone();
		}
		void refreshImage()
		{
			pictureBox1.Image = (Image)image.Clone();
		}
		void renderOverlay()
		{
			if (pictureBox1.Image == null) return;
			Bitmap bitmap = (Bitmap)pictureBox1.Image;
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(bitmap, 0, 0);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(69, Color.Azure)), 0, 0, bitmap.Width, bitmap.Height);
			pictureBox1.Image = bitmap;
		}
		
		//
		//	Pass events to handle (to delegate)
		//
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			handleCalledEvent(item_id, MouseEvents.Click);
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			//renderOverlay();
			//handleCalledEvent(item_id, MouseEvents.Enter);
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			refreshImage();
			handleCalledEvent(item_id, MouseEvents.Leave);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			handleCalledEvent(item_id, MouseEvents.LocationChanged);
		}
		private void pictureBox1_MouseHover(object sender, EventArgs e)
		{
			renderOverlay();
			handleCalledEvent(item_id, MouseEvents.Enter);
		}

		//
		//	Objects
		// 
		public enum MouseEvents
		{
			Enter,
			LocationChanged,
			Click,
			Leave
		}
	}



}
