#if (!defined(ADEBUG))

AINLINESTATIC void aSetPix0(AWindow *w, AInt16 x, AInt16 y, AInt32 c) { w->buf[x + y * w->xsiz] = c; }
AINLINESTATIC AInt32 aRgb8(AInt32 r, AInt32 g, AInt32 b) { return r << 16 | g << 8 | b; }
AINLINESTATIC void aSetMode(AWindow *w, int m) { w->mode = m; }

#endif

ASTATIC void aWait(AInt32 ms)
{
	aFlushAll(0);
	aWait0(ms);
}

ASTATIC void aSetPix(AWindow *w, AInt32 x, AInt32 y, AInt32 c)
{
	if (c >= 0 && 0 <= x && x < w->xsiz && 0 <= y && y < w->ysiz) {
		if (w->mode == AMODE_SET) w->buf[y * w->xsiz + x] =  c;
		if (w->mode == AMODE_OR)  w->buf[y * w->xsiz + x] |= c;
		if (w->mode == AMODE_AND) w->buf[y * w->xsiz + x] &= c;
		if (w->mode == AMODE_XOR) w->buf[y * w->xsiz + x] ^= c;
	}
}

ASTATIC void aFillRect0(AWindow *w, AInt16 sx, AInt16 sy, AInt16 x0, AInt16 y0, AInt32 c)
{
	int i, j;
	AInt32a *b = w->buf + x0 + y0 * w->xsiz;
	for (j = 0; j < sy; j++) {
		for (i = 0; i < sx; i++)
			b[i] = c;
		b += w->xsiz;
	}
}

ASTATIC void aFillRect(AWindow *w, AInt32 sx, AInt32 sy, AInt32 x0, AInt32 y0, AInt32 c)
{
	AInt32 i, j;
	if (w->mode == AMODE_SET && x0 >= 0 && y0 >= 0 && x0 + sx < w->xsiz && y0 + sy < w->ysiz) {
		aFillRect0(w, sx, sy, x0, y0, c);
		return;
	}
	for (j = 0; j < sy; j++) {
		for (i = 0; i < sx; i++) {
			aSetPix(w, x0 + i, y0 + j, c);
		}
	}
}

ASTATIC void aDrawRect0(AWindow *w, AInt16 sx, AInt16 sy, AInt16 x0, AInt16 y0, AInt32 c)
{
	if (sx <= 1 || sy <= 1)
		aFillRect0(w, sx, sy, x0, y0, c);
	else {
		aFillRect0(w, sx, 1,      x0,          y0,          c);
		aFillRect0(w, sx, 1,      x0,          y0 + sy - 1, c);
		aFillRect0(w, 1,  sy - 2, x0,          y0 + 1,      c);
		aFillRect0(w, 1,  sy - 2, x0 + sx - 1, y0 + 1,      c);
	}
}

ASTATIC void aDrawRect(AWindow *w, AInt32 sx, AInt32 sy, AInt32 x0, AInt32 y0, AInt32 c)
{
	if (sx <= 1 || sy <= 1)
		aFillRect(w, sx, sy, x0, y0, c);
	else {
		aFillRect(w, sx, 1,      x0,          y0,          c);
		aFillRect(w, sx, 1,      x0,          y0 + sy - 1, c);
		aFillRect(w, 1,  sy - 2, x0,          y0 + 1,      c);
		aFillRect(w, 1,  sy - 2, x0 + sx - 1, y0 + 1,      c);
	}
}

ASTATIC void aDrawLine0(AWindow *w, AInt16 x0, AInt16 y0, AInt16 x1, AInt16 y1, AInt32 c)
{
	AInt16 x, y, dx, dy, sx, sy, err, e2;
	dx = x1 - x0;
	dy = y1 - y0;
	sx = sy = 1;
	if (dx < 0) { dx *= -1; sx = -1; }
	if (dy < 0) { dy *= -1; sy = -1; }
	err = dx - dy;
	x = x0;
	y = y0;
	for (;;) {
		aSetPix0(w, x, y, c);
		if (x == x1 && y == y1) break;
		e2 = err * 2;
		if (e2 > - dy) {
			err -= dy;
			x += sx;
		}
		if (e2 <   dx) {
			err += dx;
			y += sy;
		}
	}
}

