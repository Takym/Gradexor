/****
 * Gradexor - 排他的論理和色彩変化画像
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

// これは C# スクリプトで実装した静的画像を生成する Gradexor です。
// C# インタラクティブで実行してください。
// .NET 5 以降を推奨します。

#r "System.Drawing.dll"
using System.Drawing;
using System.Drawing.Imaging;

const int width = 1024, height = 1024;
var bmp = new Bitmap(width, height);

for (int x = 0; x < width; ++x) {
	for (int y = 0; y < height; ++y) {
		bmp.SetPixel(x, y, Color.FromArgb(x % 256, (x ^ y) % 256, y % 256));
	}
}

bmp.Save("output.png", ImageFormat.Png);
