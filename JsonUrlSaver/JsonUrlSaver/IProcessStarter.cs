﻿/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using JsonUrlSaver.UrlSources;

namespace JsonUrlSaver
{
	public interface IProcessStarter
	{
		public void OpenForCaches(string cacheDir, IUrlSource source);
	}
}