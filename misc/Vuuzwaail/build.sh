#!/bin/bash

# GDB を使う時は -O2 を -O0 -g -D_DEBUG に書き換える。
# アセンブリを出力する時は -o vuuzwaail.elf を -S -masm=intel に書き換える。

if [ -z "$GCC_CMD" ]; then
	GCC_CMD="g++ -Wall -m64 -std=gnu++17"
fi

if [ -z "$GCC_OPT" ]; then
	GCC_OPT="-O2 -o vuuzwaail.elf"
fi

$GCC_CMD $GCC_OPT \
	logging.cpp   \
	processor.cpp \
	main.cpp
