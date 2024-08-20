/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics.CodeAnalysis;

namespace JsonUrlSaver
{
	public interface ICacheFileIndexSelector
	{
		public bool TrySelectIndex(Uri url, uint minIndexExclusive, uint maxIndexInclusive, [NotNullWhen(true)][MaybeNullWhen(false)] out uint result);
	}
}
