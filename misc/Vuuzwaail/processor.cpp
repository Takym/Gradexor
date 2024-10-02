/****
 * Vuuzwaail
 * Copyright (C) 2024 Takym.
 *
 * distributed under the MIT License.
****/

#include "vuuzwaail.hpp"

namespace vzwl::processor
{
	using namespace logging;
	#define LOG_NAME	((LogName)("processor"))
	#define BEGIN		VZWL_LOG_BEGIN(LOG_NAME);
	#define ENDED		VZWL_LOG_ENDED(LOG_NAME);

	typedef struct _COMPONENT_DATA_ {
		size_t      compCount;
		LpComponent components[];
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
		// TODO: 処理装置

		// 順次
		//  システムのバージョン情報取得
		//  API設定 実行環境設定(実行文脈変更) 権限設定
		//  メモリ/レジスタの値コピー
		//  入出力命令
		//  スタック/キュー操作
		//  算術演算 加減乗除剰 和差積商余 符号反転
		//  論理演算 論理積 包括的論理和 排他的論理和 論理否定 論理包含 逆含意
		//  シフト演算 算術 論理 循環 左右
		//  比較演算 等価演算 不等価演算 大小
		// 分岐・繰返
		//  条件分岐 値一致多分岐(SWITCH) 関数呼び出し 返却 JMP/GOTO/BREAK/CONTINUE 指定回数繰返 列挙
		//  API呼び出し

		ENDED;
		return true;
	}

	static bool _deinit(LpComponent lpThisComp)
	{
		BEGIN;

		if (lpThisComp->type != Processor) {
			lWARNln(LOG_NAME, "The specified component is not a processor. Type ID: %d", lpThisComp->type);
			ENDED;
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

		lpThisComp->type    = Uninitialized;
		lpThisComp->send    = nullptr;
		lpThisComp->receive = nullptr;
		lpThisComp->run     = nullptr;
		lpThisComp->deinit  = nullptr;
		lpThisComp->data    = nullptr;

		ENDED;
		return true;
	}

	bool init(LpComponent lpComp, size_t compCount)
	{
		BEGIN;

		if (lpComp->type != Uninitialized) {
			lWARNln(LOG_NAME, "The specified component is initialized already. Type ID: %d", lpComp->type);
			ENDED;
			return false;
		}

		if (compCount < 1) {
			lWARNln(LOG_NAME, "The component count should be greater than or equal to 1. Actual value: %d", compCount);
			ENDED;
			return false;
		}

		auto data = ((LpComponentData)(malloc(sizeof(ComponentData) + (sizeof(LpComponent) * compCount))));
		if (data == nullptr) {
			lWARNln(LOG_NAME, "Could not allocate memory buffer.");
			ENDED;
			return false;
		}
		data->compCount     = compCount;
		data->components[0] = lpComp;

		lpComp->type    = Processor;
		lpComp->id      = 0;
		lpComp->send    = _send;
		lpComp->receive = _receive;
		lpComp->run     = _run;
		lpComp->deinit  = _deinit;
		lpComp->data    = data;

		ENDED;
		return true;
	}
}
