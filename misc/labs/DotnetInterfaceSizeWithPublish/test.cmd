@echo off
chcp 65001

pushd %~dp0

rmdir /S /Q bin
rmdir /S /Q obj

dotnet publish -c Release -p:PublishProfile=FolderProfile DotnetInterfaceSizeWithPublish.Included.csproj
dotnet publish -c Release -p:PublishProfile=FolderProfile DotnetInterfaceSizeWithPublish.Excluded.csproj

pushd bin\Release\net8.0\publish\win-x64\

pushd DotnetInterfaceSizeWithPublish.Included
dir
popd

pushd DotnetInterfaceSizeWithPublish.Excluded
dir
popd

popd
popd
