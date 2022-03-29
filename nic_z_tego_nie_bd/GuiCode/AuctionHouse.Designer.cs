
namespace nic_z_tego_nie_bd
{
	partial class AuctionHouse
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
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.listViewItemDetails = new System.Windows.Forms.ListView();
			this.itemNameHeader = new System.Windows.Forms.ColumnHeader();
			this.priceHeader = new System.Windows.Forms.ColumnHeader();
			this.ahIdHeader = new System.Windows.Forms.ColumnHeader();
			this.listViewItems = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timer2
			// 
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// listViewItemDetails
			// 
			this.listViewItemDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewItemDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemNameHeader,
            this.priceHeader,
            this.ahIdHeader});
			this.listViewItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewItemDetails.FullRowSelect = true;
			this.listViewItemDetails.HideSelection = false;
			this.listViewItemDetails.LabelWrap = false;
			this.listViewItemDetails.Location = new System.Drawing.Point(3, 3);
			this.listViewItemDetails.MultiSelect = false;
			this.listViewItemDetails.Name = "listViewItemDetails";
			this.listViewItemDetails.Size = new System.Drawing.Size(794, 444);
			this.listViewItemDetails.TabIndex = 1;
			this.listViewItemDetails.UseCompatibleStateImageBehavior = false;
			this.listViewItemDetails.View = System.Windows.Forms.View.Details;
			this.listViewItemDetails.Visible = false;
			this.listViewItemDetails.DoubleClick += new System.EventHandler(this.listViewItemDetails_DoubleClick);
			// 
			// itemNameHeader
			// 
			this.itemNameHeader.Text = "Item name";
			this.itemNameHeader.Width = 180;
			// 
			// priceHeader
			// 
			this.priceHeader.Text = "Price";
			this.priceHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.priceHeader.Width = 100;
			// 
			// ahIdHeader
			// 
			this.ahIdHeader.Text = "AuctionHouse ID";
			this.ahIdHeader.Width = 240;
			// 
			// listViewItems
			// 
			this.listViewItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewItems.HideSelection = false;
			this.listViewItems.Location = new System.Drawing.Point(3, 3);
			this.listViewItems.MultiSelect = false;
			this.listViewItems.Name = "listViewItems";
			this.listViewItems.Size = new System.Drawing.Size(794, 444);
			this.listViewItems.TabIndex = 2;
			this.listViewItems.UseCompatibleStateImageBehavior = false;
			this.listViewItems.View = System.Windows.Forms.View.List;
			this.listViewItems.DoubleClick += new System.EventHandler(this.listViewItems_DoubleClick);
			// 
			// AuctionHouse
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.ControlBox = false;
			this.Controls.Add(this.listViewItems);
			this.Controls.Add(this.listViewItemDetails);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AuctionHouse";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Text = "AuctionHouseFetcher";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.ListView listViewItemDetails;
		private System.Windows.Forms.ColumnHeader itemNameHeader;
		private System.Windows.Forms.ColumnHeader priceHeader;
		private System.Windows.Forms.ColumnHeader ahIdHeader;
		private System.Windows.Forms.ListView listViewItems;
	}
}