/****
 * Gradexor - 排他的論理和色彩変化画像
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

/****
 * THIRD PARTY NOTICE
 * This program uses aclib. (http://essen.osask.jp/?aclib05)
 * License: KL-01 (http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)
****/

#define AARCH_X64
#include "acl.c"

void drawGradexor_sanko(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
			aSetPix0(w, x, y, ((x ^ y) | (x & y)) * mode);
		}
	}
}

void drawGradexor_hello(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
			aSetPix0(w, x, y, (x & mode) ^ (y | mode));
		}
	}
}

void drawGradexor_waves(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
			aSetPix0(w, x, y, (x ^ y) * mode);
		}
	}
}

void drawGradexor_boxes(AWindow *w, AInt16 width, AInt16 height, AInt16 mode)
{
	AInt16 x, y;
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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
	for (y = 0; y < width; y++) {
		for (x = 0; x < height; x++) {
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

void drawGradexor(AWindow *w, AInt16 width, AInt16 height)
{
	AInt16 i;
	while (1) {
#ifdef SANKO
		drawGradexor_sanko(w, width, height, i); // 推奨待機時間は1ミリ秒
		aWait(1);
#endif
#ifdef HELLO
		drawGradexor_hello(w, width, height, i); // 推奨待機時間は1ミリ秒
		aWait(1);
#endif
#ifdef WAVES
		drawGradexor_waves(w, width, height, i); // 推奨待機時間は1ミリ秒
		aWait(1);
#endif
#ifdef BOXES
		drawGradexor_boxes(w, width, height, i); // 推奨待機時間は1ミリ秒
		aWait(1);
#endif
#ifdef LINES
		drawGradexor_lines(w, width, height, i); // 推奨待機時間は1ミリ秒
		aWait(1);
#endif
#ifdef SCALE
		drawGradexor_scale(w, width, height, i); // 推奨待機時間は75ミリ秒
		aWait(75);
#endif
#ifdef TYPES
		drawGradexor_types(w, width, height, i); // 推奨待機時間は1000ミリ秒
		aWait(1000);
#endif
#ifdef TYPED
		drawGradexor_typed(w, width, height, i); // 推奨待機時間は1000ミリ秒
		aWait(1000);
#endif
#ifdef TYPER
		drawGradexor_typer(w, width, height, i); // 推奨待機時間は1000ミリ秒
		aWait(1000);
#endif
		++i;
	}
}

void aMain()
{
	const AInt16 width  = 1024;
	const AInt16 height = 1024;
	AWindow *w = aOpenWin(width, height, "Gradexor - 排他的論理和色彩変化画像", 1);
	drawGradexor(w, width, height);
	aWait(-1);
}
