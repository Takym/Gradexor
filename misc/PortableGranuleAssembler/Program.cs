/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;

namespace PortableGranuleAssembler
{
	internal static class Program
	{
		private static void Run(string[] args)
		{
			// TODO: start from here

			using (var ms = new MemoryStream())
			using (var bw = new BinaryWriter(ms)) {
				"""
				DQ

				1111 ""

				$b1111 ""
				$q1111 ""
				$o1111 ""
				$d1111 ""
				$x1111 ""

				$01111 ""
				$?1111 ""

				$$01111 ""
				$$11111 ""
				$$91111 ""
				$$F1111 ""
				$$Z1111 ""

				$$$Z1111 ""
				""".Tokenize().ParseAndEmit(bw, Console.Out);
			}
		}

		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				Run(args);
				return 0;
			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine();
				Console.Error.WriteLine(e.ToString());
				Console.ResetColor();
				return e.HResult;
			}
		}

#if DEBUG
		private static class DebugEnvironment
		{
			private static void Main(string[] args) => Run(args);
		}
#endif
	}
}
