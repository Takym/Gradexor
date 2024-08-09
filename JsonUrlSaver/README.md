# JsonUrlSaver
Copyright (C) 2024 Takym.

## 概要
JSON ファイル内の URL から資源をダウンロードして保存するツールです。
最新版及びソースコードは「<https://github.com/Takym/Gradexor/tree/master/JsonUrlSaver/>」からダウンロードできます。

## 使用法

### ダウンロード
下記のコマンドを実行する事で、ファイルをダウンロードできます。
相対パスでも動作しますが、絶対パス（完全パス）を指定する事を推奨します。
```cmd
> JsonUrlSaver.exe dir=<JSON ファイルを含むディレクトリへのパス>
```

ダウンロード中にエラーが発生する事がありますが、自動的に次のファイルに続行されます。
また、非 JSON ファイルを読み込んだ時のエラーは無視して構いません。

### ZIP ファイルの展開
ディレクトリの代わりに、下記の様に ZIP ファイルを指定する事もできます。
ZIP ファイルは自動的に展開されます。
```cmd
> JsonUrlSaver.exe zip=<JSON ファイルを含む ZIP ファイルへのパス>
```

ZIP ファイルの展開先は `dir=...` で指定できます。
```cmd
> JsonUrlSaver.exe zip=<JSON ファイルを含む ZIP ファイルへのパス> dir=<ZIP ファイルの展開先のパス>
```

展開先が存在する場合、上書きエラーが発生します。回避するには、`zipOverwrite=true` を指定します。
ただし、このエラーを抑制すると、重要なデータが消えてしまう恐れがありますので、注意してご使用ください。

* 展開先を既定値にする場合
	```cmd
	> JsonUrlSaver.exe zip=<JSON ファイルを含む ZIP ファイルへのパス> zipOverwrite=true
	```
* 展開先を指定する場合
	```cmd
	> JsonUrlSaver.exe zip=<JSON ファイルを含む ZIP ファイルへのパス> dir=<ZIP ファイルの展開先のパス> zipOverwrite=true
	```

### Slack からデータをダウンロードする
Slack からエクスポートして得られるアーカイブの JSON ファイルからファイルをダウンロードする時は、
`token=...` で User OAuth Token を指定する必要があります。
また、`filters=slack` を指定し、ダウンロードする URL を絞り込みます。
```cmd
> JsonUrlSaver.exe zip=<Slack のアーカイブへのパス> token=<User OAuth Token> filters=slack
```

ワークスペースのセキュリティを保つ為には、User OAuth Token は使用後に直ちに破棄する様にしてください。

#### User OAuth Token の発行方法
1. <https://api.slack.com/apps/> にアクセスします。
2. 「Create New App」をクリックします。
3. 「From scratch」を選択します。
4. 必要事項を入力してアプリを作成します。
5. 左のメニューの「OAuth & Permissions」をクリックします。
6. 「Scopes」の「Bot Token Scopes」と「User Token Scopes」の両方に「files:read」を追加します。
7. ページ上部の「OAuth Tokens for Your Workspace」セクションからワークスペースへインストールします。
8. 使い終わったら、「Revoke Tokens」ボタンをクリックします。

#### 参考文献
* [https://api.slack.com/web](Using the Slack Web API | Slack)
* [https://api.slack.com/concepts/token-types](Token types | Slack)

### 表示
ダウンロードしたファイルは下記の手順で表示できます。
`mode=openOnly` を付ける場合、事前にダウンロードが必要になります。

1. 下記のコマンドでツールを起動します。
	```cmd
	> JsonUrlSaver.exe /I
	```
2. ツールに入力が求められるので、下記の引数を渡します。空行を入力すると、引数の受け付けを停止します。
	```
	dir=<JSON ファイルを含むディレクトリへのパス>
	json=<ここに URL を含む JSON 文字列を引用符付きで貼り付ける>
	mode=openOnly

	```
3. 下記の様に、JSON 文字列ではなく URL を直接入力する事もできます。
	```
	dir=<JSON ファイルを含むディレクトリへのパス>
	url=<表示するファイルの URL>
	mode=openOnly

	```

### 設定ファイル
`appSettings.json` を下記の様に書き換える事で、引数に `dir=...` を指定する必要が無くなります。
```json
{
	"dir": "<JSON ファイルを含むディレクトリへのパス>"
}
```

ZIP ファイルの展開時の上書きエラーを常に抑制する場合、下記の設定を追加します。
```json
{
	"zipOverwrite": true
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
