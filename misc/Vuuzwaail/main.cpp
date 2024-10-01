/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

using namespace vzwl;

int main(int argc, char *argv[], char *envp[])
{
	if (argc <= 1) {
		printf("Usage> ./vuuzwaail.elf");
	}

	Component proc;
	proc.type = Unknown;
	processor::init(&proc, 1);

	return 0;
}
