/****
 * CppRepl - C++ Read Eval Print Loop
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

// 実行方法：wsl g++ CppRepl.cpp -o CppRepl.elf; ./CppRepl.elf

#define BUFFER_SIZE 512

#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>

typedef struct _REPL_OPTIONS_ {
	bool        showLogo;
	const char *tmpCppFile;
	const char *tmpElfFile;
} ReplOptions, *LpReplOptions;

void loadDefaultOptions(LpReplOptions options)
{
	options->showLogo   = true;
	options->tmpCppFile = "__REPL_TMP.CPP";
	options->tmpElfFile = "__REPL_TMP.ELF";
}

void printLogo(LpReplOptions options)
{
	if (options->showLogo) {
		printf("CppRepl - C++ Read Eval Print Loop\r\n");
		printf("Copyright (C) 2024 Takym.\r\n\r\n");
	}
}

int main(int argc, char *argv[], char *envp[])
{
	ReplOptions cfg;
	loadDefaultOptions(&cfg);
	printLogo(&cfg);

	char gccCmd[BUFFER_SIZE];
	sprintf(gccCmd, "g++ %s -o %s; ./%s", cfg.tmpCppFile, cfg.tmpElfFile, cfg.tmpElfFile);

	FILE *fp = fopen(cfg.tmpCppFile, "w+");
	if (fp == NULL) {
		printf("File Open Error: %s\r\n", cfg.tmpCppFile);
		return -1;
	}

	fprintf(fp, "#include <stdio.h>\r\n");
	fprintf(fp, "int main(int argc,char*argv[],char*envp[]){\r\n");

	const char finalLine[] = "return 0;}";

	while (true) {
		char buf[BUFFER_SIZE];
		printf("> ");
		fprintf(fp, "%s", fgets(buf, sizeof(buf), stdin));
		fprintf(fp, finalLine);
		fflush(fp);
		system(gccCmd);
		fseek(fp, -sizeof(finalLine), SEEK_CUR);
	}

	fclose(fp);
	return 0;
}
