#define AMAXWINDOWS		1
#define ANOUSE_FASTMALLOC
#define ANOUSE_ATERRMSG
#define ANOUSE_LEAPFLUSH
#define ANOUSE_KEY
#include <acl.c>

void setB(AWindow *w, unsigned char *b, int x, int y, int c)
{
	if (b != 0)
		b[y * 8 + x] = c;
	aFillRect(w, 44, 44, 26 + x * 48, 26 + y * 48, 0x009f00);
	if (c == 1) {
		aFillOval(w, 40, 40, 29 + x * 48, 29 + y * 48, 0xffffff);
		aFillOval(w, 40, 40, 27 + x * 48, 27 + y * 48, 0x000000);
	}
	if (c == 2) {
		aFillOval(w, 40, 40, 29 + x * 48, 29 + y * 48, 0x000000);
		aFillOval(w, 40, 40, 27 + x * 48, 27 + y * 48, 0xffffff);
	}
}

int testBsub(AWindow *w, unsigned char *b, int x, int y, int c, int f, int u, int v)
{
	int s = 0;
	for (;;) {
		x += u;
		y += v;
		if (x < 0 || 7 < x) return 0;
		if (y < 0 || 7 < y) return 0;
		if (b[x + y * 8] == c) return s;
		if (b[x + y * 8] == 0) return 0;
		if (f != 0) {
			setB(w, b, x, y, c);
			aWait(500);
		}
		s++;
	}
}

int testB(AWindow *w, unsigned char *b, int x, int y, int c, int f)
{
	int t, s = 0, u, v;
	if (f == 0 && b[x + y * 8] != 0) return 0;
	for (v = -1; v <= +1; v++) {
		for (u = -1; u <= +1; u++) {
			if (u == 0 && v == 0) continue;
			s += t = testBsub(w, b, x, y, c, 0, u, v);
			if (f != 0 && t > 0)
				testBsub(w, b, x, y, c, 1, u, v);
		}
	}
	return s;
}

void waitClick(AWindow *w, int *x, int *y, int f)
{
	int i;
	aFlushAll0(w);
	for (;;) {
		i = aInkey(w, 1);
		if (i == 0) {
			if (f != 0) {
				rand();
				aWait0(8);
			} else
				aWait0(64);
			continue;
		}
		if (i == 0x4000) {
			*x = w->inkeyPrm[1];
			*y = w->inkeyPrm[2];
			return;
		}
	}
}

void ai(int c, unsigned char *b, unsigned char *e, int k, int *px, int *py);

