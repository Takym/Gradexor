/****
 * JsonUrlSaver
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Windows.Forms;

namespace JsonUrlSaver.GUI.WinForms
{
	public partial class CacheFilePage : UserControl
	{
		private ReadOnlyMemory<string> _default_args;

		public CacheFilePage()
		{
			this.InitializeComponent();
		}

		public void SetDefaultArgs(ReadOnlyMemory<string> defaultArgs)
			=> _default_args = defaultArgs;

		private void CacheFilePage_Load(object sender, EventArgs e)
		{

		}
	}
}
