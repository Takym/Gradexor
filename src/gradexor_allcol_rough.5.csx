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

void Draw(int r, int g, int b, int x, int y, int width, int height)
{
	int r1_1 = 255, g1_1 = 255, b1_1 = 255;
	int r1_2 = r,   g1_2 = g,   b1_2 = b;
	int r2_1 = 128, g2_1 = 128, b2_1 = 128;
	int r2_2 = 000, g2_2 = 000, b2_2 = 000;

	for (int ix = 0; ix < width; ++ix) {
		double h = (((double)(ix)) / ((double)(width - 1)));
		double i = 1.0D - h;

		double r1 = (r1_1 * i) + (r1_2 * h);
		double g1 = (g1_1 * i) + (g1_2 * h);
		double b1 = (b1_1 * i) + (b1_2 * h);

		double r2 = (r2_1 * i) + (r2_2 * h);
		double g2 = (g2_1 * i) + (g2_2 * h);
		double b2 = (b2_1 * i) + (b2_2 * h);

		for (int iy = 0; iy < height; ++iy) {
			double j = (((double)(iy)) / ((double)(height - 1)));
			double k = 1.0D - j;

			bmp.SetPixel(x + ix, y + iy, Color.FromArgb(
				((int)((r1 * k) + (r2 * j))),
				((int)((g1 * k) + (g2 * j))),
				((int)((b1 * k) + (b2 * j)))
			));
		}
	}
}

int newWid = width / 4, newHei = height / 4;
Draw(000, 000, 000, newWid * 0, newHei * 0, newWid, newHei);
Draw(128, 000, 000, newWid * 1, newHei * 0, newWid, newHei);
Draw(000, 128, 000, newWid * 2, newHei * 0, newWid, newHei);
Draw(000, 000, 128, newWid * 3, newHei * 0, newWid, newHei);
Draw(096, 096, 096, newWid * 0, newHei * 1, newWid, newHei);
Draw(000, 128, 128, newWid * 1, newHei * 1, newWid, newHei);
Draw(128, 000, 128, newWid * 2, newHei * 1, newWid, newHei);
Draw(128, 128, 000, newWid * 3, newHei * 1, newWid, newHei);
Draw(160, 160, 160, newWid * 0, newHei * 2, newWid, newHei);
Draw(255, 000, 000, newWid * 1, newHei * 2, newWid, newHei);
Draw(000, 255, 000, newWid * 2, newHei * 2, newWid, newHei);
Draw(000, 000, 255, newWid * 3, newHei * 2, newWid, newHei);
Draw(255, 255, 255, newWid * 0, newHei * 3, newWid, newHei);
Draw(000, 255, 255, newWid * 1, newHei * 3, newWid, newHei);
Draw(255, 000, 255, newWid * 2, newHei * 3, newWid, newHei);
Draw(255, 255, 000, newWid * 3, newHei * 3, newWid, newHei);

bmp.Save("output.png", ImageFormat.Png);
