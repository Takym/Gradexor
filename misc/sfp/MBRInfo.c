/****
 * MBRInfo - IPL を除く MBR の情報を表示する。
 * Copyright (C) 2020-2022 Takym.
 *
 * distributed under the MIT License.
****/

// 実行方法：wsl gcc MBRInfo.c -o MBRInfo.elf; ./MBRInfo.elf "ディスクイメージ"

// 参考
// http://elm-chan.org/docs/fat.html
// https://ja.wikipedia.org/wiki/%E3%83%9E%E3%82%B9%E3%82%BF%E3%83%BC%E3%83%96%E3%83%BC%E3%83%88%E3%83%AC%E3%82%B3%E3%83%BC%E3%83%89
// https://en.wikipedia.org/wiki/Master_boot_record
// https://uefi.org/specifications
// https://uefi.org/sites/default/files/resources/UEFI_Spec_2_9_2021_03_18.pdf

#ifdef __GNUC__
#define PACKED __attribute__((packed))
#else
#define PACKED
#endif

typedef unsigned char  int8_t;
typedef unsigned short int16_t;
typedef unsigned int   int32_t;

typedef struct _FAT_HEADER_ {
	int8_t  BS_JmpBoot[3];
	int8_t  BS_OEMName[8];
	int16_t BPB_BytsPerSec;
	int8_t  BPB_SecPerClus;
	int16_t BPB_RsvdSecCnt;
	int8_t  BPB_NumFATs;
	int16_t BPB_RootEntCnt;
	int16_t BPB_TotSec16;
	int8_t  BPB_Media;
	int16_t BPB_FATSz16;
	int16_t BPB_SecPerTrk;
	int16_t BPB_NumHeads;
	int32_t BPB_HiddSec;
	int32_t BPB_TotSec32;
} PACKED FATHeader;

typedef struct _FAT_HEADER_12_16_ {
	int8_t  BS_DrvNum;
	int8_t  BS_Reserved;
	int8_t  BS_BootSig;
	int32_t BS_VolID;
	int8_t  BS_VolLab[11];
	int8_t  BS_FilSysType[8];
} PACKED FATHeader1216;

typedef struct _FAT_HEADER_32_ {
	int32_t BPB_FATSz32;
	int16_t BPB_ExtFlags;
	int8_t  BPB_FSVer[2];
	int32_t BPB_RootClus;
	int16_t BPB_FSInfo;
	int16_t BPB_BkBootSec;
	int8_t  BPB_Reserved[12];
	int8_t  BS_DrvNum;
	int8_t  BS_Reserved;
	int8_t  BS_BootSig;
	int32_t BS_VolID;
	int8_t  BS_VolLab[11];
	int8_t  BS_FilSysType[8];
} PACKED FATHeader32;

typedef struct _PARTITION_RECORD_ {
	int8_t  BootIndicator;
	int8_t  StartingCHS[3];
	int8_t  OSType;
	int8_t  EndingCHS[3];
	int32_t StartingLBA;
	int32_t SizeInLBA;
} PACKED PartitionRecord;

typedef struct _PARTITION_TABLE_ {
	PartitionRecord PartitionRecords[4];
} PACKED PartitionTable;

typedef struct _CHS_ {
	int32_t Head;
	int32_t Cylinder;
	int32_t Sector;
} CHS;

CHS decodeCHS(int8_t chsData[3])
{
	CHS chs;
	chs.Head     =   chsData[0];
	chs.Cylinder =   chsData[2]
	             | ((chsData[1] & 0b11000000) << 2);
	chs.Sector   =   chsData[1] & 0b00111111;
	return chs;
}

#include <stdio.h>
#include <string.h>

#define MBR_SIZE 512

