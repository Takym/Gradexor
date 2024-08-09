/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace JsonUrlSaver.UrlFilters
{
	public sealed class CombinedUrlFilter : IUrlFilter
	{
		private readonly ReadOnlyMemory<IUrlFilter?> _filters;

		public CombinedUrlFilter(ReadOnlyMemory<IUrlFilter?> filters)
		{
			_filters = filters;
		}

		public bool ShouldDownload(Uri url)
		{
			var span = _filters.Span;
			for (int i = 0; i < span.Length; ++i) {
				if (span[i]?.ShouldDownload(url) ?? false) {
					return true;
				}
			}
			return false;
		}
	}
}
