/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System.Runtime.CompilerServices;

namespace PortableGranuleAssembler.Labels
{
	public static class LabelDereferenceModes
	{
		public static ILabelDereferenceMode Absolute                  => AbsoluteImpl                 ._inst;
		public static ILabelDereferenceMode RelativeFromCursorToLabel => RelativeFromCursorToLabelImpl._inst;
		public static ILabelDereferenceMode RelativeFromLabelToCursor => RelativeFromLabelToCursorImpl._inst;

		private sealed class AbsoluteImpl : ILabelDereferenceMode
		{
			internal static readonly AbsoluteImpl _inst = new();

			public string DisplayName => "absolute";

			private AbsoluteImpl() { }

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Calculate(ref long addr, long curPos) { }
		}

		private sealed class RelativeFromCursorToLabelImpl : ILabelDereferenceMode
		{
			internal static readonly RelativeFromCursorToLabelImpl _inst = new();

			public string DisplayName => "relative (cursor --> label)";

			private RelativeFromCursorToLabelImpl() { }

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Calculate(ref long addr, long curPos)
				=> addr = unchecked(addr - curPos);
		}

		private sealed class RelativeFromLabelToCursorImpl : ILabelDereferenceMode
		{
			internal static readonly RelativeFromLabelToCursorImpl _inst = new();

			public string DisplayName => "relative (label --> cursor)";

			private RelativeFromLabelToCursorImpl() { }

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Calculate(ref long addr, long curPos)
				=> addr = unchecked(curPos - addr);
		}
	}

	public interface ILabelDereferenceMode
	{
		public string DisplayName { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Calculate(ref long addr, long curPos);
	}
}
