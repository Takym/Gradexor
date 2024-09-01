#include <acl.c>

void aMain()
{
    AWindow *w = aOpenWin(512, 384, "mandel", 1);
    AInt16 x, y;
    for (y = 0; y < 384; y++) {
        for (x = 0; x < 512; x++) {
            AInt16 sn = 0, sx, sy, n;
            AInt32 c, cx, cy, zx, zy, xx, yy;
            for (sx = 0; sx < 4; sx++) {
                cx = (x * 4 + sx) * 56 + 4673536;
                for (sy = 0; sy < 4; sy++) {
                    cy = (y * 4 + sy) * (-56) - 124928;
                    zx = cx; zy = cy;
                    for (n = 1; n < 447; n++) {
                        AInt64 zzx = zx, zzy = zy;
                        xx = (zzx * zzx) >> 24;
                        yy = (zzy * zzy) >> 24;
                        if (xx + yy > 0x4000000) break;
                        zy = (zzy * zzx) >> 23;
                        zx = xx + cx - yy;
                        zy += cy;
                    }
                    sn += n;
                }
            }
            n = sn >> 4;
            c = aRgb8(n, 0, 0);
            if (n >= 256) {
                c = aRgb8(0, 0, 0);
                if (n < 447)
                    c = aRgb8(255, n - 255, 0);
            }
            aSetPix0(w, x, y, c);
        }
        aLeapFlushAll(w, 128);
    }
    aWait(-1);
}
