
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelItemNameTip = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeaderBuyPrice = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderBuyAmount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSpace = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSellPrice = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSellAmount = new System.Windows.Forms.ColumnHeader();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(630, 405);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// labelItemNameTip
			// 
			this.labelItemNameTip.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.labelItemNameTip.AutoSize = true;
			this.labelItemNameTip.Enabled = false;
			this.labelItemNameTip.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.labelItemNameTip.ForeColor = System.Drawing.SystemColors.Info;
			this.labelItemNameTip.Location = new System.Drawing.Point(6, 5);
			this.labelItemNameTip.Name = "labelItemNameTip";
			this.labelItemNameTip.Size = new System.Drawing.Size(42, 21);
			this.labelItemNameTip.TabIndex = 0;
			this.labelItemNameTip.Text = "NaN";
			this.labelItemNameTip.Visible = false;
			// 
			// listView1
			// 
			this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSpace,
            this.columnHeaderBuyPrice,
            this.columnHeaderBuyAmount,
            this.columnHeaderSellPrice,
            this.columnHeaderSellAmount});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Enabled = false;
			this.listView1.ForeColor = System.Drawing.SystemColors.Info;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.LabelWrap = false;
			this.listView1.Location = new System.Drawing.Point(3, 3);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(630, 405);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.Visible = false;
			// 
			// columnHeaderBuyPrice
			// 
			this.columnHeaderBuyPrice.DisplayIndex = 0;
			this.columnHeaderBuyPrice.Text = "Buy Price";
			this.columnHeaderBuyPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeaderBuyPrice.Width = 120;
			// 
			// columnHeaderBuyAmount
			// 
			this.columnHeaderBuyAmount.DisplayIndex = 1;
			this.columnHeaderBuyAmount.Text = "Buy Amount";
			this.columnHeaderBuyAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeaderBuyAmount.Width = 120;
			// 
			// columnHeaderSpace
			// 
			this.columnHeaderSpace.Text = "";
			this.columnHeaderSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// columnHeaderSellPrice
			// 
			this.columnHeaderSellPrice.Text = "Sell Price";
			this.columnHeaderSellPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeaderSellPrice.Width = 120;
			// 
			// columnHeaderSellAmount
			// 
			this.columnHeaderSellAmount.Text = "Sell Amount";
			this.columnHeaderSellAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeaderSellAmount.Width = 120;
			// 
			// timer2
			// 
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// Bazaar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
			this.ClientSize = new System.Drawing.Size(636, 411);
			this.Controls.Add(this.labelItemNameTip);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Bazaar";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bazaar";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label labelItemNameTip;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.ColumnHeader columnHeaderBuyPrice;
		private System.Windows.Forms.ColumnHeader columnHeaderBuyAmount;
		private System.Windows.Forms.ColumnHeader columnHeaderSellPrice;
		private System.Windows.Forms.ColumnHeader columnHeaderSellAmount;
		private System.Windows.Forms.ColumnHeader columnHeaderSpace;
	}
}