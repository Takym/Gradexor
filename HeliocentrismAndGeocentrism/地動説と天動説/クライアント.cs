/****
 * 地動説と天動説
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace 地動説と天動説
{
	public partial class クライアント : Control
	{
		private readonly 星 太陽;
		private readonly 星 地球;
		private readonly 星 _stars;
		private          星 _fixed;

		public クライアント()
		{
			var 火星A = new 惑星(Color.FromArgb(0xFF, 0x00, 0xFF), Color.FromArgb(0x00, 0x80, 0xFF), 170.0F, 0.8F) {
				幅 = 6.0F
			};

			var 火星B = new 惑星(Color.FromArgb(0xFF, 0x00, 0xFF), Color.FromArgb(0x00, 0x80, 0xFF), 130.0F, 0.1F) {
				幅 = 6.0F
			};

			var 月 = new 惑星(Color.FromArgb(0x80, 0x80, 0x80), Color.FromArgb(0x00, 0x80, 0x00), 10.0F, 1.0F) {
				幅 = 4.0F
			};

			地球 = new 惑星(Color.FromArgb(0x00, 0x00, 0xFF), Color.FromArgb(0x00, 0x80, 0xFF), 80.0F, 0.2F) {
				幅 = 6.0F
			};
			地球.衛星.Add(月);

			var 金星 = new 惑星(Color.FromArgb(0xFF, 0xFF, 0x00), Color.FromArgb(0xFF, 0xFF, 0x00), 40.0F, 0.5F) {
				幅 = 6.0F
			};

			太陽 = new 恒星(Color.FromArgb(0xFF, 0x80, 0x00), Color.FromArgb(0xFF, 0x00, 0x00)) {
				幅 = 6.0F
			};
			太陽.衛星.Add(火星A);
			太陽.衛星.Add(火星B);
			太陽.衛星.Add(地球);
			太陽.衛星.Add(金星);

			_stars = 太陽;
			_fixed = 太陽;

			this.SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.Opaque |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true
			);

			this.InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			_stars.描画(pe, _fixed);
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			_stars.座標再設定(null, true);
			this.Invalidate();
		}

		public void 地動説() => _fixed = 太陽;
		public void 天動説() => _fixed = 地球;
	}
}
