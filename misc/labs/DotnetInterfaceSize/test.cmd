@echo off

pushd %~dp0

rmdir /S /Q bin
rmdir /S /Q obj

dotnet build -c Release DotnetInterfaceSize.Included.csproj
dotnet build -c Release DotnetInterfaceSize.Excluded.csproj

pushd bin\Release\net8.0
dir
popd
popd
