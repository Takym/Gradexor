/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JsonUrlSaver.GUI.WinForms
{
	internal static class LoggerWrapper
	{
		internal static async ValueTask<string> RunAsync(string tag, Func<ValueTask> task)
		{
			var stdout = Console.Out;

			string logDir = Path.Combine(AppContext.BaseDirectory, "Logs");
			Directory.CreateDirectory(logDir);

			string logFile = Path.Combine(logDir, $"{DateTime.Now:yyyyMMddHHmmssfffffff}.{tag}.{Guid.NewGuid()}.log");

			var sw = new StreamWriter(logFile, false, Encoding.UTF8);
			await using (sw.ConfigureAwait(false)) {
				Console.SetOut(sw);

				await task().ConfigureAwait(false);
			}

			Console.SetOut(stdout);

			return logFile;
		}
	}
}
