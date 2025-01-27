/****
 * JsonUrlSaver
 * Copyright (C) 2025 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Windows.Forms;

namespace JsonUrlSaver.GUI.WinForms
{
	public partial class CacheFileIndexSelectorDialog : Form
	{
		private readonly Uri  _url;
		private readonly uint _min_excl;
		private readonly uint _max_incl;

		public uint Index => unchecked((uint)(nudIndex.Value));

		public CacheFileIndexSelectorDialog(Uri url, uint minIndexExclusive, uint maxIndexInclusive)
		{
			ArgumentNullException.ThrowIfNull(url);

			_url = url;
			_min_excl = minIndexExclusive;
			_max_incl = maxIndexInclusive;

			this.InitializeComponent();
		}

		private void CacheFileIndexSelectorDialog_Load(object sender, EventArgs e)
		{
			lblURL.Text = _url.ToString();

			nudIndex.Minimum = _min_excl + 1;
			nudIndex.Maximum = _max_incl;
		}

		private void lblURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			=> FormMain.Open(_url.ToString());

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
