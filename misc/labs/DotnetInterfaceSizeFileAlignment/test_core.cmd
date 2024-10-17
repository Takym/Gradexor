@echo off
setlocal

pushd %~dp0

rmdir /S /Q bin
rmdir /S /Q obj

set AlignArg="-p:FileAlignment=%1"
echo %AlignArg%

dotnet publish -c Release -p:PublishProfile=FolderProfile %AlignArg% DotnetInterfaceSizeFileAlignment.Included.csproj
dotnet publish -c Release -p:PublishProfile=FolderProfile %AlignArg% DotnetInterfaceSizeFileAlignment.Excluded.csproj

pushd bin\Release\net8.0\publish\win-x64\

pushd DotnetInterfaceSizeFileAlignment.Included
dir
popd

pushd DotnetInterfaceSizeFileAlignment.Excluded
dir
popd

popd
popd

endlocal
