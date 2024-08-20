/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace JsonUrlSaver.GUI.WinForms.Internals
{
	internal sealed class WinFormsCacheFileIndexSelector : ICacheFileIndexSelector
	{
		private readonly ILogger      _logger;
		private readonly IWin32Window _owner;

		public WinFormsCacheFileIndexSelector(ILogger<WinFormsCacheFileIndexSelector> logger, IWin32Window owner)
		{
			ArgumentNullException.ThrowIfNull(logger);
			ArgumentNullException.ThrowIfNull(owner );

			_logger = logger;
			_owner  = owner;
		}

		public bool TrySelectIndex(Uri url, uint minIndexExclusive, uint maxIndexInclusive, [NotNullWhen(true)][MaybeNullWhen(false)] out uint result)
		{
			using var dialog = new CacheFileIndexSelectorDialog(url, minIndexExclusive, maxIndexInclusive);
			if (dialog.ShowDialog(_owner) == DialogResult.OK) {
				result = dialog.Index;
				_logger.LogCacheFileIndexSelectorDialogResult(result);
				return true;
			} else {
				result = 0;
				return false;
			}
		}
	}
}
