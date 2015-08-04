namespace Zalgo
{
	partial class ZalgoService
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZalgoService));
			this._hotkeyLabel = new System.Windows.Forms.Label();
			this._trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this._linkLabel = new System.Windows.Forms.LinkLabel();
			this._densityNumeric = new System.Windows.Forms.NumericUpDown();
			this._densityLabel = new System.Windows.Forms.Label();
			this._enabledLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this._densityNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// _hotkeyLabel
			// 
			this._hotkeyLabel.AutoSize = true;
			this._hotkeyLabel.Location = new System.Drawing.Point(12, 53);
			this._hotkeyLabel.Name = "_hotkeyLabel";
			this._hotkeyLabel.Size = new System.Drawing.Size(117, 13);
			this._hotkeyLabel.TabIndex = 0;
			this._hotkeyLabel.Text = "Toggle: Shift + Win + Z";
			// 
			// _trayIcon
			// 
			this._trayIcon.Text = "Zalgo";
			this._trayIcon.Visible = true;
			this._trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
			this._trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
			// 
			// _linkLabel
			// 
			this._linkLabel.AutoSize = true;
			this._linkLabel.Location = new System.Drawing.Point(167, 53);
			this._linkLabel.Name = "_linkLabel";
			this._linkLabel.Size = new System.Drawing.Size(57, 13);
			this._linkLabel.TabIndex = 1;
			this._linkLabel.TabStop = true;
			this._linkLabel.Text = "pt-get.com";
			this._linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
			// 
			// _densityNumeric
			// 
			this._densityNumeric.Location = new System.Drawing.Point(60, 7);
			this._densityNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._densityNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._densityNumeric.Name = "_densityNumeric";
			this._densityNumeric.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this._densityNumeric.Size = new System.Drawing.Size(60, 20);
			this._densityNumeric.TabIndex = 2;
			this._densityNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// _densityLabel
			// 
			this._densityLabel.AutoSize = true;
			this._densityLabel.Location = new System.Drawing.Point(12, 9);
			this._densityLabel.Name = "_densityLabel";
			this._densityLabel.Size = new System.Drawing.Size(42, 13);
			this._densityLabel.TabIndex = 3;
			this._densityLabel.Text = "Density";
			// 
			// _enabledLabel
			// 
			this._enabledLabel.AutoSize = true;
			this._enabledLabel.Location = new System.Drawing.Point(167, 9);
			this._enabledLabel.Name = "_enabledLabel";
			this._enabledLabel.Size = new System.Drawing.Size(48, 13);
			this._enabledLabel.TabIndex = 4;
			this._enabledLabel.Text = "Disabled";
			// 
			// ZalgoService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(236, 75);
			this.Controls.Add(this._enabledLabel);
			this.Controls.Add(this._densityLabel);
			this.Controls.Add(this._densityNumeric);
			this.Controls.Add(this._linkLabel);
			this.Controls.Add(this._hotkeyLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ZalgoService";
			this.Text = "Zalgo";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZalgoService_FormClosing);
			this.Load += new System.EventHandler(this.ZalgoService_Load);
			this.Shown += new System.EventHandler(this.ZalgoService_Shown);
			this.Resize += new System.EventHandler(this.ZalgoService_Resize);
			((System.ComponentModel.ISupportInitialize)(this._densityNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _hotkeyLabel;
		private System.Windows.Forms.NotifyIcon _trayIcon;
		private System.Windows.Forms.LinkLabel _linkLabel;
		private System.Windows.Forms.NumericUpDown _densityNumeric;
		private System.Windows.Forms.Label _densityLabel;
		private System.Windows.Forms.Label _enabledLabel;
	}
}

