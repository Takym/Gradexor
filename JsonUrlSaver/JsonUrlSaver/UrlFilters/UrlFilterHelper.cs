/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Microsoft.Extensions.DependencyInjection;

namespace JsonUrlSaver.UrlFilters
{
	public static class UrlFilterHelper
	{
		public static IUrlFilter? GetUrlFilter(this IServiceProvider services, string key)
		{
			ArgumentNullException.ThrowIfNull(services);
			ArgumentNullException.ThrowIfNull(key     );

			string[] keys = key.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			switch (keys.Length) {
			case 0:
				return null;
			case 1:
				return services.GetKeyedService<IUrlFilter>(keys[0]);
			default:
				var filters = new IUrlFilter?[keys.Length];
				for (int i = 0; i < filters.Length; ++i) {
					filters[i] = services.GetKeyedService<IUrlFilter>(keys[i]);
				}
				return new CombinedUrlFilter(filters);
			}
		}
	}
}
