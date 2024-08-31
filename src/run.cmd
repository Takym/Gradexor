@echo off

setlocal

rem 環境に合わせて下記の画面設定を書き直してください。
if "%DISP%"=="" set DISP=DISPLAY=localhost:0.0

echo %DISP%

pushd %~dp0
wsl ./gradexor.build.sh
wsl %DISP% ./gradexor.run_all.sh
popd

endlocal
