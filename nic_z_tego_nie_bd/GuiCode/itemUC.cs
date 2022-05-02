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
using System.Drawing.Imaging;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class itemUC : UserControl
	{
		public delegate void HandleCalledEvent(string sender, MouseEvents whatsGoingOn);
		private static readonly object BrushLock = new object();
		HandleCalledEvent handleCalledEvent;
		public static TextureBrush enchantmentBrush;
		string item_id;
		public bool isGlowing;
		bool isMouseOver;
		Image image;
		Image nextImage;
		public itemUC()
		{
			InitializeComponent();
		}

		public void initialize(string item_id, HandleCalledEvent handleCalledEvent)
		{
			this.item_id = item_id;
			isGlowing = false;
			isMouseOver = false;
			preRenderImage();
			this.handleCalledEvent = handleCalledEvent;
		}

		void preRenderImage()
		{
			var referedItem = Properties.AllItemsREPO.IDtoITEM(item_id);
			string materialid = Properties.AllItemsREPO.IDtoMATERIAL(item_id).ToLower();
			if (Properties.AllItemsREPO.vanillaItems.ContainsKey(materialid)) pictureBox1.Image = (Image)(Properties.AllItemsREPO.vanillaItems[materialid].Texture.Clone());
			image = (Image)pictureBox1.Image.Clone();
			if (referedItem.glowing == true)
			{
				isGlowing = true;
				if (enchantmentBrush == null)
				{
					enchantmentBrush = new TextureBrush(Properties.UsefulConversions.ChangeImageOpacity(Properties.Resources.enchanted_item_glint, 0.3));
					enchantmentBrush.RotateTransform(30);
					//enchantmentBrush.ScaleTransform(0.5F, 0.5F);
				}
			}
		}
		//Move image a little
		public static void TickEnchBrush()
		{
			if (enchantmentBrush == null) return;
			lock (BrushLock)
			{
				enchantmentBrush.TranslateTransform(2, -3);
			}
		}

		public void redrawImageWithBrush() //TextureBrush encBrush
		{
			if (image == null) return;
			//https://stackoverflow.com/questions/16055667/graphics-drawimage-out-of-memory-exception 2nd answear
			Bitmap bitmap = new(image);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(bitmap, 0, 0);
			lock (BrushLock)
			{
				graphics.FillRectangle(enchantmentBrush, 0, 0, bitmap.Width, bitmap.Height);
			}
			if (isMouseOver == false) nextImage = bitmap;
			else
			{
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(69, Color.Azure)), 0, 0, bitmap.Width, bitmap.Height);
				nextImage = bitmap;
			}
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

		public void loadNextImage()
		{
			pictureBox1.Image = (Image)nextImage.Clone();
		}

		void refreshImage()
		{
			pictureBox1.Image = (Image)image.Clone();
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
			isMouseOver = false;
			refreshImage();
			handleCalledEvent(item_id, MouseEvents.Leave);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			handleCalledEvent(item_id, MouseEvents.LocationChanged);
		}
		private void pictureBox1_MouseHover(object sender, EventArgs e)
		{
			isMouseOver = true;
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
