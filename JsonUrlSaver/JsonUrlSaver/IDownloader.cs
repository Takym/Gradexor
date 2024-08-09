/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
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
