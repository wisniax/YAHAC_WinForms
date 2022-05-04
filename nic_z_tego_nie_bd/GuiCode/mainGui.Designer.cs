
namespace nic_z_tego_nie_bd
{
    partial class MainGui
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.timerBZ = new System.Windows.Forms.Timer(this.components);
			this.panelSideMenu = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonSettings = new System.Windows.Forms.Button();
			this.buttonAh = new System.Windows.Forms.Button();
			this.buttonBazaar = new System.Windows.Forms.Button();
			this.timerAH = new System.Windows.Forms.Timer(this.components);
			this.ahAgeBox = new System.Windows.Forms.TextBox();
			this.bzAgeBox = new System.Windows.Forms.TextBox();
			this.apiReqBox = new System.Windows.Forms.TextBox();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.timerRefScreenTimer = new System.Windows.Forms.Timer(this.components);
			this.buttonBetterAh = new System.Windows.Forms.Button();
			this.panelSideMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// timerBZ
			// 
			this.timerBZ.Enabled = true;
			this.timerBZ.Interval = 1000;
			this.timerBZ.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// panelSideMenu
			// 
			this.panelSideMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelSideMenu.BackColor = System.Drawing.Color.Black;
			this.panelSideMenu.Controls.Add(this.buttonBetterAh);
			this.panelSideMenu.Controls.Add(this.button1);
			this.panelSideMenu.Controls.Add(this.buttonSettings);
			this.panelSideMenu.Controls.Add(this.buttonAh);
			this.panelSideMenu.Controls.Add(this.buttonBazaar);
			this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
			this.panelSideMenu.Margin = new System.Windows.Forms.Padding(0);
			this.panelSideMenu.Name = "panelSideMenu";
			this.panelSideMenu.Padding = new System.Windows.Forms.Padding(3);
			this.panelSideMenu.Size = new System.Drawing.Size(150, 411);
			this.panelSideMenu.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.button1.Dock = System.Windows.Forms.DockStyle.Top;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.button1.Location = new System.Drawing.Point(3, 83);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(144, 40);
			this.button1.TabIndex = 3;
			this.button1.Text = "Item Crafts";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonSettings
			// 
			this.buttonSettings.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.buttonSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSettings.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonSettings.Location = new System.Drawing.Point(3, 368);
			this.buttonSettings.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonSettings.Name = "buttonSettings";
			this.buttonSettings.Size = new System.Drawing.Size(144, 40);
			this.buttonSettings.TabIndex = 2;
			this.buttonSettings.Text = "Settings";
			this.buttonSettings.UseVisualStyleBackColor = false;
			this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
			// 
			// buttonAh
			// 
			this.buttonAh.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.buttonAh.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonAh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAh.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonAh.Location = new System.Drawing.Point(3, 43);
			this.buttonAh.Margin = new System.Windows.Forms.Padding(1);
			this.buttonAh.Name = "buttonAh";
			this.buttonAh.Size = new System.Drawing.Size(144, 40);
			this.buttonAh.TabIndex = 1;
			this.buttonAh.Text = "AH";
			this.buttonAh.UseVisualStyleBackColor = false;
			this.buttonAh.Click += new System.EventHandler(this.buttonAh_Click);
			// 
			// buttonBazaar
			// 
			this.buttonBazaar.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.buttonBazaar.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonBazaar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonBazaar.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonBazaar.Location = new System.Drawing.Point(3, 3);
			this.buttonBazaar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonBazaar.Name = "buttonBazaar";
			this.buttonBazaar.Size = new System.Drawing.Size(144, 40);
			this.buttonBazaar.TabIndex = 0;
			this.buttonBazaar.Text = "Bazaar";
			this.buttonBazaar.UseVisualStyleBackColor = false;
			this.buttonBazaar.Click += new System.EventHandler(this.buttonBazaar_Click);
			// 
			// timerAH
			// 
			this.timerAH.Enabled = true;
			this.timerAH.Interval = 1000;
			this.timerAH.Tick += new System.EventHandler(this.timerAH_Tick);
			// 
			// ahAgeBox
			// 
			this.ahAgeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ahAgeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ahAgeBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.ahAgeBox.Location = new System.Drawing.Point(748, 383);
			this.ahAgeBox.MaxLength = 4;
			this.ahAgeBox.Name = "ahAgeBox";
			this.ahAgeBox.ReadOnly = true;
			this.ahAgeBox.Size = new System.Drawing.Size(24, 16);
			this.ahAgeBox.TabIndex = 3;
			this.ahAgeBox.Text = "AH";
			this.ahAgeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// bzAgeBox
			// 
			this.bzAgeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bzAgeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.bzAgeBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.bzAgeBox.Location = new System.Drawing.Point(718, 383);
			this.bzAgeBox.MaxLength = 4;
			this.bzAgeBox.Name = "bzAgeBox";
			this.bzAgeBox.ReadOnly = true;
			this.bzAgeBox.Size = new System.Drawing.Size(24, 16);
			this.bzAgeBox.TabIndex = 4;
			this.bzAgeBox.Text = "bz";
			this.bzAgeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// apiReqBox
			// 
			this.apiReqBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.apiReqBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.apiReqBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.apiReqBox.Location = new System.Drawing.Point(718, 361);
			this.apiReqBox.MaxLength = 10;
			this.apiReqBox.Name = "apiReqBox";
			this.apiReqBox.ReadOnly = true;
			this.apiReqBox.Size = new System.Drawing.Size(54, 16);
			this.apiReqBox.TabIndex = 5;
			this.apiReqBox.Text = "req/sec";
			this.apiReqBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.Location = new System.Drawing.Point(148, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(636, 411);
			this.mainPanel.TabIndex = 2;
			// 
			// timerRefScreenTimer
			// 
			this.timerRefScreenTimer.Enabled = true;
			this.timerRefScreenTimer.Tick += new System.EventHandler(this.timerRefScreenTimer_Tick);
			// 
			// buttonBetterAh
			// 
			this.buttonBetterAh.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.buttonBetterAh.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonBetterAh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonBetterAh.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonBetterAh.Location = new System.Drawing.Point(3, 123);
			this.buttonBetterAh.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonBetterAh.Name = "buttonBetterAh";
			this.buttonBetterAh.Size = new System.Drawing.Size(144, 40);
			this.buttonBetterAh.TabIndex = 4;
			this.buttonBetterAh.Text = "Better AH";
			this.buttonBetterAh.UseVisualStyleBackColor = false;
			this.buttonBetterAh.Click += new System.EventHandler(this.buttonBetterAh_Click);
			// 
			// MainGui
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 411);
			this.Controls.Add(this.apiReqBox);
			this.Controls.Add(this.panelSideMenu);
			this.Controls.Add(this.ahAgeBox);
			this.Controls.Add(this.bzAgeBox);
			this.Controls.Add(this.mainPanel);
			this.Name = "MainGui";
			this.Text = "Form1";
			this.panelSideMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerBZ;
		private System.Windows.Forms.Panel panelSideMenu;
		private System.Windows.Forms.Button buttonBazaar;
		private System.Windows.Forms.Button buttonAh;
		private System.Windows.Forms.Timer timerAH;
		private System.Windows.Forms.TextBox ahAgeBox;
		private System.Windows.Forms.TextBox bzAgeBox;
		private System.Windows.Forms.TextBox apiReqBox;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Timer timerRefScreenTimer;
		private System.Windows.Forms.Button buttonSettings;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonBetterAh;
	}
}

