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
		string item_id;
		Image image;
		public itemUC()
		{
		 	InitializeComponent();
		}

		public void initialize(string item_id)
		{
			this.item_id = item_id;
			renderImage();
			//ParseStaticData();
			//refreshData();
		}
		void renderImage()
		{
			//pictureBox1.Image = Properties.Resources.iconof;
			string materialid = Properties.AllItemsREPO.IDtoMATERIAL(item_id).ToLower();
			if (MinecraftTextures.VanillaTextures.ContainsKey(materialid)) pictureBox1.Image = (Image)MinecraftTextures.VanillaTextures[materialid].Clone();
			image = pictureBox1.Image;
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

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}



		private void pictureBox1_MouseHover(object sender, EventArgs e)
		{

		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			renderOverlay();
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			renderImage();
		}
	}
	public static class MinecraftTextures
	{
		public static Dictionary<string, Image> VanillaTextures { get;}
		static MinecraftTextures()
		{
			VanillaTextures = assignVanillaTextures();
		}
		private static Dictionary<string, Image> assignVanillaTextures()
		{
			var memoryStream = new MemoryStream(Properties.Resources.Minecraft_Textures_x32);
			var VanillaTexturesArch = new ZipArchive(memoryStream, ZipArchiveMode.Read);
			var images = VanillaTexturesArch.Entries.Where((a) => a.FullName.EndsWith(".png") && a.FullName.Contains("assets/minecraft/textures/items"));
			var tempTextures = new Dictionary<string, Image>();
			foreach (ZipArchiveEntry item in images)
			{
				var image = Image.FromStream(item.Open());
				tempTextures.Add(item.Name.Split('.')[0], image);
			}



			//var cosie = MinecraftVanillaTextures.GetEntry("assets/minecraft/mcpatcher/cit/armor/farm/icons/helm.png");
			//var wosie = cosie.Open();
			//var cos = Image.FromStream(wosie);
			return tempTextures;
		}
		public static void main()
		{

		}
	}
}
