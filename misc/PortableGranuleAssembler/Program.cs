/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace PortableGranuleAssembler
{
	internal static class Program
	{
		private static void Run(string[] args)
		{
			// TODO: start from here
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
