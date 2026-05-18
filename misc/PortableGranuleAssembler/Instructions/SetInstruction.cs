/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public sealed class SetInstruction : CompoundInstruction
	{
		private string? _name;

		public override IEnumerable<string> EnumerateNames()
		{
			yield return "set";
			yield return "def";
			yield return "define";
		}

		protected override void InitializeCore(NameToken nt, Emitter em, string name)
		{
			_name = null;

			if (name == "set") {
				em.LogInfo(nt, $"Setting a variable...");
			} else {
				em.LogInfo(nt, $"Defining a variable...");
			}
		}

		protected override bool NeedArgument()
			=> _name is null;

		protected override void InputArgument(Token token, Emitter em)
		{
			if (token is NameToken nt1) {
				_name = nt1.Name;

				em.LogInfo(token, $"The name is \'{_name}\'.");
			} else {
				em.LogUnexpectedToken(token, $"A name is expected.");
			}
		}

		protected override void Invoke(Token token, Emitter em)
		{
			em.Variables[_name?.ToLowerInvariant() ?? string.Empty] = new([ ..this.TokenList ]);
			em.LogInfo(token, $"The variable \'{_name}\' is updated.");
		}
	}
}
