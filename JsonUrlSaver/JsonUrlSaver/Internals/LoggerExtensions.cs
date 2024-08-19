/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
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
	}
}
