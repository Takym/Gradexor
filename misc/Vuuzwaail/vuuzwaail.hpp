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

	namespace logging
	{
		typedef struct ::tm *LpDateTime;
		typedef std::string  LogLevel;
		typedef std::string  LogName;

#define VZWL_LOG_LEVEL			vzwl::logging::LogLevel
#define VZWL_LOG_LEVEL_NULL		((VZWL_LOG_LEVEL)("NULL" ))
#define VZWL_LOG_LEVEL_PRINT	((VZWL_LOG_LEVEL)("PRINT"))
#define VZWL_LOG_LEVEL_TRACE	((VZWL_LOG_LEVEL)("TRACE"))
#define VZWL_LOG_LEVEL_DEBUG	((VZWL_LOG_LEVEL)("DEBUG"))
#define VZWL_LOG_LEVEL_INFO		((VZWL_LOG_LEVEL)("INFO" ))
#define VZWL_LOG_LEVEL_WARN		((VZWL_LOG_LEVEL)("WARN" ))
#define VZWL_LOG_LEVEL_BUG		((VZWL_LOG_LEVEL)("BUG"  ))
#define VZWL_LOG_LEVEL_ERROR	((VZWL_LOG_LEVEL)("ERROR"))
#define VZWL_LOG_LEVEL_FATAL	((VZWL_LOG_LEVEL)("FATAL"))

#define VZWL_LOG_BEGIN(name)		lTRACEln(name, "Executing `%s`...", __PRETTY_FUNCTION__);
#define VZWL_LOG_ENDED(name)		lTRACEln(name, "Completed `%s`!",   __PRETTY_FUNCTION__);

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
}

#endif // VUUZWAAIL_HPP
