﻿/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver
{
	public static class HostExtensions
	{
		private static void ConfigureServices(this IServiceCollection services) => services
			.AddCoreWorker()
			.AddDownloader()
			.AddHttpClient()
			.AddUrlFileNameConverter()
			.AddProcessStarter()
			.AddProcessCreator();

		public static IHostBuilder InitializeJsonUrlSaver(this IHostBuilder builder)
		{
			ArgumentNullException.ThrowIfNull(builder);

			builder.ConfigureServices(ConfigureServices);
			return builder;
		}

		public static IHostApplicationBuilder InitializeJsonUrlSaver(this IHostApplicationBuilder builder)
		{
			ArgumentNullException.ThrowIfNull(builder);

			builder.Services.ConfigureServices();
			return builder;
		}

		public static IHost Build(this IHostApplicationBuilder builder)
		{
			ArgumentNullException.ThrowIfNull(builder);

			if (builder is HostApplicationBuilder hab) {
				return hab.Build();
			}

			return builder.Services.BuildServiceProvider().GetRequiredService<IHost>();
		}

		public static void RunJsonUrlSaver(this IHost host)
		{
			ArgumentNullException.ThrowIfNull(host);

			using (host) {
				host.Start();

				var services = host.Services;
				var logger   = services.GetRequiredService<ILoggerFactory>()
					.CreateLogger(typeof(HostExtensions).FullName ?? string.Empty);

				logger.LogCoreWorkerBegin();
				services.GetRequiredService<ICoreWorker>().Run();
				logger.LogCoreWorkerEnded();

				host.StopAsync().ConfigureAwait(false).GetAwaiter().GetResult();
			}
		}

		public static IServiceCollection AddCoreWorker(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddSingleton<ICoreWorker, CoreWorker>();
			return services;
		}

		public static IServiceCollection AddDownloader(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddSingleton<IDownloader, DefaultDownloader>();
			return services;
		}

		public static IServiceCollection AddHttpClient(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddTransient<HttpClient>(_ => new());
			return services;
		}

		public static IServiceCollection AddUrlFileNameConverter(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddSingleton<IUrlFileNameConverter, DefaultUrlFileNameConverter>();
			return services;
		}

		public static IServiceCollection AddProcessStarter(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddSingleton<IProcessStarter, DefaultProcessStarter>();
			return services;
		}

		public static IServiceCollection AddProcessCreator(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddSingleton<IProcessCreator, DefaultProcessCreator>();
			return services;
		}
	}
}