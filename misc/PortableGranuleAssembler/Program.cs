/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PortableGranuleAssembler.Instructions;

namespace PortableGranuleAssembler
{
	internal static class Program
	{
		private static void Run(string[] args)
		{
			// TODO: start from here

			switch (args) {
			case [ "repl", ..string[] rest ]:
				REPL(rest);
				break;
			case [ "test", ..string[] rest ]:
				Test(rest);
				break;
			default:
				string? srcFile = null;
				string? dstFile = null;
				string? logFile = null;

				for (int i = 0; i < args.Length; ++i) {
					switch (args[i]) {
					case [ 's', 'r', 'c', ':', ..string s ]:
						srcFile = s;
						break;
					case [ 'd', 's', 't', ':', ..string d ]:
						dstFile = d;
						break;
					case [ 'l', 'o', 'g', ':', ..string l ]:
						logFile = l;
						break;
					case [ 's', ':', ..string s ]:
						srcFile = s;
						break;
					case [ 'd', ':', ..string d ]:
						dstFile = d;
						break;
					case [ 'l', ':', ..string l ]:
						logFile = l;
						break;
					default:
						Console.Error.WriteLine("BAD ARG!");
						break;
					}
				}

				using (var em = new Emitter(
					dstFile is null ? Stream.Null : new FileStream(dstFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None),
					logFile is null ? Console.Out : new StreamWriter(logFile, true, Encoding.UTF8)
				)) {
					if (srcFile is not null) {
						em.LogStarted();
						Parser.TryParseAndEmitFromFile(srcFile, em);
					} else {
						Console.Error.WriteLine("NO SOURCE FILE!");
					}
				}

				break;
			}
		}

		private static void REPL(string[] args)
		{
			using (var ms = new MemoryStream())
			using (var em = new Emitter(ms)) {
				var exitInst = new ExitInstruction();
				em.AddInstruction(exitInst);

				Console.WriteLine("Type \"exit\" or \"quit\" to terminate the REPL.");
				Console.WriteLine();

				while (!exitInst._is_termination_requested) {
					Console.Write("> ");
					string? line = Console.ReadLine();

					if (!string.IsNullOrEmpty(line)) {
						line.Tokenize("<INPUT>").ParseAndEmit(em);
						Console.WriteLine();
					}
				}
			}
		}

		private static void Test(string[] args)
		{
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

		private sealed class ExitInstruction : PseudoInstruction
		{
			internal bool _is_termination_requested;

			public override IEnumerable<string> EnumerateNames()
			{
				yield return "exit";
				yield return "quit";
			}

			protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
			{
				_is_termination_requested = true;
				em.LogInfo(nt, $"REPL is terminating...");

				return false;
			}

			protected override bool VisitNextTokenCore(Token token, Emitter em)
				=> throw new NotImplementedException();
		}
	}
}
