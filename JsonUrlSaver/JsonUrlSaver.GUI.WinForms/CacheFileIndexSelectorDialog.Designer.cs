namespace JsonUrlSaver.GUI.WinForms
{
	partial class CacheFileIndexSelectorDialog
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
			if (disposing && (components != null)) {
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
			lblURLDesc = new System.Windows.Forms.Label();
			lblURL = new System.Windows.Forms.LinkLabel();
			lblIndex = new System.Windows.Forms.Label();
			nudIndex = new System.Windows.Forms.NumericUpDown();
			btnOK = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)nudIndex).BeginInit();
			this.SuspendLayout();
			// 
			// lblURLDesc
			// 
			lblURLDesc.AutoSize = true;
			lblURLDesc.Location = new System.Drawing.Point(8, 8);
			lblURLDesc.Name = "lblURLDesc";
			lblURLDesc.Size = new System.Drawing.Size(77, 15);
			lblURLDesc.TabIndex = 0;
			lblURLDesc.Text = "対象の URL：";
			// 
			// lblURL
			// 
			lblURL.AutoSize = true;
			lblURL.Location = new System.Drawing.Point(8, 32);
			lblURL.Name = "lblURL";
			lblURL.Size = new System.Drawing.Size(41, 15);
			lblURL.TabIndex = 1;
			lblURL.TabStop = true;
			lblURL.Text = "lblURL";
			lblURL.LinkClicked += this.lblURL_LinkClicked;
			// 
			// lblIndex
			// 
			lblIndex.AutoSize = true;
			lblIndex.Location = new System.Drawing.Point(8, 56);
			lblIndex.Name = "lblIndex";
			lblIndex.Size = new System.Drawing.Size(130, 15);
			lblIndex.TabIndex = 2;
			lblIndex.Text = "キャッシュファイルの番号：";
			// 
			// nudIndex
			// 
			nudIndex.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			nudIndex.Location = new System.Drawing.Point(8, 80);
			nudIndex.Name = "nudIndex";
			nudIndex.Size = new System.Drawing.Size(324, 23);
			nudIndex.TabIndex = 3;
			nudIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// btnOK
			// 
			btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOK.Location = new System.Drawing.Point(256, 128);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(75, 23);
			btnOK.TabIndex = 4;
			btnOK.Text = "決定(&O)";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += this.btnOK_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.Location = new System.Drawing.Point(176, 128);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(75, 23);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "取り消す(&C)";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += this.btnCancel_Click;
			// 
			// CacheFileIndexSelectorDialog
			// 
			this.AcceptButton = btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = btnCancel;
			this.ClientSize = new System.Drawing.Size(336, 153);
			this.ControlBox = false;
			this.Controls.Add(btnCancel);
			this.Controls.Add(btnOK);
			this.Controls.Add(nudIndex);
			this.Controls.Add(lblIndex);
			this.Controls.Add(lblURL);
			this.Controls.Add(lblURLDesc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CacheFileIndexSelectorDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "キャッシュファイルの番号を選んでください。";
			this.Load += this.CacheFileIndexSelectorDialog_Load;
			((System.ComponentModel.ISupportInitialize)nudIndex).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label lblURLDesc;
		private System.Windows.Forms.LinkLabel lblURL;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.NumericUpDown nudIndex;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}