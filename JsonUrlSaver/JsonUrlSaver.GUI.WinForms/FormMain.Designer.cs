namespace JsonUrlSaver.GUI.WinForms
{
	partial class FormMain
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
			settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			commonSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			developmentSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			productionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			getSourceCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			releaseNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			helpToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			readmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			statusStrip = new System.Windows.Forms.StatusStrip();
			tabControl = new System.Windows.Forms.TabControl();
			downloadTab = new System.Windows.Forms.TabPage();
			downloadPage = new DownloadPage();
			cacheFileTab = new System.Windows.Forms.TabPage();
			cacheFilePage = new CacheFilePage();
			argsTab = new System.Windows.Forms.TabPage();
			tbArgs = new System.Windows.Forms.TextBox();
			menuStrip.SuspendLayout();
			tabControl.SuspendLayout();
			downloadTab.SuspendLayout();
			cacheFileTab.SuspendLayout();
			argsTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsToolStripMenuItem, helpToolStripMenuItem });
			menuStrip.Location = new System.Drawing.Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new System.Drawing.Size(800, 24);
			menuStrip.TabIndex = 1;
			menuStrip.Text = "menuStrip";
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { commonSettingsToolStripMenuItem, developmentSettingsToolStripMenuItem, productionSettingsToolStripMenuItem });
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			settingsToolStripMenuItem.Text = "設定(&S)";
			// 
			// commonSettingsToolStripMenuItem
			// 
			commonSettingsToolStripMenuItem.Name = "commonSettingsToolStripMenuItem";
			commonSettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q;
			commonSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			commonSettingsToolStripMenuItem.Text = "共通(&C)";
			commonSettingsToolStripMenuItem.Click += this.commonSettingsToolStripMenuItem_Click;
			// 
			// developmentSettingsToolStripMenuItem
			// 
			developmentSettingsToolStripMenuItem.Name = "developmentSettingsToolStripMenuItem";
			developmentSettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W;
			developmentSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			developmentSettingsToolStripMenuItem.Text = "開発(&D)";
			developmentSettingsToolStripMenuItem.Click += this.developmentSettingsToolStripMenuItem_Click;
			// 
			// productionSettingsToolStripMenuItem
			// 
			productionSettingsToolStripMenuItem.Name = "productionSettingsToolStripMenuItem";
			productionSettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
			productionSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			productionSettingsToolStripMenuItem.Text = "製品(&P)";
			productionSettingsToolStripMenuItem.Click += this.productionSettingsToolStripMenuItem_Click;
			// 
			// helpToolStripMenuItem
			// 
			helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { getSourceCodeToolStripMenuItem, releaseNotesToolStripMenuItem, helpToolStripSeparator1, readmeToolStripMenuItem, licenseToolStripMenuItem });
			helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			helpToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
			helpToolStripMenuItem.Text = "ヘルプ(&H)";
			// 
			// getSourceCodeToolStripMenuItem
			// 
			getSourceCodeToolStripMenuItem.Name = "getSourceCodeToolStripMenuItem";
			getSourceCodeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E;
			getSourceCodeToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			getSourceCodeToolStripMenuItem.Text = "最新のソースコードを取得する(&C)";
			getSourceCodeToolStripMenuItem.Click += this.getSourceCodeToolStripMenuItem_Click;
			// 
			// releaseNotesToolStripMenuItem
			// 
			releaseNotesToolStripMenuItem.Name = "releaseNotesToolStripMenuItem";
			releaseNotesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R;
			releaseNotesToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			releaseNotesToolStripMenuItem.Text = "リリースノート(&R)";
			releaseNotesToolStripMenuItem.Click += this.releaseNotesToolStripMenuItem_Click;
			// 
			// helpToolStripSeparator1
			// 
			helpToolStripSeparator1.Name = "helpToolStripSeparator1";
			helpToolStripSeparator1.Size = new System.Drawing.Size(258, 6);
			// 
			// readmeToolStripMenuItem
			// 
			readmeToolStripMenuItem.Name = "readmeToolStripMenuItem";
			readmeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			readmeToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			readmeToolStripMenuItem.Text = "簡易説明書(&M)";
			readmeToolStripMenuItem.Click += this.readmeToolStripMenuItem_Click;
			// 
			// licenseToolStripMenuItem
			// 
			licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
			licenseToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			licenseToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			licenseToolStripMenuItem.Text = "使用許諾書(&L)";
			licenseToolStripMenuItem.Click += this.licenseToolStripMenuItem_Click;
			// 
			// statusStrip
			// 
			statusStrip.Location = new System.Drawing.Point(0, 428);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new System.Drawing.Size(800, 22);
			statusStrip.TabIndex = 0;
			statusStrip.Text = "statusStrip";
			// 
			// tabControl
			// 
			tabControl.Controls.Add(downloadTab);
			tabControl.Controls.Add(cacheFileTab);
			tabControl.Controls.Add(argsTab);
			tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControl.Location = new System.Drawing.Point(0, 24);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(800, 404);
			tabControl.TabIndex = 2;
			// 
			// downloadTab
			// 
			downloadTab.Controls.Add(downloadPage);
			downloadTab.Location = new System.Drawing.Point(4, 24);
			downloadTab.Name = "downloadTab";
			downloadTab.Padding = new System.Windows.Forms.Padding(3);
			downloadTab.Size = new System.Drawing.Size(792, 376);
			downloadTab.TabIndex = 0;
			downloadTab.Text = "ダウンロード";
			downloadTab.UseVisualStyleBackColor = true;
			// 
			// downloadPage
			// 
			downloadPage.Dock = System.Windows.Forms.DockStyle.Fill;
			downloadPage.Location = new System.Drawing.Point(3, 3);
			downloadPage.Name = "downloadPage";
			downloadPage.Size = new System.Drawing.Size(786, 370);
			downloadPage.TabIndex = 0;
			// 
			// cacheFileTab
			// 
			cacheFileTab.Controls.Add(cacheFilePage);
			cacheFileTab.Location = new System.Drawing.Point(4, 24);
			cacheFileTab.Name = "cacheFileTab";
			cacheFileTab.Padding = new System.Windows.Forms.Padding(3);
			cacheFileTab.Size = new System.Drawing.Size(792, 376);
			cacheFileTab.TabIndex = 1;
			cacheFileTab.Text = "キャッシュファイル";
			cacheFileTab.UseVisualStyleBackColor = true;
			// 
			// cacheFilePage
			// 
			cacheFilePage.Dock = System.Windows.Forms.DockStyle.Fill;
			cacheFilePage.Location = new System.Drawing.Point(3, 3);
			cacheFilePage.Name = "cacheFilePage";
			cacheFilePage.Size = new System.Drawing.Size(786, 370);
			cacheFilePage.TabIndex = 0;
			// 
			// argsTab
			// 
			argsTab.Controls.Add(tbArgs);
			argsTab.Location = new System.Drawing.Point(4, 24);
			argsTab.Name = "argsTab";
			argsTab.Padding = new System.Windows.Forms.Padding(3);
			argsTab.Size = new System.Drawing.Size(792, 376);
			argsTab.TabIndex = 2;
			argsTab.Text = "コマンド行引数";
			argsTab.UseVisualStyleBackColor = true;
			// 
			// tbArgs
			// 
			tbArgs.Dock = System.Windows.Forms.DockStyle.Fill;
			tbArgs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			tbArgs.Location = new System.Drawing.Point(3, 3);
			tbArgs.Multiline = true;
			tbArgs.Name = "tbArgs";
			tbArgs.ReadOnly = true;
			tbArgs.Size = new System.Drawing.Size(786, 370);
			tbArgs.TabIndex = 0;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(tabControl);
			this.Controls.Add(menuStrip);
			this.Controls.Add(statusStrip);
			this.MainMenuStrip = menuStrip;
			this.MinimumSize = new System.Drawing.Size(720, 400);
			this.Name = "FormMain";
			this.Text = "JsonUrlSaver";
			this.Load += this.FormMain_Load;
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			tabControl.ResumeLayout(false);
			downloadTab.ResumeLayout(false);
			cacheFileTab.ResumeLayout(false);
			argsTab.ResumeLayout(false);
			argsTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem commonSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem developmentSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem productionSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getSourceCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem releaseNotesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator helpToolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem readmeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage downloadTab;
		private System.Windows.Forms.TabPage cacheFileTab;
		private System.Windows.Forms.TabPage argsTab;
		private DownloadPage downloadPage;
		private CacheFilePage cacheFilePage;
		private System.Windows.Forms.TextBox tbArgs;
	}
}
