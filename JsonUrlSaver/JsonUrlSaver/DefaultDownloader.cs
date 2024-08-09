/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUrlSaver.UrlFilters;
using JsonUrlSaver.UrlSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver
{
	internal sealed class DefaultDownloader : IDownloader
	{
		private readonly ILogger               _logger;
		private readonly IServiceProvider      _services;
		private readonly IUrlFileNameConverter _ufn_conv;
		private readonly IUrlFilter?           _url_filter;

		public DefaultDownloader(ILogger<DefaultDownloader> logger, IServiceProvider services, IConfiguration config, IUrlFileNameConverter ufnConv)
		{
			ArgumentNullException.ThrowIfNull(logger  );
			ArgumentNullException.ThrowIfNull(services);
			ArgumentNullException.ThrowIfNull(config  );
			ArgumentNullException.ThrowIfNull(ufnConv );

			_logger   = logger;
			_services = services;
			_ufn_conv = ufnConv;

			string? filters = config[nameof(filters)];
			if (!string.IsNullOrEmpty(filters)) {
				_url_filter = services.GetUrlFilter(filters);
			}
		}

		public void Download(string cacheDir, IUrlSource source)
		{
			_logger.LogDownloading();

			var task = this.DownloadAsync(cacheDir, source);

			if (!task.IsCompleted) {
				task.ConfigureAwait(false).GetAwaiter().GetResult();
			}

			_logger.LogDownloaded();
		}

		private async ValueTask DownloadAsync(string cacheDir, IUrlSource source)
		{
			using (var scope = _services.CreateScope())
			using (var hc    = scope.ServiceProvider.GetRequiredService<HttpClient>()) {
				foreach (var url in source) {
					if (_url_filter?.ShouldDownload(url) ?? true) {
						Stream src;

						try {
							src = await hc.GetStreamAsync(url).ConfigureAwait(false);
						} catch (Exception e) {
							_logger.LogFailedToDownload(url, e);
							continue;
						}

						await using (src.ConfigureAwait(false)) {
							Directory.CreateDirectory(_ufn_conv.GetCacheDirectoryPath(cacheDir, url));

							var dst = new FileStream(
								_ufn_conv.GetCacheFilePath(cacheDir, url),
								FileMode.Create, FileAccess.Write, FileShare.None
							);

							await using (dst.ConfigureAwait(false)) {
								await src.CopyToAsync(dst).ConfigureAwait(false);
							}
						}

						_logger.LogSucceededToDownload(url);
					} else {
						_logger.LogSkippedToDownload(url);
					}
				}
			}
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Information, "Downloading...")]
		internal static partial void LogDownloading(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "Downloaded!")]
		internal static partial void LogDownloaded(this ILogger logger);

		[LoggerMessage(LogLevel.Error, "Failed to download from: {url}")]
		internal static partial void LogFailedToDownload(this ILogger logger, Uri url, Exception e);

		[LoggerMessage(LogLevel.Information, "Succeeded to download from: {url}")]
		internal static partial void LogSucceededToDownload(this ILogger logger, Uri url);

		[LoggerMessage(LogLevel.Information, "Skipped to download by URL filters ignorant settings from: {url}")]
		internal static partial void LogSkippedToDownload(this ILogger logger, Uri url);
	}
}
