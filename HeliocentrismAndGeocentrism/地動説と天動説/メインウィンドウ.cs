/****
 * 地動説と天動説
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 地動説と天動説
{
	public sealed partial class メインウィンドウ : Form
	{
		private readonly ReadOnlyMemory<string> _args;
		private readonly bool                   _is_debug_mode;

		public メインウィンドウ(string[] args, bool isDebugMode)
		{
			_args          = args;
			_is_debug_mode = isDebugMode;

			this.InitializeComponent();
		}

		private void 地動説ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			クライアント.地動説();

			地動説ToolStripMenuItem.Checked = true;
			天動説ToolStripMenuItem.Checked = false;
		}

		private void 天動説ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			クライアント.天動説();

			地動説ToolStripMenuItem.Checked = false;
			天動説ToolStripMenuItem.Checked = true;
		}

		private void デバッグ情報ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var sb = new StringBuilder();

			var asm = this.GetType().Assembly;
			sb.AppendLine("== バージョン情報 ==");
			sb.AppendLine($"バージョン：{asm.GetName().Version}");
			sb.AppendLine($"著作権　　：{asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright}");
			sb.AppendLine($"ライセンス：The MIT License");

			var args = _args.Span;
			sb.AppendLine("== コマンド行引数 ==");
			for (int i = 0; i < args.Length; ++i) {
				sb.Append('[').Append(i).Append("]: ").AppendLine(args[i]);
			}

			sb.AppendLine("== 内部変数 ==");
			sb.AppendLine($"地動説　モード：{(地動説ToolStripMenuItem.Checked ? "有効" : "無効")}");
			sb.AppendLine($"天動説　モード：{(天動説ToolStripMenuItem.Checked ? "有効" : "無効")}");
			sb.AppendLine($"デバッグモード：{(_is_debug_mode                  ? "有効" : "無効")}");

			MessageBox.Show(
				this,
				sb.ToString(),
				"デバッグ情報",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
		}

		private static void Run(string[] args, bool isDebugMode)
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new メインウィンドウ(args, isDebugMode));
		}

		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				Run(args, false);
				return 0;
			} catch (Exception e) {
				MessageBox.Show(
					e.ToString(),
					"予期せぬエラーが発生しました。",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return e.HResult;
			}
		}

#if DEBUG
		private static class デバッグ環境
		{
			[STAThread()]
			[Conditional("DEBUG")]
			private static void Main(string[] args) => Run(args, true);
		}
#endif
	}
}
