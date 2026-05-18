/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public sealed class RepeatInstruction : CompoundInstruction
	{
		private ulong? _repcnt;

		public override IEnumerable<string> EnumerateNames()
		{
			yield return "rep";
			yield return "repeat";
		}

		protected override void InitializeCore(NameToken nt, Emitter em, string name)
		{
			_repcnt = null;

			em.LogInfo(nt, $"Repeating...");
		}

		protected override bool NeedArgument()
			=> !_repcnt.HasValue;

		protected override void InputArgument(Token token, Emitter em)
		{
			if (token is IntegerToken it1) {
				_repcnt = it1.Value;
				em.LogInfo(token, $"The repeat count is \'{_repcnt.Value}\'.");
			} else {
				em.LogUnexpectedToken(token, $"A repeat count integer is expected.");
			}
		}

		protected override void Invoke(Token token, Emitter em)
		{
			var ary = this.TokenList.ToArray();

			ulong repcnt = _repcnt ?? 0;
			for (ulong i = 0; i < repcnt; ++i) {
				double i2 = i + 1;
				em.LogNote(token, $"The current progress: {100.0D * i2 / repcnt:000.00}% ({i2}/{repcnt})");
				ary.ParseAndEmit(em);
			}

			em.LogInfo(token, $"The repetition is finished.");
		}
	}
}
