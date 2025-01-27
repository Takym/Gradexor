/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Net;

namespace JsonUrlSaver.UrlFilters
{
	public sealed class AllUrlFilter : IUrlFilter
	{
		public bool ShouldDownload(Uri url) => true;
	}

	public sealed class LocalhostUrlFilter : IUrlFilter
	{
		public bool ShouldDownload(Uri url)
		{
			if (url.Host == "localhost") {
				return true;
			}

			if (IPAddress.TryParse(url.Host, out var ip)) {
				return IPAddress.IsLoopback(ip);
			}

			return false;
		}
	}

	public sealed class SlackUrlFilter : IUrlFilter
	{
		public bool ShouldDownload(Uri url)
			=> url.Host == "files.slack.com";
	}
}
