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
	public partial class FormMain : Form
	{
		private readonly ReadOnlyMemory<string> _default_args;

		public FormMain(string[] args)
		{
			_default_args = args;
			this.InitializeComponent();
		}
	}
}
