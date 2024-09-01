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
	AWindow *w = aOpenWin(640, 432, "lv4", 1);
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

void ai_end(int c, unsigned char *b, unsigned char *e, int k, int *px, int *py);

void ai(int c, unsigned char *b, unsigned char *e, int k, int *px, int *py)
{
	static unsigned char t0[64] =
		"90866809"
		"00666600"
		"86777768"
		"66700766"
		"66700766"
		"86777768"
		"00666600"
		"90866809";
	unsigned char t[64], f[60];
	int i, l = 0, sc = 0;
	for (i = 0; i < 64; i++) {
		if (b[i] == 0)
			l++;
	}
	if (l <= 17) {
		ai_end(c, b, e, k, px, py);
		return;
	}
	for (i = 0; i < 64; i++)
		t[i] = t0[i];
	if (b[ 0] != 0) t[ 1] = t[ 8] = t[ 9] = '6';
	if (b[ 7] != 0) t[ 6] = t[14] = t[15] = '6';
 	if (b[56] != 0) t[48] = t[49] = t[57] = '6';
 	if (b[63] != 0) t[54] = t[55] = t[62] = '6';
	for (i = 0; i < k; i++) {
		if (sc < t[e[i]]) {
			sc = t[e[i]];
			l = 0;
		}
		if (sc == t[e[i]]) {
			f[l] = e[i];
			l++;
		}
	}
	i = rand() % l;
	*px = f[i] % 8;
	*py = f[i] / 8;
}

typedef unsigned long long ull;

int popcnt64(ull b)
{
    b -= b >> 1 & 0x5555555555555555ULL;
    b = (b & 0x3333333333333333ULL) + (b >> 2 & 0x3333333333333333ULL);
    b = (b + (b >> 4)) & 0x0f0f0f0f0f0f0f0fULL;
    return (b * 0x0101010101010101ULL) >> 56;
}

void setE8(ull p, ull q, ull *e);
void setP(ull *pp, ull *qq, ull *e, ull i);

int negaAlpha0(ull p, ull q, int beta, int ps)
{
	int pcp, pcq, t, i, j, alpha = -99, sc[64], k, ii[64];
	ull e = ~(p | q), pp, qq, e8[8], f8[8], f;
	setE8(p, q, e8);
	e &= e8[0] | e8[1] | e8[2] | e8[3] | e8[4] | e8[5] | e8[6] | e8[7];
	if (e == 0ULL) {
		if (ps != 0) {
			pcp = popcnt64(p);
			pcq = popcnt64(q);
			if (pcp == 0) return -64;
			if (pcq == 0) return +64;
			return pcp - pcq;
		}
		alpha = - negaAlpha0(q, p, - alpha, 1);
	} else if (((e - 1) & e) == 0ULL) {
		pp = p;
		qq = q;
		setP(&pp, &qq, e8, e);
		if ((pp | qq) == 0xffffffffffffffffULL)
			alpha = popcnt64(pp) * 2 - 64;
		else
			alpha = - negaAlpha0(qq, pp, +99, 0);
	} else {
		setE8(q, p, f8);
		f = f8[0] | f8[1] | f8[2] | f8[3] | f8[4] | f8[5] | f8[6] | f8[7];
		k = 0;
		for (i = 0; i < 64; i++) {
			if ((e >> i & 1) == 0) continue;
			ii[k] = i;
			pp = p;
			qq = q;
			setP(&pp, &qq, e8, 1ULL << i);
			setE8(qq, pp, f8);
			sc[k] = popcnt64((~(pp | qq)) & (f8[0] | f8[1] | f8[2] | f8[3] | f8[4] | f8[5] | f8[6] | f8[7]));
			if ((f >> i & 1) != 0)
				sc[k] -= 2;
			k++;
		}
		do {
			t = j = 99;
			for (i = 0; i < k; i++) {
				if (t > sc[i]) {
					t = sc[i];
					j = i;
				}
			}
			t = ii[j];
			ii[j] = ii[k - 1];
			sc[j] = sc[k - 1];
			pp = p;
			qq = q;
			setP(&pp, &qq, e8, 1ULL << t);
			t = - negaAlpha0(qq, pp, - alpha, 0);
			if (alpha < t)
				alpha = t;
			if (alpha >= beta) break;
			k--;
		} while (k > 0);
	}
	return alpha;
}

