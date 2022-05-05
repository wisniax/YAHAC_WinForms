
namespace nic_z_tego_nie_bd.GuiCode
{
	partial class BetterAH
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxItemSelect = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxString = new System.Windows.Forms.TextBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.panelConfig = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelItemNameTip = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.panelConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Lemon", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Query to look for";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Lemon", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
			this.label2.Location = new System.Drawing.Point(12, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Item name";
			// 
			// comboBoxItemSelect
			// 
			this.comboBoxItemSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.comboBoxItemSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboBoxItemSelect.FormattingEnabled = true;
			this.comboBoxItemSelect.Location = new System.Drawing.Point(12, 56);
			this.comboBoxItemSelect.Name = "comboBoxItemSelect";
			this.comboBoxItemSelect.Size = new System.Drawing.Size(123, 23);
			this.comboBoxItemSelect.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Lemon", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
			this.label3.Location = new System.Drawing.Point(208, 33);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Text to match";
			// 
			// textBoxString
			// 
			this.textBoxString.Enabled = false;
			this.textBoxString.Location = new System.Drawing.Point(208, 56);
			this.textBoxString.Name = "textBoxString";
			this.textBoxString.Size = new System.Drawing.Size(112, 23);
			this.textBoxString.TabIndex = 5;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Enabled = false;
			this.buttonAdd.Location = new System.Drawing.Point(326, 55);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(45, 23);
			this.buttonAdd.TabIndex = 7;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Enabled = false;
			this.buttonSave.Location = new System.Drawing.Point(530, 55);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(79, 23);
			this.buttonSave.TabIndex = 8;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.button2_Click);
			// 
			// panelConfig
			// 
			this.panelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelConfig.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panelConfig.Controls.Add(this.button2);
			this.panelConfig.Controls.Add(this.button1);
			this.panelConfig.Controls.Add(this.numericUpDown1);
			this.panelConfig.Controls.Add(this.label4);
			this.panelConfig.Controls.Add(this.buttonSave);
			this.panelConfig.Controls.Add(this.buttonAdd);
			this.panelConfig.Controls.Add(this.textBoxString);
			this.panelConfig.Controls.Add(this.label3);
			this.panelConfig.Controls.Add(this.comboBoxItemSelect);
			this.panelConfig.Controls.Add(this.label2);
			this.panelConfig.Controls.Add(this.label1);
			this.panelConfig.Location = new System.Drawing.Point(12, 12);
			this.panelConfig.Name = "panelConfig";
			this.panelConfig.Size = new System.Drawing.Size(612, 89);
			this.panelConfig.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(141, 56);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(61, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "Select";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(380, 55);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(110, 23);
			this.numericUpDown1.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Lemon", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.ForeColor = System.Drawing.SystemColors.InfoText;
			this.label4.Location = new System.Drawing.Point(380, 32);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Max price";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 107);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(612, 292);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// labelItemNameTip
			// 
			this.labelItemNameTip.AutoSize = true;
			this.labelItemNameTip.Enabled = false;
			this.labelItemNameTip.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.labelItemNameTip.Location = new System.Drawing.Point(0, 0);
			this.labelItemNameTip.Name = "labelItemNameTip";
			this.labelItemNameTip.Size = new System.Drawing.Size(31, 15);
			this.labelItemNameTip.TabIndex = 2;
			this.labelItemNameTip.Text = "NaN";
			this.labelItemNameTip.Visible = false;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(530, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(79, 23);
			this.button2.TabIndex = 12;
			this.button2.Text = "Remove all";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// BetterAH
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(636, 411);
			this.Controls.Add(this.labelItemNameTip);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.panelConfig);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BetterAH";
			this.Text = "BetterAH";
			this.panelConfig.ResumeLayout(false);
			this.panelConfig.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxItemSelect;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxString;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Panel panelConfig;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label labelItemNameTip;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button button2;
	}
}