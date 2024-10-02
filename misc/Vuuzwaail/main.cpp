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

ReturnCode main(int argc, char *argv[], char *envp[])
{
#ifdef _DEBUG
	printf("====================================\r\n");
	printf("==== NOW RUNNING IN DEBUG MODE! ====\r\n");
	printf("====================================\r\n");
#endif // _DEBUG

	if (!logging::init()) {
		printf("Log File Error.\r\n");
		return VZWL_RET_FAILED_STARTUP_INIT_LOGGING;
	}

	logging::lTRACEln("args", "argc: %d", argc);

	for (int i = 0; i < argc; ++i) {
		logging::lTRACEln("args", "argv[%d]: %s", i, argv[i]);
	}

	for (int i = 0; envp[i] != nullptr; ++i) {
		logging::lTRACEln("args", "envp[%d]: %s", i, envp[i]);
	}

	if (argc <= 1) {
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
