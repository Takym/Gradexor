#include <acl.c>

void aMain()
{
    AWindow *w = aOpenWin(256, 256, "gradation", 1);
    AInt16 x, y;
    AInt32 c = 0;
    for (y = 0; y < 256; y++) {
        for (x = 0; x < 256; x++) {
            aSetPix0(w, x, y, c);
            c += 0x100;
        }
    }
    aWait(-1);
}
