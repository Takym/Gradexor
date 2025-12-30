@echo off
pushd %~dp0
wsl -d Ubuntu-24.04 -- ./run.sh %*
popd
