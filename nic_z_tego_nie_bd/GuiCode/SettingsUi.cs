using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nic_z_tego_nie_bd.GuiCode
{
	public partial class SettingsUi : Form
	{
		public SettingsUi()
		{
			InitializeComponent();
			comboBoxChoooseStartUi.SelectedItem = Properties.Settings.Default.Starting_Ui;
			comboBoxItemsToCraft.Text = "Adding new item";
			generateComboList();
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			var cos = comboBoxChoooseStartUi.SelectedItem.ToString();
			Properties.Settings.Default.Starting_Ui = cos;
			Properties.Settings.Default.Save();
		}
		private void generateComboList()
		{
			comboBoxItemsToCraft.Items.Add("ABC");
		}
	}
}