ASTATIC void aDrawLine(AWindow *w, AInt32 x0, AInt32 y0, AInt32 x1, AInt32 y1, AInt32 c)
{
	AInt32 x, y, dx, dy, sx, sy, err, e2;
	dx = x1 - x0;
	dy = y1 - y0;
	sx = sy = 1;
	if (dx < 0) { dx *= -1; sx = -1; }
	if (dy < 0) { dy *= -1; sy = -1; }
	err = dx - dy;
	x = x0;
	y = y0;
	for (;;) {
		aSetPix(w, x, y, c);
		if (x == x1 && y == y1) break;
		e2 = err * 2;
		if (e2 > - dy) {
			err -= dy;
			x += sx;
		}
		if (e2 <   dx) {
			err += dx;
			y += sy;
		}
	}
}

ASTATIC void aFillOval0(AWindow *w, AInt16 sx, AInt16 sy, AInt16 x0, AInt16 y0, AInt16 c)
{
	double dcx, dcy, dcxy, dtx, dty;
	AInt16 x, y;
	dcx = 0.5 * (sx - 1);
	dcy = 0.5 * (sy - 1);
	dcxy = (dcx + 0.5) * (dcy + 0.5) - 0.1;
	dcxy *= dcxy;
	for (y = 0; y < sy; y++) {
		dty = (y - dcy) * dcx;
		for (x = 0; x < sx; x++) {
			dtx = (x - dcx) * dcy;
			if (dtx * dtx + dty * dty > dcxy) continue;
				aSetPix0(w, x + x0, y + y0, c);
		}
	}
}

ASTATIC void aFillOval(AWindow *w, AInt32 sx, AInt32 sy, AInt32 x0, AInt32 y0, AInt32 c)
{
	double dcx, dcy, dcxy, dtx, dty;
	AInt32 x, y;
	dcx = 0.5 * (sx - 1);
	dcy = 0.5 * (sy - 1);
	dcxy = (dcx + 0.5) * (dcy + 0.5) - 0.1;
	dcxy *= dcxy;
	for (y = 0; y < sy; y++) {
		dty = (y - dcy) * dcx;
		for (x = 0; x < sx; x++) {
			dtx = (x - dcx) * dcy;
			if (dtx * dtx + dty * dty > dcxy) continue;
				aSetPix(w, x + x0, y + y0, c);
		}
	}
}

#define DRAWOVALPARAM	1

ASTATIC void aDrawOval(AWindow *w, AInt32 sx, AInt32 sy, AInt32 x0, AInt32 y0, AInt32 c)
{
	double dcx, dcy, dcxy, dtx, dty, dcx1, dcy1, dcxy1, dtx1, dty1;
	int x, y;
	dcx = 0.5 * (sx - 1);
	dcy = 0.5 * (sy - 1);
	dcxy = (dcx + 0.5) * (dcy + 0.5) - 0.1;
	dcxy *= dcxy;
	dcx1 = 0.5 * (sx - (1 + DRAWOVALPARAM * 2));
	dcy1 = 0.5 * (sy - (1 + DRAWOVALPARAM * 2));
	dcxy1 = (dcx1 + 0.5) * (dcy1 + 0.5) - 0.1;
	dcxy1 *= dcxy1;
	for (y = 0; y < sy; y++) {
		dty  = (y - dcy) * dcx;
		dty1 = (y - dcy) * dcx1;
		for (x = 0; x < sx; x++) {
			dtx = (x - dcx) * dcy;
			dtx1 = (x - dcx) * dcy1;
			if (dtx * dtx + dty * dty > dcxy) continue;
			if (DRAWOVALPARAM <= x && x < sx - DRAWOVALPARAM && DRAWOVALPARAM <= y && y < sy - DRAWOVALPARAM) {
				if (dtx1 * dtx1 + dty1 * dty1 < dcxy1) continue;
			}
			aSetPix(w, x + x0, y + y0, c);
		}
	}
}

#if 0

typedef struct APoint_ {
	int x, y;
} APoint;

