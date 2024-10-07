/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

namespace vzwl::memory
{
	using namespace logging;
	#define LOG_NAME	((LogName)("memory"))
	#define BEGIN		VZWL_LOG_BEGIN(LOG_NAME);
	#define ENDED		VZWL_LOG_ENDED(LOG_NAME);

	typedef struct _COMPONENT_DATA_ {
	} ComponentData, *LpComponentData;

	static void _send(LpComponent lpThisComp, int32_t key, int32_t data)
	{
		BEGIN;
		ENDED;
	}

	static int32_t _receive(LpComponent lpThisComp, int32_t key)
	{
		BEGIN;
		ENDED;
		return 0;
	}

	static bool _run(LpComponent lpComp)
	{
		BEGIN;
		ENDED;
		return true;
	}

	static bool _deinit(LpComponent lpThisComp)
	{
		BEGIN;

		VZWL_DEINIT_CHECK_TYPE(Memory, memory);

		// TODO: lpThisComp->data

		VZWL_DEINIT_COMMON;

		ENDED;
		return true;
	}

	bool init(LpComponent lpComp, ComponentId id, size_t size)
	{
		BEGIN;

		VZWL_INIT_CHECK_TYPE;

		// TODD: check size
		// TODO: lpThisComp->data
		void *data;

		VZWL_INIT_COMMON(Memory, id);

		ENDED;
		return true;
	}
}
