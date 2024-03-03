/****
 * 地動説と天動説
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 地動説と天動説
{
	public abstract class 星 : IDisposable
	{
		private readonly Brush _b;
		private readonly Pen   _p;
		private          bool  _is_disposed;

		public         List<星> 衛星 { get; }
		public virtual float    半径 { get; }
		public         float    角度 { get; set; }
		public         float    X    { get; set; }
		public         float    Y    { get; set; }
		public         float    幅   { get; init; }

		protected bool 破棄された => _is_disposed;

		protected 星(Color 背景色, Color 枠の色)
		{
			_b           = new SolidBrush(背景色);
			_p           = new(枠の色);
			_is_disposed = false;

			this.衛星 = [];
		}

		~星()
		{
			this.Dispose(false);
		}

		public void 座標再設定(星? 中心, bool 角度を更新する)
		{
			ObjectDisposedException.ThrowIf(this.破棄された, this);

			if (角度を更新する) {
				this.角度更新();
			}

			this.X = (this.半径 * MathF.Cos(this.角度)) + (中心?.X ?? 0.0F);
			this.Y = (this.半径 * MathF.Sin(this.角度)) + (中心?.Y ?? 0.0F);

			int count = this.衛星.Count;
			for (int i = 0; i < count; ++i) {
				var star = this.衛星[i];
				star.座標再設定(this, 角度を更新する);
			}
		}

		protected abstract void 角度更新();

		public void 描画(PaintEventArgs pe, 星? 固定する星)
		{
			ObjectDisposedException.ThrowIf(this.破棄された, this);
			ArgumentNullException.ThrowIfNull(pe);

			int count = this.衛星.Count;
			for (int i = 0; i < count; ++i) {
				var star = this.衛星[i];
				star.描画(pe, 固定する星);
			}

			var rect = pe.ClipRectangle;
			this.描画Core(
				pe.Graphics,
				((rect.X + rect.Width ) / 2.0F) + (this.X - (固定する星?.X ?? 0)),
				((rect.Y + rect.Height) / 2.0F) - (this.Y - (固定する星?.Y ?? 0))
			);
		}

		protected virtual void 描画Core(Graphics g, float x, float y)
		{
			float halfwid = this.幅 / 2.0F;

			x -= halfwid;
			y -= halfwid;

			g.FillEllipse(_b, x, y, this.幅, this.幅);
			g.DrawEllipse(_p, x, y, this.幅, this.幅);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_is_disposed) {
				return;
			}

			if (disposing) {
				_b.Dispose();
				_p.Dispose();
			}

			this.衛星.Clear();
			this.衛星.TrimExcess();

			_is_disposed = true;
		}
	}
}