void setE8(ull p, ull q, ull *e)
{
	ull mq, t;
	mq = q & 0x7e7e7e7e7e7e7e7eULL;	// 左右両端をマスクで消す.
	t = mq & (p << 1);
	t |= mq & (t << 1);
	t |= mq & (t << 1);
	t |= mq & (t << 1);
	t |= mq & (t << 1);
	t |= mq & (t << 1);
	e[0] = t << 1;
	t = mq & (p >> 1);
	t |= mq & (t >> 1);
	t |= mq & (t >> 1);
	t |= mq & (t >> 1);
	t |= mq & (t >> 1);
	t |= mq & (t >> 1);
	e[1] = t >> 1;

	mq = q & 0x00ffffffffffff00ULL; // 上下両端をマスクで消す.
	t = mq & (p << 8);
	t |= mq & (t << 8);
	t |= mq & (t << 8);
	t |= mq & (t << 8);
	t |= mq & (t << 8);
	t |= mq & (t << 8);
	e[2] = t << 8;
	t = mq & (p >> 8);
	t |= mq & (t >> 8);
	t |= mq & (t >> 8);
	t |= mq & (t >> 8);
	t |= mq & (t >> 8);
	t |= mq & (t >> 8);
	e[3] = t >> 8;

	mq = q & 0x007e7e7e7e7e7e00ULL; // 外側全部をマスクで消す.
	t = mq & (p << 7);
	t |= mq & (t << 7);
	t |= mq & (t << 7);
	t |= mq & (t << 7);
	t |= mq & (t << 7);
	t |= mq & (t << 7);
	e[4] = t << 7;
	t = mq & (p << 9);
	t |= mq & (t << 9);
	t |= mq & (t << 9);
	t |= mq & (t << 9);
	t |= mq & (t << 9);
	t |= mq & (t << 9);
	e[5] = t << 9;

	t = mq & (p >> 7);
	t |= mq & (t >> 7);
	t |= mq & (t >> 7);
	t |= mq & (t >> 7);
	t |= mq & (t >> 7);
	t |= mq & (t >> 7);
	e[6] = t >> 7;
	t = mq & (p >> 9);
	t |= mq & (t >> 9);
	t |= mq & (t >> 9);
	t |= mq & (t >> 9);
	t |= mq & (t >> 9);
	t |= mq & (t >> 9);
	e[7] = t >> 9;
}

void setP(ull *pp, ull *qq, ull *e, ull i)
{
	ull p = *pp, q = *qq, b;
	if ((e[0] & i) != 0) { b = i >> 1; do { p |= b; q ^= b; b >>= 1; } while ((p & b) == 0); }
	if ((e[1] & i) != 0) { b = i << 1; do { p |= b; q ^= b; b <<= 1; } while ((p & b) == 0); }
	if ((e[2] & i) != 0) { b = i >> 8; do { p |= b; q ^= b; b >>= 8; } while ((p & b) == 0); }
	if ((e[3] & i) != 0) { b = i << 8; do { p |= b; q ^= b; b <<= 8; } while ((p & b) == 0); }
	if ((e[4] & i) != 0) { b = i >> 7; do { p |= b; q ^= b; b >>= 7; } while ((p & b) == 0); }
	if ((e[5] & i) != 0) { b = i >> 9; do { p |= b; q ^= b; b >>= 9; } while ((p & b) == 0); }
	if ((e[6] & i) != 0) { b = i << 7; do { p |= b; q ^= b; b <<= 7; } while ((p & b) == 0); }
	if ((e[7] & i) != 0) { b = i << 9; do { p |= b; q ^= b; b <<= 9; } while ((p & b) == 0); }
	p |= i;
	*pp = p;
	*qq = q;
}

void ai_end(int c, unsigned char *b, unsigned char *e, int k, int *px, int *py)
{
	int i, j, d = 3 - c, sc = -99, t, l = 0, ss[64];
	unsigned char f[60];

	ull p = 0ULL, q = 0ULL, pp, qq, e8[8], f8[8], ee, ff;
	for (i = 0; i < 64; i++) {
		if (b[i] == c) p |= 1ULL << i;
		if (b[i] == d) q |= 1ULL << i;
	}
	setE8(p, q, e8);
	setE8(q, p, f8);
//	for (i = 0; i < 64; i++) printf("%d", b[i]); printf(":c=%d ", c);
	printf("n=%02d ", popcnt64(~(p | q)));
	ee = (~(p | q)) & (e8[0] | e8[1] | e8[2] | e8[3] | e8[4] | e8[5] | e8[6] | e8[7]);
	ff = f8[0] | f8[1] | f8[2] | f8[3] | f8[4] | f8[5] | f8[6] | f8[7];
	for (i = 0; i < 64; i++) {
		ss[i] = 99;
		if ((ee >> i & 1) == 0) continue;
		pp = p;
		qq = q;
		setP(&pp, &qq, e8, 1ULL << i);
		setE8(qq, pp, f8);
		ss[i] = popcnt64((~(pp | qq)) & (f8[0] | f8[1] | f8[2] | f8[3] | f8[4] | f8[5] | f8[6] | f8[7]));
		if ((ff >> i & 1) != 0)
			ss[i] -= 2;
	}
	for (;;) {
		t = j = 99;
		for (i = 0; i < 64; i++) {
			if (t > ss[i]) {
				t = ss[i];
				j = i;
			}
		}
		if (t == 99) break;
		ss[j] = 99;
		pp = p;
		qq = q;
		setP(&pp, &qq, e8, 1ULL << j);
		t = - negaAlpha0(qq, pp, - (sc - 1), 0);
		printf("[i=%02d sc=%03d] ", j, t);
		if (sc < t) {
			sc = t;
			l = 0;
		}
		if (sc == t) {
			f[l] = j;
			l++;
		}
	}
	i = rand() % l;
	printf("i=%02d\n", f[i]);
	*px = f[i] % 8;
	*py = f[i] / 8;
}
