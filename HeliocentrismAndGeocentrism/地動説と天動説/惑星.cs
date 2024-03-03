/****
 * 地動説と天動説
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Drawing;

namespace 地動説と天動説
{
	public sealed class 惑星 : 星
	{
		private readonly float _radius;
		private readonly float _angular_velocity;

		public override float 半径 => _radius;

		public 惑星(Color 背景色, Color 枠の色, float 半径, float 角速度)
			: base(背景色, 枠の色)
		{
			_radius           = 半径;
			_angular_velocity = 角速度;
		}

		protected override void 角度更新()
		{
			float a = this.角度 + _angular_velocity;

			const float PI2 = 2.0F * MathF.PI;

			this.角度 = a switch {
				<  0   => (a % PI2) + PI2,
				>= PI2 => (a % PI2),
				_      => a
			};
		}
	}
}
