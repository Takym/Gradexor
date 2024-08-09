/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using JsonUrlSaver.UrlSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver
{
	internal sealed class CoreWorker : ICoreWorker
	{
		private const    string           DEFAULT_CACHE_PATH = ".json_url_saver_cache";
		private readonly ILogger          _logger;
		private readonly IServiceProvider _services;
		private readonly IConfiguration   _config;
		private readonly IDownloader      _downloader;
		private readonly IProcessStarter  _proc_starter;

		public CoreWorker(ILogger<CoreWorker> logger, IServiceProvider services, IConfiguration config, IDownloader downloader, IProcessStarter processStarter)
		{
			ArgumentNullException.ThrowIfNull(logger        );
			ArgumentNullException.ThrowIfNull(services      );
			ArgumentNullException.ThrowIfNull(config        );
			ArgumentNullException.ThrowIfNull(downloader    );
			ArgumentNullException.ThrowIfNull(processStarter);

			_logger       = logger;
			_services     = services;
			_config       = config;
			_downloader   = downloader;
			_proc_starter = processStarter;
		}

		public void Run()
		{
			bool doDownload = _config.GetValue(nameof(doDownload), true );
			bool doOpen     = _config.GetValue(nameof(doOpen),     false);

			switch (_config["mode"]) {
			case "downloadOnly":
				doDownload = true;
				doOpen     = false;
				break;
			case "openOnly":
				doDownload = false;
				doOpen     = true;
				break;
			}

			string? dir = _config[nameof(dir)];

			if (string.IsNullOrEmpty(dir)) {
				_logger.LogDirectoryNotSpecified();
				return;
			}

			if (!Directory.Exists(dir)) {
				_logger.LogDirectoryNotFound(dir);
				return;
			}

			dir = Path.GetFullPath(dir);

			string cache = Path.Combine(
				dir,
				_config.GetValue(nameof(cache), DEFAULT_CACHE_PATH) ?? DEFAULT_CACHE_PATH
			);
			Directory.CreateDirectory(cache);

			bool loadDir = true;

			string? file = _config[nameof(file)];
			if (!string.IsNullOrEmpty(file)) {
				_logger.LogModeIsFile(file);

				if (File.Exists(file)) {
					RunCore(new FileUrlSource(file, _services.GetRequiredService<ILogger<FileUrlSource>>()));
				} else {
					_logger.LogFileNotFound(file);
				}

				loadDir = false;
			}

			string? json = _config[nameof(json)];
			if (!string.IsNullOrEmpty(json)) {
				_logger.LogModeIsJson(json);

				RunCore(new JsonUrlSource(json, _services.GetRequiredService<ILogger<JsonUrlSource>>()));

				loadDir = false;
			}

			string? url = _config[nameof(url)];
			if (!string.IsNullOrEmpty(url)) {
				_logger.LogModeIsUrl(url);

				RunCore(new StringUrlSource(url));

				loadDir = false;
			}

			if (loadDir) {
				_logger.LogModeIsDir(dir);

				RunCore(new DirectoryUrlSource(dir, cache, _services.GetRequiredService<ILogger<DirectoryUrlSource>>()));
			}

			void RunCore(IUrlSource source)
			{
				if (doDownload) {
					_downloader.Download(cache, source);
				}

				if (doOpen) {
					_proc_starter.OpenForCaches(cache, source);
				}
			}
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Error, "The target directory is not specified.")]
		internal static partial void LogDirectoryNotSpecified(this ILogger logger);

		[LoggerMessage(LogLevel.Error, "The target directory (\"{path}\") is not found.")]
		internal static partial void LogDirectoryNotFound(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Information, "URLs will be loaded from the file (\"{path}\").")]
		internal static partial void LogModeIsFile(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Error, "The source file (\"{path}\") is not found.")]
		internal static partial void LogFileNotFound(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Information, "URLs will be loaded from the JSON text: {textData}")]
		internal static partial void LogModeIsJson(this ILogger logger, string textData);

		[LoggerMessage(LogLevel.Information, "The specified URL is \".{textData}\".")]
		internal static partial void LogModeIsUrl(this ILogger logger, string textData);

		[LoggerMessage(LogLevel.Information, "URLs will be loaded from the directory (\"{path}\").")]
		internal static partial void LogModeIsDir(this ILogger logger, string path);
	}
}
