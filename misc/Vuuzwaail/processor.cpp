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

		VZWL_DEINIT_CHECK_TYPE(Processor, processor);

		auto data = ((LpComponentData)(lpThisComp->data));
		for (size_t i = 0; i < data->compCount; ++i) {
			if (i != lpThisComp->id) {
				auto childComp = data->components[i];
				childComp->deinit(childComp);
			}
		}
		free(data);

		VZWL_DEINIT_COMMON;

		ENDED;
		return true;
	}

	bool init(LpComponent lpComp, size_t compCount)
	{
		BEGIN;

		VZWL_INIT_CHECK_TYPE;

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

		VZWL_INIT_COMMON(Processor, 0);

		ENDED;
		return true;
	}
}