ASTATIC void aFill(AWindow *w, AInt32 x, AInt32 y, AInt32 c)
{
	int b, sp, sx = w->xsiz, sy = w->ysiz, u, v, *q = w->buf;
	APoint *p;
	if (x < 0 || x >= sx || y < 0 || y >= sy || c < 0) return;
	b = q[x + y * sx];
	if (b == c) return;
	p = aMalloc(sx * sy * aSizeof (APoint));
	sp = 0; p[sp].x = x; p[sp].y = y; q[x + y * sx] = c;
	do {
		x = p[sp].x;
		y = p[sp].y;
		sp--;
		u = x;     v = y + 1; if (v < sy && q[u + v * sx] == b) { sp++; p[sp].x = u; p[sp].y = v; q[u + v * sx] = c; }
		u = x;     v = y - 1; if (v >= 0 && q[u + v * sx] == b) { sp++; p[sp].x = u; p[sp].y = v; q[u + v * sx] = c; }
		u = x + 1; v = y;     if (u < sx && q[u + v * sx] == b) { sp++; p[sp].x = u; p[sp].y = v; q[u + v * sx] = c; }
		u = x - 1; v = y;     if (u >= 0 && q[u + v * sx] == b) { sp++; p[sp].x = u; p[sp].y = v; q[u + v * sx] = c; }
	} while (sp >= 0);
	aFree(p, sx * sy * aSizeof (APoint));
}

#endif

#if (AKEYBUFSIZ > 0)

ASTATIC void aPutKeybuf(AWindow *w, AInt32 c)
{
	if (((w->kbw + 1) & (AKEYBUFSIZ - 1)) != w->kbr) {
		w->keybuf[w->kbw] = c;
		w->kbw = (w->kbw + 1) & (AKEYBUFSIZ - 1);
	}
}

ASTATIC AInt aGetSpcKeybuf(AWindow *w)
{
	if (w->kbr <= w->kbw)
		return AKEYBUFSIZ - (w->kbw - w->kbr);
	return w->kbr - w->kbw - 1;
}

AINLINESTATIC AInt32 aInkeySub1(AWindow *w, AInt i) { return w->keybuf[(w->kbr + i) & (AKEYBUFSIZ - 1)]; }

ASTATIC AInt aInkeySub0(AWindow *w)
{
	AInt32 i, j;
	if (w->kbr == w->kbw) return 0;
	i = w->keybuf[w->kbr];
	w->inkeyPrm[0] = i;
	if ((i & 0xc000) == 0) return 1;
	if ((i & 0xc000) == 0x4000) {
		j = w->keybuf[(w->kbr + 1) & (AKEYBUFSIZ - 1)];
		w->inkeyPrm[1] = j & 0xffff;
		if ((w->inkeyPrm[1] & 0x8000) != 0) {
			w->inkeyPrm[1] -= 0x10000;
			if (w->inkeyPrm[1] == -0x8000)
				w->inkeyPrm[1] = (-1) << 31;
		}
		w->inkeyPrm[2] = (j >> 16) & 0xffff;
		if ((w->inkeyPrm[2] & 0x8000) != 0)
			w->inkeyPrm[2] -= 0x10000;
		return 2;
	}
	aDbgErrExit("aInkeySub0: internal error: i=0x%x", i);
	return 0; // dummy
}

ASTATIC AInt32 aInkey(AWindow *w, AInt flg)
{
	aWait0(0);
	AInt i = aInkeySub0(w);
	if (i == 0) return 0;
	if ((flg & 1) != 0)
		w->kbr = (w->kbr + i) & (AKEYBUFSIZ - 1);
	i = w->inkeyPrm[0];
	if ((flg & 2) == 0) i &= 0xffff;
	return i;
}

#endif

#if (!defined(ANOUSE_DRAWSTR))

