
GCC_CMD="gcc -Wall -m32 -std=gnu17 -O2 $1.c"

$GCC_CMD -o $1.asm -S -masm=intel
$GCC_CMD -o $1.elf

PROGRAM=./$1.elf

shift
$PROGRAM $*
