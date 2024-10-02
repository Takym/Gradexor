/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

using namespace vzwl;

#define RET(code)                     \
	logging::deinit(VZWL_RET_##code); \
	return VZWL_RET_##code;

ReturnCode run(EntryPointArguments args)
{
	if (!logging::init()) {
		printf("Log File Error.\r\n");
		return VZWL_RET_FAILED_STARTUP_INIT_LOGGING;
	}

	for (int i = 0; i < args.count; ++i) {
		logging::lTRACEln("args", "values[%d]: %s", i, args.values[i]);
	}

	for (int i = 0; args.environmentVariables[i] != nullptr; ++i) {
		logging::lTRACEln("args", "environmentVariables[%d]: %s", i, args.environmentVariables[i]);
	}

	if (args.count <= 1) {
		logging::lPRINTln("", "Usage> ./vuuzwaail.elf");
		RET(FAILED_STARTUP_MISSING_CMDLINE_ARGS);
	}

	Component proc{};
	if (processor::init(&proc, 1)) {
		if (proc.run(&proc)) {
			if (proc.deinit(&proc)) {
				RET(SUCCEEDED);
			} else {
				RET(FAILED_PROCESSOR_DEINIT);
			}
		} else {
			if (proc.deinit(&proc)) {
				RET(FAILED_PROCESSOR_RUN);
			} else {
				RET(FAILED_PROCESSOR_RUN_AND_DEINIT);
			}
		}
	} else {
		RET(FAILED_PROCESSOR_INIT);
	}
}

int main(int argc, char *argv[], char *envp[])
{
#ifdef _DEBUG
	printf("====================================\r\n");
	printf("==== NOW RUNNING IN DEBUG MODE! ====\r\n");
	printf("====================================\r\n");
#endif // _DEBUG

	return ((int)(run({ argc, argv, envp })));
}

#if LOGGER_TESTER
ReturnCode logger_tester(EntryPointArguments args)
{
	if (!logging::init()) {
		printf("Log File Error.\r\n");
		return VZWL_RET_FAILED_STARTUP_INIT_LOGGING;
	}

	logging::lprintf ("", "", "aab");
	logging::lprintln("", "", "bcc");
	logging::lprintf ("", "", "xyz");
	logging::lprintln("", "", "xyz");

#define LOG(lvl)                    \
	logging::l##lvl##f ("", "aab"); \
	logging::l##lvl##ln("", "bcc"); \
	logging::l##lvl##f ("", "xyz"); \
	logging::l##lvl##ln("", "xyz");

	LOG(PRINT);
	LOG(TRACE);
	LOG(DEBUG);
	LOG(INFO );
	LOG(WARN );
	LOG(BUG  );
	LOG(ERROR);
	LOG(FATAL);

	logging::lprintf("123", "abc", "xyz");
	logging::lprintf("456", "def", "uvw");
	logging::lprintln("456", "def", "uvw");

	logging::lPRINTln("this", "abcdefgh");
	logging::lERRORf("a", "123");
	logging::lERRORf("a", "456");
	logging::lFATALf("a", "789");
	logging::lFATALf("a", "abc");
	logging::lFATALln("a", "xyz");
	logging::lFATALln("a", "1qaz2wsx");

	RET(SUCCEEDED);
}
#endif
