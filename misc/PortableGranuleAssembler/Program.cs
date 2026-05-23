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
			case [ "help", ..string[] rest ]:
				Help(rest);
				break;
			case [ "comp", ..string[] rest ]:
				Compile(rest);
				break;
			default:
				Compile(args);
				break;
			}
		}

		private static void REPL(string[] args)
		{
			using (var ms = new MemoryStream())
			using (var em = new Emitter(ms)) {
				var exitInst = new ExitInstruction();
				em.AddInstruction(exitInst);
				em.AddInstruction(new HelpInstruction (  ));
				em.AddInstruction(new ClearInstruction(  ));
				em.AddInstruction(new DumpInstruction (ms));

				Console.WriteLine("Type \"exit\" or \"quit\" to terminate the REPL.");
				Console.WriteLine("Type \"help\" for more REPL mode commands.");
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
				Dump(ms);
			}
		}

		private static void Help(string[] args)
		{
			Console.WriteLine("The Portable Granule Assembler Command-line Manual");

			switch (args) {
			case [ "repl" ]:
				Console.WriteLine("Usage> poga repl");
				Console.WriteLine();
				Console.WriteLine("No options.");
				break;
			case [ "test" ]:
				Console.WriteLine("Usage> poga test");
				Console.WriteLine();
				Console.WriteLine("No options.");
				break;
			case [ "help" ]:
				Console.WriteLine("Usage> poga help [command]");
				Console.WriteLine();
				Console.WriteLine("If you specify a command name, then it shows the command-specific manual.");
				break;
			case [ "comp" ]:
				Console.WriteLine("Usage> poga comp [options...]");
				Console.WriteLine("Usage> poga [options...]");
				Console.WriteLine();
				Console.WriteLine("Options:");
				Console.WriteLine(" - \"src:<path>\"/\"s:<path>\": The source file name. This is required, and only the last argument is used.");
				Console.WriteLine(" - \"dst:<path>\"/\"d:<path>\": The destination file name. This is optional, and only the last argument is used.");
				Console.WriteLine(" - \"log:<path>\"/\"l:<path>\": The log file name. This is optional, and only the last argument is used.");
				Console.WriteLine();
				Console.WriteLine("Please do not add any spaces before and after the colon (:).");
				break;
			default:
				Console.WriteLine("Usage> poga [command] [options...]");
				Console.WriteLine();

				Console.WriteLine("Commands:");
				Console.WriteLine(" - repl: Runs the Read-Eval-Print-Loop mode.");
				Console.WriteLine(" - test: Runs the test mode.");
				Console.WriteLine(" - help: Shows this help message.");
				Console.WriteLine(" - comp: Compiles the source file. This command name can be omitted.");
				break;
			}
		}

		private static void Compile(string[] args)
		{
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
		}

		private static void Dump(MemoryStream ms)
		{
			long pos = ms.Position;
			ms.Position = 0;

			Span<byte> buf = stackalloc byte[16];

			int len;
			while ((len = ms.Read(buf)) == buf.Length) {
				DumpCore(buf, buf.Length);
			}

			DumpCore(buf, len);

			ms.Position = pos;

			static void DumpCore(Span<byte> buf, int len)
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

		private sealed class HelpInstruction : PseudoInstruction
		{
			public override IEnumerable<string> EnumerateNames()
			{
				yield return "help";
			}

			protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
			{
				em.LogInfo(nt, $"Type \"exit\" or \"quit\" to terminate the REPL.");
				em.LogInfo(nt, $"Type \"help\" to show this help message.");
				em.LogInfo(nt, $"Type \"clear\" or \"cls\" to clear the screen.");
				em.LogInfo(nt, $"Type \"dump\" to show the output data.");

				return false;
			}

			protected override bool VisitNextTokenCore(Token token, Emitter em)
				=> throw new NotImplementedException();
		}

		private sealed class ClearInstruction : PseudoInstruction
		{
			public override IEnumerable<string> EnumerateNames()
			{
				yield return "clear";
				yield return "cls";
			}

			protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
			{
				Console.Clear();

				em.LogInfo(nt, $"Cleared!");

				return false;
			}

			protected override bool VisitNextTokenCore(Token token, Emitter em)
				=> throw new NotImplementedException();
		}

		private sealed class DumpInstruction : PseudoInstruction
		{
			private readonly MemoryStream _ms;

			internal DumpInstruction(MemoryStream ms)
			{
				_ms = ms;
			}

			public override IEnumerable<string> EnumerateNames()
			{
				yield return "dump";
			}

			protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
			{
				Dump(_ms);
				em.LogInfo(nt, $"Dumped!");

				return false;
			}

			protected override bool VisitNextTokenCore(Token token, Emitter em)
				=> throw new NotImplementedException();
		}
	}
}
