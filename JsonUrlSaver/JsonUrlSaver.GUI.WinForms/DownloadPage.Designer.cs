namespace JsonUrlSaver.GUI.WinForms
{
	partial class DownloadPage
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			fbd = new System.Windows.Forms.FolderBrowserDialog();
			cmbInputType = new System.Windows.Forms.ComboBox();
			tbInputPath = new System.Windows.Forms.TextBox();
			btnInputDialog = new System.Windows.Forms.Button();
			ofd = new System.Windows.Forms.OpenFileDialog();
			slackCfg = new System.Windows.Forms.GroupBox();
			tbSlackToken = new System.Windows.Forms.TextBox();
			lblSlackToken = new System.Windows.Forms.Label();
			cbSlackFilter = new System.Windows.Forms.CheckBox();
			lblSlackDesc = new System.Windows.Forms.Label();
			btnDownload = new System.Windows.Forms.Button();
			cbPreserveOnlyOne = new System.Windows.Forms.CheckBox();
			slackCfg.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbInputType
			// 
			cmbInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbInputType.FormattingEnabled = true;
			cmbInputType.Location = new System.Drawing.Point(8, 8);
			cmbInputType.Name = "cmbInputType";
			cmbInputType.Size = new System.Drawing.Size(121, 23);
			cmbInputType.TabIndex = 0;
			cmbInputType.SelectedIndexChanged += this.cmbInputType_SelectedIndexChanged;
			// 
			// tbInputPath
			// 
			tbInputPath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbInputPath.Location = new System.Drawing.Point(136, 8);
			tbInputPath.Name = "tbInputPath";
			tbInputPath.Size = new System.Drawing.Size(608, 23);
			tbInputPath.TabIndex = 1;
			// 
			// btnInputDialog
			// 
			btnInputDialog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnInputDialog.Location = new System.Drawing.Point(752, 8);
			btnInputDialog.Name = "btnInputDialog";
			btnInputDialog.Size = new System.Drawing.Size(27, 23);
			btnInputDialog.TabIndex = 2;
			btnInputDialog.Text = "...";
			btnInputDialog.UseVisualStyleBackColor = true;
			btnInputDialog.Click += this.btnInputDialog_Click;
			// 
			// ofd
			// 
			ofd.AddExtension = false;
			ofd.Filter = "ZIP ファイル|*.zip|全てのファイル|*";
			ofd.SupportMultiDottedExtensions = true;
			// 
			// slackCfg
			// 
			slackCfg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			slackCfg.Controls.Add(tbSlackToken);
			slackCfg.Controls.Add(lblSlackToken);
			slackCfg.Controls.Add(cbSlackFilter);
			slackCfg.Controls.Add(lblSlackDesc);
			slackCfg.Location = new System.Drawing.Point(8, 72);
			slackCfg.Name = "slackCfg";
			slackCfg.Size = new System.Drawing.Size(768, 104);
			slackCfg.TabIndex = 4;
			slackCfg.TabStop = false;
			slackCfg.Text = "Slack 設定";
			// 
			// tbSlackToken
			// 
			tbSlackToken.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbSlackToken.Location = new System.Drawing.Point(120, 72);
			tbSlackToken.Name = "tbSlackToken";
			tbSlackToken.Size = new System.Drawing.Size(640, 23);
			tbSlackToken.TabIndex = 3;
			tbSlackToken.UseSystemPasswordChar = true;
			// 
			// lblSlackToken
			// 
			lblSlackToken.AutoSize = true;
			lblSlackToken.Location = new System.Drawing.Point(8, 80);
			lblSlackToken.Name = "lblSlackToken";
			lblSlackToken.Size = new System.Drawing.Size(105, 15);
			lblSlackToken.TabIndex = 2;
			lblSlackToken.Text = "User OAuth Token:";
			lblSlackToken.Click += this.lblSlackToken_Click;
			// 
			// cbSlackFilter
			// 
			cbSlackFilter.AutoSize = true;
			cbSlackFilter.Location = new System.Drawing.Point(8, 48);
			cbSlackFilter.Name = "cbSlackFilter";
			cbSlackFilter.Size = new System.Drawing.Size(155, 19);
			cbSlackFilter.TabIndex = 1;
			cbSlackFilter.Text = "URL を Slack 用に絞り込む";
			cbSlackFilter.UseVisualStyleBackColor = true;
			// 
			// lblSlackDesc
			// 
			lblSlackDesc.AutoSize = true;
			lblSlackDesc.Location = new System.Drawing.Point(8, 24);
			lblSlackDesc.Name = "lblSlackDesc";
			lblSlackDesc.Size = new System.Drawing.Size(520, 15);
			lblSlackDesc.TabIndex = 0;
			lblSlackDesc.Text = "Slack からエクスポートして得られるアーカイブの JSON ファイルからファイルをダウンロードする為の設定項目です。";
			// 
			// btnDownload
			// 
			btnDownload.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnDownload.Location = new System.Drawing.Point(688, 344);
			btnDownload.Name = "btnDownload";
			btnDownload.Size = new System.Drawing.Size(91, 23);
			btnDownload.TabIndex = 5;
			btnDownload.Text = "ダウンロード開始";
			btnDownload.UseVisualStyleBackColor = true;
			btnDownload.Click += this.btnDownload_Click;
			// 
			// cbPreserveOnlyOne
			// 
			cbPreserveOnlyOne.AutoSize = true;
			cbPreserveOnlyOne.Location = new System.Drawing.Point(8, 40);
			cbPreserveOnlyOne.Name = "cbPreserveOnlyOne";
			cbPreserveOnlyOne.Size = new System.Drawing.Size(448, 19);
			cbPreserveOnlyOne.TabIndex = 3;
			cbPreserveOnlyOne.Text = "同じ URL のファイルは一つのみ保持する（既にキャッシュファイルがある場合、上書きする）";
			cbPreserveOnlyOne.UseVisualStyleBackColor = true;
			// 
			// DownloadPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(cbPreserveOnlyOne);
			this.Controls.Add(btnDownload);
			this.Controls.Add(slackCfg);
			this.Controls.Add(btnInputDialog);
			this.Controls.Add(tbInputPath);
			this.Controls.Add(cmbInputType);
			this.Name = "DownloadPage";
			this.Size = new System.Drawing.Size(786, 370);
			this.Load += this.DownloadPage_Load;
			slackCfg.ResumeLayout(false);
			slackCfg.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.ComboBox cmbInputType;
		private System.Windows.Forms.TextBox tbInputPath;
		private System.Windows.Forms.Button btnInputDialog;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.GroupBox slackCfg;
		private System.Windows.Forms.Label lblSlackDesc;
		private System.Windows.Forms.CheckBox cbSlackFilter;
		private System.Windows.Forms.Label lblSlackToken;
		private System.Windows.Forms.TextBox tbSlackToken;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.CheckBox cbPreserveOnlyOne;
	}
}
