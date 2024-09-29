# 検証：インターフェースの容量はどれくらいか
Copyright (C) 2024 Takym.

## 方法
空のインターフェース（`public interface IEmpty;`）を含むアセンブリと含まないアセンブリの容量を比較する。

### 結果
出力ファイルは、アライメントの影響なのか同じ大きさだった。

## 再検証
内容のある大き目のインターフェースを作ってもう一度検証してみた。

### 結果
やはり同じ大きさで出力された。

## 結論
おそらく .NET のアライメントは不必要に大きい。

## データ
筆者の環境における [`test.cmd`](./test.cmd) の実行結果です。

```log
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\DotnetInterfaceSize.Included.csproj を復元しました (59 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\DotnetInterfaceSize.Included.csproj]
  DotnetInterfaceSize.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\bin\Release\net8.0\DotnetInterfaceSize.Included.dll

ビルドに成功しました。
    0 個の警告
    0 エラー

経過時間 00:00:01.03
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\DotnetInterfaceSize.Excluded.csproj を復元しました (79 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\DotnetInterfaceSize.Excluded.csproj]
  DotnetInterfaceSize.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\bin\Release\net8.0\DotnetInterfaceSize.Excluded.dll

ビルドに成功しました。
    0 個の警告
    0 エラー

経過時間 00:00:01.05
 ドライブ D のボリューム ラベルは Hard Disk Drive です
 ボリューム シリアル番号は 9A99-9DAD です

 D:\Takym\Gradexor\misc\labs\DotnetInterfaceSize\bin\Release\net8.0 のディレクトリ

2024/09/29  22:22    <DIR>          .
2024/09/29  22:22    <DIR>          ..
2024/09/29  22:22               476 DotnetInterfaceSize.Excluded.deps.json
2024/09/29  22:22             5,632 DotnetInterfaceSize.Excluded.dll
2024/09/29  22:22           139,264 DotnetInterfaceSize.Excluded.exe
2024/09/29  22:22               340 DotnetInterfaceSize.Excluded.runtimeconfig.json
2024/09/29  22:22               476 DotnetInterfaceSize.Included.deps.json
2024/09/29  22:22             5,632 DotnetInterfaceSize.Included.dll
2024/09/29  22:22           139,264 DotnetInterfaceSize.Included.exe
2024/09/29  22:22               340 DotnetInterfaceSize.Included.runtimeconfig.json
               8 個のファイル             291,424 バイト
               2 個のディレクトリ  4,825,462,784,000 バイトの空き領域\
```
