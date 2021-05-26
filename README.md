# Gradexor - 排他的論理和色彩変化画像
Copyright (C) 2020-2021 Takym.

## 概要
[aclib](http://essen.osask.jp/?aclib05)を利用したグラデーションを描画するプログラムです。

## [AclibMarkup](./AclibMarkup) とは
[aclib](http://essen.osask.jp/?aclib05)を用いたコードを生成するマークアップ言語です。
現在開発中です。
元々は独立した[別のリポジトリ](https://github.com/Takym/AclibMarkup)で保管していましたが、現在は本リポジトリに統合されています。

## ブランチ一覧
* [master](https://github.com/Takym/Gradexor/tree/master) - 最新の **Gradexor** と **AclibMarkup** を保管しています。
* [Legacy/Gradexor](https://github.com/Takym/Gradexor/tree/Legacy/Gradexor) - 古い **Gradexor** を保管しています。
* [Legacy/AclibMarkup](https://github.com/Takym/Gradexor/tree/Legacy/AclibMarkup) - 古い **AclibMarkup** を保管しています。

## 画面
ここではグラデーションのスクリーンショットを紹介します。

### TYPES
**TYPES** を [VcXsrv](https://sourceforge.net/projects/vcxsrv/) 経由で WSL から起動した例です。

[<img src="./Screenshots/Types.png" width="384" />](./Screenshots/Types.png)

### SANKO 「三湖」
三角形の模様が変化していくのが特徴です。
* [動画をダウンロードする](./Screenshots/Sanko.mp4?raw=true)

## 起動方法
0. `bash` を開きます。
	* **Windows** を利用している場合は WSL を開いてください。
1. このリポジトリをクローンします。
	* `git clone https://github.com/Takym/Gradexor.git`
	* `cd Gradexor/src`
2. <http://essen.osask.jp/?aclib05> から最新の **aclib** をダウンロードして `Gradexor` ディレクトリにコピーします。
3. `./gradexor.build.sh` を実行してビルドします。
4. `./gradexor.run_all.sh` を実行します。
	* このコマンドを実行すると、全種類のグラデーションプログラムが起動します。

## 問題点
* 現在はマクロを利用してプログラムを切り替えている。
	* 今後はマクロを使わない形でプログラムの切り替えられる様にする。
	* [議論](https://github.com/Takym/Gradexor/issues/1)
* プログラムを整理・最適化する。
	* [議論](https://github.com/Takym/Gradexor/issues/2)

## 謝辞
このプログラムでは [aclib](http://essen.osask.jp/?aclib05) を利用しています。
製作者の川合秀実さんにこの場を借りてお礼申し上げます。ありがとうございます。
**aclib** は[KL-01ライセンス](http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)に基づいて配布されています。

## 利用規約
このプログラムは[MITライセンス](./LICENSE.md)に基づいて配布されています。
