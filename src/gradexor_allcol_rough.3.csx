/****
 * Gradexor - 排他的論理和色彩変化画像
 * Copyright (C) 2020-2022 Takym.
 *
 * distributed under the MIT License.
****/

// All Colors

// これは C# スクリプトで実装した静的画像を生成する Gradexor です。
// C# インタラクティブで実行してください。
// .NET 5 以降を推奨します。

#r "System.Drawing.dll"
using System.Drawing;
using System.Drawing.Imaging;

const int width = 4096, height = 4096;
var bmp = new Bitmap(width, height);

/* for (int x = 0; x < width; ++x) {
	for (int y = 0; y < height; ++y) {
		//int xi = x >> 8, xj = x & 0xFF, xk = 0xFF - xj;
		//int yi = y >> 8, yj = y & 0xFF, yk = 0xFF - yj;

		//int xi = x & 0x0F, xj = x >> 4, xk = 0xFF - xj;
		//int yi = y & 0x0F, yj = y >> 4, yk = 0xFF - yj;

		int xi = x >> 8, xj = (x >> 4) & 0x0F, xk = x & 0x0F;
		int yi = y >> 8, yj = (y >> 4) & 0x0F, yk = y & 0x0F;

		bmp.SetPixel(x, y, Color.FromArgb(
			(xi << 4) | xj,
			(yk << 4) | xk,
			(yi << 4) | yj
		));
	}
} //*/

for (int r = 0; r < 256; ++r) {
	for (int g = 0; g < 256; ++g) {
		for (int b = 0; b < 256; ++b) {
			int rh = r >> 4, rl = r & 0x0F;
			int gh = g >> 4, gl = g & 0x0F;
			int bh = b >> 4, bl = b & 0x0F;

			if ((gl & 1) == 1) {
				rh = 0x0F - rh;
				rl = 0x0F - rl;
			}

			if ((gh & 1) == 1) {
				bh = 0x0F - bh;
				bl = 0x0F - bl;
			}

			bmp.SetPixel(
				(gl << 8) | (rh << 4) | rl,
				(gh << 8) | (bh << 4) | bl,
				Color.FromArgb(r, g, b)
			);
		}
	}
}

bmp.Save("output.png", ImageFormat.Png);