ASTATIC AUInt8a aFontData[] = {
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x00, 0x00, 0x10, 0x10, 0x00, 0x00,
	0x28, 0x28, 0x28, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x44, 0x44, 0x44, 0xfe, 0x44, 0x44, 0x44, 0x44, 0x44, 0xfe, 0x44, 0x44, 0x44, 0x00, 0x00,
	0x10, 0x3a, 0x56, 0x92, 0x92, 0x90, 0x50, 0x38, 0x14, 0x12, 0x92, 0x92, 0xd4, 0xb8, 0x10, 0x10,
	0x62, 0x92, 0x94, 0x94, 0x68, 0x08, 0x10, 0x10, 0x20, 0x2c, 0x52, 0x52, 0x92, 0x8c, 0x00, 0x00,
	0x00, 0x70, 0x88, 0x88, 0x88, 0x90, 0x60, 0x47, 0xa2, 0x92, 0x8a, 0x84, 0x46, 0x39, 0x00, 0x00,
	0x04, 0x08, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x02, 0x04, 0x08, 0x08, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x08, 0x08, 0x04, 0x02, 0x00,
	0x80, 0x40, 0x20, 0x20, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x20, 0x20, 0x40, 0x80, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x92, 0x54, 0x38, 0x54, 0x92, 0x10, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x10, 0x10, 0xfe, 0x10, 0x10, 0x10, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x08, 0x08, 0x10,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xfe, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00,
	0x02, 0x02, 0x04, 0x04, 0x08, 0x08, 0x08, 0x10, 0x10, 0x20, 0x20, 0x40, 0x40, 0x40, 0x80, 0x80,
	0x00, 0x18, 0x24, 0x24, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x24, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x08, 0x18, 0x28, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x3e, 0x00, 0x00,
	0x00, 0x18, 0x24, 0x42, 0x42, 0x02, 0x04, 0x08, 0x10, 0x20, 0x20, 0x40, 0x40, 0x7e, 0x00, 0x00,
	0x00, 0x18, 0x24, 0x42, 0x02, 0x02, 0x04, 0x18, 0x04, 0x02, 0x02, 0x42, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x0c, 0x0c, 0x0c, 0x14, 0x14, 0x14, 0x24, 0x24, 0x44, 0x7e, 0x04, 0x04, 0x1e, 0x00, 0x00,
	0x00, 0x7c, 0x40, 0x40, 0x40, 0x58, 0x64, 0x02, 0x02, 0x02, 0x02, 0x42, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x18, 0x24, 0x42, 0x40, 0x58, 0x64, 0x42, 0x42, 0x42, 0x42, 0x42, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x7e, 0x42, 0x42, 0x04, 0x04, 0x08, 0x08, 0x08, 0x10, 0x10, 0x10, 0x10, 0x38, 0x00, 0x00,
	0x00, 0x18, 0x24, 0x42, 0x42, 0x42, 0x24, 0x18, 0x24, 0x42, 0x42, 0x42, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x18, 0x24, 0x42, 0x42, 0x42, 0x42, 0x42, 0x26, 0x1a, 0x02, 0x42, 0x24, 0x18, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x00, 0x00, 0x18, 0x18, 0x08, 0x08, 0x10,
	0x00, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xfe, 0x00, 0x00, 0xfe, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x00,
	0x00, 0x38, 0x44, 0x82, 0x82, 0x82, 0x04, 0x08, 0x10, 0x10, 0x00, 0x00, 0x18, 0x18, 0x00, 0x00,
	0x00, 0x38, 0x44, 0x82, 0x9a, 0xaa, 0xaa, 0xaa, 0xaa, 0xaa, 0x9c, 0x80, 0x46, 0x38, 0x00, 0x00,
	0x00, 0x18, 0x18, 0x18, 0x18, 0x24, 0x24, 0x24, 0x24, 0x7e, 0x42, 0x42, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0xf0, 0x48, 0x44, 0x44, 0x44, 0x48, 0x78, 0x44, 0x42, 0x42, 0x42, 0x44, 0xf8, 0x00, 0x00,
	0x00, 0x3a, 0x46, 0x42, 0x82, 0x80, 0x80, 0x80, 0x80, 0x80, 0x82, 0x42, 0x44, 0x38, 0x00, 0x00,
	0x00, 0xf8, 0x44, 0x44, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x44, 0x44, 0xf8, 0x00, 0x00,
	0x00, 0xfe, 0x42, 0x42, 0x40, 0x40, 0x44, 0x7c, 0x44, 0x40, 0x40, 0x42, 0x42, 0xfe, 0x00, 0x00,
	0x00, 0xfe, 0x42, 0x42, 0x40, 0x40, 0x44, 0x7c, 0x44, 0x44, 0x40, 0x40, 0x40, 0xf0, 0x00, 0x00,
	0x00, 0x3a, 0x46, 0x42, 0x82, 0x80, 0x80, 0x9e, 0x82, 0x82, 0x82, 0x42, 0x46, 0x38, 0x00, 0x00,
	0x00, 0xe7, 0x42, 0x42, 0x42, 0x42, 0x42, 0x7e, 0x42, 0x42, 0x42, 0x42, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0x7c, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x7c, 0x00, 0x00,
	0x00, 0x1f, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x84, 0x48, 0x30, 0x00,
	0x00, 0xe7, 0x42, 0x44, 0x48, 0x50, 0x50, 0x60, 0x50, 0x50, 0x48, 0x44, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0xf0, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x42, 0x42, 0xfe, 0x00, 0x00,
	0x00, 0xc3, 0x42, 0x66, 0x66, 0x66, 0x5a, 0x5a, 0x5a, 0x42, 0x42, 0x42, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0xc7, 0x42, 0x62, 0x62, 0x52, 0x52, 0x52, 0x4a, 0x4a, 0x4a, 0x46, 0x46, 0xe2, 0x00, 0x00,
	0x00, 0x38, 0x44, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x44, 0x38, 0x00, 0x00,
	0x00, 0xf8, 0x44, 0x42, 0x42, 0x42, 0x44, 0x78, 0x40, 0x40, 0x40, 0x40, 0x40, 0xf0, 0x00, 0x00,
	0x00, 0x38, 0x44, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x82, 0x92, 0x8a, 0x44, 0x3a, 0x00, 0x00,
	0x00, 0xfc, 0x42, 0x42, 0x42, 0x42, 0x7c, 0x44, 0x42, 0x42, 0x42, 0x42, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0x3a, 0x46, 0x82, 0x82, 0x80, 0x40, 0x38, 0x04, 0x02, 0x82, 0x82, 0xc4, 0xb8, 0x00, 0x00,
	0x00, 0xfe, 0x92, 0x92, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x7c, 0x00, 0x00,
	0x00, 0xe7, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x24, 0x3c, 0x00, 0x00,
	0x00, 0xe7, 0x42, 0x42, 0x42, 0x42, 0x24, 0x24, 0x24, 0x24, 0x18, 0x18, 0x18, 0x18, 0x00, 0x00,
	0x00, 0xe7, 0x42, 0x42, 0x42, 0x5a, 0x5a, 0x5a, 0x5a, 0x24, 0x24, 0x24, 0x24, 0x24, 0x00, 0x00,
	0x00, 0xe7, 0x42, 0x42, 0x24, 0x24, 0x24, 0x18, 0x24, 0x24, 0x24, 0x42, 0x42, 0xe7, 0x00, 0x00,
	0x00, 0xee, 0x44, 0x44, 0x44, 0x28, 0x28, 0x28, 0x10, 0x10, 0x10, 0x10, 0x10, 0x7c, 0x00, 0x00,
	0x00, 0xfe, 0x84, 0x84, 0x08, 0x08, 0x10, 0x10, 0x20, 0x20, 0x40, 0x42, 0x82, 0xfe, 0x00, 0x00,
	0x00, 0x3e, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x3e, 0x00,
	0x80, 0x80, 0x40, 0x40, 0x20, 0x20, 0x20, 0x10, 0x10, 0x08, 0x08, 0x04, 0x04, 0x04, 0x02, 0x02,
	0x00, 0x7c, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x7c, 0x00,
	0x00, 0x10, 0x28, 0x44, 0x82, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xfe, 0x00,
	0x10, 0x08, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x70, 0x08, 0x04, 0x3c, 0x44, 0x84, 0x84, 0x8c, 0x76, 0x00, 0x00,
	0xc0, 0x40, 0x40, 0x40, 0x40, 0x58, 0x64, 0x42, 0x42, 0x42, 0x42, 0x42, 0x64, 0x58, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x4c, 0x84, 0x84, 0x80, 0x80, 0x82, 0x44, 0x38, 0x00, 0x00,
	0x0c, 0x04, 0x04, 0x04, 0x04, 0x34, 0x4c, 0x84, 0x84, 0x84, 0x84, 0x84, 0x4c, 0x36, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x38, 0x44, 0x82, 0x82, 0xfc, 0x80, 0x82, 0x42, 0x3c, 0x00, 0x00,
	0x0e, 0x10, 0x10, 0x10, 0x10, 0x7c, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x7c, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x36, 0x4c, 0x84, 0x84, 0x84, 0x84, 0x4c, 0x34, 0x04, 0x04, 0x78,
	0xc0, 0x40, 0x40, 0x40, 0x40, 0x58, 0x64, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0xe3, 0x00, 0x00,
	0x00, 0x10, 0x10, 0x00, 0x00, 0x30, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x38, 0x00, 0x00,
	0x00, 0x04, 0x04, 0x00, 0x00, 0x0c, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x04, 0x08, 0x08, 0x30,
	0xc0, 0x40, 0x40, 0x40, 0x40, 0x4e, 0x44, 0x48, 0x50, 0x60, 0x50, 0x48, 0x44, 0xe6, 0x00, 0x00,
	0x30, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x38, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xf6, 0x49, 0x49, 0x49, 0x49, 0x49, 0x49, 0x49, 0xdb, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xd8, 0x64, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0xe3, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x38, 0x44, 0x82, 0x82, 0x82, 0x82, 0x82, 0x44, 0x38, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xd8, 0x64, 0x42, 0x42, 0x42, 0x42, 0x42, 0x64, 0x58, 0x40, 0xe0,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x34, 0x4c, 0x84, 0x84, 0x84, 0x84, 0x84, 0x4c, 0x34, 0x04, 0x0e,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xdc, 0x62, 0x42, 0x40, 0x40, 0x40, 0x40, 0x40, 0xe0, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0x7a, 0x86, 0x82, 0xc0, 0x38, 0x06, 0x82, 0xc2, 0xbc, 0x00, 0x00,
	0x00, 0x00, 0x10, 0x10, 0x10, 0x7c, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x0e, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xc6, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42, 0x46, 0x3b, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xe7, 0x42, 0x42, 0x42, 0x24, 0x24, 0x24, 0x18, 0x18, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xe7, 0x42, 0x42, 0x5a, 0x5a, 0x5a, 0x24, 0x24, 0x24, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xc6, 0x44, 0x28, 0x28, 0x10, 0x28, 0x28, 0x44, 0xc6, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xe7, 0x42, 0x42, 0x24, 0x24, 0x24, 0x18, 0x18, 0x10, 0x10, 0x60,
	0x00, 0x00, 0x00, 0x00, 0x00, 0xfe, 0x82, 0x84, 0x08, 0x10, 0x20, 0x42, 0x82, 0xfe, 0x00, 0x00,
	0x00, 0x06, 0x08, 0x10, 0x10, 0x10, 0x10, 0x60, 0x10, 0x10, 0x10, 0x10, 0x08, 0x06, 0x00, 0x00,
	0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10,
	0x00, 0x60, 0x10, 0x08, 0x08, 0x08, 0x08, 0x06, 0x08, 0x08, 0x08, 0x08, 0x10, 0x60, 0x00, 0x00,
	0x00, 0x72, 0x8c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
	0x00, 0x00, 0x00, 0x00, 0x10, 0x28, 0x44, 0x82, 0xfe, 0x82, 0xfe, 0x00, 0x00, 0x00, 0x00, 0x00
};

