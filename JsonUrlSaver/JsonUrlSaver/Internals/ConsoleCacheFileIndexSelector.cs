/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal sealed class ConsoleCacheFileIndexSelector : ICacheFileIndexSelector
	{
		private readonly ILogger _logger;

		public ConsoleCacheFileIndexSelector(ILogger<ConsoleCacheFileIndexSelector> logger)
		{
			ArgumentNullException.ThrowIfNull(logger);
			_logger = logger;
		}

		public bool TrySelectIndex(Uri url, uint minIndexExclusive, uint maxIndexInclusive, [NotNullWhen(true)][MaybeNullWhen(false)] out uint result)
		{
			while (true) {
				Console.Write(LoggerExtensions.CacheFileIndexPrompt);
				string? line = Console.ReadLine();

				_logger.LogCacheFileIndexPrompt(line);

				if (string.IsNullOrEmpty(line)) {
					result = 0;
					return false;
				} else if (uint.TryParse(line, out result)) {
					if (minIndexExclusive < result && result <= maxIndexInclusive) {
						return true;
					} else {
						_logger.LogInvalidCacheFileIndex(result, minIndexExclusive, maxIndexInclusive);
					}
				} else {
					_logger.LogInvalidCacheFileIndex(line);
				}
			}
		}
	}

	partial class LoggerExtensions
	{
		internal const string CacheFileIndexPrompt = "Type a cache file index here: ";

		[LoggerMessage(LogLevel.Trace, $"{CacheFileIndexPrompt} {{userInput}}")]
		internal static partial void LogCacheFileIndexPrompt(this ILogger logger, string? userInput);

		[LoggerMessage(LogLevel.Warning, "The cache file index (actual: {userInput}) should be more than {minExcl} and less than or equal to {maxIncl}.")]
		internal static partial void LogInvalidCacheFileIndex(this ILogger logger, uint userInput, uint minExcl, uint maxIncl);

		[LoggerMessage(LogLevel.Warning, "The specified index (\"{userInput}\") is not a valid positive integer.")]
		internal static partial void LogInvalidCacheFileIndex(this ILogger logger, string? userInput);
	}
}
