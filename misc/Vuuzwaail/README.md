# Vuuzwaail
Copyright (C) 2024 Takym.

## 概要
独自設計の仮想機械です。
名称に特に深い意味はありません。乱数を使って生成しました。

## 起動方法
* ビルドしてから起動する場合
	```sh
	> wsl
	$ ./run.sh <引数>
	```
* ビルドせずに起動する場合
	```sh
	> wsl
	$ ./vuuzwaail.elf <引数>
	```
* デバッグする場合
	```sh
	> wsl
	$ ./debug.sh
	(gdb) run <引数>
	```
* ビルドのみを行う場合
	```sh
	> wsl
	$ ./build.sh
	```

## 部品
Vuuzwaail は部品単位で管理されます。
部品は分割不可能であり、単一の機能を持ちます。

### 処理装置

### 記憶装置

## 想定環境
* `wsl -v`
	> ```
	> WSL バージョン: 2.2.4.0
	> カーネル バージョン: 5.15.153.1-2
	> WSLg バージョン: 1.0.61
	> MSRDC バージョン: 1.2.5326
	> Direct3D バージョン: 1.611.1-81528511
	> DXCore バージョン: 10.0.26091.1-240325-1447.ge-release
	> Windows バージョン: 10.0.19045.4894
	> ```
* `lsb_release -a`
	> ```
	> No LSB modules are available.
	> Distributor ID: Ubuntu
	> Description:    Ubuntu 18.04.6 LTS
	> Release:        18.04
	> Codename:       bionic
	> ```
* `bash --version`
	> ```
	> GNU bash, バージョン 4.4.20(1)-release (x86_64-pc-linux-gnu)
	> Copyright (C) 2016 Free Software Foundation, Inc.
	> ライセンス GPLv3+: GNU GPL バージョン 3 またはそれ以降 <http://gnu.org/licenses/gpl.html>
	>
	> This is free software; you are free to change and redistribute it.
	> There is NO WARRANTY, to the extent permitted by law.
	> ```
* `g++ -v`
	> ```
	> Using built-in specs.
	> COLLECT_GCC=g++
	> COLLECT_LTO_WRAPPER=/usr/lib/gcc/x86_64-linux-gnu/7/lto-wrapper
	> OFFLOAD_TARGET_NAMES=nvptx-none
	> OFFLOAD_TARGET_DEFAULT=1
	> Target: x86_64-linux-gnu
	> Configured with: ../src/configure -v --with-pkgversion='Ubuntu 7.5.0-3ubuntu1~18.04' --with-bugurl=file:///usr/share/doc/gcc-7/README.Bugs --enable-languages=c,ada,c++,go,brig,d,fortran,objc,obj-c++ --prefix=/usr --with-gcc-major-version-only --program-suffix=-7 --program-prefix=x86_64-linux-gnu- --enable-shared --enable-linker-build-id --libexecdir=/usr/lib --without-included-gettext --enable-threads=posix --libdir=/usr/lib --enable-nls --enable-bootstrap --enable-clocale=gnu --enable-libstdcxx-debug --enable-libstdcxx-time=yes --with-default-libstdcxx-abi=new --enable-gnu-unique-object --disable-vtable-verify --enable-libmpx --enable-plugin --enable-default-pie --with-system-zlib --with-target-system-zlib --enable-objc-gc=auto --enable-multiarch --disable-werror --with-arch-32=i686 --with-abi=m64 --with-multilib-list=m32,m64,mx32 --enable-multilib --with-tune=generic --enable-offload-targets=nvptx-none --without-cuda-driver --enable-checking=release --build=x86_64-linux-gnu --host=x86_64-linux-gnu --target=x86_64-linux-gnu
	> Thread model: posix
	> gcc version 7.5.0 (Ubuntu 7.5.0-3ubuntu1~18.04)
	> ```
* `gdb -v`
	> ```
	> GNU gdb (Ubuntu 8.1.1-0ubuntu1) 8.1.1
	> Copyright (C) 2018 Free Software Foundation, Inc.
	> License GPLv3+: GNU GPL version 3 or later <http://gnu.org/licenses/gpl.html>
	> This is free software: you are free to change and redistribute it.
	> There is NO WARRANTY, to the extent permitted by law.  Type "show copying"
	> and "show warranty" for details.
	> This GDB was configured as "x86_64-linux-gnu".
	> Type "show configuration" for configuration details.
	> For bug reporting instructions, please see:
	> <http://www.gnu.org/software/gdb/bugs/>.
	> Find the GDB manual and other documentation resources online at:
	> <http://www.gnu.org/software/gdb/documentation/>.
	> For help, type "help".
	> Type "apropos word" to search for commands related to "word".
	> ```
* `ld -v`
	> ```
	> GNU ld (GNU Binutils for Ubuntu) 2.30
	> ```

## 利用規約
このプログラムは[MITライセンス](https://github.com/Takym/Gradexor/blob/master/LICENSE.md)に基づいて配布されています。
