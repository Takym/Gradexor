/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.UrlSources
{
	public sealed class FileUrlSource : IUrlSource
	{
		private readonly ILogger _logger;

		public string Path { get; }

		public FileUrlSource(string path, ILogger<FileUrlSource> logger)
		{
			ArgumentNullException.ThrowIfNull(path  );
			ArgumentNullException.ThrowIfNull(logger);

			_logger   = logger;
			this.Path = path;
		}

		public IEnumerator<Uri> GetEnumerator()
			=> CreateEnumerable(this.Path, _logger).GetEnumerator();

		internal static IEnumerable<Uri> CreateEnumerable(string fname, ILogger logger)
		{
			try {
				byte[] buf;

				using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					buf = new byte[fs.Length];
					fs.ReadExactly(buf);
				}

				var jr = new Utf8JsonReader(buf, JsonUrlSource._jro);
				return JsonUrlSource.CreateEnumerable(ref jr, logger);
			} catch (Exception e) {
				logger.LogException(e.Message, e);
				return [];
			}
		}
	}
}
