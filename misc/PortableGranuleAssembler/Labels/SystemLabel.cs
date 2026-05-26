/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.IO;

namespace PortableGranuleAssembler.Labels
{
	public sealed record class SystemLabel : AddressLabel
	{
		// TODO: SystemLabel

		protected override void EmitCore(BinaryWriter bw, Emitter em, LabelDereferenceToken ldt)
			=> throw new System.NotImplementedException();
	}
}
