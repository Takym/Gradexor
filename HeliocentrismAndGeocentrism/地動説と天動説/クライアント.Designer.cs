namespace 地動説と天動説
{
	partial class クライアント
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
			// !!! 手動で書き加えたコード ここから
			if (disposing) {
				_stars.Dispose();
			}
			// !!! 手動で書き加えたコード ここまで

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
			components = new System.ComponentModel.Container();
			_timer = new System.Windows.Forms.Timer(components);
			this.SuspendLayout();
			// 
			// _timer
			// 
			_timer.Enabled = true;
			_timer.Tick += this._timer_Tick;
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Timer _timer;
	}
}
