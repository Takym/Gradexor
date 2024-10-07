/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#pragma once
#ifndef VUUZWAAIL_HPP
#define VUUZWAAIL_HPP

#define VZWL_TITLE				"Vuuzwaail"
#define VZWL_COPYRIGHT			"Copyright (C) 2024 Takym."
#define VZWL_VERSION_MAJOR		0
#define VZWL_VERSION_MINOR		0
#define VZWL_VERSION_PATCH		0
#define VZWL_VERSION_BUILD		0

#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <string.h>
#include <time.h>

namespace vzwl
{
	extern bool debugModeEnabled;

	typedef int ReturnCode;
#define VZWL_RET						vzwl::ReturnCode
#define VZWL_RET_SUCCEEDED				((VZWL_RET)(0))
#define VZWL_RET_FAILED_STARTUP(num)	((VZWL_RET)(0x00000000 | num))
#define VZWL_RET_FAILED_PROCESSOR(num)	((VZWL_RET)(0x00010000 | num))

#define VZWL_RET_FAILED_STARTUP_MISSING_CMDLINE_ARGS	VZWL_RET_FAILED_STARTUP  (0x0001)
#define VZWL_RET_FAILED_STARTUP_INIT_LOGGING			VZWL_RET_FAILED_STARTUP  (0x0002)
#define VZWL_RET_FAILED_PROCESSOR_ANY					VZWL_RET_FAILED_PROCESSOR(0x0000)
#define VZWL_RET_FAILED_PROCESSOR_INIT					VZWL_RET_FAILED_PROCESSOR(0x0001)
#define VZWL_RET_FAILED_PROCESSOR_DEINIT				VZWL_RET_FAILED_PROCESSOR(0x0002)
#define VZWL_RET_FAILED_PROCESSOR_RUN					VZWL_RET_FAILED_PROCESSOR(0x0003)
#define VZWL_RET_FAILED_PROCESSOR_RUN_AND_DEINIT		VZWL_RET_FAILED_PROCESSOR(0x0004)

	typedef struct _CMDLINE_OPTIONS_ {
		int    argc;
		char **argv;
		char **envp;
		bool   showLogo;
		bool   showVersion;
		bool   showHelp;
	} CmdlineOptions, *LpCmdlineOptions;

	typedef enum _COMPONENT_TYPE_ {
		Unknown = -1,
		Uninitialized,
		Processor,
		Memory,
		Storage
	} ComponentType;

	typedef uint32_t ComponentId;

	typedef struct _COMPONENT_ Component, *LpComponent;

	typedef void    (*SendFunc      )(LpComponent lpThisComp, int32_t key, int32_t data);
	typedef int32_t (*ReceiveFunc   )(LpComponent lpThisComp, int32_t key              );
	typedef bool    (*TesterDoerFunc)(LpComponent lpThisComp                           );

