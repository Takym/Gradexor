/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace JsonUrlSaver
{
	public interface IUrlFileNameConverter
	{
		public string GetCacheDirectoryPath(string baseDir, Uri uri);

		public string GetCacheFilePath(string baseDir, Uri uri, ulong index = 0);
	}
}
