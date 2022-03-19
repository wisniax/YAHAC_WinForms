
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
			this.chuj = new System.Windows.Forms.Timer(this.components);
			this.panelSideMenu = new System.Windows.Forms.Panel();
			this.buttonBazaar = new System.Windows.Forms.Button();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.panelSideMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// chuj
			// 
			this.chuj.Enabled = true;
			this.chuj.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// panelSideMenu
			// 
			this.panelSideMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelSideMenu.BackColor = System.Drawing.Color.Black;
			this.panelSideMenu.Controls.Add(this.buttonBazaar);
			this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
			this.panelSideMenu.Name = "panelSideMenu";
			this.panelSideMenu.Padding = new System.Windows.Forms.Padding(3);
			this.panelSideMenu.Size = new System.Drawing.Size(150, 450);
			this.panelSideMenu.TabIndex = 0;
			// 
			// buttonBazaar
			// 
			this.buttonBazaar.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.buttonBazaar.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonBazaar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonBazaar.Font = new System.Drawing.Font("Sigmar One", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonBazaar.Location = new System.Drawing.Point(3, 3);
			this.buttonBazaar.Name = "buttonBazaar";
			this.buttonBazaar.Size = new System.Drawing.Size(144, 40);
			this.buttonBazaar.TabIndex = 0;
			this.buttonBazaar.Text = "Open Bazaar";
			this.buttonBazaar.UseVisualStyleBackColor = false;
			this.buttonBazaar.Click += new System.EventHandler(this.buttonBazaar_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.Location = new System.Drawing.Point(153, 3);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(646, 447);
			this.mainPanel.TabIndex = 2;
			// 
			// MainGui
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelSideMenu);
			this.Controls.Add(this.mainPanel);
			this.Name = "MainGui";
			this.Text = "Form1";
			this.panelSideMenu.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer chuj;
		private System.Windows.Forms.Panel panelSideMenu;
		private System.Windows.Forms.Button buttonBazaar;
		private System.Windows.Forms.Panel mainPanel;
	}
}

