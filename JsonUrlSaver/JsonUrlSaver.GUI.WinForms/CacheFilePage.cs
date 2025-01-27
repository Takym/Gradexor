/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonUrlSaver.GUI.WinForms.Internals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JsonUrlSaver.GUI.WinForms
{
	public partial class CacheFilePage : UserControl
	{
		private ReadOnlyMemory<string> _default_args;

		public CacheFilePage()
		{
			this.InitializeComponent();
		}

		public void SetDefaultArgs(ReadOnlyMemory<string> defaultArgs)
			=> _default_args = defaultArgs;

		private void CacheFilePage_Load(object sender, EventArgs e)
		{
			// do nothing
		}

		private void btnDir_Click(object sender, EventArgs e)
		{
			if (fbd.ShowDialog(this) == DialogResult.OK) {
				tbDir.Text = fbd.SelectedPath;
			}

			this.lblDir_Click(sender, e);
		}

		private void lblDir_Click(object sender, EventArgs e)
		{
			tbDir.Select();
			tbDir.Focus();
		}

		private void lblURL_Click(object sender, EventArgs e)
		{
			tbURL.Select();
			tbURL.Focus();
		}

		private void lblJSON_Click(object sender, EventArgs e)
		{
			tbJSON.Select();
			tbJSON.Focus();
		}

		private async void btnOpen_Click(object sender, EventArgs e)
		{
			this.Enabled = false;

			string? dir  = tbDir .Text;
			string? url  = tbURL .Text;
			string? json = tbJSON.Text;

			if (string.IsNullOrEmpty(url)  &&
				string.IsNullOrEmpty(json) &&
				MessageBox.Show(
					this,
					$"URL または JSON が入力されていません。\r\n代わりにディレクトリ内の全ての JSON ファイル内の URL からキャッシュファイルを開きますか？",
					nameof(JsonUrlSaver),
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				) == DialogResult.No) {
				this.Enabled = true;
				return;
			}

			string logFile = await LoggerWrapper.RunAsync("CF",
				async () =>
					await Task.Run(() =>
						Host.CreateDefaultBuilder(_default_args.ToArray())
							.InitializeJsonUrlSaver()
							.ConfigureServices(builder => builder
								.AddSingleton<ICacheFileIndexSelector, WinFormsCacheFileIndexSelector>()
							)
							.ConfigureAppConfiguration(builder => builder
								.AddInMemoryCollection([
									new(nameof(dir ), dir       ),
									new(nameof(url ), url       ),
									new(nameof(json), json      ),
									new("mode",       "openOnly")
								])
							)
							.Build()
							.RunJsonUrlSaver()
					).ConfigureAwait(false)
			).ConfigureAwait(true);

			var dr = MessageBox.Show(
				this,
				$"選択された全てのキャッシュファイルを開きました。ログファイルを閲覧しますか？\r\nログファイルのパス：{logFile}",
				nameof(JsonUrlSaver),
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question
			);

			this.Enabled = true;

			if (dr == DialogResult.Yes) {
				FormMain.Open(logFile);
			}
		}
	}
}
