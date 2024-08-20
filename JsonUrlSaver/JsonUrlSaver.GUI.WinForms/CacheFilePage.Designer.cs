namespace JsonUrlSaver.GUI.WinForms
{
	partial class CacheFilePage
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
			lblDir = new System.Windows.Forms.Label();
			tbDir = new System.Windows.Forms.TextBox();
			btnDir = new System.Windows.Forms.Button();
			lblURL = new System.Windows.Forms.Label();
			tbURL = new System.Windows.Forms.TextBox();
			lblJSON = new System.Windows.Forms.Label();
			tbJSON = new System.Windows.Forms.TextBox();
			btnOpen = new System.Windows.Forms.Button();
			fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// lblDir
			// 
			lblDir.AutoSize = true;
			lblDir.Location = new System.Drawing.Point(8, 16);
			lblDir.Name = "lblDir";
			lblDir.Size = new System.Drawing.Size(69, 15);
			lblDir.TabIndex = 0;
			lblDir.Text = "ディレクトリ：";
			lblDir.Click += this.lblDir_Click;
			// 
			// tbDir
			// 
			tbDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbDir.Location = new System.Drawing.Point(80, 8);
			tbDir.Name = "tbDir";
			tbDir.Size = new System.Drawing.Size(664, 23);
			tbDir.TabIndex = 1;
			// 
			// btnDir
			// 
			btnDir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnDir.Location = new System.Drawing.Point(752, 8);
			btnDir.Name = "btnDir";
			btnDir.Size = new System.Drawing.Size(27, 23);
			btnDir.TabIndex = 2;
			btnDir.Text = "...";
			btnDir.UseVisualStyleBackColor = true;
			btnDir.Click += this.btnDir_Click;
			// 
			// lblURL
			// 
			lblURL.AutoSize = true;
			lblURL.Location = new System.Drawing.Point(8, 48);
			lblURL.Name = "lblURL";
			lblURL.Size = new System.Drawing.Size(40, 15);
			lblURL.TabIndex = 3;
			lblURL.Text = "URL：";
			lblURL.Click += this.lblURL_Click;
			// 
			// tbURL
			// 
			tbURL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbURL.Location = new System.Drawing.Point(48, 40);
			tbURL.Name = "tbURL";
			tbURL.Size = new System.Drawing.Size(728, 23);
			tbURL.TabIndex = 4;
			// 
			// lblJSON
			// 
			lblJSON.AutoSize = true;
			lblJSON.Location = new System.Drawing.Point(8, 80);
			lblJSON.Name = "lblJSON";
			lblJSON.Size = new System.Drawing.Size(211, 15);
			lblJSON.TabIndex = 5;
			lblJSON.Text = "下記に URL を含む JSON を入力できます。";
			lblJSON.Click += this.lblJSON_Click;
			// 
			// tbJSON
			// 
			tbJSON.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbJSON.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			tbJSON.Location = new System.Drawing.Point(8, 104);
			tbJSON.Multiline = true;
			tbJSON.Name = "tbJSON";
			tbJSON.Size = new System.Drawing.Size(768, 232);
			tbJSON.TabIndex = 6;
			// 
			// btnOpen
			// 
			btnOpen.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOpen.Location = new System.Drawing.Point(704, 344);
			btnOpen.Name = "btnOpen";
			btnOpen.Size = new System.Drawing.Size(75, 23);
			btnOpen.TabIndex = 7;
			btnOpen.Text = "全て開く";
			btnOpen.UseVisualStyleBackColor = true;
			btnOpen.Click += this.btnOpen_Click;
			// 
			// CacheFilePage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(btnOpen);
			this.Controls.Add(tbJSON);
			this.Controls.Add(lblJSON);
			this.Controls.Add(tbURL);
			this.Controls.Add(lblURL);
			this.Controls.Add(btnDir);
			this.Controls.Add(tbDir);
			this.Controls.Add(lblDir);
			this.Name = "CacheFilePage";
			this.Size = new System.Drawing.Size(786, 370);
			this.Load += this.CacheFilePage_Load;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tbDir;
		private System.Windows.Forms.Button btnDir;
		private System.Windows.Forms.Label lblURL;
		private System.Windows.Forms.TextBox tbURL;
		private System.Windows.Forms.Label lblJSON;
		private System.Windows.Forms.TextBox tbJSON;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.FolderBrowserDialog fbd;
	}
}
