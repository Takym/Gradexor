/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace JsonUrlSaver.GUI.WinForms
{
	public partial class DownloadPage : UserControl
	{
		private ReadOnlyMemory<string> _default_args;

		public DownloadPage()
		{
			this.InitializeComponent();
		}

		public void SetDefaultArgs(ReadOnlyMemory<string> defaultArgs)
			=> _default_args = defaultArgs;

		private void DownloadPage_Load(object sender, EventArgs e)
		{
			cmbInputType.Items.AddRange([
				new InputType(
					"ディレクトリ",
					static @this => {
						if (@this.fbd.ShowDialog(@this) == DialogResult.OK) {
							@this.tbInputPath.Text = @this.fbd.SelectedPath;
						}
					},
					static (@this, dict) => dict["dir"] = @this.tbInputPath.Text
				),
				new InputType(
					"ZIP ファイル",
					static @this => {
						if (@this.ofd.ShowDialog(@this) == DialogResult.OK) {
							@this.tbInputPath.Text = @this.ofd.FileName;
						}
					},
					static (@this, dict) => {
						dict["zip"         ] = @this.tbInputPath.Text;
						dict["zipOverwrite"] = true.ToString();
					}
				)
			]);
			cmbInputType.SelectedIndex = 0;
		}

		private void btnInputDialog_Click(object sender, EventArgs e)
		{
			if (cmbInputType.SelectedItem is InputType inputType) {
				inputType.OnInputDialogButtonClicked(this);
			}
		}

		private void lblSlackToken_Click(object sender, EventArgs e)
		{
			tbSlackToken.Select();
			tbSlackToken.Focus();
		}

		private async void btnDownload_Click(object sender, EventArgs e)
		{
			this.Enabled = false;

			var config = new Dictionary<string, string?>();

			if (cmbInputType.SelectedItem is InputType inputType) {
				inputType.OnInitConfig(this, config);
			}

			if (cbSlackFilter.Checked) {
				config["filters"] = "slack";
			}

			string token = tbSlackToken.Text;
			if (string.IsNullOrEmpty(token)) {
				config[nameof(token)] = token;
			}

			string logFile = await LoggerWrapper.RunAsync("DL",
				async () =>
					await Task.Run(() =>
						Host.CreateDefaultBuilder(_default_args.ToArray())
							.InitializeJsonUrlSaver()
							.ConfigureAppConfiguration(builder => builder
								.AddInMemoryCollection(config)
							)
							.Build()
							.RunJsonUrlSaver()
					).ConfigureAwait(false)
			).ConfigureAwait(true);

			var dr = MessageBox.Show(
				this,
				$"ダウンロードが完了しました。ログファイルを開きますか？\r\nログファイルのパス：{logFile}",
				nameof(JsonUrlSaver),
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Information
			);

			this.Enabled = true;

			if (dr == DialogResult.Yes) {
				FormMain.Open(logFile);
			}
		}

		private sealed record InputType(
			string                                            DisplayText,
			Action<DownloadPage>                              OnInputDialogButtonClicked,
			Action<DownloadPage, Dictionary<string, string?>> OnInitConfig)
		{
			public override string ToString()
				=> this.DisplayText;
		}
	}
}
