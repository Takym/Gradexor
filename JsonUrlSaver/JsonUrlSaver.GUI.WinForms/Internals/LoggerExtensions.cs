/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.GUI.WinForms.Internals
{
	internal static partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Trace, "Input cache file index: {userInput}")]
		internal static partial void LogCacheFileIndexSelectorDialogResult(this ILogger logger, uint userInput);
	}
}
