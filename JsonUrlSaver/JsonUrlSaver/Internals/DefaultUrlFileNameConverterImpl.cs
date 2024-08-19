/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal sealed class DefaultUrlFileNameConverterImpl : IUrlFileNameConverter
	{
		private readonly ILogger _logger;

		public DefaultUrlFileNameConverterImpl(ILogger<DefaultUrlFileNameConverterImpl> logger)
		{
			ArgumentNullException.ThrowIfNull(logger);
			_logger = logger;
		}

		public string GetCacheDirectoryPath(string baseDir, Uri uri)
		{
			ArgumentNullException.ThrowIfNull(baseDir);
			ArgumentNullException.ThrowIfNull(uri);

			string result = GetCacheDirectoryPathCore(baseDir, uri);

			_logger.LogGetCacheDirectoryPathResult(baseDir, uri, result);

			return result;
		}

		private static string GetCacheDirectoryPathCore(string baseDir, Uri uri)
		{
			string scheme_and_host = $"{uri.Scheme}--{GetSafePath(uri.Host)}";

			return Path.Combine(
				baseDir,
				uri.IsDefaultPort
					? $"{scheme_and_host}"
					: $"{scheme_and_host}--{uri.Port}",
				GetSafePath(uri.LocalPath[1..])
			);
		}

		private static string GetSafePath(string unsafePath)
		{
			var sb         = new StringBuilder();
			int sectionLen = 0;

			for (int i = 0; i < unsafePath.Length; ++i) {
				char ch = unsafePath[i];

				string? escaped = ch switch {
					':'  => "-C-", // Colon
					'*'  => "-A-", // Asterisk
					'?'  => "-Q-", // Question
					'\"' => "-D-", // Double quote
					'<'  => "-L-", // Less than
					'>'  => "-G-", // Greater than
					'|'  => "-V-", // Vertical bar
					'-'  => "-H-", // Hyphen - エスケープ文字として使っている為
					_    => null
				};

				if (escaped is null) {
					sb.Append(ch);
				} else {
					sb.Append(escaped);
				}

				if (ch == '/' || ch == '\\') {
					sectionLen = 0;
				} else {
					++sectionLen;

					if (sectionLen > 48) {
						sb.Append("\\-S-"); // Splitted
						sectionLen = 3;
					}
				}
			}
			return sb.ToString();
		}

		/* private static string GetSafePath(string unsafePath)
			=> unsafePath
				.Replace(":",  "--colon--")
				.Replace("*",  "--asterisk--")
				.Replace("?",  "--question--")
				.Replace("\"", "--double-quote--")
				.Replace("<",  "--less-than--")
				.Replace(">",  "--greater-than--")
				.Replace("|",  "--vertical-bar--")
				.Replace("-",  "--hyphen--"); //*/

		public string GetCacheFilePath(string baseDir, Uri uri, uint index)
		{
			ArgumentNullException.ThrowIfNull(baseDir);
			ArgumentNullException.ThrowIfNull(uri    );

			string cacheDir = GetCacheDirectoryPathCore(baseDir, uri);
			string ext      = Path.GetExtension(cacheDir);
			string result;

			if (index == 0) {
				do {
					++index;
					result = GetCacheFilePathCore();
				} while (File.Exists(result));
			} else {
				result = GetCacheFilePathCore();
			}

			_logger.LogGetCacheFilePathResult(baseDir, uri, index, result);

			return result;

			string GetCacheFilePathCore()
				=> Path.Combine(cacheDir, $"{index}{ext}");
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Trace, "GetCacheDirectoryPath(\"{baseDir}\", \"{uri}\") => {path}")]
		internal static partial void LogGetCacheDirectoryPathResult(this ILogger logger, string baseDir, Uri uri, string path);

		[LoggerMessage(LogLevel.Trace, "GetCacheFilePath(\"{baseDir}\", \"{uri}\", {index}) => {path}")]
		internal static partial void LogGetCacheFilePathResult(this ILogger logger, string baseDir, Uri uri, uint index, string path);
	}
}
