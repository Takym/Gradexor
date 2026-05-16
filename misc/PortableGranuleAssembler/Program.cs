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

			foreach (var token in Lexer.Tokenize(
				"""
					,  ,;
					,  ,; # comment
				# comment 123, 456
				hoge fuga piyo _a @123 @hello W_O_R_L_D @@
				123$_$0_$0_11$111 $$ $# !? @$ # 123
				0a 01a 0a1 $999 $F_80 $fFF $ZZ
				"abcd efgh" '"' "''
				"!? !? ‚ ‚¢‚¤‚¦‚¨"'‚©‚«‚­‚¯‚±''()()'"!!!!""????"
				!? @
				"""//*/
				// "XYZ"
				// "$"
				// "$8"
				// "$0123"
				// "12345"
				// "\"hello\""
				// "\"hello\'\""
				// "\"hello\"\'"
				// "\"hello\'"
				// "\"hello"
				// "\'world\'"
				// "\'world\"\'"
				// "\'world\'\""
				// "\'world\""
				// "\'world"
				// "*"
				// "#aaa"
				// "a\r\nb\n\rc\rd\ne"
			)) {
				Console.WriteLine(token);
				Console.WriteLine();
				Console.ReadKey(true);
			}

			using (var ms = new MemoryStream())
			using (var bw = new BinaryWriter(ms)) {
				"""
				Hello, World!!
				$ZZZ DQ $ZZZ;
				"Hello, World!!";
				UTF16 "Hello, World!!";
				AA
				SET AA DW 1234;
				GET AA AA
				SET SET UTF8 "abcd";
				GET SET SET
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
