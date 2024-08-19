/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System.Diagnostics.CodeAnalysis;

namespace JsonUrlSaver
{
	public interface ICacheFileIndexSelector
	{
		public bool TrySelectIndex(uint minIndexExclusive, uint maxIndexInclusive, [NotNullWhen(true)][MaybeNullWhen(false)] out uint result);
	}
}
