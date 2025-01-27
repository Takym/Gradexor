/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace JsonUrlSaver.UrlFilters
{
	public interface IUrlFilter
	{
		public bool ShouldDownload(Uri url);
	}
}
