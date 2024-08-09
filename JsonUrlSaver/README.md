# JsonUrlSaver
Copyright (C) 2024 Takym.

## 概要
JSON ファイル内の URL から資源をダウンロードして保存するツールです。
最新版及びソースコードは「<https://github.com/Takym/Gradexor/tree/master/JsonUrlSaver/>」からダウンロードできます。

## 使用法

### ダウンロード
下記のコマンドを実行する事で、ファイルをダウンロードできます。
```cmd
> JsonUrlSaver.exe dir=<JSON ファイルを含むディレクトリへの完全パス>
```

ダウンロード中にエラーが発生する事がありますが、自動的に次のファイルに続行されます。

### 表示
ダウンロードしたファイルは下記の手順で表示できます。
`mode=openOnly` を付ける場合、事前にダウンロードが必要になります。

1. 下記のコマンドでツールを起動します。
	```cmd
	> JsonUrlSaver.exe /I
	```
2. ツールに入力が求められるので、下記の引数を渡します。空行を入力すると、引数の受け付けを停止します。
	```
	dir=<JSON ファイルを含むディレクトリへの完全パス>
	json=<ここに URL を含む JSON 文字列を引用符付きで貼り付ける>
	mode=openOnly

	```
3. 下記の様に、JSON 文字列ではなく URL を直接入力する事もできます。
	```
	dir=<JSON ファイルを含むディレクトリへの完全パス>
	url=<表示するファイルの URL>
	mode=openOnly

	```

### 設定ファイル
`appSettings.json` を下記の様に書き換える事で、引数に `dir=...` を指定する必要が無くなります。
```json
{
	"dir": "<JSON ファイルを含むディレクトリへの完全パス>"
}
```

## 実行環境
* **OS**: Microsoft Windows 10 またはそれ以降
* **ランタイム**: .NET 8.0 またはそれ以降

## 利用SDK
* **Microsoft.NET.Sdk**
	* [.NET プロジェクト SDK](https://docs.microsoft.com/ja-jp/dotnet/core/project-sdk/overview)
	* 著作権表記：Copyright (c) .NET Foundation and Contributors
	* リポジトリ：<https://github.com/dotnet/sdk>
	* 使用許諾：[MITライセンス](https://github.com/dotnet/sdk/blob/main/LICENSE.TXT)
* **Microsoft.Extensions.Hosting**
	* 著作権表記：Copyright (c) .NET Foundation and Contributors
	* 使用許諾：[The MIT License](https://github.com/dotnet/runtime/blob/main/LICENSE.TXT)
	* リポジトリ：<https://github.com/dotnet/runtime>
	* パッケージ：<https://www.nuget.org/packages/Microsoft.Extensions.Hosting/>

## 利用規約
このプログラムは[MITライセンス](https://github.com/Takym/Gradexor/blob/master/LICENSE.md)に基づいて配布されています。
