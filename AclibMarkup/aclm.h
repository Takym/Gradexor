/****
 * AclibMarkup
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

#pragma once
#ifndef ACLIB_MARKUP
#define ACLIB_MARKUP

// 標準ライブラリ
#include <assert.h>
#include <complex.h>
#include <ctype.h>
#include <errno.h>
#include <fenv.h>
#include <float.h>
#include <inttypes.h>
#include <iso646.h>
#include <limits.h>
#include <locale.h>
#include <math.h>
#include <setjmp.h>
#include <signal.h>
#include <stdarg.h>
#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <tgmath.h>
#include <time.h>
#include <uchar.h>
#include <wchar.h>
#include <wctype.h>

//================================
// THIRD PARTY NOTICE
// This program uses aclib. (http://essen.osask.jp/?aclib05)
// License: KL-01 (http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)
#define AARCH_X64
#include "../lib/acl.c"
//================================

#define ACLIB_MARKUP_CONFIGURE(width, height, title)  \
	const AInt16  Width  = width;                     \
	const AInt16  Height = height;                    \
	const char   *Title  = title;                     \
	AWindow      *Window = NULL;                      \
	AInt32        Pen    = 0x00000000;                \
	AInt32        Brush  = 0x00FFFFFF;

#define ACLIB_MARKUP_BEGIN \
	void aMain()           \
	{                      \
		Window = aOpenWin(Width, Height, Title, 1);

#define ACLIB_MARKUP_BEGIN_WITH_DEFAULT                      \
	ACLIB_MARKUP_CONFIGURE(512, 384, "Untitled Application") \
	ACLIB_MARKUP_BEGIN

#define ACLIB_MARKUP_END aWait(-1); }

#define SetPixel(x, y, r, g, b)		aSetPix0(Window, x, y, aRgb8(r, g, b));
#define SetPixelRaw(x, y, c)		aSetPix0(Window, x, y, c);

#define Loop	{ AInt16 x, y; for (x = 0; x < Width; ++x) { for (y = 0; y < Height; ++y) {
#define Next	} } }

#define Animate(interval, process)	{ AInt16 i = 0; while (1) { process; aWait(interval); ++i; } }

#define Gradexor                    \
	Loop                            \
		SetPixel(x, y, x, x ^ y, y) \
	Next                            \

#define GradexorSanko(i)                           \
	Loop                                           \
		SetPixelRaw(x, y, ((x ^ y) | (x & y)) * i) \
	Next                                           \

#define SetPen(r, g, b)		Pen   = aRgb8(r, g, b);
#define SetBrush(r, g, b)	Brush = aRgb8(r, g, b);

#define Line(beginX, beginY, endX, endY) \
	aDrawLine(Window, beginX, beginY, endX, endY, Pen);

#define Rectangle(x, y, width, height)                  \
	aFillRect(Window, width, height, x, y, Brush); \
	aDrawRect(Window, width, height, x, y, Pen);

#define Ellipse(x, y, width, height)                  \
	aFillOval(Window, width, height, x, y, Brush); \
	aDrawOval(Window, width, height, x, y, Pen);

#define Text(x, y, s) \
	aDrawStr(Window, x, y, Pen, Brush, s);

#endif // ACLIB_MARKUP
