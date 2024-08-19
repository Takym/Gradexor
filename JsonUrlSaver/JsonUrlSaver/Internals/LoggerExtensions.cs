/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal static partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Trace, "Executing the core worker...")]
		internal static partial void LogCoreWorkerBegin(this ILogger logger);

		[LoggerMessage(LogLevel.Trace, "Completed the core worker!")]
		internal static partial void LogCoreWorkerEnded(this ILogger logger);

		[LoggerMessage(LogLevel.Error, "{msg}")]
		internal static partial void LogException(this ILogger logger, string msg, Exception e);

		[LoggerMessage(LogLevel.Warning, "{msg}")]
		internal static partial void LogExceptionAsWarning(this ILogger logger, string msg, Exception e);

		internal static void LogCurrentVersion(this ILogger logger)
		{
			var asm = Assembly.GetExecutingAssembly();
			var ver = asm.GetName().Version;

			if (ver is null) {
				logger.LogNoVersionInfo();
			} else {
				string name = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? nameof(JsonUrlSaver);

				logger.LogCurrentVersionCore(name, ver);

				if (asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright is not null and string copyright) {
					logger.LogCopyright(name, copyright);
				}
			}
		}

		[LoggerMessage(LogLevel.Warning, "Could not load the version information.")]
		private static partial void LogNoVersionInfo(this ILogger logger);

		[LoggerMessage(LogLevel.Information, "The current {productName} version is: {version}")]
		private static partial void LogCurrentVersionCore(this ILogger logger, string productName, Version version);

		[LoggerMessage(LogLevel.Information, "The copyright of {productName} is: {copyright}")]
		private static partial void LogCopyright(this ILogger logger, string productName, string copyright);
	}
}
