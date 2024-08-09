/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections;
using System.Collections.Generic;

namespace JsonUrlSaver.UrlSources
{
	public interface IUrlSource : IEnumerable<Uri>
	{
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