void aMain()
{
	AWindow *w = aOpenWin(640, 432, "lv1", 1);
	aSetInkey(w, AKEY_LV0 | AMOS_LV1);
	int c, i, j, k, x, y, ps;
	char mod = 0;
	unsigned char b[64], e[60];
	char s[32];
	aFillRect(w, 432, 432, 0, 0, 0x009f00);
	for (i = 0; i < 9; i++) {
		aFillRect(w, 386, 2, 23, 23 + i * 48, 0x000000);
		aFillRect(w, 2, 386, 23 + i * 48, 23, 0x000000);
	}
restart:
	for (y = 0; y < 8; y++) {
		for (x = 0; x < 8; x++)
			setB(w, b, x, y, 0);
		sprintf(s, "%c", 'A' + y);
		aDrawStr(w, 44 + 48 * y, 4, 0x000000, -1, s);
		sprintf(s, "%c", '1' + y);
		aDrawStr(w, 8, 40 + 48 * y, 0x000000, -1, s);
	}
	setB(w, b, 3, 3, 2);
	setB(w, b, 3, 4, 1);
	setB(w, b, 4, 3, 1);
	setB(w, b, 4, 4, 2);
	aFillRect(w, 208, 432, 432,   0, 0x000000);
	aFillRect(w, 176, 104, 448, 116, 0x003f3f);
	setB(w, 0, 9, 2, 1); aDrawStr(w, 512, 136, 0xffffff, -1, "human");
	setB(w, 0, 9, 3, 2); aDrawStr(w, 512, 184, 0xffffff, -1, "computer");
	aFillRect(w, 176, 104, 448, 260, 0x003f3f);
	setB(w, 0, 9, 5, 1); aDrawStr(w, 512, 280, 0xffffff, -1, "computer");
	setB(w, 0, 9, 6, 2); aDrawStr(w, 512, 328, 0xffffff, -1, "human");
	for (mod = 2; mod == 2; ) {
		waitClick(w, &x, &y, 1);
		if (448 <= x && x < 448 + 176) {
			if (116 <= y && y < 116 + 104)
				mod = 0;
			if (260 <= y && y < 260 + 104)
				mod = 1;
		}
	}
	aFillRect(w, 208, 432, 432,   0, 0x000000);
	setB(w, 0, 9, 2, 1);
	setB(w, 0, 9, 4, 2);
	if (mod == 0) {
		aDrawStr(w, 512, 136, 0xffffff, 0x000000, "human");
		aDrawStr(w, 512, 232, 0xffffff, 0x000000, "computer");
	} else {
		aDrawStr(w, 512, 136, 0xffffff, 0x000000, "computer");
		aDrawStr(w, 512, 232, 0xffffff, 0x000000, "human");
	}
	setB(w, 0,  9, 7, 1);
	setB(w, 0, 11, 7, 2);
	c = 1;
	ps = 0;
	for (;;) {
		i = j = 0;
		for (y = 0; y < 8; y++) {
			for (x = 0; x < 8; x++) {
				if (b[x + y * 8] == 1) i++;
				if (b[x + y * 8] == 2) j++;
			}
		}
		sprintf(s, "%02d-%02d", i, j);
		aDrawStr(w, 508, 376, 0xffffff, 0x000000, s);
		if (i + j >= 64) break;
		if (i == 0 || j == 0) break;

		k = 0;
		for (y = 0; y < 8; y++) {
			for (x = 0; x < 8; x++) {
				if (testB(w, b, x, y, c, 0) != 0) {
					e[k] = x + y * 8;
					k++;
				}
			}
		}
		aDrawRect(w, 184, 56, 448, 116 + (c - 1) * 96, 0xffff00);
		aDrawStr(w, 592, 136 + (c - 1) * 96, 0x00ffff, 0x000000, "    ");
		if (k == 0) {
			aDrawStr(w, 592, 136 + (c - 1) * 96, 0x00ffff, 0x000000, "pass");
			ps++;
		} else
			ps = 0;
		if ((mod == 0 && c == 1) || (mod == 1 && c == 2)) {
			for (;;) {
				waitClick(w, &x, &y, 0);
				if (k == 0) {
					if (592 <= x && x < 592 + 32 && 136 + (c - 1) * 96 <= y && y < 136 + (c - 1) * 96 + 16) break;
					continue;
				}
				if (24 <= x && x < 24 + 48 * 8 && 24 <= y && y < 24 + 48 * 8) {
					i = (x - 24) % 48;
					j = (y - 24) % 48;
					if (3 <= i && i < 45 && 3 <= j && j < 45) {
						x = (x - 24) / 48;
						y = (y - 24) / 48;
						if (testB(w, b, x, y, c, 0) != 0)
							break;
					}
				}
			}
		} else {
			i = clock();
			if (k > 0) {
				x = e[0] % 8;
				y = e[0] / 8;
				if (k > 1) {
					aFlushAll0(w);
					ai(c, b, e, k, &x, &y);
				}
			}
			i = (clock() - i) * 1000 / (int) CLOCKS_PER_SEC;
			if (i < 1000)
			aWait(1000 - i);
		}
		if (k > 0) {
			sprintf(s, "%c%c", 'A' + x, '1' + y);
			aDrawStr(w, 592, 136 + (c - 1) * 96, 0x00ffff, 0x000000, s);
			setB(w, b, x, y, c);
			aDrawOval(w, 20, 20, 37 + x * 48, 37 + y * 48, 0xff0000);
			aWait(1000);
			testB(w, b, x, y, c, 1);
			setB(w, b, x, y, c);
		}
		aDrawRect(w, 184, 56, 448, 116 + (c - 1) * 96, 0x000000);
		if (ps == 2) break;
		c = 3 - c;
	}
	aDrawRect(w, 162, 56, 448, 116 + 240, 0xffff00);
	for (;;) {
		waitClick(w, &x, &y, 0);
		if (448 <= x && x < 448 + 162 && 116 + 240 <= y && y < 116 + 240 + 56)
			goto restart;
	}
}

void ai(int c, unsigned char *b, unsigned char *e, int k, int *px, int *py)
{
	k = rand() % k;
	*px = e[k] % 8;
	*py = e[k] / 8;
}
