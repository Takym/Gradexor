﻿/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using JsonUrlSaver.UrlSources;

namespace JsonUrlSaver
{
	public interface IDownloader
	{
		public void Download(string cacheDir, IUrlSource source);
	}
}
