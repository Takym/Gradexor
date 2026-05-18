/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public sealed class GetInstruction : PseudoInstruction
	{
		public override IEnumerable<string> EnumerateNames()
		{
			yield return "get";
			yield return "put";
		}

		protected override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
		{
			if (nt.Name == "put") {
				em.LogInfo(nt, $"Printing a variable...");
			} else {
				em.LogInfo(nt, $"Getting a variable...");
			}

			return true;
		}

		protected override bool VisitNextTokenCore(Token token, Emitter em)
		{
			if (token is NameToken nt2) {
				string name = nt2.Name;
				em.LogInfo(token, $"The name is \'{name}\'.");

				if (em.Variables.TryGetValue(name.ToLowerInvariant(), out var rom)) {
					rom.ToArray().ParseAndEmit(em);
					em.LogInfo(token, $"The variable \'{name}\' is expanded.");
				} else {
					em.LogError(token, $"The specified variable does not exist.");
				}

				return false;
			} else {
				em.LogUnexpectedToken(token, $"A name is expected.");
				return true;
			}
		}
	}
}
