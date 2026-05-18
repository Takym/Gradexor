/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;
using PortableGranuleAssembler.Instructions;

namespace PortableGranuleAssembler
{
	public static class Parser
	{
		internal const string FNAME_POGA = "<POGA>";

		public static bool TryParseAndEmitFromFile(string fname, Emitter em)
		{
			if (em is null) {
				return false;
			}

			string src;

			try {
				if (!File.Exists(fname)) {
					fname = Path.Combine(AppContext.BaseDirectory, fname);

					if (!File.Exists(fname)) {
						return false;
					}
				}

				using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
				using (var sr = new StreamReader(fs, true)) {
					src = sr.ReadToEnd();
				}
			} catch (Exception e) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Error.WriteLine();
				Console.Error.WriteLine(e.ToString());
				Console.ResetColor();
				return false;
			}

			src.Tokenize(fname).ParseAndEmit(em);
			return true;
		}

		public static void ParseAndEmit(this IEnumerable<Token> tokens, Emitter em)
		{
			ArgumentNullException.ThrowIfNull(tokens);
			ArgumentNullException.ThrowIfNull(em    );

			PseudoInstruction? inst  = null;
			var                insts = em.Instructions;
			var                vars  = em.Variables;

			foreach (var token in tokens) {
				if (inst is null) {
					switch (token) {
					case SeparatorToken st:
						em.LogInfo(st, $"A separator appeared.");
						break;
					case EscapeToken et:
						em.LogWarn(et, $"An excess escape appeared.");
						break;
					case NameToken nt:
						string instn = nt.Name;
						string instl = instn.ToLowerInvariant();

						switch (instl) {
						case "db" or "byte" or "b1" or "i8" or "i08":
							em.SetDataSizeToByte(nt);
							break;
						case "dw" or "word" or "short" or "b2" or "i16":
							em.SetDataSizeToWord(nt);
							break;
						case "dd" or "dword" or "int" or "b4" or "i32":
							em.SetDataSizeToDWord(nt);
							break;
						case "dq" or "qword" or "long" or "b8" or "i64":
							em.SetDataSizeToQWord(nt);
							break;
						case "utf8":
							em.SetTextEncodingToUTF8(nt);
							break;
						case "utf16" or "utf16le":
							em.SetTextEncodingToUTF16LE(nt);
							break;
						case "utf16be":
							em.SetTextEncodingToUTF16BE(nt);
							break;
						case "utf32" or "utf32le":
							em.SetTextEncodingToUTF32LE(nt);
							break;
						case "utf32be":
							em.SetTextEncodingToUTF32BE(nt);
							break;
						default:
							if (insts.TryGetValue(instl, out inst)) {
								if (!inst.VisitNameToken(nt, em, instl)) {
									inst = null;
								}
							} else if (vars.TryGetValue(instl, out var rom)) {
								em.LogInfo(nt, $"Expanding the custom instruction \'{instn}\'...");
								rom.ToArray().ParseAndEmit(em);
								em.LogInfo(nt, $"The custom instruction \'{instn}\' is expanded.");
							} else {
								em.LogError(nt, $"An unsupported instruction \'{instn}\' appeared.");
							}
							break;
						}
						break;
					case UnexpectedToken ut:
						em.LogError(ut, $"An unexpected character \'{ut.Actual}\' appeared.");
						break;
					default:
						em.Emit(token);
						break;
					}
				} else if (!inst.VisitNextToken(token, em)) {
					inst = null;
				}
			}
		}
	}
}
