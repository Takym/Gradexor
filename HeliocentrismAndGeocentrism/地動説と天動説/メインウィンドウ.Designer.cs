namespace 地動説と天動説
{
	partial class メインウィンドウ
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
			if (disposing && (components != null)) {
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
			menuStrip = new System.Windows.Forms.MenuStrip();
			動作モードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			地動説ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			天動説ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			クライアント = new クライアント();
			デバッグ情報ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { 動作モードToolStripMenuItem, デバッグ情報ToolStripMenuItem });
			menuStrip.Location = new System.Drawing.Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new System.Drawing.Size(1257, 24);
			menuStrip.TabIndex = 0;
			menuStrip.Text = "menuStrip1";
			// 
			// 動作モードToolStripMenuItem
			// 
			動作モードToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { 地動説ToolStripMenuItem, 天動説ToolStripMenuItem });
			動作モードToolStripMenuItem.Name = "動作モードToolStripMenuItem";
			動作モードToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			動作モードToolStripMenuItem.Text = "動作モード";
			// 
			// 地動説ToolStripMenuItem
			// 
			地動説ToolStripMenuItem.Name = "地動説ToolStripMenuItem";
			地動説ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			地動説ToolStripMenuItem.Text = "地動説";
			地動説ToolStripMenuItem.Click += this.地動説ToolStripMenuItem_Click;
			// 
			// 天動説ToolStripMenuItem
			// 
			天動説ToolStripMenuItem.Name = "天動説ToolStripMenuItem";
			天動説ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			天動説ToolStripMenuItem.Text = "天動説";
			天動説ToolStripMenuItem.Click += this.天動説ToolStripMenuItem_Click;
			// 
			// クライアント
			// 
			クライアント.Dock = System.Windows.Forms.DockStyle.Fill;
			クライアント.Location = new System.Drawing.Point(0, 24);
			クライアント.Name = "クライアント";
			クライアント.Size = new System.Drawing.Size(1257, 636);
			クライアント.TabIndex = 1;
			クライアント.Text = "クライアント";
			// 
			// デバッグ情報ToolStripMenuItem
			// 
			デバッグ情報ToolStripMenuItem.Name = "デバッグ情報ToolStripMenuItem";
			デバッグ情報ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			デバッグ情報ToolStripMenuItem.Text = "デバッグ情報";
			デバッグ情報ToolStripMenuItem.Click += this.デバッグ情報ToolStripMenuItem_Click;
			// 
			// メインウィンドウ
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1257, 660);
			this.Controls.Add(クライアント);
			this.Controls.Add(menuStrip);
			this.MainMenuStrip = menuStrip;
			this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
			this.Name = "メインウィンドウ";
			this.Text = "地動説と天動説";
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private クライアント クライアント;
		private System.Windows.Forms.ToolStripMenuItem 動作モードToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 地動説ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 天動説ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem デバッグ情報ToolStripMenuItem;
	}
}
