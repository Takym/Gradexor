/****
 * AclibMarkup
 * Copyright (C) 2020-2022 Takym.
 *
 * distributed under the MIT License.
****/

/****
 * THIRD PARTY NOTICE
 * This program uses aclib. (http://essen.osask.jp/?aclib05)
 * License: KL-01 (http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)
****/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BUFFER_MAX_SIZE				0x00010000
#define EXIT_CODE_SUCCESS			0
#define EXIT_CODE_CMDLINE			1
#define EXIT_CODE_INPUT				2
#define EXIT_CODE_SYNTAX			3
#define EXIT_CODE_OUTPUT			4

typedef struct _FILE_INFO_ {
	char   *buf;
	size_t  size;
} FileInfo;

typedef enum _TOKEN_TYPE_ {
	Root,
	String,
	BeginBlock,
	EndBlock,
	Semicolon
} TokenType;

typedef struct _TOKEN_ {
	int             start;
	int             len;
	TokenType       type;
	struct _TOKEN_ *next;
} *PToken;

FileInfo loadFile(char *inFile)
{
	FILE     *fp;
	FileInfo  fi;

	fp = fopen(inFile, "r");
	if (fp == NULL) {
		printf("Could not load the specified input file: \"%s\"\r\n", inFile);
		fi.buf = NULL;
	} else {
		fi.buf  = ((char *)(malloc(BUFFER_MAX_SIZE)));
		fi.size = fread(fi.buf, 1, BUFFER_MAX_SIZE, fp);
		fclose(fp);
	}
	return fi;
}

PToken createToken()
{
	PToken result = ((PToken)(malloc(sizeof(PToken))));
	result->start = 0;
	result->len   = 0;
	result->type  = Root;
	result->next  = NULL;
	return result;
}

#define SKIP			++i;
#define CREATE_TOKEN	t = createToken();
#define APPEND_TOKEN	tail->next = t; tail = t;

PToken parse(FileInfo *pFi)
{
	int i = 0;
	char ch;
	PToken head, tail, t;

	head = createToken();
	tail = head;

	while (i < pFi->size) {
		ch = pFi->buf[i];
		switch (ch) {
		case ' ': case '\n': case '\r': case '\t': case '\v':
			SKIP;
			break;
		case '\"':
			SKIP;
			CREATE_TOKEN;
			t->start = i;
			while (i < pFi->size && pFi->buf[i] != '\"') {
				SKIP;
			}
			t->len  = i - t->start;
			t->type = String;
			APPEND_TOKEN;
			SKIP;
			break;
		case '{':
			SKIP;
			CREATE_TOKEN;
			t->type = BeginBlock;
			APPEND_TOKEN;
			break;
		case '}':
			SKIP;
			CREATE_TOKEN;
			t->type = EndBlock;
			APPEND_TOKEN;
			break;
		case ';':
			SKIP;
			CREATE_TOKEN;
			t->type = Semicolon;
			APPEND_TOKEN;
			break;
		default:
			printf("Failed: detected the syntax error.\r\n");
			return NULL;
		}
	}

	return head;
}

int saveFile(char *outFile, FileInfo *pFi, PToken root)
{
	FILE *fp;
	PToken token = root->next;
	int braces = 0, ret = EXIT_CODE_SUCCESS;
	char *str;

	fp = fopen(outFile, "w");
	if (fp == NULL) {
		printf("Could not load the specified output file: \"%s\"\r\n", outFile);
		return EXIT_CODE_OUTPUT;
	} else {
		// Write the file header.
		fprintf(fp, "// This file is generated by AclibMarkup.\r\n\r\n#define AARCH_X64\r\n#include \"acl.c\"\r\n\r\n");

		// Write begin the entry point.
		fprintf(fp, "void aMain() {\r\n\r\n");

		while (token != NULL) {
			switch (token->type) {
			case String:
				str = &pFi->buf[token->start];
				if (token->len == 6) {
					if (strncmp("window", str, 6) == 0) {
						fprintf(fp, "AWindow *w = aOpenWin(256, 256, \"The Window\", 1);\r\n\r\n");
					}
				}
				break;
			case BeginBlock:
				++braces;
				fprintf(fp, "{\r\n\r\n");
				break;
			case EndBlock:
				if (braces == 0) {
					printf("Warning: detected an invalid token (end block).\r\n");
					ret = EXIT_CODE_SYNTAX;
					break;
				}
				--braces;
				fprintf(fp, "}\r\n\r\n");
				break;
			case Semicolon:
				// ignore
				break;
			default:
				printf("Warning: detected an invalid token.\r\n");
				ret = EXIT_CODE_SYNTAX;
				break;
			}
			token = token->next;
		}

		if (braces != 0) {
			printf("Warning: end blocks are missing.\r\n");
			ret = EXIT_CODE_SYNTAX;
		}

		// Write end the entry point.
		fprintf(fp, "aWait(-1); }\r\n");

		fclose(fp);
		return EXIT_CODE_SUCCESS;
	}
}

int main(int argc, char *argv[])
{
	FileInfo src;
	PToken root;

	if (argc == 3) {
		src = loadFile(argv[1]);
		if (src.buf == NULL) {
			return EXIT_CODE_INPUT;
		}
		root = parse(&src);
		if (root == NULL) {
			return EXIT_CODE_SYNTAX;
		}
		return saveFile(argv[2], &src, root);
	} else {
		printf("usage> ./AclibMarkup.elf <input-file.am> <output-file.c>\r\n");
		return EXIT_CODE_CMDLINE;
	}
}
