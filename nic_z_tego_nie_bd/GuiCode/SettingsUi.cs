﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Media;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class SettingsUi : Form
	{
		ItemsToCraft.ItemRecipe itemRecipe;
		public SettingsUi()
		{
			InitializeComponent();
			var uisToChooseFrom = new List<string> { "AuctionHouse", "Bazaar","Item Crafts", "Settings", "Better AH" };
			comboBoxChoooseStartUi.DataSource = uisToChooseFrom;
			comboBoxChoooseStartUi.SelectedItem = Properties.Settings.Default.Starting_Ui;
			if (uisToChooseFrom.Contains(comboBoxChoooseStartUi.SelectedItem)==false) { comboBoxChoooseStartUi.SelectedItem = uisToChooseFrom.First(s => s.Contains("Settings")); }
			checkBoxPlaySound.Checked = Properties.Settings.Default.playSound;
			numericUpDownIconsSize.Value = Properties.Settings.Default.itemsUCsize;
			itemRecipe = new();
			itemRecipe.reqItems = new();
			generateComboItemToCraftList();
			comboBoxAddItemToRecipe.DisplayMember = "name";
			comboBoxAddItemToRecipe.ValueMember = "id";
			comboBoxAddItemToRecipe.DataSource = Properties.AllItemsREPO.itemRepo.items;
			this.checkBoxPlaySound.CheckedChanged += new System.EventHandler(this.checkBoxPlaySound_CheckedChanged);
		}


		///
		///		METHODS
		///

		//Generates combo box responsible for selecting stored item recipes
		private void generateComboItemToCraftList()
		{
			comboBoxItemToCraft.Items.Clear();
			var recipePairs = new List<ItemRecipePair>();
			var addNewItem = new ItemRecipePair { item_name = "Add new item", item_dictKey = "Add new item" };
			recipePairs.Add(addNewItem);
			if (ItemsToCraft.items != null)
			{
				foreach (var item in ItemsToCraft.items)
				{
					recipePairs.Add(new ItemRecipePair { item_name = Properties.AllItemsREPO.IDtoNAME(item.item_dictKey), item_dictKey = item.item_dictKey });
				}
			}
			comboBoxItemToCraft.DisplayMember = "item_name";
			comboBoxItemToCraft.ValueMember = "item_dictKey";
			foreach (var item in recipePairs)
			{
				comboBoxItemToCraft.Items.Add(item);
			}
			comboBoxItemToCraft.SelectedItem = addNewItem;
			buttonRemoveWholeSelectedItem.Enabled = false;
			buttonSaveItem.Enabled = false;
			labelItemToAddAmount.Enabled = false;
			numericUpDownItemToAddAmount.Enabled = false;
			textBoxRecipe.Clear();
		}

		//Generates recipe of provided itemRecipe on to textBox
		private void genRecipeInTextBox(ItemsToCraft.ItemRecipe itemRecipo)
		{
			textBoxRecipe.Clear();
			if (itemRecipo.reqItems == null) return;
			foreach (var item in itemRecipo.reqItems)
			{
				textBoxRecipe.AppendText(item.amount.ToString() + " × " + Properties.AllItemsREPO.IDtoNAME(item.item_dictKey) + Environment.NewLine); //New line is "\r\n"
			}
		}

		///
		///		BUTTON CLICKS
		///
		private void buttonSaveItem_Click(object sender, EventArgs e)
		{
			ItemsToCraft.items.Add(itemRecipe);
			itemRecipe = new();
			itemRecipe.reqItems = new();
			generateComboItemToCraftList();
		}

		private void buttonAddToItemReqList_Click(object sender, EventArgs e)
		{
			//Check whether we generate new item recipe or add req items to already existing one
			if (!Properties.AllItemsREPO.itemRepo.items.Contains((Properties.AllItemsREPO.Item)comboBoxAddItemToRecipe.SelectedItem)) return;
			if (((ItemRecipePair)comboBoxItemToCraft.SelectedItem).item_dictKey == "Add new item")
			{
				itemRecipe.item_dictKey = ((Properties.AllItemsREPO.Item)comboBoxAddItemToRecipe.SelectedItem).id;
				var itemek = new ItemRecipePair { item_name = Properties.AllItemsREPO.IDtoNAME(itemRecipe.item_dictKey), item_dictKey = itemRecipe.item_dictKey };
				comboBoxItemToCraft.Items.Add(itemek);
				buttonRemoveWholeSelectedItem.Enabled = true;
				labelItemToAddAmount.Enabled = true;
				numericUpDownItemToAddAmount.Enabled = true;
				comboBoxItemToCraft.SelectedItem = itemek;
			}
			else
			{
				buttonSaveItem.Enabled = true;
				ItemsToCraft.reqItem item = new();
				item.item_dictKey = ((Properties.AllItemsREPO.Item)comboBoxAddItemToRecipe.SelectedItem).id;
				item.amount = (UInt32)numericUpDownItemToAddAmount.Value;
				itemRecipe.reqItems.Add(item);
				genRecipeInTextBox(itemRecipe);
			}
		}


		private void buttonRemoveWholeSelectedItem_Click(object sender, EventArgs e)
		{
			ItemsToCraft.items.Remove(itemRecipe);
			itemRecipe = new();
			itemRecipe.reqItems = new();
			generateComboItemToCraftList();
		}


		private void buttonSave_Click(object sender, EventArgs e)
		{
			var startUi = comboBoxChoooseStartUi.SelectedItem.ToString();
			Properties.Settings.Default.Starting_Ui = startUi;
			Properties.Settings.Default.playSound = checkBoxPlaySound.Checked;
			Properties.Settings.Default.itemsUCsize = (uint)numericUpDownIconsSize.Value;
			Properties.Settings.Default.Save();
			if (buttonSaveItem.Enabled)
			{
				ItemsToCraft.items.Add(itemRecipe);
				itemRecipe = new();
				itemRecipe.reqItems = new();
				generateComboItemToCraftList();
			}
			ItemsToCraft.saveRecipes();
		}

		///
		///		EVENTS
		///
		private void comboBoxItemToCraft_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.ComboBox)sender).Items.Count == 0) return;
			var selectedPair = (ItemRecipePair)comboBoxItemToCraft.SelectedItem;
			if (selectedPair.item_dictKey == "Add new item")
			{
				itemRecipe = new();
				itemRecipe.reqItems = new();
				generateComboItemToCraftList();
			}
			else
			{
				itemRecipe = ItemsToCraft.items.Find(a => a.item_dictKey == selectedPair.item_dictKey);
				if (itemRecipe.reqItems == null) itemRecipe.reqItems = new();
				buttonRemoveWholeSelectedItem.Enabled = true;
				buttonSaveItem.Enabled = true;
				labelItemToAddAmount.Enabled = true;
				numericUpDownItemToAddAmount.Enabled = true;
				genRecipeInTextBox(itemRecipe);
			}
		}


		//Structure for comboboxes objects
		private class ItemRecipePair
		{
			public string item_name { get; set; }
			public string item_dictKey { get; set; }
		}

		private void checkBoxUseItemID_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxAddItemToRecipe.DisplayMember = checkBoxUseItemID.Checked?"id":"name";
		}

		private void buttonSwitch_Click(object sender, EventArgs e)
		{
			Point location = groupBox1.Location;
			groupBox1.Hide();
			groupBox1.Enabled = false;
			var cos = new AddItemUC();
			cos.Location = location;
			this.Controls.Add(cos);
			cos.BringToFront();
		}

		private void checkBoxPlaySound_CheckedChanged(object sender, EventArgs e)
		{
			SoundPlayer soundPlayer = new(Properties.Resources.notify_sound);
			soundPlayer.Play();
			Properties.Settings.Default.playSound = checkBoxPlaySound.Checked;
		}
	}



	//Load recipes from memo
	public static class ItemsToCraft
	{
		public static List<ItemRecipe> items;
		static ItemsToCraft()
		{
			refresh();
		}




		public static void refresh()
		{
			try
			{
				items = loadRecipes();
				if (items == null) throw new Exception();
			}
			catch (Exception e)
			{
				items = new();
			}
		}


		//Load/Save recipes to memory
		public static void saveRecipes()
		{
			var stronk = JsonSerializer.Serialize(items);
			Properties.Settings.Default.items = stronk;
			Properties.Settings.Default.Save();
		}
		public static List<ItemRecipe> loadRecipes()
		{
			var stronk = JsonSerializer.Deserialize<List<ItemRecipe>>(Properties.Settings.Default.items);
			return stronk;
		}



		//To store item recipes
		public struct ItemRecipe
		{
			//public string item_name { get; set; }
			public string item_dictKey { get; set; }
			public Source sellTo { get; set; }
			public List<reqItem> reqItems { get; set; }
		}
		public struct reqItem
		{
			//public string item_name { get; set; }
			public string item_dictKey { get; set; }
			public Source source { get; set; }
			public UInt32 amount { get; set; }
		}
		public enum Source
		{
			Bazaar,
			AuctionHouse
		}
	}
}
