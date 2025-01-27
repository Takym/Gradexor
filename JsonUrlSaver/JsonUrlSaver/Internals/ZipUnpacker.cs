/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal sealed class ZipUnpacker : ICoreWorker
	{
		private readonly ILogger        _logger;
		private readonly IConfiguration _config;
		private readonly CoreWorker     _core_worker;

		public ZipUnpacker(ILogger<ZipUnpacker> logger, IConfiguration config, CoreWorker coreWorker)
		{
			ArgumentNullException.ThrowIfNull(logger    );
			ArgumentNullException.ThrowIfNull(config    );
			ArgumentNullException.ThrowIfNull(coreWorker);

			_logger      = logger;
			_config      = config;
			_core_worker = coreWorker;
		}

		public void Run()
		{
			string? zip = _config[nameof(zip)];
			if (!string.IsNullOrEmpty(zip)) {
				if (File.Exists(zip)) {
					string? dir = _config[nameof(dir)];
					if (string.IsNullOrEmpty(dir)) {
						dir = Path.ChangeExtension(zip, null);
						if (dir == zip) {
							dir = Path.ChangeExtension(zip, "out");
						}
						_config[nameof(dir)] = dir;
					}

					_logger.LogTargetDirectoryPath(dir);

					try {
						ZipFile.ExtractToDirectory(zip, dir, _config.GetValue("zipOverwrite", false));
						_logger.LogSucceededToExtractZipFile(zip, dir);
					} catch (Exception e) {
						_logger.LogFailedToExtractZipFile(zip, dir, e);
						return;
					}
				} else {
					_logger.LogZipFileNotFound(zip);
					return;
				}
			}

			_core_worker.Run();
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Error, "The zip file (\"{path}\") is not found.")]
		internal static partial void LogZipFileNotFound(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Trace, "The target directory path is: {path}")]
		internal static partial void LogTargetDirectoryPath(this ILogger logger, string path);

		[LoggerMessage(LogLevel.Information, "Succeeded to extract the zip file: \"{pathFrom}\" ---> \"{pathTo}\"")]
		internal static partial void LogSucceededToExtractZipFile(this ILogger logger, string pathFrom, string pathTo);

		[LoggerMessage(LogLevel.Error, "Failed to extract the zip file: \"{pathFrom}\" ---> \"{pathTo}\"")]
		internal static partial void LogFailedToExtractZipFile(this ILogger logger, string pathFrom, string pathTo, Exception e);
	}
}
