/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using JsonUrlSaver.Internals;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.UrlSources
{
	public sealed class JsonUrlSource : IUrlSource
	{
		internal static readonly JsonReaderOptions _jro = new() {
			AllowTrailingCommas = true,
			CommentHandling     = JsonCommentHandling.Skip
		};

		private readonly ILogger _logger;

		public string TextData { get; }

		public JsonUrlSource(string json, ILogger<JsonUrlSource> logger)
		{
			ArgumentNullException.ThrowIfNull(json  );
			ArgumentNullException.ThrowIfNull(logger);

			_logger       = logger;
			this.TextData = json;
		}

		public IEnumerator<Uri> GetEnumerator()
		{
			var jr = new Utf8JsonReader(Encoding.UTF8.GetBytes(this.TextData), _jro);
			return CreateEnumerable(ref jr, _logger).GetEnumerator();
		}

		internal static IEnumerable<Uri> CreateEnumerable(ref Utf8JsonReader reader, ILogger logger)
		{
			try {
				if (JsonDocument.TryParseValue(ref reader, out var json)) {
					return FromJsonToUris(json.RootElement);

					static IEnumerable<Uri> FromJsonToUris(JsonElement elem)
					{
						switch (elem.ValueKind) {
						case JsonValueKind.Object:
							foreach (var child in elem.EnumerateObject()) {
								foreach (var item in FromJsonToUris(child.Value)) {
									yield return item;
								}
							}
							break;
						case JsonValueKind.Array:
							foreach (var child in elem.EnumerateArray()) {
								foreach (var item in FromJsonToUris(child)) {
									yield return item;
								}
							}
							break;
						case JsonValueKind.String:
							if (StringUrlSource.TryCreateUri(elem.GetString(), out var result)) {
								yield return result;
							}
							break;
						}
					}
				} else {
					return [];
				}
			} catch (Exception e) {
				logger.LogExceptionAsWarning(e.Message, e);
				return [];
			}
		}
	}
}
