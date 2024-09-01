#define AMAXWINDOWS		1
#define ANOUSE_FASTMALLOC
#define ANOUSE_ATERRMSG
#define ANOUSE_LEAPFLUSH
#define ANOUSE_MOUSE
#include <acl.c>

AInt32 col[6] = { 0xbf0000, 0xbfbf00, 0x00bf00, 0x00bfbf, 0x0000bf, 0xbf00bf };

void aMain()
{
	AInt32 high = 0, score, bx, by, vx, vy, i, j, k, mx, my, ml, s100, blocks;
	char c[40][30], d, s[80], ph;
	AWindow *win = aOpenWin(640, 480, "blocks & ball", 1);
	for (;;) {
		ml = 10;
		score = 0;
reload:
		blocks = 0;
		aFillRect(win, 640, 480, 0, 0, 0);
		bx = 19; by = 20; vx = 1; vy = -1;
		mx = 15; my = 29;
		for (i = 0; i < 40; i++) {
			for (j = 0; j < 30; j++)
				c[i][j] = 0;
		}
		for (i = 0; i < 40; i++)
			c[i][1] = 1;
		aFillRect(win, 640, 16, 0, 16, 0xffffff);
		for (j = 2; j < 30; j++)
			c[0][j] = c[39][j] = 1;
		aFillRect(win, 16, 448,   0, 32, 0xffffff);
		aFillRect(win, 16, 448, 624, 32, 0xffffff);

		for (i = 0; i < 6; i++) {
			aFillRect(win, 608-128, 16, 16+64, (4 + i) * 16, col[i]);
			for (j = 5; j < 34; j += 2) {
				c[j + 0][4 + i] = 8 - i;
				c[j + 1][4 + i] = 8 - i;
				aFillRect(win, 32, 1, j * 16, (4 + i) * 16 + 15, 0);
				aFillRect(win, 1, 16, j * 16 + 31, (4 + i) * 16, 0);
				aFillRect(win, 32, 1, j * 16, (4 + i) * 16, 0xffffff);
				aFillRect(win, 1, 16, j * 16, (4 + i) * 16, 0xffffff);
				blocks++;
			}
		}
		aFillOval(win, 16, 16, bx * 16, by * 16, 0xffffff);
		aFillRect(win, ml * 16, 16, mx * 16, my * 16, 0xffffff);
		aWait(500);

		for (ph = 0;; ph ^= 1) {
			s100 = score / 100;
			if (high < score) high = score;

			aFillRect(win, 640, 16, 0, 0, 0x000000);
			sprintf(s, "SCORE:%05d     HIGH:%05d", score, high);
			aDrawStr(win, 160, 0, 0xffffff, 0x000000, s);

			// ball
			if (ph != 0) goto skip;
			aFillRect(win, 16, 16, bx * 16, by * 16, 0);
			c[bx][by] = 0;
			if (c[bx + vx][by] == 2 || c[bx][by + vy] == 2 || c[bx + vx][by + vy] == 2)
				score += 29 - by;
			if (c[bx + vx][by] >= 3) {
				i = bx + vx; j = by;
				score += c[i][j] - 2;
				if (i % 2 == 0) i--;
				aFillRect(win, 32, 16, i * 16, j * 16, 0);
				vx *= -1;
				c[i][j] = 0;
				c[i + 1][j] = 0;
				blocks--;
			}
			if (c[bx][by + vy] >= 3) {
				i = bx; j = by + vy;
				score += c[i][j] - 2;
				if (i % 2 == 0) i--;
				aFillRect(win, 32, 16, i * 16, j * 16, 0);
				vy *= -1;
				c[i][j] = 0;
				c[i + 1][j] = 0;
				blocks--;
			}
			if (c[bx + vx][by + vy] >= 3) {
				i = bx + vx; j = by + vy;
				score += c[i][j] - 2;
				if (i % 2 == 0) i--;
				aFillRect(win, 32, 16, i * 16, j * 16, 0);
				vx *= -1;
				vy *= -1;
				c[i][j] = 0;
				c[i + 1][j] = 0;
				blocks--;
			}
			if (c[bx + vx][by] != 0) vx *= -1;
			if (c[bx][by + vy] != 0) vy *= -1;
			if (c[bx + vx][by + vy] != 0) { vx *= -1; vy *= -1; }
			if (c[bx + vx][by + vy] == 0) {
				bx += vx;
				by += vy;
			}
			aFillOval(win, 16, 16, bx * 16, by * 16, 0xffffff);
			c[bx][by] = 1;
			if (by >= 29) break;
			if (score / 100 != s100 && ml > 4) {
				aFillRect(win, ml * 16, 16, mx * 16, my * 16, 0);
				for (i = 0; i < ml; i++)
					c[mx + i][my] = 0;
				ml--;
			}

skip:
			// pad
			aFillRect(win, ml * 16, 16, mx * 16, my * 16, 0);
			for (i = 0; i < ml; i++)
				c[mx + i][my] = 0;
			j = k = 0;
			i = aInkey(win, 1);
			if (i == AKEY_LEFT)  j = -1;
			if (i == AKEY_RIGHT) j = +1;
			if (i == AKEY_UP)    k = -1;
			if (i == AKEY_DOWN && my <= 28)  k = +1;
			if (i != 0) {
				while ((aInkey(win, 0)) == i)
					aInkey(win, 1);
			} 
			d = 0;
			for (i = 0; i < ml; i++)
				d += c[mx + j + i][my + k];
			if (d == 0) {
				mx += j;
				my += k;
			}
			aFillRect(win, ml * 16, 16, mx * 16, my * 16, 0xffffff);
			for (i = 0; i < ml; i++)
				c[mx + i][my] = 2;

			aWait(50);
			if (blocks == 0) goto reload;
		}
		while (aInkey(win, 1) != AKEY_ENTER)
			aWait(100);
	}
	aWait(-1);
}

