/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Text;

namespace PortableGranuleAssembler
{
	internal static class Program
	{
		private static void Run(string[] args)
		{
			// TODO: start from here

			using (var ms = new MemoryStream())
			using (var em = new Emitter(ms)) {
				"INCL\"TempTestCode.poga\"INCL\"RoughSample.poga\"DB$x0A$x0B$x0C$x0D".Tokenize().ParseAndEmit(em);

				Console.WriteLine();

				ms.Position = 0;

				Span<byte> buf = stackalloc byte[16];

				int len;
				while ((len = ms.Read(buf)) == buf.Length) {
					Dump(buf, buf.Length);
				}

				Dump(buf, len);

				static void Dump(Span<byte> buf, int len)
				{
					for (int i = 0; i < len; ++i) {
						Console.Write("{0:X2} ", buf[i]);
					}

					for (int i = 0; i < (16 - len); ++i) {
						Console.Write("   ");
					}

					Console.WriteLine(
						Encoding.UTF8
							.GetString(buf[..len])
							.Replace('\r', '␍')
							.Replace('\n', '␊')
					);
				}
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
