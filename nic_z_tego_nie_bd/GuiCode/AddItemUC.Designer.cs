
namespace nic_z_tego_nie_bd.GuiCode
{
	partial class AddItemUC
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDownMaxPrice = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBoxUseItemID = new System.Windows.Forms.CheckBox();
			this.buttonSaveItem = new System.Windows.Forms.Button();
			this.buttonRemoveWholeSelectedItem = new System.Windows.Forms.Button();
			this.buttonAddToItemReqList = new System.Windows.Forms.Button();
			this.numericUpDownItemPriority = new System.Windows.Forms.NumericUpDown();
			this.labelItemToAddAmount = new System.Windows.Forms.Label();
			this.labelItemToAddName = new System.Windows.Forms.Label();
			this.comboBoxAddItemToRecipe = new System.Windows.Forms.ComboBox();
			this.labelAddItem = new System.Windows.Forms.Label();
			this.textBoxRecipe = new System.Windows.Forms.TextBox();
			this.labelItemsReq = new System.Windows.Forms.Label();
			this.comboBoxItemToCraft = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPrice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemPriority)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDownMaxPrice);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.checkBoxUseItemID);
			this.groupBox1.Controls.Add(this.buttonSaveItem);
			this.groupBox1.Controls.Add(this.buttonRemoveWholeSelectedItem);
			this.groupBox1.Controls.Add(this.buttonAddToItemReqList);
			this.groupBox1.Controls.Add(this.numericUpDownItemPriority);
			this.groupBox1.Controls.Add(this.labelItemToAddAmount);
			this.groupBox1.Controls.Add(this.labelItemToAddName);
			this.groupBox1.Controls.Add(this.comboBoxAddItemToRecipe);
			this.groupBox1.Controls.Add(this.labelAddItem);
			this.groupBox1.Controls.Add(this.textBoxRecipe);
			this.groupBox1.Controls.Add(this.labelItemsReq);
			this.groupBox1.Controls.Add(this.comboBoxItemToCraft);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox1.Size = new System.Drawing.Size(636, 254);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Items To Search Config";
			// 
			// numericUpDownMaxPrice
			// 
			this.numericUpDownMaxPrice.Location = new System.Drawing.Point(237, 124);
			this.numericUpDownMaxPrice.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.numericUpDownMaxPrice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownMaxPrice.Name = "numericUpDownMaxPrice";
			this.numericUpDownMaxPrice.Size = new System.Drawing.Size(95, 23);
			this.numericUpDownMaxPrice.TabIndex = 22;
			this.numericUpDownMaxPrice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(237, 94);
			this.label2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 21);
			this.label2.TabIndex = 21;
			this.label2.Text = "Max price";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// checkBoxUseItemID
			// 
			this.checkBoxUseItemID.AutoSize = true;
			this.checkBoxUseItemID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.checkBoxUseItemID.Location = new System.Drawing.Point(171, 98);
			this.checkBoxUseItemID.Name = "checkBoxUseItemID";
			this.checkBoxUseItemID.Size = new System.Drawing.Size(67, 19);
			this.checkBoxUseItemID.TabIndex = 20;
			this.checkBoxUseItemID.Tag = "Use hypixel item id\'s for name resolving";
			this.checkBoxUseItemID.Text = "Use ID\'s";
			this.checkBoxUseItemID.UseVisualStyleBackColor = true;
			// 
			// buttonSaveItem
			// 
			this.buttonSaveItem.Location = new System.Drawing.Point(136, 219);
			this.buttonSaveItem.Name = "buttonSaveItem";
			this.buttonSaveItem.Size = new System.Drawing.Size(95, 23);
			this.buttonSaveItem.TabIndex = 19;
			this.buttonSaveItem.Text = "Save item";
			this.buttonSaveItem.UseVisualStyleBackColor = true;
			this.buttonSaveItem.Click += new System.EventHandler(this.buttonSaveItem_Click_1);
			// 
			// buttonRemoveWholeSelectedItem
			// 
			this.buttonRemoveWholeSelectedItem.Location = new System.Drawing.Point(237, 219);
			this.buttonRemoveWholeSelectedItem.Name = "buttonRemoveWholeSelectedItem";
			this.buttonRemoveWholeSelectedItem.Size = new System.Drawing.Size(95, 23);
			this.buttonRemoveWholeSelectedItem.TabIndex = 18;
			this.buttonRemoveWholeSelectedItem.Text = "Remove item";
			this.buttonRemoveWholeSelectedItem.UseVisualStyleBackColor = true;
			this.buttonRemoveWholeSelectedItem.Click += new System.EventHandler(this.buttonRemoveWholeSelectedItem_Click_1);
			// 
			// buttonAddToItemReqList
			// 
			this.buttonAddToItemReqList.Location = new System.Drawing.Point(13, 182);
			this.buttonAddToItemReqList.Name = "buttonAddToItemReqList";
			this.buttonAddToItemReqList.Size = new System.Drawing.Size(319, 23);
			this.buttonAddToItemReqList.TabIndex = 15;
			this.buttonAddToItemReqList.Text = "Add to list";
			this.buttonAddToItemReqList.UseVisualStyleBackColor = true;
			this.buttonAddToItemReqList.Click += new System.EventHandler(this.buttonAddToItemReqList_Click_1);
			// 
			// numericUpDownItemPriority
			// 
			this.numericUpDownItemPriority.Location = new System.Drawing.Point(237, 153);
			this.numericUpDownItemPriority.Name = "numericUpDownItemPriority";
			this.numericUpDownItemPriority.Size = new System.Drawing.Size(95, 23);
			this.numericUpDownItemPriority.TabIndex = 13;
			// 
			// labelItemToAddAmount
			// 
			this.labelItemToAddAmount.AutoSize = true;
			this.labelItemToAddAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemToAddAmount.Location = new System.Drawing.Point(12, 155);
			this.labelItemToAddAmount.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
			this.labelItemToAddAmount.Name = "labelItemToAddAmount";
			this.labelItemToAddAmount.Size = new System.Drawing.Size(61, 21);
			this.labelItemToAddAmount.TabIndex = 12;
			this.labelItemToAddAmount.Text = "Priority";
			this.labelItemToAddAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelItemToAddName
			// 
			this.labelItemToAddName.AutoSize = true;
			this.labelItemToAddName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemToAddName.Location = new System.Drawing.Point(17, 94);
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
			this.comboBoxAddItemToRecipe.Location = new System.Drawing.Point(12, 124);
			this.comboBoxAddItemToRecipe.Name = "comboBoxAddItemToRecipe";
			this.comboBoxAddItemToRecipe.Size = new System.Drawing.Size(219, 23);
			this.comboBoxAddItemToRecipe.TabIndex = 9;
			// 
			// labelAddItem
			// 
			this.labelAddItem.AutoSize = true;
			this.labelAddItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelAddItem.Location = new System.Drawing.Point(17, 64);
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
			this.textBoxRecipe.Location = new System.Drawing.Point(377, 83);
			this.textBoxRecipe.Multiline = true;
			this.textBoxRecipe.Name = "textBoxRecipe";
			this.textBoxRecipe.ReadOnly = true;
			this.textBoxRecipe.Size = new System.Drawing.Size(198, 158);
			this.textBoxRecipe.TabIndex = 7;
			// 
			// labelItemsReq
			// 
			this.labelItemsReq.AutoSize = true;
			this.labelItemsReq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelItemsReq.Location = new System.Drawing.Point(377, 59);
			this.labelItemsReq.Name = "labelItemsReq";
			this.labelItemsReq.Size = new System.Drawing.Size(101, 21);
			this.labelItemsReq.TabIndex = 6;
			this.labelItemsReq.Text = "Search terms";
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
			this.comboBoxItemToCraft.SelectionChangeCommitted += new System.EventHandler(this.comboBoxItemToCraft_SelectionChangeCommitted_1);
			// 
			// AddItemUC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MaximumSize = new System.Drawing.Size(636, 254);
			this.MinimumSize = new System.Drawing.Size(636, 254);
			this.Name = "AddItemUC";
			this.Size = new System.Drawing.Size(636, 254);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPrice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemPriority)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxUseItemID;
		private System.Windows.Forms.Button buttonSaveItem;
		private System.Windows.Forms.Button buttonRemoveWholeSelectedItem;
		private System.Windows.Forms.Button buttonAddToItemReqList;
		private System.Windows.Forms.Label labelItemToAddAmount;
		private System.Windows.Forms.Label labelItemToAddName;
		private System.Windows.Forms.ComboBox comboBoxAddItemToRecipe;
		private System.Windows.Forms.Label labelAddItem;
		private System.Windows.Forms.TextBox textBoxRecipe;
		private System.Windows.Forms.Label labelItemsReq;
		private System.Windows.Forms.ComboBox comboBoxItemToCraft;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownMaxPrice;
		private System.Windows.Forms.NumericUpDown numericUpDownItemPriority;
	}
}
