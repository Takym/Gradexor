# Gradexor - 排他的論理和色彩変化画像
Copyright (C) 2020-2021 Takym.

## 概要
[aclib](http://essen.osask.jp/?aclib05) を利用したグラデーションを描画するプログラムです。

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
	* `cd Gradexor`
2. <http://essen.osask.jp/?aclib05> から最新の **aclib** をダウンロードして `Gradexor` ディレクトリにコピーします。
3. `./gradexor.build.sh` を実行してビルドします。
4. `./gradexor.run_all.sh` を実行します。
	* このコマンドを実行すると、全種類のグラデーションプログラムが起動します。

## 問題点
* 現在はマクロを利用してプログラムを切り替えている。
	* 今後はマクロを使わない形でプログラムの切り替えられる様にする。
* プログラムを整理・最適化する。

## 謝辞
このプログラムでは [aclib](http://essen.osask.jp/?aclib05) を利用しています。
製作者の川合秀実さんにこの場を借りてお礼申し上げます。ありがとうございます。
**aclib** は[KL-01ライセンス](http://web.archive.org/web/20040402101233/http://www.imasy.org/~mone/kawaido/license01-1.0.html)に基づいて配布されています。

## 利用規約
このプログラムは[MITライセンス](./LICENSE.md)に基づいて配布されています。
