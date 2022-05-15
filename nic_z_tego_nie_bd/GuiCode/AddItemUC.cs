using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class AddItemUC : UserControl
	{
		List<BetterAH.ItemToSearchFor> itemsToSearchFor;
		BetterAH.ItemToSearchFor newItem;
		public AddItemUC()
		{
			newItem = new();
			newItem.searchQueries = new();
			try
			{
				itemsToSearchFor = loadRecipes();
				if (itemsToSearchFor == null) throw new Exception();
			}
			catch (Exception e)
			{
				itemsToSearchFor = new();
			}
			InitializeComponent();
			generateComboItemToCraftList();
			comboBoxAddItemToRecipe.DisplayMember = "name";
			comboBoxAddItemToRecipe.ValueMember = "id";
			comboBoxAddItemToRecipe.DataSource = Properties.AllItemsREPO.itemRepo.items;

		}

		///
		///		METHODS
		///
		void saveRecipes()
		{
			var stronk = JsonSerializer.Serialize(itemsToSearchFor);
			Properties.Settings.Default.BetterAHQuery = stronk;
			Properties.Settings.Default.Save();
		}
		List<BetterAH.ItemToSearchFor> loadRecipes()
		{
			var stronk = JsonSerializer.Deserialize<List<BetterAH.ItemToSearchFor>>(Properties.Settings.Default.BetterAHQuery);
			return stronk;
		}
		//Generates combo box responsible for selecting stored item recipes
		private void generateComboItemToCraftList()
		{
			comboBoxItemToCraft.Items.Clear();
			var recipePairs = new List<ItemRecipePair>();
			var addNewItem = new ItemRecipePair { item_name = "Add new item", item_dictKey = "Add new item" };
			recipePairs.Add(addNewItem);
			if (itemsToSearchFor != null)
			{
				foreach (var item in itemsToSearchFor)
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
			buttonAddToItemReqList.Enabled = true;
			numericUpDownMaxPrice.Enabled = false;
			numericUpDownItemPriority.Enabled = false;
			textBoxRecipe.Clear();
		}

		//Generates recipe of provided itemRecipe on to textBox
		private void genRecipeInTextBox(BetterAH.ItemToSearchFor itemRecipo)
		{
			textBoxRecipe.Clear();
			if (itemRecipo.searchQueries == null) return;
			foreach (var item in itemRecipo.searchQueries)
			{
				textBoxRecipe.AppendText(item + Environment.NewLine); //New line is "\r\n"
			}
		}

		///
		///		BUTTON CLICKS
		///


		private class ItemRecipePair
		{
			public string item_name { get; set; }
			public string item_dictKey { get; set; }
		}

		private void buttonSaveItem_Click_1(object sender, EventArgs e)
		{
			newItem.maxPrice = (uint)numericUpDownMaxPrice.Value;
			newItem.priority = (ushort)numericUpDownItemPriority.Value;
			if (!itemsToSearchFor.Contains(newItem))
			{
				itemsToSearchFor.Add(newItem);
			}
			newItem = new();
			newItem.searchQueries = new();
			saveRecipes();
			generateComboItemToCraftList();
		}

		private void buttonAddToItemReqList_Click_1(object sender, EventArgs e)
		{
			//Check whether we generate new item recipe or add req items to already existing one
			if (((ItemRecipePair)comboBoxItemToCraft.SelectedItem).item_dictKey == "Add new item")
			{
				if (!Properties.AllItemsREPO.itemRepo.items.Contains((Properties.AllItemsREPO.Item)comboBoxAddItemToRecipe.SelectedItem)) return;
				newItem.item_dictKey = ((Properties.AllItemsREPO.Item)comboBoxAddItemToRecipe.SelectedItem).id;
				var itemek = new ItemRecipePair { item_name = Properties.AllItemsREPO.IDtoNAME(newItem.item_dictKey), item_dictKey = newItem.item_dictKey };
				comboBoxItemToCraft.Items.Add(itemek);
				buttonRemoveWholeSelectedItem.Enabled = true;
				buttonSaveItem.Enabled = true;
				buttonAddToItemReqList.Enabled = true;
				numericUpDownMaxPrice.Enabled = true;
				numericUpDownItemPriority.Enabled = true;
				comboBoxItemToCraft.SelectedItem = itemek;
			}
			else
			{
				buttonSaveItem.Enabled = true;
				string str = comboBoxAddItemToRecipe.Text;
				newItem.searchQueries.Add(str);
				genRecipeInTextBox(newItem);
			}
		}

		private void buttonRemoveWholeSelectedItem_Click_1(object sender, EventArgs e)
		{
			itemsToSearchFor.Remove(newItem);
			newItem = new();
			newItem.searchQueries = new();
			saveRecipes();
			generateComboItemToCraftList();
		}

		///
		///		EVENTS
		///

		private void comboBoxItemToCraft_SelectionChangeCommitted_1(object sender, EventArgs e)
		{
			if (((System.Windows.Forms.ComboBox)sender).Items.Count == 0) return;
			var selectedPair = (ItemRecipePair)comboBoxItemToCraft.SelectedItem;
			if (selectedPair.item_dictKey == "Add new item")
			{
				newItem = new();
				newItem.searchQueries = new();
				//numericUpDownItemPriority.Value = newItem.priority;
				//numericUpDownMaxPrice.Value = newItem.maxPrice;
				buttonRemoveWholeSelectedItem.Enabled = false;
				buttonSaveItem.Enabled = false;
				buttonAddToItemReqList.Enabled = true;
				numericUpDownMaxPrice.Enabled = true;
				numericUpDownItemPriority.Enabled = true;
				generateComboItemToCraftList();
			}
			else
			{
				newItem = itemsToSearchFor.Find(a => a.item_dictKey == selectedPair.item_dictKey);
				if (newItem.searchQueries == null) newItem.searchQueries = new();
				numericUpDownItemPriority.Value = newItem.priority;
				numericUpDownMaxPrice.Value = newItem.maxPrice;
				buttonRemoveWholeSelectedItem.Enabled = true;
				buttonSaveItem.Enabled = true;
				buttonAddToItemReqList.Enabled = true;
				numericUpDownMaxPrice.Enabled = true;
				numericUpDownItemPriority.Enabled = true;
				genRecipeInTextBox(newItem);
			}
		}
	}
}
