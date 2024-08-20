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

namespace JsonUrlSaver.Internals
{
	internal sealed class DefaultProcessStarterImpl : IProcessStarter
	{
		private readonly ILogger                 _logger;
		private readonly IUrlFileNameConverter   _ufn_conv;
		private readonly ICacheFileIndexSelector _idx_sel;
		private readonly IProcessCreator         _proc_creator;

		public DefaultProcessStarterImpl(
			ILogger<DefaultProcessStarterImpl> logger,
			IUrlFileNameConverter              ufnConv,
			ICacheFileIndexSelector            idxSel,
			IProcessCreator                    processCreator)
		{
			ArgumentNullException.ThrowIfNull(logger        );
			ArgumentNullException.ThrowIfNull(ufnConv       );
			ArgumentNullException.ThrowIfNull(idxSel        );
			ArgumentNullException.ThrowIfNull(processCreator);

			_logger       = logger;
			_ufn_conv     = ufnConv;
			_idx_sel      = idxSel;
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

				if (_idx_sel.TrySelectIndex(url, 0, unchecked((uint)(files.Length)), out uint index)) {
					_proc_creator.CreateProcess(
						_ufn_conv.GetCacheFilePath(cacheDir, url, index)
					);
				} else {
					_logger.LogCanceledToOpenCacheFile(url);
				}
			}

			_logger.LogOpened();
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Information, "Opening for caches...")]
		internal static partial void LogOpening(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "Opened for caches!")]
		internal static partial void LogOpened(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "The count of cache files for \"{url}\" is {count}.")]
		internal static partial void LogCacheFilesCount(this ILogger logger, Uri url, int count);

		[LoggerMessage(LogLevel.Trace, "The full path of a cache file is: {path}")]
		internal static partial void LogCacheFileFullPath(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Warning, "Canceled to open the cache file for \"{url}\".")]
		internal static partial void LogCanceledToOpenCacheFile(this ILogger logger, Uri url);
	}
}
