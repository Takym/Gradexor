using System;

namespace DotnetInterfaceSize
{
	internal static class Program
	{
		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				Console.WriteLine("This application is not supported to run.");
				return 0;
			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine();
				Console.Error.WriteLine(e.ToString());
				Console.ResetColor();
				return e.HResult;
			}
		}
	}
}