int main(int argc, char *argv[])
{
	FILE            *fp;
	size_t           sz;
	char             buf[MBR_SIZE];
	char             tmp[12];
	int              i;
	FATHeader       *fh;
	FATHeader1216   *fh1216;
	FATHeader32     *fh32;
	PartitionTable  *pt;
	PartitionRecord *pr;
	int8_t          *bootSig;
	CHS              chs;

	if (argc != 2) {
		printf("Usage> %s <disk image file>\r\n", argv[0]);
		return 1;
	}
	fp = fopen(argv[1], "rb");
	if (fp == NULL) {
		printf("Error: could not open the file \"%s\"\r\n", argv[1]);
		return 1;
	}

	sz = fread(buf, 1, MBR_SIZE, fp);
	if (sz != MBR_SIZE) {
		printf("Error: could not read the file \"%s\"\r\n", argv[1]);
		fclose(fp);
		return 1;
	}

	fclose(fp);

	fh      = ((FATHeader      *)(&buf[  0]));
	fh1216  = ((FATHeader1216  *)(&buf[ 36]));
	fh32    = ((FATHeader32    *)(&buf[ 36]));
	pt      = ((PartitionTable *)(&buf[446]));
	bootSig = ((int8_t         *)(&buf[510]));

	printf("FATHeader\r\n");
	printf("---------\r\n");
	printf(
		"BS_JmpBoot     : %02X %02X %02X\r\n",
		((int32_t)(fh->BS_JmpBoot[0])),
		((int32_t)(fh->BS_JmpBoot[1])),
		((int32_t)(fh->BS_JmpBoot[2]))
	);
	strncpy(tmp, ((char *)(fh->BS_OEMName)), 8);
	tmp[8] = '\0';
	printf("BS_OEMName     : %s\r\n",     tmp);
	printf("BPB_BytsPerSec : %d\r\n",     ((int32_t)(fh->BPB_BytsPerSec)));
	printf("BPB_SecPerClus : %d\r\n",     ((int32_t)(fh->BPB_SecPerClus)));
	printf("BPB_RsvdSecCnt : %d\r\n",     ((int32_t)(fh->BPB_RsvdSecCnt)));
	printf("BPB_NumFATs    : %d\r\n",     ((int32_t)(fh->BPB_NumFATs)));
	printf("BPB_RootEntCnt : %d\r\n",     ((int32_t)(fh->BPB_RootEntCnt)));
	printf("BPB_TotSec16   : %d\r\n",     ((int32_t)(fh->BPB_TotSec16)));
	printf("BPB_Media      : 0x%02X\r\n", ((int32_t)(fh->BPB_Media)));
	printf("BPB_FATSz16    : %d\r\n",     ((int32_t)(fh->BPB_FATSz16)));
	printf("BPB_SecPerTrk  : %d\r\n",     ((int32_t)(fh->BPB_SecPerTrk)));
	printf("BPB_NumHeads   : %d\r\n",     ((int32_t)(fh->BPB_NumHeads)));
	printf("BPB_HiddSec    : %d\r\n",     ((int32_t)(fh->BPB_HiddSec)));
	printf("BPB_TotSec32   : %d\r\n",     ((int32_t)(fh->BPB_TotSec32)));

	printf("\r\n\r\n");
	printf("FATHeader1216\r\n");
	printf("-------------\r\n");
	printf("BS_DrvNum      : 0x%02X\r\n",   ((int32_t)(fh1216->BS_DrvNum)));
	printf("BS_Reserved    : 0x%02X\r\n",   ((int32_t)(fh1216->BS_Reserved)));
	printf("BS_BootSig     : 0x%02X\r\n",   ((int32_t)(fh1216->BS_BootSig)));
	printf("BS_VolID       : 0x%08X\r\n",   ((int32_t)(fh1216->BS_VolID)));
	strncpy(tmp, ((char *)(fh1216->BS_VolLab)), 11);
	tmp[11] = '\0';
	printf("BS_VolLab      : %s\r\n", tmp);
	strncpy(tmp, ((char *)(fh1216->BS_FilSysType)), 8);
	tmp[8] = '\0';
	printf("BS_FilSysType  : %s\r\n", tmp);

	printf("\r\n\r\n");
	printf("FATHeader32\r\n");
	printf("-----------\r\n");
	printf("BPB_FATSz32    : %d\r\n",       ((int32_t)(fh32->BPB_FATSz32)));
	printf("BPB_ExtFlags   : 0x%04X\r\n",   ((int32_t)(fh32->BPB_ExtFlags)));
	printf("BPB_FSVer      : %d.%d\r\n",    ((int32_t)(fh32->BPB_FSVer[0])), ((int32_t)(fh32->BPB_FSVer[1])));
	printf("BPB_RootClus   : %d\r\n",       ((int32_t)(fh32->BPB_RootClus)));
	printf("BPB_FSInfo     : %d\r\n",       ((int32_t)(fh32->BPB_FSInfo)));
	printf("BPB_BkBootSec  : %d\r\n",       ((int32_t)(fh32->BPB_BkBootSec)));
	printf("BPB_Reserved   : ");
	for (i = 0; i < 12; ++i) {
		printf("0x%02X ", fh32->BPB_Reserved[i]);
	}
	printf("\r\n");
	printf("BS_DrvNum      : 0x%02X\r\n",   ((int32_t)(fh32->BS_DrvNum)));
	printf("BS_Reserved    : 0x%02X\r\n",   ((int32_t)(fh32->BS_Reserved)));
	printf("BS_BootSig     : 0x%02X\r\n",   ((int32_t)(fh32->BS_BootSig)));
	printf("BS_VolID       : 0x%08X\r\n",   ((int32_t)(fh32->BS_VolID)));
	strncpy(tmp, ((char *)(fh32->BS_VolLab)), 11);
	tmp[11] = '\0';
	printf("BS_VolLab      : %s\r\n", tmp);
	strncpy(tmp, ((char *)(fh32->BS_FilSysType)), 8);
	tmp[8] = '\0';
	printf("BS_FilSysType  : %s\r\n", tmp);

	for (i = 0; i < 4; ++i) {
		pr = &pt->PartitionRecords[i];
		printf("\r\n\r\n");
		printf("PartitionEntry #%d\r\n", i);
		printf("------------------\r\n");
		printf("BootIndicator  : 0x%02X\r\n", ((int32_t)(pr->BootIndicator)));
		chs = decodeCHS(pr->StartingCHS);
		printf("StartingCHS    : C = %d; H = %d; S = %d\r\n", chs.Cylinder, chs.Head, chs.Sector);
		printf("OSType         : 0x%02X\r\n", pr->OSType);
		chs = decodeCHS(pr->EndingCHS);
		printf("EndingCHS      : C = %d; H = %d; S = %d\r\n", chs.Cylinder, chs.Head, chs.Sector);
		printf("StartingLBA    : 0x%08X\r\n", ((int32_t)(pr->StartingLBA)));
		printf("SizeInLBA      : %d\r\n",     ((int32_t)(pr->SizeInLBA)));
	}

	printf("\r\n\r\n");
	printf("Boot Signature = 0x%02X, 0x%02X\r\n", ((int32_t)(bootSig[0])), ((int32_t)(bootSig[1])));

	return 0;
}
