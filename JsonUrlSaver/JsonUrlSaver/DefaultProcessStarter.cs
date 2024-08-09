/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using JsonUrlSaver.UrlSources;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver
{
	internal sealed class DefaultProcessStarter : IProcessStarter
	{
		private readonly ILogger               _logger;
		private readonly IUrlFileNameConverter _ufn_conv;
		private readonly IProcessCreator       _proc_creator;

		public DefaultProcessStarter(ILogger<DefaultProcessStarter> logger, IUrlFileNameConverter ufnConv, IProcessCreator processCreator)
		{
			ArgumentNullException.ThrowIfNull(logger        );
			ArgumentNullException.ThrowIfNull(ufnConv       );
			ArgumentNullException.ThrowIfNull(processCreator);

			_logger       = logger;
			_ufn_conv     = ufnConv;
			_proc_creator = processCreator;
		}

		public void OpenForCaches(string cacheDir, IUrlSource source)
		{
			_logger.LogOpening();

			foreach (var url in source) {
				string   dir   = _ufn_conv.GetCacheDirectoryPath(cacheDir, url);
				string[] files = Directory.GetFiles(dir, "*", SearchOption.TopDirectoryOnly);

				_logger.LogCacheFilesCount(url, files.Length);

				if (_logger.IsEnabled(LogLevel.Trace)) {
					for (int i = 0; i < files.Length; ++i) {
						_logger.LogCacheFileFullPath(files[i]);
					}
				}

				while (true) {
					Console.Write(LoggerExtensions.CacheFileIndexPrompt);
					string? line = Console.ReadLine();

					_logger.LogCacheFileIndexPrompt(line);

					if (ulong.TryParse(line, out ulong index)) {
						_proc_creator.CreateProcess(
							_ufn_conv.GetCacheFilePath(cacheDir, url, index)
						);
						break;
					} else {
						_logger.LogInvalidCacheFileIndex(line);
						continue;
					}
				}
			}

			_logger.LogOpened();
		}
	}

	partial class LoggerExtensions
	{
		internal const string CacheFileIndexPrompt = "Type a cache file index here: ";

		[LoggerMessage(LogLevel.Information, "Opening for caches...")]
		internal static partial void LogOpening(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "Opened for caches!")]
		internal static partial void LogOpened(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "The count of cache files for \"{url}\" is {count}.")]
		internal static partial void LogCacheFilesCount(this ILogger logger, Uri url, int count);

		[LoggerMessage(LogLevel.Trace, "The full path of a cache file is: {path}")]
		internal static partial void LogCacheFileFullPath(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Trace, $"{CacheFileIndexPrompt} {{userInput}}")]
		internal static partial void LogCacheFileIndexPrompt(this ILogger logger, string? userInput);

		[LoggerMessage(LogLevel.Warning, "The specified index (\"{userInput}\") is not a valid positive integer.")]
		internal static partial void LogInvalidCacheFileIndex(this ILogger logger, string? userInput);
	}
}