	struct _COMPONENT_ {
		ComponentType   type;
		ComponentId     id;
		SendFunc        send;
		ReceiveFunc     receive;
		TesterDoerFunc  run;
		TesterDoerFunc  deinit;
		void           *data;
	};

#define VZWL_INIT_CHECK_TYPE                                                                            \
	if (lpComp->type != Uninitialized) {                                                                \
		lWARNln(LOG_NAME, "The specified component is initialized already. Type ID: %d", lpComp->type); \
		ENDED;                                                                                          \
		return false;                                                                                   \
	}

#define VZWL_INIT_COMMON(typeValue, idValue) \
	lpComp->type    = (typeValue);           \
	lpComp->id      = (idValue);             \
	lpComp->send    = _send;                 \
	lpComp->receive = _receive;              \
	lpComp->run     = _run;                  \
	lpComp->deinit  = _deinit;               \
	lpComp->data    = data;

#define VZWL_DEINIT_CHECK_TYPE(typeValue, displayName)                                                         \
	if (lpThisComp->type != (typeValue)) {                                                                     \
		lWARNln(LOG_NAME, "The specified component is not a " #displayName ". Type ID: %d", lpThisComp->type); \
		ENDED;                                                                                                 \
		return false;                                                                                          \
	}

#define VZWL_DEINIT_COMMON               \
	lpThisComp->type    = Uninitialized; \
	lpThisComp->send    = nullptr;       \
	lpThisComp->receive = nullptr;       \
	lpThisComp->run     = nullptr;       \
	lpThisComp->deinit  = nullptr;       \
	lpThisComp->data    = nullptr;

	namespace logging
	{
		typedef struct ::tm *LpDateTime;
		typedef std::string  LogLevel;
		typedef std::string  LogName;

#define VZWL_LOG_NS				vzwl::logging
#define VZWL_LOG_LEVEL			VZWL_LOG_NS::LogLevel
#define VZWL_LOG_LEVEL_NULL		((VZWL_LOG_LEVEL)("NULL" ))
#define VZWL_LOG_LEVEL_PRINT	((VZWL_LOG_LEVEL)("PRINT"))
#define VZWL_LOG_LEVEL_TRACE	((VZWL_LOG_LEVEL)("TRACE"))
#define VZWL_LOG_LEVEL_DEBUG	((VZWL_LOG_LEVEL)("DEBUG"))
#define VZWL_LOG_LEVEL_INFO		((VZWL_LOG_LEVEL)("INFO" ))
#define VZWL_LOG_LEVEL_WARN		((VZWL_LOG_LEVEL)("WARN" ))
#define VZWL_LOG_LEVEL_BUG		((VZWL_LOG_LEVEL)("BUG"  ))
#define VZWL_LOG_LEVEL_ERROR	((VZWL_LOG_LEVEL)("ERROR"))
#define VZWL_LOG_LEVEL_FATAL	((VZWL_LOG_LEVEL)("FATAL"))

#define VZWL_LOG_BEGIN(name)		VZWL_LOG_NS::lTRACEln(name, "Executing `%s`...", __PRETTY_FUNCTION__);
#define VZWL_LOG_ENDED(name)		VZWL_LOG_NS::lTRACEln(name, "Completed `%s`!",   __PRETTY_FUNCTION__);

		bool       init    (void                                                                    );
		void       deinit  (ReturnCode ret                                                          );
		LpDateTime getDtNow(void                                                                    );
		void       lprintf (LogLevel logLevel, LogName logName, const char *const messageFormat, ...);
		void       lprintln(LogLevel logLevel, LogName logName, const char *const messageFormat, ...);
		void       lPRINTf (                   LogName logName, const char *const messageFormat, ...);
		void       lPRINTln(                   LogName logName, const char *const messageFormat, ...);
		void       lTRACEf (                   LogName logName, const char *const messageFormat, ...);
		void       lTRACEln(                   LogName logName, const char *const messageFormat, ...);
		void       lDEBUGf (                   LogName logName, const char *const messageFormat, ...);
		void       lDEBUGln(                   LogName logName, const char *const messageFormat, ...);
		void       lINFOf  (                   LogName logName, const char *const messageFormat, ...);
		void       lINFOln (                   LogName logName, const char *const messageFormat, ...);
		void       lWARNf  (                   LogName logName, const char *const messageFormat, ...);
		void       lWARNln (                   LogName logName, const char *const messageFormat, ...);
		void       lBUGf   (                   LogName logName, const char *const messageFormat, ...);
		void       lBUGln  (                   LogName logName, const char *const messageFormat, ...);
		void       lERRORf (                   LogName logName, const char *const messageFormat, ...);
		void       lERRORln(                   LogName logName, const char *const messageFormat, ...);
		void       lFATALf (                   LogName logName, const char *const messageFormat, ...);
		void       lFATALln(                   LogName logName, const char *const messageFormat, ...);
	}

	namespace processor
	{
		bool init(LpComponent lpComp, size_t compCount);
	}

	namespace memory
	{
		bool init(LpComponent lpComp, ComponentId id, size_t size);
	}

	namespace storage
	{
		bool init(LpComponent lpComp, ComponentId id, size_t size, void *buf);
	}
}

#endif // VUUZWAAIL_HPP
