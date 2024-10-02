#!/bin/bash

if [ -z "$GCC_OPT_COMMON" ]; then
	GCC_OPT_COMMON="-O0 -g -D_DEBUG"
fi

GCC_OPT="$GCC_OPT_COMMON -S -masm=intel"         ./build.sh
GCC_OPT="$GCC_OPT_COMMON -o vuuzwaail_debug.elf" ./build.sh

gdb ./vuuzwaail_debug.elf
