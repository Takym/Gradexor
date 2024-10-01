/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#pragma once
#ifndef VUUZWAAIL_HPP
#define VUUZWAAIL_HPP

#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>

namespace vzwl
{
	typedef enum _COMPONENT_TYPE_ {
		Unknown,
		Processor,
		Memory,
		Storage
	} ComponentType;

	typedef uint32_t ComponentId;

	typedef struct _COMPONENT_ Component, *LpComponent;

	typedef void    (*SendFunc   )(LpComponent lpThisComp, int32_t key, int32_t data);
	typedef int32_t (*ReceiveFunc)(LpComponent lpThisComp, int32_t key              );
	typedef bool    (*DeinitFunc )(LpComponent lpThisComp                           );

	struct _COMPONENT_ {
		ComponentType  type;
		ComponentId    id;
		SendFunc       send;
		ReceiveFunc    receive;
		DeinitFunc     deinit;
		void          *data;
	};

	namespace processor
	{
		bool init(LpComponent lpComp, size_t compCount);
		bool run (LpComponent lpComp                  );
	}
}

#endif // VUUZWAAIL_HPP
