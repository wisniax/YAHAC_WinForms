
namespace nic_z_tego_nie_bd.GuiCode
{
	partial class SettingsUi
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBoxChoooseStartUi = new System.Windows.Forms.ComboBox();
			this.textBoxChooseUi = new System.Windows.Forms.TextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxItemsToCraft = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBoxChoooseStartUi
			// 
			this.comboBoxChoooseStartUi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxChoooseStartUi.FormattingEnabled = true;
			this.comboBoxChoooseStartUi.Items.AddRange(new object[] {
            "AuctionHouse",
            "Bazaar",
            "None :("});
			this.comboBoxChoooseStartUi.Location = new System.Drawing.Point(12, 41);
			this.comboBoxChoooseStartUi.Name = "comboBoxChoooseStartUi";
			this.comboBoxChoooseStartUi.Size = new System.Drawing.Size(121, 23);
			this.comboBoxChoooseStartUi.Sorted = true;
			this.comboBoxChoooseStartUi.TabIndex = 0;
			// 
			// textBoxChooseUi
			// 
			this.textBoxChooseUi.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxChooseUi.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBoxChooseUi.Location = new System.Drawing.Point(12, 19);
			this.textBoxChooseUi.Name = "textBoxChooseUi";
			this.textBoxChooseUi.ReadOnly = true;
			this.textBoxChooseUi.Size = new System.Drawing.Size(121, 16);
			this.textBoxChooseUi.TabIndex = 1;
			this.textBoxChooseUi.Text = "Choose starting tab";
			this.textBoxChooseUi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(588, 365);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(50, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.comboBoxItemsToCraft);
			this.groupBox1.Location = new System.Drawing.Point(0, 148);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(7);
			this.groupBox1.Size = new System.Drawing.Size(650, 302);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Items To Craft Config";
			// 
			// comboBoxItemsToCraft
			// 
			this.comboBoxItemsToCraft.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxItemsToCraft.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxItemsToCraft.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBoxItemsToCraft.FormattingEnabled = true;
			this.comboBoxItemsToCraft.Location = new System.Drawing.Point(7, 23);
			this.comboBoxItemsToCraft.Name = "comboBoxItemsToCraft";
			this.comboBoxItemsToCraft.Size = new System.Drawing.Size(636, 23);
			this.comboBoxItemsToCraft.TabIndex = 5;
			// 
			// SettingsUi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 450);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxChooseUi);
			this.Controls.Add(this.comboBoxChoooseStartUi);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SettingsUi";
			this.Text = "SettingsUi";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxChoooseStartUi;
		private System.Windows.Forms.TextBox textBoxChooseUi;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxItemsToCraft;
	}
}