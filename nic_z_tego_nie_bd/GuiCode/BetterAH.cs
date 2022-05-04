using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class BetterAH : Form
	{
		List<ItemToSearchFor> itemsToSearchFor;
		public BetterAH()
		{
			itemsToSearchFor = new();
			InitializeComponent();
			comboBoxItemSelect.DisplayMember = "name";
			comboBoxItemSelect.DataSource = Properties.AllItemsREPO.itemRepo.items;
		}


		class ItemToSearchFor
		{
			public string item_dictKey { get; set; }
			public List<String> searchQueries { get; set; }
			public UInt32 maxPrice { get; set; }
		}







		//ADDING ITEMS :)
		private void button1_Click(object sender, EventArgs e)
		{
			var item = new ItemToSearchFor();
			item.searchQueries = new();
			item.item_dictKey = ((Properties.AllItemsREPO.Item)comboBoxItemSelect.SelectedItem).id;
			itemsToSearchFor.Add(item);
			comboBoxItemSelect.Enabled = false;
			button1.Enabled = false;
			buttonAdd.Enabled = true;
		}
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			var item = itemsToSearchFor.Last();
			item.searchQueries.Add(textBoxString.Text);
			buttonSave.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var item = itemsToSearchFor.Last();
			item.maxPrice = (UInt32)numericUpDown1.Value;
			comboBoxItemSelect.Enabled = true;
			button1.Enabled = true;
			buttonAdd.Enabled = false;
			buttonSave.Enabled = false;
		}
	}















	public class nbtReader
	{
		public nbtReader(string strong)
		{
			strong = @"H4sIAAAAAAAAAEWQ3U4bMRCFZxNKkwUVCXFZoeGn6g0pyfIT6F0IIJAaqCBVL6txPMlaWu9Gtpc2vEmfIO+RB0PMRmq58VjHZ75jnRigCZGJASCqQc3o6G8E7/pFmYcohnqgSR2aN0bzdUYTL66XGN5r46cZzZqw8q1w3BB1DdYWc3XhmJ8Zb2FvMe/+TDnHWVFiSk+cfw6oWARNlias0eSwK6aQMmbkAy7mdOLl7BYOrUAPqlUH26JYyglHhQ8eyTFqHjkmLww1g3V5l9XOJ5lf5B8fZfYLq0zO+NuEFE1g6yUNJQl2lm7VC8EZVQbG69KbIq9iLeclhgK25E7TaTZD+ufyFViCxot51r8fDO7vGrByR5ZhU8Q32GNKTkMMG1d/gqP/uo8hfmPVYVUtW6oKh0bVOGz0hsOH24sfw6tfjze9h0vBl6Xo+zwaj5LzRLXUqT5rHY8paVHntN1Sie50k3bC+kw1oBmMZR/ITuHDyeHxYZLg0dd2B78PAGqwerlsXOLgFXUUxnPsAQAA";
			strong = strong.Replace(@"\u003d", "="); //Must have bc HYPIXEL :)
			var byteArray = Convert.FromBase64String(strong);
			MemoryStream memoryStream = new(byteArray);
			GZipStream gZipStream = new(memoryStream, CompressionMode.Decompress, false);
			SharpNBT.TagReader tagReader = new(gZipStream, SharpNBT.FormatOptions.BigEndian);
			SharpNBT.TagContainer tag = tagReader.ReadTag() as SharpNBT.TagContainer;
			tag.ToJsonString();
		}
	}
}
