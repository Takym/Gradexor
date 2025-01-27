/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.Internals
{
	internal sealed class DefaultProcessCreatorImpl : IProcessCreator
	{
		private readonly ILogger _logger;

		public DefaultProcessCreatorImpl(ILogger<DefaultProcessCreatorImpl> logger)
		{
			ArgumentNullException.ThrowIfNull(logger);
			_logger = logger;
		}

		public void CreateProcess(string fname)
		{
			var psi = new ProcessStartInfo();
			psi.FileName        = fname;
			psi.Verb            = "open";
			psi.UseShellExecute = true;

			try {
				using (var proc = Process.Start(psi)) {
					if (proc is null) {
						_logger.LogFailedToStartProcess(fname, null);
					} else {
						_logger.LogProcessStarted(fname, proc.Id);
					}
				}
			} catch (Exception e) {
				_logger.LogFailedToStartProcess(fname, e);
			}
		}
	}

	partial class LoggerExtensions
	{
		[LoggerMessage(LogLevel.Information, "The process for \"{fname}\" has been started with PID {pid}.")]
		internal static partial void LogProcessStarted(this ILogger logger, string fname, int pid);

		[LoggerMessage(LogLevel.Error, "Failed to start a process for \"{fname}\".")]
		internal static partial void LogFailedToStartProcess(this ILogger logger, string fname, Exception? e);
	}
}
