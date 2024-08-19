/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JsonUrlSaver.GUI.WinForms
{
	internal static class Program
	{
		private static void Run(string[] args)
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new FormMain(args));
		}

		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

				Run(args);
				return 0;
			} catch (Exception e) {
				MessageBox.Show(
					e.ToString(),
					nameof(JsonUrlSaver),
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return e.HResult;
			}
		}

#if DEBUG
		private static class DebugEnvironment
		{
			[Conditional("DEBUG")]
			private static void Main(string[] args)
				=> Run(args);
		}
#endif
	}
}
