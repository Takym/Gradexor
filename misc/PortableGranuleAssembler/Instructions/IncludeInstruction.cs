/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public sealed class IncludeInstruction : PseudoInstruction
	{
		private bool _is_src;

		public override IEnumerable<string> EnumerateNames()
		{
			yield return "incl";
			yield return "include";
			yield return "inclb";
			yield return "stdenv";
		}

		protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
		{
			if (name == "stdenv") {
				em.LogInfo(nt, $"Loading the standard environment...");
				"INCL\"StandardEnvironment.poga\"".Tokenize(Parser.FNAME_POGA).ParseAndEmit(em);
				em.LogInfo(nt, $"The standard environment is loaded.");

				return false;
			} else if (name == "inclb") {
				_is_src = false;
				em.LogInfo(nt, $"Including another binary file...");

				return true;
			} else {
				_is_src = true;
				em.LogInfo(nt, $"Including another source code file...");

				return true;
			}
		}

		protected override bool VisitNextTokenCore(Token token, Emitter em)
		{
			if (token is StringToken st1) {
				string name = st1.Value;
				em.LogInfo(token, $"The file name is \'{name}\'.");

				if (_is_src) {
					if (Parser.TryParseAndEmitFromFile(name, em)) {
						em.LogInfo(token, $"The source code file is included and expanded.");
					} else {
						em.LogSystemError(token, $"The source code file cannot be loaded.");
					}
				} else {
					if (Parser.TryUseFile(name, static fs => {
						byte[] buf = new byte[fs.Length];
						fs.ReadExactly(buf.AsSpan());
						return buf;
					}, out byte[]? buf)) {
						em.Emit(token, buf);
						em.LogInfo(token, $"The binary file is included and expanded.");
					} else {
						em.LogSystemError(token, $"The binary file cannot be loaded.");
					}
				}

				return false;
			} else {
				em.LogUnexpectedToken(token, $"A file name string is expected.");
				return true;
			}
		}
	}
}
