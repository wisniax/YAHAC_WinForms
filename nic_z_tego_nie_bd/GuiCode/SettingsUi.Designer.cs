
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
			this.checkBoxUseItemID = new System.Windows.Forms.CheckBox();
			this.buttonSaveItem = new System.Windows.Forms.Button();
			this.buttonRemoveWholeSelectedItem = new System.Windows.Forms.Button();
			this.comboBoxChooseItemToRemove = new System.Windows.Forms.ComboBox();
			this.buttonRemoveItem = new System.Windows.Forms.Button();
			this.buttonAddToItemReqList = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownItemToAddAmount = new System.Windows.Forms.NumericUpDown();
			this.labelItemToAddAmount = new System.Windows.Forms.Label();
			this.labelItemToAddName = new System.Windows.Forms.Label();
			this.comboBoxAddItemToRecipe = new System.Windows.Forms.ComboBox();
			this.labelAddItem = new System.Windows.Forms.Label();
			this.textBoxRecipe = new System.Windows.Forms.TextBox();
			this.labelItemsReq = new System.Windows.Forms.Label();
			this.comboBoxItemToCraft = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemToAddAmount)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBoxChoooseStartUi
			// 
			this.comboBoxChoooseStartUi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxChoooseStartUi.FormattingEnabled = true;
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
			this.buttonSave.Location = new System.Drawing.Point(574, 326);
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
			this.groupBox1.Controls.Add(this.checkBoxUseItemID);
			this.groupBox1.Controls.Add(this.buttonSaveItem);
			this.groupBox1.Controls.Add(this.buttonRemoveWholeSelectedItem);
			this.groupBox1.Controls.Add(this.comboBoxChooseItemToRemove);
			this.groupBox1.Controls.Add(this.buttonRemoveItem);
			this.groupBox1.Controls.Add(this.buttonAddToItemReqList);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numericUpDownItemToAddAmount);
			this.groupBox1.Controls.Add(this.labelItemToAddAmount);
			this.groupBox1.Controls.Add(this.labelItemToAddName);
			this.groupBox1.Controls.Add(this.comboBoxAddItemToRecipe);
			this.groupBox1.Controls.Add(this.labelAddItem);
			this.groupBox1.Controls.Add(this.textBoxRecipe);
			this.groupBox1.Controls.Add(this.labelItemsReq);
			this.groupBox1.Controls.Add(this.comboBoxItemToCraft);
			this.groupBox1.Location = new System.Drawing.Point(0, 148);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox1.Size = new System.Drawing.Size(636, 263);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Items To Craft Config";
			// 
			// checkBoxUseItemID
			// 
			this.checkBoxUseItemID.AutoSize = true;
			this.checkBoxUseItemID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.checkBoxUseItemID.Location = new System.Drawing.Point(164, 91);
			this.checkBoxUseItemID.Name = "checkBoxUseItemID";
			this.checkBoxUseItemID.Size = new System.Drawing.Size(67, 19);
			this.checkBoxUseItemID.TabIndex = 20;
			this.checkBoxUseItemID.Tag = "Use hypixel item id\'s for name resolving";
			this.checkBoxUseItemID.Text = "Use ID\'s";
			this.checkBoxUseItemID.UseVisualStyleBackColor = true;
			this.checkBoxUseItemID.CheckedChanged += new System.EventHandler(this.checkBoxUseItemID_CheckedChanged);
			// 
			// buttonSaveItem
			// 
			this.buttonSaveItem.Location = new System.Drawing.Point(237, 58);
			this.buttonSaveItem.Name = "buttonSaveItem";
			this.buttonSaveItem.Size = new System.Drawing.Size(95, 23);
			this.buttonSaveItem.TabIndex = 19;
			this.buttonSaveItem.Text = "Save item";
			this.buttonSaveItem.UseVisualStyleBackColor = true;
			this.buttonSaveItem.Click += new System.EventHandler(this.buttonSaveItem_Click);
			// 
			// buttonRemoveWholeSelectedItem
			// 
			this.buttonRemoveWholeSelectedItem.Location = new System.Drawing.Point(237, 182);
			this.buttonRemoveWholeSelectedItem.Name = "buttonRemoveWholeSelectedItem";
			this.buttonRemoveWholeSelectedItem.Size = new System.Drawing.Size(95, 23);
			this.buttonRemoveWholeSelectedItem.TabIndex = 18;
			this.buttonRemoveWholeSelectedItem.Text = "Remove item";
			this.buttonRemoveWholeSelectedItem.UseVisualStyleBackColor = true;
			this.buttonRemoveWholeSelectedItem.Click += new System.EventHandler(this.buttonRemoveWholeSelectedItem_Click);
			// 
			// comboBoxChooseItemToRemove
			// 
			this.comboBoxChooseItemToRemove.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxChooseItemToRemove.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxChooseItemToRemove.FormattingEnabled = true;
			this.comboBoxChooseItemToRemove.Location = new System.Drawing.Point(12, 218);
			this.comboBoxChooseItemToRemove.Name = "comboBoxChooseItemToRemove";
			this.comboBoxChooseItemToRemove.Size = new System.Drawing.Size(219, 23);
			this.comboBoxChooseItemToRemove.TabIndex = 17;
			// 
			// buttonRemoveItem
			// 
			this.buttonRemoveItem.Location = new System.Drawing.Point(237, 218);
			this.buttonRemoveItem.Name = "buttonRemoveItem";
			this.buttonRemoveItem.Size = new System.Drawing.Size(95, 23);
			this.buttonRemoveItem.TabIndex = 16;
			this.buttonRemoveItem.Text = "Remove item";
			this.buttonRemoveItem.UseVisualStyleBackColor = true;
			// 
			// buttonAddToItemReqList
			// 
			this.buttonAddToItemReqList.Location = new System.Drawing.Point(12, 140);
			this.buttonAddToItemReqList.Name = "buttonAddToItemReqList";
			this.buttonAddToItemReqList.Size = new System.Drawing.Size(319, 23);
			this.buttonAddToItemReqList.TabIndex = 15;
			this.buttonAddToItemReqList.Text = "Add to list";
			this.buttonAddToItemReqList.UseVisualStyleBackColor = true;
			this.buttonAddToItemReqList.Click += new System.EventHandler(this.buttonAddToItemReqList_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(13, 181);
			this.label1.Margin = new System.Windows.Forms.Padding(5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 21);
			this.label1.TabIndex = 14;
			this.label1.Text = "Remove Item";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// numericUpDownItemToAddAmount
			// 
			this.numericUpDownItemToAddAmount.Location = new System.Drawing.Point(237, 111);
			this.numericUpDownItemToAddAmount.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numericUpDownItemToAddAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownItemToAddAmount.Name = "numericUpDownItemToAddAmount";
			this.numericUpDownItemToAddAmount.Size = new System.Drawing.Size(95, 23);
			this.numericUpDownItemToAddAmount.TabIndex = 13;
			this.numericUpDownItemToAddAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// labelItemToAddAmount
			// 
			this.labelItemToAddAmount.AutoSize = true;
			this.labelItemToAddAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemToAddAmount.Location = new System.Drawing.Point(237, 87);
			this.labelItemToAddAmount.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.labelItemToAddAmount.Name = "labelItemToAddAmount";
			this.labelItemToAddAmount.Size = new System.Drawing.Size(66, 21);
			this.labelItemToAddAmount.TabIndex = 12;
			this.labelItemToAddAmount.Text = "Amount";
			this.labelItemToAddAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelItemToAddName
			// 
			this.labelItemToAddName.AutoSize = true;
			this.labelItemToAddName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemToAddName.Location = new System.Drawing.Point(10, 87);
			this.labelItemToAddName.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.labelItemToAddName.Name = "labelItemToAddName";
			this.labelItemToAddName.Size = new System.Drawing.Size(87, 21);
			this.labelItemToAddName.TabIndex = 11;
			this.labelItemToAddName.Text = "Item Name";
			this.labelItemToAddName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBoxAddItemToRecipe
			// 
			this.comboBoxAddItemToRecipe.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxAddItemToRecipe.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxAddItemToRecipe.FormattingEnabled = true;
			this.comboBoxAddItemToRecipe.Location = new System.Drawing.Point(12, 111);
			this.comboBoxAddItemToRecipe.Name = "comboBoxAddItemToRecipe";
			this.comboBoxAddItemToRecipe.Size = new System.Drawing.Size(219, 23);
			this.comboBoxAddItemToRecipe.TabIndex = 9;
			// 
			// labelAddItem
			// 
			this.labelAddItem.AutoSize = true;
			this.labelAddItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelAddItem.Location = new System.Drawing.Point(10, 57);
			this.labelAddItem.Margin = new System.Windows.Forms.Padding(5);
			this.labelAddItem.Name = "labelAddItem";
			this.labelAddItem.Size = new System.Drawing.Size(73, 21);
			this.labelAddItem.TabIndex = 8;
			this.labelAddItem.Text = "Add item";
			this.labelAddItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBoxRecipe
			// 
			this.textBoxRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxRecipe.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxRecipe.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxRecipe.Location = new System.Drawing.Point(370, 76);
			this.textBoxRecipe.Multiline = true;
			this.textBoxRecipe.Name = "textBoxRecipe";
			this.textBoxRecipe.ReadOnly = true;
			this.textBoxRecipe.Size = new System.Drawing.Size(198, 175);
			this.textBoxRecipe.TabIndex = 7;
			// 
			// labelItemsReq
			// 
			this.labelItemsReq.AutoSize = true;
			this.labelItemsReq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemsReq.Location = new System.Drawing.Point(373, 52);
			this.labelItemsReq.Name = "labelItemsReq";
			this.labelItemsReq.Size = new System.Drawing.Size(194, 21);
			this.labelItemsReq.TabIndex = 6;
			this.labelItemsReq.Text = "Items required to craft one";
			this.labelItemsReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboBoxItemToCraft
			// 
			this.comboBoxItemToCraft.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxItemToCraft.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxItemToCraft.Dock = System.Windows.Forms.DockStyle.Top;
			this.comboBoxItemToCraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxItemToCraft.FormattingEnabled = true;
			this.comboBoxItemToCraft.Location = new System.Drawing.Point(10, 26);
			this.comboBoxItemToCraft.Name = "comboBoxItemToCraft";
			this.comboBoxItemToCraft.Size = new System.Drawing.Size(616, 23);
			this.comboBoxItemToCraft.TabIndex = 5;
			this.comboBoxItemToCraft.SelectionChangeCommitted += new System.EventHandler(this.comboBoxItemToCraft_SelectionChangeCommitted);
			// 
			// SettingsUi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 411);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxChooseUi);
			this.Controls.Add(this.comboBoxChoooseStartUi);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SettingsUi";
			this.Text = "SettingsUi";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemToAddAmount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxChoooseStartUi;
		private System.Windows.Forms.TextBox textBoxChooseUi;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxItemToCraft;
		private System.Windows.Forms.ComboBox comboBoxAddItemToRecipe;
		private System.Windows.Forms.Label labelAddItem;
		private System.Windows.Forms.TextBox textBoxRecipe;
		private System.Windows.Forms.Label labelItemsReq;
		private System.Windows.Forms.Label labelItemToAddName;
		private System.Windows.Forms.Label labelItemToAddAmount;
		private System.Windows.Forms.NumericUpDown numericUpDownItemToAddAmount;
		private System.Windows.Forms.Button buttonRemoveWholeSelectedItem;
		private System.Windows.Forms.ComboBox comboBoxChooseItemToRemove;
		private System.Windows.Forms.Button buttonRemoveItem;
		private System.Windows.Forms.Button buttonAddToItemReqList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonSaveItem;
		private System.Windows.Forms.CheckBox checkBoxUseItemID;
	}
}