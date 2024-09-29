# 検証：インターフェースの容量はどれくらいか２
Copyright (C) 2024 Takym.

## 再々検証
`dotnet build` ではなく `dotnet publish` を使う形へ変更した。

## 結果
出力ファイルの大きさが約 500 バイト程度変化した。

## データ
筆者の環境における [`test.cmd`](./test.cmd) の実行結果。

```log
Active code page: 65001
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\DotnetInterfaceSizeWithPublish.Included.csproj を復元しました (203 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\DotnetInterfaceSizeWithPublish.Included.csproj]
  DotnetInterfaceSizeWithPublish.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\win-x64\DotnetInterfaceSizeWithPublish.Included.dll
  DotnetInterfaceSizeWithPublish.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeWithPublish.Included\
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\DotnetInterfaceSizeWithPublish.Excluded.csproj を復元しました (207 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\DotnetInterfaceSizeWithPublish.Excluded.csproj]
  DotnetInterfaceSizeWithPublish.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\win-x64\DotnetInterfaceSizeWithPublish.Excluded.dll
  DotnetInterfaceSizeWithPublish.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeWithPublish.Excluded\
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeWithPublish.Included

2024/09/29  22:51    <DIR>          .
2024/09/29  22:51    <DIR>          ..
2024/09/29  22:51           146,073 DotnetInterfaceSizeWithPublish.Included.exe
               1 File(s)        146,073 bytes
               2 Dir(s)  4,825,454,182,400 bytes free
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeWithPublish\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeWithPublish.Excluded

2024/09/29  22:51    <DIR>          .
2024/09/29  22:51    <DIR>          ..
2024/09/29  22:51           145,561 DotnetInterfaceSizeWithPublish.Excluded.exe
               1 File(s)        145,561 bytes
               2 Dir(s)  4,825,454,182,400 bytes free
```
