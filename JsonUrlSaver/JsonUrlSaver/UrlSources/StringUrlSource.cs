/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace JsonUrlSaver.UrlSources
{
	public sealed class StringUrlSource : IUrlSource
	{
		public string? TextData { get; }

		public StringUrlSource([StringSyntax(StringSyntaxAttribute.Uri)] string? url)
		{
			this.TextData = url;
		}

		public IEnumerator<Uri> GetEnumerator()
		{
			if (TryCreateUri(this.TextData, out var result)) {
				yield return result;
			}
		}

		internal static bool TryCreateUri([StringSyntax(StringSyntaxAttribute.Uri)] string? uriString, [NotNullWhen(true)][MaybeNullWhen(false)] out Uri? result)
			=> Uri.TryCreate(uriString, UriKind.Absolute, out result);
	}
}
