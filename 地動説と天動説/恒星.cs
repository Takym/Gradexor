/****
 * 地動説と天動説
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

using System.Drawing;

namespace 地動説と天動説
{
	public sealed class 恒星 : 星
	{
		public 恒星(Color 背景色, Color 枠の色) : base(背景色, 枠の色) { }

		protected override void 角度更新()
		{
			// 何もしない。
		}
	}
}
