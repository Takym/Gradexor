#!/bin/bash
mkdir ../bin -p
gcc aclm_sample.c -lSDL2 -o ../bin/aclm_sample.elf
../bin/aclm_sample.elf
