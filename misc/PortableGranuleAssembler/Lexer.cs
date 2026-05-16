/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;

namespace PortableGranuleAssembler
{
	public static class Lexer
	{
		public static IEnumerable<Token> Tokenize(ReadOnlySpan<char> src, string? fname = null)
		{
			int row = 1;
			int col = 1;

			for (int i = 0; i < src.Length; ++i) {
				char ch = src[i];

				switch (ch) {
				case '\0':
					yield break;
				case ' ' or '\t' or ',' or ';':
					++col;
					break;
				case '\n' or '\r':
					int ii = i + 1;

					if (ii < src.Length) {
						char chNext = src[ii];

						if (chNext is '\n' or '\r' && chNext != ch) {
							i = ii;
						}

						++row;
						col = 1;
					} else {
						yield break;
					}

					break;
				case '#':
					do {
						if (++i >= src.Length) {
							yield break;
						}
					} while (src[i] is not ('\n' or '\r'));

					--i;
					break;
				case (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or '_' or '@':
					if (ch == '@') {
						if (++i >= src.Length) {
							yield return new UnexpectedToken() {
								Row = row, Column = col, FileName = fname, Actual = ch
							};
							yield break;
						}

						if (src[i] is not ((>= '0' and <= '9') or (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or '_')) {
							yield return new UnexpectedToken() {
								Row = row, Column = col++, FileName = fname, Actual = '@'
							};

							--i;
							break;
						}

						++col;
					}

					int i0 = i;

					do {
						++col;

						if (++i >= src.Length) {
							yield return new NameToken() {
								Row = row, Column = col2, FileName = fname, Name = src[i0..i]
							};
							yield break;
						}
					} while (src[i] is (>= '0' and <= '9') or (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or '_');

					yield return new NameToken() {
						Row = row, Column = col2, FileName = fname, Name = src[i0..i]
					};

					--i;
					break;
				case >= '0' and <= '9':
					ulong v    = 0;
					int   col2 = col;

					do {
						++col;

						if (ch != '_') {
							v *= 10;
							v += ch - '0';
						}

						if (++i >= src.Length) {
							yield return new IntegerToken() {
								Row = row, Column = col2, FileName = fname, Value = v
							};
							yield break;
						}

						ch = src[i];
					} while (ch is (>= '0' and <= '9') or '_');

					yield return new IntegerToken() {
						Row = row, Column = col2, FileName = fname, Value = v
					};

					--i;
					break;
				case '\"' or '\'':
					char quote = ch;
					int  i0    = i + 1;

					do {
						++col;

						if (++i >= src.Length) {
							yield return new StringToken() {
								Row = row, Column = col2, FileName = fname, Name = src[i0..i]
							};
							yield break;
						}

						ch = src[i];
					} while (ch != quote && ch is not ('\n' or '\r'));

					yield return new StringToken() {
						Row = row, Column = col2, FileName = fname, Name = src[i0..i]
					};

					if (ch == quote) {
						++col;
					} else {
						--i;
					}
					break;
				default:
					yield return new UnexpectedToken() {
						Row = row, Column = col++, FileName = fname, Actual = ch
					};
					break;
				}
			}
		}
	}
}
