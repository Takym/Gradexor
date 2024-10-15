/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

#ifdef _DEBUG
#define LOG_FILENAME	"vuuzwaail_debug.log"
#else
#define LOG_FILENAME	"vuuzwaail.log"
#endif // _DEBUG

namespace vzwl::logging
{
	static FILE      *fpLogFile;
	static struct tm  dtInvalid;
	static LogLevel   lastLogLevel;
	static LogName    lastLogName;

	static void _writeHeader(cstr_t lvl, cstr_t name)
	{
		auto dt = getDtNow();

		fprintf(
			fpLogFile, "%04d/%02d/%02d %02d:%02d:%02d [%-5s] %-24s\t",
			dt->tm_year + 1900, dt->tm_mon + 1, dt->tm_mday,
			dt->tm_hour,        dt->tm_min,     dt->tm_sec,
			lvl, name
		);
	}

	static void _vlprintf(LogLevel logLevel, LogName logName, cstr_t messageFormat, va_list ap)
	{
		if (logLevel.empty()) {
			logLevel = VZWL_LOG_LEVEL_NULL;
		}

		if (logName.empty()) {
			logName = "system";
		}

		cstr_t lvl  = logLevel.c_str();
		cstr_t name = logName .c_str();
		bool writeHeader = false, printLineBreak = false;

		if (logLevel != lastLogLevel || logName != lastLogName) {
			writeHeader    = true;
			printLineBreak = true;

			lastLogLevel = logLevel;
			lastLogName  = logName;
		}

		if (fseek(fpLogFile, -1, SEEK_CUR) == 0) {
			int ch = fgetc(fpLogFile);
			if ('\n' <= ch && ch <= '\r') { // '\n', '\v', '\f', '\r'
				writeHeader    = true;
				printLineBreak = false;
			}
		} else {
			writeHeader = true;
		}

		if (printLineBreak) {
			fprintf(fpLogFile, "\r\n");
		}
		if (writeHeader) {
			_writeHeader(lvl, name);
		}

		va_list tmp;
		va_copy(tmp, ap);
		vfprintf(fpLogFile, messageFormat, tmp);
		fflush(fpLogFile);

#if _DEBUG
		if (printLineBreak) {
			printf("\r\n");
		}
		if (writeHeader) {
			printf("[%s] %s: ", lvl, name);
		}
		vprintf(messageFormat, ap);
#else
		if (logLevel == VZWL_LOG_LEVEL_PRINT) {
			if (printLineBreak) {
				printf("\r\n");
			}
			vprintf(messageFormat, ap);
		} else if (logLevel == VZWL_LOG_LEVEL_ERROR || logLevel == VZWL_LOG_LEVEL_FATAL) {
			if (printLineBreak) {
				fprintf(stderr, "\r\n");
			}
			if (writeHeader) {
				fprintf(stderr, "%s: ", lvl);
			}
			vfprintf(stderr, messageFormat, ap);
		}
#endif // _DEBUG
	}

	static void _vlprintln(LogLevel logLevel)
	{
		fprintf(fpLogFile, "\r\n");
		fflush(fpLogFile);

#if _DEBUG
		printf("\r\n");
#else
		if (logLevel == VZWL_LOG_LEVEL_PRINT) {
			printf("\r\n");
		} else if (logLevel == VZWL_LOG_LEVEL_ERROR || logLevel == VZWL_LOG_LEVEL_FATAL) {
			fprintf(stderr, "\r\n");
		}
#endif // _DEBUG
	}

	bool init()
	{
		fpLogFile = fopen(LOG_FILENAME, "a+");

		dtInvalid.tm_year = -1900;
		dtInvalid.tm_mon  = -1;
		dtInvalid.tm_mday = 0;
		dtInvalid.tm_hour = 99;
		dtInvalid.tm_min  = 99;
		dtInvalid.tm_sec  = 99;

		lastLogLevel = "";
		lastLogName  = "";

		if (fpLogFile == nullptr) {
			return false;
		}

		lINFOln("", VZWL_TITLE " has been started");
		return true;
	}

	void deinit(ReturnCode ret)
	{
		if (ret == VZWL_RET_SUCCEEDED) {
			lINFOln("", VZWL_TITLE " is closing normally...");
		} else {
			lWARNln("", VZWL_TITLE " is closing abnormally (ret: %d)...", ret);
		}

		fclose(fpLogFile);
	}

	LpDateTime getDtNow()
	{
		time_t t;
		time(&t);

		auto result = localtime(&t);

		if (result == nullptr) {
			return &dtInvalid;
		} else {
			return result;
		}
	}

#define LPRINT_DEF(funcName) \
	void l##funcName(LogName logName, cstr_t messageFormat, ...)

#define LPRINTF_CORE(lvl)                           \
	{                                               \
		va_list ap;                                 \
		va_start(ap, messageFormat);                \
		_vlprintf(lvl, logName, messageFormat, ap); \
		va_end(ap);                                 \
	}

#define LPRINTLN_CORE(lvl) \
	{                      \
		LPRINTF_CORE(lvl); \
		_vlprintln(lvl);   \
	}

#define LPRINTF(lvl) \
	LPRINT_DEF(lvl##f) LPRINTF_CORE(VZWL_LOG_LEVEL_##lvl)

#define LPRINTLN(lvl) \
	LPRINT_DEF(lvl##ln) LPRINTLN_CORE(VZWL_LOG_LEVEL_##lvl)

#define LPRINT(lvl) \
	LPRINTF(lvl) LPRINTLN(lvl)

	void lprintf(LogLevel logLevel, LogName logName, cstr_t messageFormat, ...)
		LPRINTF_CORE(logLevel)

	void lprintln(LogLevel logLevel, LogName logName, cstr_t messageFormat, ...)
		LPRINTLN_CORE(logLevel)

	LPRINT(PRINT);
	LPRINT(TRACE);
	LPRINT(DEBUG);
	LPRINT(INFO );
	LPRINT(WARN );
	LPRINT(BUG  );
	LPRINT(ERROR);
	LPRINT(FATAL);
}
