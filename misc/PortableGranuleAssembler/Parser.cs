/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;

namespace PortableGranuleAssembler
{
	public static class Parser
	{
		private const string FNAME_POGA = "<POGA>";

		public static bool TryParseAndEmitFromFile(string fname, Emitter em, Dictionary<string, ReadOnlyMemory<Token>>? vars = null)
		{
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

			src.Tokenize(fname).ParseAndEmit(em, vars);
			return true;
		}

		public static void ParseAndEmit(this IEnumerable<Token> tokens, Emitter em, Dictionary<string, ReadOnlyMemory<Token>>? vars = null)
		{
			var     mode   = Mode.Out;
			bool    escape = false;
			string? name   = null;
			ulong?  repcnt = null;
			var     list   = new List<Token>();

			vars ??= [];

			foreach (var token in tokens) {
				switch (mode) {
				case Mode.Out:
					switch (token) {
					case SeparatorToken st:
						em.LogInfo(st, $"A separator appeared.");
						break;
					case EscapeToken et:
						em.LogWarn(et, $"An excess escape appeared.");
						break;
					case NameToken nt:
						string inst  = nt.Name;
						string instl = inst.ToLowerInvariant();

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
						case "set":
							mode   = Mode.Set;
							escape = false;
							name   = null;
							list.Clear();
							em.LogInfo(nt, $"Setting a variable...");
							break;
						case "get" or "put":
							mode = Mode.Get;
							em.LogInfo(nt, $"Getting a variable...");
							break;
						case "incl" or "include":
							mode = Mode.Include;
							em.LogInfo(nt, $"Including another file...");
							break;
						case "rep" or "repeat":
							mode   = Mode.Repeat;
							escape = false;
							repcnt = null;
							list.Clear();
							em.LogInfo(nt, $"Repeating...");
							break;
						case "stdenv":
							em.LogInfo(nt, $"Loading the standard environment...");
							"INCL\"StandardEnvironment.poga\"".Tokenize(FNAME_POGA).ParseAndEmit(em, vars);
							em.LogInfo(nt, $"The standard environment is loaded.");
							break;
						default:
							if (vars.TryGetValue(instl, out var rom)) {
								em.LogInfo(nt, $"Expanding the custom instruction \'{inst}\'...");
								rom.ToArray().ParseAndEmit(em, vars);
								em.LogInfo(nt, $"The custom instruction \'{inst}\' is expanded.");
							} else {
								em.LogError(nt, $"An unsupported instruction \'{inst}\' appeared.");
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
					break;
				case Mode.Set:
					if (name is null) {
						if (token is NameToken nt1) {
							name = nt1.Name;
							em.LogInfo(token, $"The name is \'{name}\'.");
						} else {
							em.LogError(token, $"An unexpected token ({token.DisplayText}) appeared. A name is expected.");
						}
					} else if (escape) {
						escape = false;
						em.LogInfo(token, $"* [{list.Count}] = {token.DisplayText}; The next token will be unescaped.");
						list.Add(token);
					} else if (token is SeparatorToken) {
						vars[name.ToLowerInvariant()] = new([ ..list ]);
						mode = Mode.Out;
						em.LogInfo(token, $"The variable \'{name}\' is updated.");
					} else if (token is EscapeToken) {
						escape = true;
						em.LogInfo(token, $"The next token will be escaped.");
					} else {
						em.LogInfo(token, $"* [{list.Count}] = {token.DisplayText}");
						list.Add(token);
					}
					break;
				case Mode.Get:
					if (token is NameToken nt2) {
						name = nt2.Name;
						em.LogInfo(token, $"The name is \'{name}\'.");

						if (vars.TryGetValue(name.ToLowerInvariant(), out var rom)) {
							rom.ToArray().ParseAndEmit(em, vars);
							em.LogInfo(token, $"The variable \'{name}\' is expanded.");
						} else {
							em.LogError(token, $"The specified variable does not exist.");
						}

						mode = Mode.Out;
					} else {
						em.LogError(token, $"An unexpected token ({token.DisplayText}) appeared. A name is expected.");
					}
					break;
				case Mode.Include:
					if (token is StringToken st1) {
						name = st1.Value;
						em.LogInfo(token, $"The file name is \'{name}\'.");

						if (TryParseAndEmitFromFile(name, em, vars)) {
							em.LogInfo(token, $"The file is included and expanded.");
						} else {
							em.LogSystemError(token, $"The file cannot be loaded.");
						}

						mode = Mode.Out;
					} else {
						em.LogError(token, $"An unexpected token ({token.DisplayText}) appeared. A file name string is expected.");
					}
					break;
				case Mode.Repeat:
					if (!repcnt.HasValue) {
						if (token is IntegerToken it1) {
							repcnt = it1.Value;
							em.LogInfo(token, $"The repeat count is \'{repcnt.Value}\'.");
						} else {
							em.LogError(token, $"An unexpected token ({token.DisplayText}) appeared. A repeat count integer is expected.");
						}
					} else if (escape) {
						escape = false;
						em.LogInfo(token, $"* [{list.Count}] = {token.DisplayText}; The next token will be unescaped.");
						list.Add(token);
					} else if (token is SeparatorToken) {
						var ary = list.ToArray();

						for (ulong i = 0; i < repcnt.Value; ++i) {
							em.LogNote(token, $"The current progress: {i + 1}/{repcnt.Value}");
							ary.ParseAndEmit(em, vars);
						}

						mode = Mode.Out;
						em.LogInfo(token, $"The repetition is finished.");
					} else if (token is EscapeToken) {
						escape = true;
						em.LogInfo(token, $"The next token will be escaped.");
					} else {
						em.LogInfo(token, $"* [{list.Count}] = {token.DisplayText}");
						list.Add(token);
					}
					break;
				default:
					em.LogInternalError(token, $"The invalid parsing-and-emitting mode ({mode}) is specified.");
					break;
				}
			}
		}

		private enum Mode
		{
			Out,
			Set,
			Get,
			Include,
			Repeat
		}
	}
}
