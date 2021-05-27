/****
 * Gradexor - 排他的論理和色彩変化画像
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

#include <string.h>

//================================
// THIRD PARTY NOTICE
// This program uses aclib. (http://essen.osask.jp/?aclib05)
// License: KL-01 (http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)
#define AARCH_X64
#include "../lib/acl.c"
//================================

#define IsEqual(a, b)	strcmp(a, b) == 0
#define SANKO			"sanko"
#define HELLO			"hello"
#define WAVES			"waves"
#define BOXES			"boxes"
#define LINES			"lines"
#define SCALE			"scale"
#define TYPES			"types"
#define TYPED			"typed"
#define TYPER			"typer"

void drawGradexor_sanko(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, ((x ^ y) | (x & y)) * mode);
		}
	}
}

void drawGradexor_hello(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, (x & mode) ^ (y | mode));
		}
	}
}

void drawGradexor_waves(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, (x ^ y) * mode);
		}
	}
}

void drawGradexor_boxes(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      ^ mode) & 0xFF,
				((x ^ y) ^ mode) & 0xFF,
				(     y  ^ mode) & 0xFF
			));
		}
	}
}

void drawGradexor_lines(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      + mode) & 0xFF,
				((x ^ y) + mode) & 0xFF,
				(     y  + mode) & 0xFF
			));
		}
	}
}

void drawGradexor_scale(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      * mode) & 0xFF,
				((x ^ y) * mode) & 0xFF,
				(     y  * mode) & 0xFF
			));
		}
	}
}

void drawGradexor_types(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	AInt16 col[4];
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			col[0] =  x      & 0xFF;
			col[1] =      y  & 0xFF;
			col[2] = (x ^ y) & 0xFF;
			col[3] = (x * y) & 0xFF;
			aSetPix0(w, x, y, aRgb8(
				col[((mode + 0) ^ (mode >> 2)) & 3],
				col[((mode + 1) ^ (mode >> 4)) & 3],
				col[((mode + 2) ^ (mode >> 6)) & 3]
			));
		}
	}
}

void drawGradexor_typed(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y, c;
	AInt16 col[4];
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			col[0] =  x      & 0xFF;
			col[1] = (x ^ y) & 0xFF;
			col[2] =      y  & 0xFF;
			col[3] = (x ^ y) & 0xFF;
			c = mode ^ (mode >> 1) ^ ((mode >> 3) & (mode >> 6));
			aSetPix0(w, x, y, aRgb8(
				col[(c + 0) & 3],
				col[(c + 1) & 3],
				col[(c + 2) & 3]
			));
		}
	}
}

void drawGradexor_typer(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	AInt16 col[3];
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			col[0] =  x      & 0xFF;
			col[1] = (x ^ y) & 0xFF;
			col[2] =      y  & 0xFF;
			switch (mode & 7)
			{
			case 0:
				aSetPix0(w, x, y, aRgb8(col[0], col[1], col[2]));
				break;
			case 1:
				aSetPix0(w, x, y, aRgb8(col[0], col[2], col[1]));
				break;
			case 2:
				aSetPix0(w, x, y, aRgb8(col[1], col[0], col[2]));
				break;
			case 3:
				aSetPix0(w, x, y, aRgb8(col[1], col[2], col[0]));
				break;
			case 4:
				aSetPix0(w, x, y, aRgb8(col[2], col[0], col[1]));
				break;
			case 5:
				aSetPix0(w, x, y, aRgb8(col[2], col[1], col[0]));
				break;
			case 6:
				aSetPix0(w, x, y, aRgb8(col[1], col[1], col[1]));
				break;
			case 7:
				aSetPix0(w, x, y, aRgb8((x & y) & 0xFF, (x * y) & 0xFF, (x | y) & 0xFF));
				break;
			}
		}
	}
}

void drawGradexor(AWindow *w, AInt16 width, AInt16 height, const char *mode)
{
	AInt16 i;
	while (1) {
		if (IsEqual(SANKO, mode)) {
			drawGradexor_sanko(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(HELLO, mode)) {
			drawGradexor_hello(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(WAVES, mode)) {
			drawGradexor_waves(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(BOXES, mode)) {
			drawGradexor_boxes(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(LINES, mode)) {
			drawGradexor_lines(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(SCALE, mode)) {
			drawGradexor_scale(w, width, height, i); // 推奨待機時間は75ミリ秒
			aWait(75);
		} else if (IsEqual(TYPES, mode)) {
			drawGradexor_types(w, width, height, i); // 推奨待機時間は1000ミリ秒
			aWait(1000);
		} else if (IsEqual(TYPED, mode)) {
			drawGradexor_typed(w, width, height, i); // 推奨待機時間は1000ミリ秒
			aWait(1000);
		} else /* if (IsEqual(TYPER, mode)) */ {
			// 未知のモードが指定された場合は TYPER として解釈する。
			drawGradexor_typer(w, width, height, i); // 推奨待機時間は1000ミリ秒
			aWait(1000);
		}
		++i;
	}
}

void aMain()
{
	const AInt16 width  = 1024;
	const AInt16 height = 1024;
	AWindow *w = aOpenWin(width, height, "Gradexor - 排他的論理和色彩変化画像", 1);
	if (aArgc <= 1) {
		// 既定のモードは SANKO
		drawGradexor(w, width, height, SANKO);
	} else {
		drawGradexor(w, width, height, aArgv[1]);
	}
	aWait(-1);
}
