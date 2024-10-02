/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

namespace vzwl
{
	bool debugModeEnabled = false;
}

using namespace vzwl;

#define BEGIN	VZWL_LOG_BEGIN("");
#define ENDED	VZWL_LOG_ENDED("");

#define RET(code)                     \
	ENDED;                            \
	logging::deinit(VZWL_RET_##code); \
	return VZWL_RET_##code;

#define STREQU(left, right) \
	if (strcmp((left), (right)) == 0)

ReturnCode runCore(LpCmdlineOptions lpOpt)
{
	BEGIN;

	// TODO: イメージファイル読み込み
	if (lpOpt->argc <= 1) {
		logging::lPRINTln("", "Usage> ./vuuzwaail.elf /?");
		logging::lPRINTln("", "");
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

void initCmdlineOptions(LpCmdlineOptions lpOpt)
{
	BEGIN;

	lpOpt->showLogo    = true;
	lpOpt->showVersion = false;
	lpOpt->showHelp    = false;

	ENDED;
}

void parseCmdlineOptions(LpCmdlineOptions lpOpt)
{
	BEGIN;

	logging::lTRACEln("", "opt.argc: %d", lpOpt->argc);

	for (int i = 0; i < lpOpt->argc; ++i) {
		auto arg = lpOpt->argv[i];
		logging::lTRACEln("", "opt.argv[%d]: %s", i, arg);

		STREQU(arg, "/?") {
			lpOpt->showHelp = true;
		} else STREQU(arg, "/help") {
			lpOpt->showHelp = true;
		} else STREQU(arg, "/nologo") {
			lpOpt->showLogo = false;
		} else STREQU(arg, "/ver") {
			lpOpt->showVersion = true;
		}
	}

	for (int i = 0; lpOpt->envp[i] != nullptr; ++i) {
		logging::lTRACEln("", "opt.envp[%d]: %s", i, lpOpt->envp[i]);
	}

	ENDED;
}

void showLogo(LpCmdlineOptions lpOpt)
{
	BEGIN;

	if (lpOpt->showLogo) {
		logging::lPRINTln(
			"", "*=*=* " VZWL_TITLE " [v%d.%d.%d.%d]",
			VZWL_VERSION_MAJOR,
			VZWL_VERSION_MINOR,
			VZWL_VERSION_PATCH,
			VZWL_VERSION_BUILD
		);

		logging::lPRINTln("", "*=*=* " VZWL_COPYRIGHT);
		logging::lPRINTln("", "");
	}

	ENDED;
}

void showVersion(LpCmdlineOptions lpOpt)
{
	BEGIN;

	if (lpOpt->showVersion) {
		logging::lPRINTln("", "Title             : " VZWL_TITLE);
		logging::lPRINTln(
			"", "Version           : %d.%d.%d.%d",
			VZWL_VERSION_MAJOR,
			VZWL_VERSION_MINOR,
			VZWL_VERSION_PATCH,
			VZWL_VERSION_BUILD
		);
		logging::lPRINTln("", "Author Copyright  : " VZWL_COPYRIGHT);
		logging::lPRINTln("", "Debug Mode Enabled: %s", debugModeEnabled ? "yes" : "no");
		logging::lPRINTln("", "");
	}

	ENDED;
}

void showHelp(LpCmdlineOptions lpOpt)
{
	BEGIN;

	if (lpOpt->showHelp) {
		logging::lPRINTln("", "The Command-Line Argument Manual");
		logging::lPRINTln("", "");
		logging::lPRINTln("", "Usage> ./vuuzwaail.elf [options...]");
		logging::lPRINTln("", "");
		logging::lPRINTln("", "Options:");
		logging::lPRINTln("", " /?         Same with the /help option.");
		logging::lPRINTln("", " /help      Show this manual.");
		logging::lPRINTln("", " /nologo    Suppress to show the title, the version, and the copyright.");
		logging::lPRINTln("", " /ver       Show the version information.");
		logging::lPRINTln("", "");
	}

	ENDED;
}

ReturnCode main(int argc, char *argv[], char *envp[])
{
#ifdef _DEBUG
	printf("====================================\r\n");
	printf("==== NOW RUNNING IN DEBUG MODE! ====\r\n");
	printf("====================================\r\n");
	debugModeEnabled = true;
#endif // _DEBUG

	if (!logging::init()) {
		printf("Log File Error.\r\n");
		return VZWL_RET_FAILED_STARTUP_INIT_LOGGING;
	}

	CmdlineOptions opt{ argc, argv, envp };
	initCmdlineOptions (&opt);
	parseCmdlineOptions(&opt);
	showLogo           (&opt);
	showVersion        (&opt);
	showHelp           (&opt);

	return runCore(&opt);
}
