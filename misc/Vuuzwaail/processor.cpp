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
	}
}
