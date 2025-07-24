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

int i = 0;
for (int x = 0; x < width; ++x) {
	for (int y = 0; y < height; ++y) {
		bmp.SetPixel(x, y, Color.FromArgb(i >> 16, (i >> 8) & 0xFF, i & 0xFF));
		++i;
	}
}

bmp.Save("output.png", ImageFormat.Png);
