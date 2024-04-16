# The BinFuck Interpreter
Copyright (C) 2020-2024 Takym.

[日本語](#概要)

## Summary
BinFuck is an esoteric programming language.
I developed by drawing on [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck).
The naming origin is a programming language like machine language (binary).
Try `1[>rw<]` or `irw>.99998cw<iw` in REPL mode!
Use `Dd_` to dump data.

## How to use

### Run in REPL mode
```cmd
> bfk repl
> 999999999992=3>=>7=>=>3<<<<W
hello
> ir>r[-<+>]<w
1
2
3
```

### Run a script file
```cmd
> bfk run sample.bfk
```

### Show manuals
* Please type the below command to refer to the command-line manual:
	```cmd
	> bfk help
	```
* You can just use the short command `bfk` instead of the long command `csi.exe BinFuck.csx`.

## Terms
This interpreter is distributed under the [MIT License](LICENSE.md).


## 概要
BinFuck は難解プログラミング言語です。
[Brainfuck](https://ja.wikipedia.org/wiki/Brainfuck) を参考に開発しました。
機械語（binary）の様なプログラミング言語であるというのが名称の由来です。
REPL モードで `1[>rw<]` または `irw>.99998cw<iw` を試してみてください。
内部データを表示するには `Dd_` と入力してください。

## 使い方

### REPL モードで実行する
```cmd
> bfk repl
> 999999999992=3>=>7=>=>3<<<<W
hello
> ir>r[-<+>]<w
1
2
3
```

### スクリプトファイルを実行する
```cmd
> bfk run sample.bfk
```

### 説明書を表示する（英語のみ）
* 下記のコマンドからコマンド行引数説明書を表示できます。
	```cmd
	> bfk help
	```
* `csi.exe BinFuck.csx` は `bfk` に省略できます。

## 規約
このインタープリタは[MITライセンス](LICENSE.md)に基づいて配布されています。
