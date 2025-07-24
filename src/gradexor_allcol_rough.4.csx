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

//*
int r1_1 = 064, g1_1 = 160, b1_1 = 064;
int r1_2 = 255, g1_2 = 128, b1_2 = 000;
int r2_1 = 000, g2_1 = 128, b2_1 = 255;
int r2_2 = 192, g2_2 = 096, b2_2 = 192; //*/

/*
int r1_1 = 0xFF, g1_1 = 0xFF, b1_1 = 0xFF;
int r1_2 = 0xFF, g1_2 = 0xFF, b1_2 = 0x00;
int r2_1 = 0x00, g2_1 = 0x00, b2_1 = 0xFF;
int r2_2 = 0x00, g2_2 = 0x00, b2_2 = 0x00; //*/

for (int x = 0; x < width; ++x) {
	double h = (((double)(x)) / ((double)(width - 1)));
	double i = 1.0D - h;

	double r1 = (r1_1 * i) + (r1_2 * h);
	double g1 = (g1_1 * i) + (g1_2 * h);
	double b1 = (b1_1 * i) + (b1_2 * h);

	double r2 = (r2_1 * i) + (r2_2 * h);
	double g2 = (g2_1 * i) + (g2_2 * h);
	double b2 = (b2_1 * i) + (b2_2 * h);

	for (int y = 0; y < height; ++y) {
		double j = (((double)(y)) / ((double)(height - 1)));
		double k = 1.0D - j;

		bmp.SetPixel(x, y, Color.FromArgb(
			((int)((r1 * k) + (r2 * j))),
			((int)((g1 * k) + (g2 * j))),
			((int)((b1 * k) + (b2 * j)))
		));
	}
}

bmp.Save("output.png", ImageFormat.Png);
