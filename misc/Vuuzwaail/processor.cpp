/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

namespace vzwl::processor
{
	typedef struct _COMPONENT_DATA_ {
		size_t      compCount;
		LpComponent components[];
	} ComponentData, *LpComponentData;

	void _send(LpComponent lpThisComp, int32_t key, int32_t data)
	{
		// do nothing
	}

	int32_t _receive(LpComponent lpThisComp, int32_t key)
	{
		// do nothing
		return 0;
	}

	bool _deinit(LpComponent lpThisComp)
	{
		if (lpThisComp->type != Processor) {
			return false;
		}

		auto data = ((LpComponentData)(lpThisComp->data));
		for (size_t i = 0; i < data->compCount; ++i) {
			if (i != lpThisComp->id) {
				auto childComp = data->components[i];
				childComp->deinit(childComp);
			}
		}
		free(data);

		lpThisComp->type    = Unknown;
		lpThisComp->send    = nullptr;
		lpThisComp->receive = nullptr;
		lpThisComp->deinit  = nullptr;
		lpThisComp->data    = nullptr;

		return true;
	}

	bool init(LpComponent lpComp, size_t compCount)
	{
		if (lpComp->type != Unknown || compCount < 1) {
			return false;
		}

		auto data = ((LpComponentData)(malloc(sizeof(ComponentData) + (sizeof(LpComponent) * compCount))));
		if (data == nullptr) {
			return false;
		}
		data->compCount     = compCount;
		data->components[0] = lpComp;

		lpComp->type    = Processor;
		lpComp->id      = 0;
		lpComp->send    = _send;
		lpComp->receive = _receive;
		lpComp->deinit  = _deinit;
		lpComp->data    = data;

		return true;
	}

	bool run(LpComponent lpComp)
	{
		// TODO: 処理装置
	}
}
