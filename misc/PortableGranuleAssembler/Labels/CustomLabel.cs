/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.IO;
using System.Runtime.CompilerServices;

namespace PortableGranuleAssembler.Labels
{
	public sealed record class CustomLabel : AddressLabel
	{
		public required long Address { get; init; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void EmitCore(BinaryWriter bw, Emitter em, LabelDereferenceToken ldt)
			=> this.EmitCore(bw, em, ldt, this.Address);
	}
}
