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

namespace PortableGranuleAssembler
{
	public static class Parser
	{
		private static readonly UTF8Encoding    _utf8    = new(       false);
		private static readonly UnicodeEncoding _utf16le = new(false, false);
		private static readonly UnicodeEncoding _utf16be = new(true,  false);
		private static readonly UTF32Encoding   _utf32le = new(false, false);
		private static readonly UTF32Encoding   _utf32be = new(true,  false);

		public static bool TryParseAndEmitFromFile(string fname, BinaryWriter bw, TextWriter tw, Dictionary<string, ReadOnlyMemory<Token>>? vars = null)
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

			src.Tokenize(fname).ParseAndEmit(bw, tw, vars);
			return true;
		}

		public static void ParseAndEmit(this IEnumerable<Token> tokens, BinaryWriter bw, TextWriter tw, Dictionary<string, ReadOnlyMemory<Token>>? vars = null)
		{
			int      size   = 1;
			Encoding enc    = _utf8;
			var      mode   = Mode.Out;
			bool     escape = false;
			string?  name   = null;
			var      list   = new List<Token>();

			vars ??= [];

			foreach (var token in tokens) {
				switch (mode) {
				case Mode.Out:
					switch (token) {
					case SeparatorToken st:
						Dump(tw, st, $"Info: A separator appeared.");
						break;
					case EscapeToken et:
						Dump(tw, et, $"Warn: An excess escape appeared.");
						break;
					case NameToken nt:
						string inst  = nt.Name;
						string instl = inst.ToLowerInvariant();
						switch (instl) {
						case "db" or "byte" or "b1" or "i8" or "i08":
							size = 1;
							Dump(tw, nt, $"Info: Size mode is set to 1 byte.");
							break;
						case "dw" or "word" or "short" or "b2" or "i16":
							size = 2;
							Dump(tw, nt, $"Info: Size mode is set to 2 bytes.");
							break;
						case "dd" or "dword" or "int" or "b4" or "i32":
							size = 4;
							Dump(tw, nt, $"Info: Size mode is set to 4 bytes.");
							break;
						case "dq" or "qword" or "long" or "b8" or "i64":
							size = 8;
							Dump(tw, nt, $"Info: Size mode is set to 8 bytes.");
							break;
						case "utf8":
							enc = _utf8;
							Dump(tw, nt, $"Info: Encoding mode is set to UTF-8.");
							break;
						case "utf16" or "utf16le":
							enc = _utf16le;
							Dump(tw, nt, $"Info: Encoding mode is set to UTF-16LE.");
							break;
						case "utf16be":
							enc = _utf16be;
							Dump(tw, nt, $"Info: Encoding mode is set to UTF-16BE.");
							break;
						case "utf32" or "utf32le":
							enc = _utf32le;
							Dump(tw, nt, $"Info: Encoding mode is set to UTF-32LE.");
							break;
						case "utf32be":
							enc = _utf32be;
							Dump(tw, nt, $"Info: Encoding mode is set to UTF-32BE.");
							break;
						case "set":
							mode   = Mode.Set;
							escape = false;
							name   = null;
							list.Clear();
							Dump(tw, nt, $"Info: Setting a variable...");
							break;
						case "get":
							mode = Mode.Get;
							Dump(tw, nt, $"Info: Getting a variable...");
							break;
						case "incl" or "include":
							mode = Mode.Include;
							Dump(tw, nt, $"Info: Including another file...");
							break;
						default:
							if (vars.TryGetValue(instl, out var rom)) {
								Dump(tw, nt, $"Info: Expanding the custom instruction \'{inst}\'...");

								rom.ToArray().ParseAndEmit(bw, tw, vars);

								Dump(tw, nt, $"Info: The custom instruction \'{inst}\' is expanded.");
							} else {
								Dump(tw, nt, $"Error: An unsupported instruction \'{inst}\' appeared.");
							}
							break;
						}
						break;
					case IntegerToken it:
						ulong value = it.Value;

						switch (size) {
						case 1:
							byte v1 = unchecked((byte)(value));

							bw.Write(v1);
							Dump(tw, it, $"Out: {v1:X4} = {v1}");

							if (value > byte.MaxValue) {
								Dump(tw, it, $"Warn: The original value \'{value:X16} = {value}\' is too large for 1 byte.");
							}
							break;
						case 2:
							ushort v2 = unchecked((ushort)(value));

							bw.Write(v2);
							Dump(tw, it, $"Out: {v2:X4} = {v2}");

							if (value > ushort.MaxValue) {
								Dump(tw, it, $"Warn: The original value \'{value:X16} = {value}\' is too large for 2 bytes.");
							}
							break;
						case 4:
							uint v4 = unchecked((uint)(value));

							bw.Write(v4);
							Dump(tw, it, $"Out: {v4:X8} = {v4}");

							if (value > uint.MaxValue) {
								Dump(tw, it, $"Warn: The original value \'{value:X16} = {value}\' is too large for 4 bytes.");
							}
							break;
						case 8:
							bw.Write(value);
							Dump(tw, it, $"Out: {value:X16} = {value}");
							break;
						default:
							Dump(tw, it, $"Internal Error: The specified byte size \'{size}\' is invalid.");
							break;
						}
						break;
					case StringToken st:
						if (enc is null) {
							Dump(tw, st, $"Internal Error: No text encoding format is specified.");
						} else {
							string text = st.Value;
							byte[] buf  = enc.GetBytes(text);

							bw.Write(buf);
							Dump(tw, st, $"Out: {Bin2Str(buf)} = {text}");
						}
						break;
					case UnexpectedToken ut:
						Dump(tw, ut, $"Error: An unexpected character \'{ut.Actual}\' appeared.");
						break;
					default:
						Dump(tw, token, $"Internal Error: An unexpected token ({token.DisplayText}) appeared.");
						break;
					}
					break;
				case Mode.Set:
					if (name is null) {
						if (token is NameToken nt1) {
							name = nt1.Name;
							Dump(tw, token, $"Info: The name is \'{name}\'.");
						} else {
							Dump(tw, token, $"Error: An unexpected token ({token.DisplayText}) appeared. A name is expected.");
						}
					} else if (escape) {
						escape = false;
						Dump(tw, token, $"Info: [{list.Count}] = {token.DisplayText}; The next token will be unescaped.");
						list.Add(token);
					} else if (token is SeparatorToken) {
						vars[name.ToLowerInvariant()] = new([.. list]);
						mode = Mode.Out;
						Dump(tw, token, $"Info: The variable \'{name}\' is updated.");
					} else if (token is EscapeToken) {
						escape = true;
						Dump(tw, token, $"Info: The next token will be escaped.");
					} else {
						Dump(tw, token, $"Info: [{list.Count}] = {token.DisplayText}");
						list.Add(token);
					}
					break;
				case Mode.Get:
					if (token is NameToken nt2) {
						name = nt2.Name;
						Dump(tw, token, $"Info: The name is \'{name}\'.");

						if (vars.TryGetValue(name.ToLowerInvariant(), out var rom)) {
							rom.ToArray().ParseAndEmit(bw, tw, vars);
							Dump(tw, token, $"Info: The variable \'{name}\' is expanded.");
						} else {
							Dump(tw, token, $"Error: The specified variable does not exist.");
						}

						mode = Mode.Out;
					} else {
						Dump(tw, token, $"Error: An unexpected token ({token.DisplayText}) appeared. A name is expected.");
					}
					break;
				case Mode.Include:
					if (token is StringToken st1) {
						name = st1.Value;
						Dump(tw, token, $"Info: The file name is \'{name}\'.");

						if (TryParseAndEmitFromFile(name, bw, tw, vars)) {
							Dump(tw, token, $"Info: The file is included and expanded.");
						} else {
							Dump(tw, token, $"System Error: The file cannot be loaded.");
						}

						mode = Mode.Out;
					} else {
						Dump(tw, token, $"Error: An unexpected token ({token.DisplayText}) appeared. A string is expected.");
					}
					break;
				default:
					Dump(tw, token, $"Internal Error: The invalid parsing-and-emitting mode ({mode}) is specified.");
					break;
				}
			}

			static void Dump(TextWriter tw, Token token, FormattableString msg)
				=> tw.WriteLine("{0}:({1},{2}): {3}", token.FileName, token.Row, token.Column, msg);

			static string Bin2Str(byte[] buf)
			{
				var sb = new StringBuilder();

				for (int i = 0; i < buf.Length; ++i) {
					sb.Append(buf[i].ToString("X2"));
				}

				return sb.ToString();
			}
		}

		private enum Mode
		{
			Out,
			Set,
			Get,
			Include
		}
	}
}
