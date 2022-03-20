
namespace nic_z_tego_nie_bd
{
	partial class Bazaar
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
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listViewSellPrice = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.listViewBuyPrice = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listBox1.ColumnWidth = 250;
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.IntegralHeight = false;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Margin = new System.Windows.Forms.Padding(0);
			this.listBox1.MultiColumn = true;
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(650, 450);
			this.listBox1.Sorted = true;
			this.listBox1.TabIndex = 2;
			this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
			// 
			// listViewSellPrice
			// 
			this.listViewSellPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewSellPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewSellPrice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listViewSellPrice.HideSelection = false;
			this.listViewSellPrice.Location = new System.Drawing.Point(0, 0);
			this.listViewSellPrice.MultiSelect = false;
			this.listViewSellPrice.Name = "listViewSellPrice";
			this.listViewSellPrice.Size = new System.Drawing.Size(270, 450);
			this.listViewSellPrice.TabIndex = 3;
			this.listViewSellPrice.UseCompatibleStateImageBehavior = false;
			this.listViewSellPrice.View = System.Windows.Forms.View.Details;
			this.listViewSellPrice.Visible = false;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "amount";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "sell price";
			this.columnHeader2.Width = 120;
			// 
			// timer2
			// 
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// listViewBuyPrice
			// 
			this.listViewBuyPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listViewBuyPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewBuyPrice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
			this.listViewBuyPrice.HideSelection = false;
			this.listViewBuyPrice.Location = new System.Drawing.Point(276, 0);
			this.listViewBuyPrice.MultiSelect = false;
			this.listViewBuyPrice.Name = "listViewBuyPrice";
			this.listViewBuyPrice.Size = new System.Drawing.Size(270, 450);
			this.listViewBuyPrice.TabIndex = 4;
			this.listViewBuyPrice.UseCompatibleStateImageBehavior = false;
			this.listViewBuyPrice.View = System.Windows.Forms.View.Details;
			this.listViewBuyPrice.Visible = false;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "amount";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "buy price";
			this.columnHeader4.Width = 120;
			// 
			// Bazaar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 450);
			this.Controls.Add(this.listViewBuyPrice);
			this.Controls.Add(this.listViewSellPrice);
			this.Controls.Add(this.listBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Bazaar";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bazaar";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListView listViewSellPrice;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView listViewBuyPrice;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
	}
}