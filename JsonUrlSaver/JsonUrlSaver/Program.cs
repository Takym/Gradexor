/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace JsonUrlSaver
{
	internal static class Program
	{
		private static void Run(string[] args)
			=> Host
				.CreateApplicationBuilder(args) // 「.CreateDefaultBuilder(args)」でも動く。
				.InitializeJsonUrlSaver()
				.Build()
				.RunJsonUrlSaver();

		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				if (args is [ "/I" ]) { // "I" - Interactive arguments inputting
					Run(InputArguments());
				} else {
					Run(args);
				}
				return 0;
			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine();
				Console.Error.WriteLine(e.ToString());
				Console.ResetColor();
				return e.HResult;
			}
		}

		private static string[] InputArguments()
		{
			var args = new List<string>();
			while (true) {
				Console.Write("{0,10}> ", args.Count);
				if (Console.ReadLine() is string line && !string.IsNullOrEmpty(line)) {
					args.Add(line);
				} else {
					break;
				}
			}
			return [ ..args ];
		}

#if DEBUG
		private static class DebugEnvironment
		{
			[Conditional("DEBUG")]
			[STAThread()]
			private static void Main()
				=> Run(InputArguments());
		}
#endif
	}
}
