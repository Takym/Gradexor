/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public sealed class IncludeInstruction : PseudoInstruction
	{
		public override IEnumerable<string> EnumerateNames()
		{
			yield return "incl";
			yield return "include";
			yield return "stdenv";
		}

		protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
		{
			if (name == "stdenv") {
				em.LogInfo(nt, $"Loading the standard environment...");
				"INCL\"StandardEnvironment.poga\"".Tokenize(Parser.FNAME_POGA).ParseAndEmit(em);
				em.LogInfo(nt, $"The standard environment is loaded.");

				return false;
			} else {
				em.LogInfo(nt, $"Including another file...");
				return true;
			}
		}

		protected override bool VisitNextTokenCore(Token token, Emitter em)
		{
			if (token is StringToken st1) {
				string name = st1.Value;
				em.LogInfo(token, $"The file name is \'{name}\'.");

				if (Parser.TryParseAndEmitFromFile(name, em)) {
					em.LogInfo(token, $"The file is included and expanded.");
				} else {
					em.LogSystemError(token, $"The file cannot be loaded.");
				}

				return false;
			} else {
				em.LogUnexpectedToken(token, $"A file name string is expected.");
				return true;
			}
		}
	}
}
