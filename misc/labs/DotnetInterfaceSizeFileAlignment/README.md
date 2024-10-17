# 検証：インターフェースの容量はどれくらいか３
Copyright (C) 2024 Takym.

## 再度検証
`FileAlignment` を指定する事で、アライメントの影響を抑制できないか検証した。

## 結果
アラインメントを最小の 512 に設定したら、インターフェースの有無で容量が変化した。
最大の 8192 では同じ容量の出力が得られた。

## データ
筆者の環境における [`test.cmd`](./test.cmd) の実行結果。

```log
Active code page: 65001
"-p:FileAlignment=512"
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Included.csproj を復元しました (226 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Included.csproj]
  DotnetInterfaceSizeFileAlignment.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\win-x64\DotnetInterfaceSizeFileAlignment.Included.dll
  DotnetInterfaceSizeFileAlignment.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Included\
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Excluded.csproj を復元しました (203 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Excluded.csproj]
  DotnetInterfaceSizeFileAlignment.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\win-x64\DotnetInterfaceSizeFileAlignment.Excluded.dll
  DotnetInterfaceSizeFileAlignment.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Excluded\
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Included

2024/10/17  11:51    <DIR>          .
2024/10/17  11:51    <DIR>          ..
2024/10/17  11:51           146,085 DotnetInterfaceSizeFileAlignment.Included.exe
               1 File(s)        146,085 bytes
               2 Dir(s)  4,811,374,473,216 bytes free
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Excluded

2024/10/17  11:51    <DIR>          .
2024/10/17  11:51    <DIR>          ..
2024/10/17  11:51           145,573 DotnetInterfaceSizeFileAlignment.Excluded.exe
               1 File(s)        145,573 bytes
               2 Dir(s)  4,811,374,473,216 bytes free
"-p:FileAlignment=8192"
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Included.csproj を復元しました (205 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Included.csproj]
  DotnetInterfaceSizeFileAlignment.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\win-x64\DotnetInterfaceSizeFileAlignment.Included.dll
  DotnetInterfaceSizeFileAlignment.Included -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Included\
  復元対象のプロジェクトを決定しています...
  D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Excluded.csproj を復元しました (218 ミリ秒)。
C:\Program Files\dotnet\sdk\9.0.100-rc.1.24452.12\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(326,5): message NETSDK1057: プレビュー版の .NET を使用しています。https://aka.ms/dotnet-support-policy をご覧ください [D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\DotnetInterfaceSizeFileAlignment.Excluded.csproj]
  DotnetInterfaceSizeFileAlignment.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\win-x64\DotnetInterfaceSizeFileAlignment.Excluded.dll
  DotnetInterfaceSizeFileAlignment.Excluded -> D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Excluded\
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Included

2024/10/17  11:52    <DIR>          .
2024/10/17  11:52    <DIR>          ..
2024/10/17  11:52           165,541 DotnetInterfaceSizeFileAlignment.Included.exe
               1 File(s)        165,541 bytes
               2 Dir(s)  4,811,374,350,336 bytes free
 Volume in drive D is Hard Disk Drive
 Volume Serial Number is 9A99-9DAD

 Directory of D:\Takym\Gradexor\misc\labs\DotnetInterfaceSizeFileAlignment\bin\Release\net8.0\publish\win-x64\DotnetInterfaceSizeFileAlignment.Excluded

2024/10/17  11:52    <DIR>          .
2024/10/17  11:52    <DIR>          ..
2024/10/17  11:52           165,541 DotnetInterfaceSizeFileAlignment.Excluded.exe
               1 File(s)        165,541 bytes
               2 Dir(s)  4,811,374,350,336 bytes free
```

## 参考文献
* <https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/compiler-options/advanced#filealignment>
