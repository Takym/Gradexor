/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.UrlSources
{
	public sealed class DirectoryUrlSource : IUrlSource
	{
		private readonly ILogger _logger;

		public string Path           { get; }
		public string CacheDirectory { get; }

		public DirectoryUrlSource(string path, string cacheDir, ILogger<DirectoryUrlSource> logger)
		{
			ArgumentNullException.ThrowIfNull(path    );
			ArgumentNullException.ThrowIfNull(cacheDir);
			ArgumentNullException.ThrowIfNull(logger  );

			_logger             = logger;
			this.Path           = path;
			this.CacheDirectory = cacheDir;
		}

		public IEnumerator<Uri> GetEnumerator()
		{
			foreach (string path in Directory.EnumerateFiles(this.Path, "*", SearchOption.AllDirectories)) {
				if (!path.StartsWith(this.CacheDirectory)) {
					foreach (var item in FileUrlSource.CreateEnumerable(path, _logger)) {
						yield return item;
					}
				}
			}
		}
	}
}
