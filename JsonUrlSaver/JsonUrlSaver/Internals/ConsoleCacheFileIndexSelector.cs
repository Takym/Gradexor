/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal sealed class ConsoleCacheFileIndexSelector : ICacheFileIndexSelector
	{
		private readonly ILogger _logger;
		private readonly uint    _cache_index;

		public ConsoleCacheFileIndexSelector(ILogger<ConsoleCacheFileIndexSelector> logger, IConfiguration config)
		{
			ArgumentNullException.ThrowIfNull(logger);
			ArgumentNullException.ThrowIfNull(config);

			_logger      = logger;
			_cache_index = config.GetValue("cacheIndex", 0U);
		}

		public bool TrySelectIndex(Uri url, uint minIndexExclusive, uint maxIndexInclusive, [NotNullWhen(true)][MaybeNullWhen(false)] out uint result)
		{
			uint cidx = _cache_index;
			if (cidx > 0) {
				if (minIndexExclusive < cidx && cidx <= maxIndexInclusive) {
					_logger.LogCacheFileIndexConfig(cidx);
					result = cidx;
					return true;
				} else {
					_logger.LogInvalidCacheFileIndexConfig(cidx);
					result = 0;
					return false;
				}
			} else {
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
	}

	partial class LoggerExtensions
	{
		internal const string CacheFileIndexPrompt = "Type a cache file index here: ";

		[LoggerMessage(LogLevel.Information, "The configuration specifies the cache file index (actual: {cacheIndex}), so skipped user input.")]
		internal static partial void LogCacheFileIndexConfig(this ILogger logger, uint cacheIndex);

		[LoggerMessage(LogLevel.Warning, "The configuration specifies the invalid cache file index (actual: {cacheIndex}), but skipped user input.")]
		internal static partial void LogInvalidCacheFileIndexConfig(this ILogger logger, uint cacheIndex);

		[LoggerMessage(LogLevel.Trace, $"{CacheFileIndexPrompt} {{userInput}}")]
		internal static partial void LogCacheFileIndexPrompt(this ILogger logger, string? userInput);

		[LoggerMessage(LogLevel.Warning, "The cache file index (actual: {userInput}) should be more than {minExcl} and less than or equal to {maxIncl}.")]
		internal static partial void LogInvalidCacheFileIndex(this ILogger logger, uint userInput, uint minExcl, uint maxIncl);

		[LoggerMessage(LogLevel.Warning, "The specified index (\"{userInput}\") is not a valid positive integer.")]
		internal static partial void LogInvalidCacheFileIndex(this ILogger logger, string? userInput);
	}
}
