/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace JsonUrlSaver.GUI.WinForms
{
	public partial class FormMain : Form
	{
		private          string?                _version_string;
		private readonly ReadOnlyMemory<string> _default_args;

		public FormMain(string[] args)
		{
			_default_args = args;
			this.InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			var     asm       = Assembly.GetExecutingAssembly();
			var     ver       = asm.GetName().Version;
			string? name      = asm.GetCustomAttribute<AssemblyProductAttribute  >()?.Product;
			string? copyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

			if (ver is not null &&
				!string.IsNullOrEmpty(name) &&
				!string.IsNullOrEmpty(copyright)) {
				this.Text = $"{name} v{ver} ({copyright})";

				_version_string = ver.ToString(4);
			}

			downloadPage.SetDefaultArgs(_default_args);
			cacheFilePage.SetDefaultArgs(_default_args);

			var sb   = new StringBuilder();
			var args = _default_args.Span;
			for (int i = 0; i < args.Length; ++i) {
				sb
					.AppendFormat("[{0,10}]: {1}", i, args[i])
					.AppendLine();
			}
			tbArgs.Text = sb.ToString();
		}

		private void commonSettingsToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open(Path.Combine(AppContext.BaseDirectory, "appSettings.json"));

		private void developmentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open(Path.Combine(AppContext.BaseDirectory, "appSettings.Development.json"));

		private void productionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open(Path.Combine(AppContext.BaseDirectory, "appSettings.Production.json"));

		private void getSourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open("https://github.com/Takym/Gradexor/tree/master/JsonUrlSaver");

		private void releaseNotesToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open($"https://github.com/Takym/Gradexor/releases/tag/JsonUrlSaver-v{_version_string}");

		private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open(Path.Combine(AppContext.BaseDirectory, "README.md"));

		private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
			=> Open(Path.Combine(AppContext.BaseDirectory, "LICENSE.md"));

		private static void Open(string file)
		{
			using var _ = Process.Start(new ProcessStartInfo() {
				FileName        = file,
				Verb            = "open",
				UseShellExecute = true
			});
		}
	}
}