ASTATIC void aDrawStr0(AWindow *w, AInt16 x, AInt16 y, AInt32 c, AInt32 b, const char *s)
{
	AInt i, j, ch;
	AInt16 k, pt;
	for (i = 0; s[i] != '\0'; i++) {
		ch = s[i];
		if (ch < ' ' || ch > 0x7f) ch = ' ';
		ch = (ch - ' ') * 16;
		for (j = 0; j < 16; j++) {
			pt = aFontData[ch + j];
			for (k = 0; k < 8; k++) {
				if ((pt & 0x80) != 0) {
					aSetPix0(w, x + k, y + j, c);
				} else {
					aSetPix0(w, x + k, y + j, b);
				}
				pt <<= 1;
			}
		}
		x += 8;
	}
}

ASTATIC void aDrawStr(AWindow *w, AInt32 x, AInt32 y, AInt32 c, AInt32 b, const char *s)
{
	AInt i, j, ch;
	AInt16 k, pt;
	for (i = 0; s[i] != '\0'; i++) {
		ch = s[i];
		if (ch < ' ' || ch > 0x7f) ch = ' ';
		ch = (ch - ' ') * 16;
		for (j = 0; j < 16; j++) {
			pt = aFontData[ch + j];
			for (k = 0; k < 8; k++) {
				if ((pt & 0x80) != 0) {
					if (c >= 0)
						aSetPix(w, x + k, y + j, c);
				} else {
					if (b >= 0)
						aSetPix(w, x + k, y + j, b);
				}
				pt <<= 1;
			}
		}
		x += 8;
	}
}

#endif

#if (AKEYBUFSIZ > 0)

AINLINESTATIC void aClrKeybuf(AWindow *w) { w->kbw = w->kbr = 0; }

ASTATIC void aSetInkey(AWindow *w, AInt8 mod)
{
	w->keyLv =  mod       & 0xf;
	w->mosLv = (mod >> 4) & 0xf;
	aClrKeybuf(w);
}

ASTATIC AInt32 aInkeyWait(AWindow *w, AInt flg)
{
	if (w->kbr != w->kbw)
		return aInkey(w, flg);
	aFlushAll(0);
	while (w->kbr == w->kbw)
		aWait0(32);
	return aInkey(w, flg);
}

#endif
