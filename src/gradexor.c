/****
 * Gradexor - 排他的論理和色彩変化画像
 * Copyright (C) 2020-2022 Takym.
 *
 * distributed under the MIT License.
****/

#include <stdio.h>
#include <string.h>

//================================
// THIRD PARTY NOTICE
// This program uses aclib. (http://essen.osask.jp/?aclib05)
// License: KL-01 (http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)
#define AARCH_X64
#include "../lib/acl.c"
//================================

typedef const char *GradexorString;

#define IsEqual(a, b)	(!strcmp((a), (b)))
#define SANKO			((GradexorString)("sanko"   ))
#define SANKO_V2		((GradexorString)("sanko_v2"))
#define HELLO			((GradexorString)("hello"   ))
#define WAVES			((GradexorString)("waves"   ))
#define BOXES			((GradexorString)("boxes"   ))
#define BOXES_V2		((GradexorString)("boxes_v2"))
#define BOXES_V3		((GradexorString)("boxes_v3"))
#define LINES			((GradexorString)("lines"   ))
#define SCALE			((GradexorString)("scale"   ))
#define TYPES			((GradexorString)("types"   ))
#define TYPED			((GradexorString)("typed"   ))
#define TYPER			((GradexorString)("typer"   ))

void drawGradexor_sanko(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, ((x ^ y) | (x & y)) * ticks);
		}
	}
}

void drawGradexor_sanko_v2(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			for (int i = 0; i < 16; ++i) {
				aSetPix0(w, x, y, ((x ^ y) | (x & y)) * ticks);
				++ticks;
			}
		}
	}
}

void drawGradexor_hello(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, (x & ticks) ^ (y | ticks));
		}
	}
}

void drawGradexor_waves(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, (x ^ y) * ticks);
		}
	}
}

void drawGradexor_boxes(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      ^ ticks) & 0xFF,
				((x ^ y) ^ ticks) & 0xFF,
				(     y  ^ ticks) & 0xFF
			));
		}
	}
}

void drawGradexor_boxes_v2(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      & ticks) & 0xFF,
				((x ^ y) & ticks) & 0xFF,
				(     y  & ticks) & 0xFF
			));
		}
	}
}

void drawGradexor_boxes_v3(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			aSetPix0(w, x, y, aRgb8(
				( x      | ticks) & 0xFF,
				((x ^ y) | ticks) & 0xFF,
				(     y  | ticks) & 0xFF
			));
		}
	}
}

void drawGradexor_lines(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			// 符号を変えても線が動く向きが変わるだけなのであまり面白くない。
			aSetPix0(w, x, y, aRgb8(
				( x      + ticks) & 0xFF,
				((x ^ y) + ticks) & 0xFF,
				(     y  + ticks) & 0xFF
			));
		}
	}
}

void drawGradexor_scale(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			// 除算にすると真っ黒になり、剰余にすると通常画面が延々と表示されるので、
			// 全く以って面白く無くなってしまう。乗算のままにすべし。
			aSetPix0(w, x, y, aRgb8(
				( x      * ticks) & 0xFF,
				((x ^ y) * ticks) & 0xFF,
				(     y  * ticks) & 0xFF
			));
		}
	}
}

void drawGradexor_types(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
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
				col[((ticks + 0) ^ (ticks >> 2)) & 3],
				col[((ticks + 1) ^ (ticks >> 4)) & 3],
				col[((ticks + 2) ^ (ticks >> 6)) & 3]
			));
		}
	}
}

void drawGradexor_typed(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y, c;
	AInt16 col[4];
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			col[0] =  x      & 0xFF;
			col[1] = (x ^ y) & 0xFF;
			col[2] =      y  & 0xFF;
			col[3] = (x ^ y) & 0xFF;
			c = ticks ^ (ticks >> 1) ^ ((ticks >> 3) & (ticks >> 6));
			aSetPix0(w, x, y, aRgb8(
				col[(c + 0) & 3],
				col[(c + 1) & 3],
				col[(c + 2) & 3]
			));
		}
	}
}

void drawGradexor_typer(AWindow *w, AInt16 width, AInt16 height, AInt16 ticks)
{
	AInt16 x, y;
	AInt16 col[3];
	for (x = 0; x < width; ++x) {
		for (y = 0; y < height; ++y) {
			col[0] =  x      & 0xFF;
			col[1] = (x ^ y) & 0xFF;
			col[2] =      y  & 0xFF;
			switch (ticks & 7) {
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

void drawGradexor(AWindow *w, AInt16 width, AInt16 height, GradexorString const mode)
{
	AInt16 i;
	while (1) {
		if (IsEqual(SANKO, mode)) {
			drawGradexor_sanko(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(SANKO_V2, mode)) {
			drawGradexor_sanko_v2(w, width, height, i); // 推奨待機時間は1ミリ秒
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
		} else if (IsEqual(BOXES_V2, mode)) {
			drawGradexor_boxes_v2(w, width, height, i); // 推奨待機時間は1ミリ秒
			aWait(1);
		} else if (IsEqual(BOXES_V3, mode)) {
			drawGradexor_boxes_v3(w, width, height, i); // 推奨待機時間は1ミリ秒
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
			// 未知のモードが指定された場合は TYPER として解釈する。（既定のモードとは異なる。）
			drawGradexor_typer(w, width, height, i); // 推奨待機時間は1000ミリ秒
			aWait(1000);
		}
		++i;
	}
}

#define then_wname return WorkNameTable
static GradexorString const WorkNameTable[] = {
	"不明な排他的論理和色彩変化画像",
	"Sanko「三湖」（バージョン１）",
	"Sanko「三湖」（バージョン２）",
	"Hello「こんにちは」",
	"Waves「波々」",
	"Boxes「箱々」（バージョン１）",
	"Boxes「箱々」（バージョン２）",
	"Boxes「箱々」（バージョン３）",
	"Lines「線々」",
	"Scale「スケール」",
	"TypeS/排他的論理和色彩変化画像",
	"TypeD/排他的論理和色彩変化画像",
	"TypeR/排他的論理和色彩変化画像"
};

const char *const GetWorkName(GradexorString const mode)
{
	     if IsEqual(SANKO,    mode) then_wname[ 1];
	else if IsEqual(SANKO_V2, mode) then_wname[ 2];
	else if IsEqual(HELLO,    mode) then_wname[ 3];
	else if IsEqual(WAVES,    mode) then_wname[ 4];
	else if IsEqual(BOXES,    mode) then_wname[ 5];
	else if IsEqual(BOXES_V2, mode) then_wname[ 6];
	else if IsEqual(BOXES_V3, mode) then_wname[ 7];
	else if IsEqual(LINES,    mode) then_wname[ 8];
	else if IsEqual(SCALE,    mode) then_wname[ 9];
	else if IsEqual(TYPES,    mode) then_wname[10];
	else if IsEqual(TYPED,    mode) then_wname[11];
	else if IsEqual(TYPER,    mode) then_wname[12];
	else                            then_wname[ 0];
}

void aMain()
{
	const AInt16 width  = 1024;
	const AInt16 height = 1024;

	GradexorString mode;
	if (aArgc <= 1) {
		mode = SANKO_V2; // 取り敢えずこれを既定のモードとする。
	} else {
		mode = aArgv[1];
	}

	char title[64];
	sprintf(title, "Gradexor - %s", GetWorkName(mode));

	AWindow *w = aOpenWin(width, height, title, 1);
	drawGradexor(w, width, height, mode);
	aWait(-1);
}
