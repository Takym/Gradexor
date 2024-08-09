# JsonUrlSaver
Copyright (C) 2024 Takym.

## �T�v
JSON �t�@�C������ URL ���玑�����_�E�����[�h���ĕۑ�����c�[���ł��B
�ŐV�ŋy�у\�[�X�R�[�h�́u<https://github.com/Takym/Gradexor/tree/master/JsonUrlSaver/>�v����_�E�����[�h�ł��܂��B

## �g�p�@

### �_�E�����[�h
���L�̃R�}���h�����s���鎖�ŁA�t�@�C�����_�E�����[�h�ł��܂��B
```cmd
> JsonUrlSaver.exe dir=<JSON �t�@�C�����܂ރf�B���N�g���ւ̊��S�p�X>
```

�_�E�����[�h���ɃG���[���������鎖������܂����A�����I�Ɏ��̃t�@�C���ɑ��s����܂��B

### �\��
�_�E�����[�h�����t�@�C���͉��L�̎菇�ŕ\���ł��܂��B
`mode=openOnly` ��t����ꍇ�A���O�Ƀ_�E�����[�h���K�v�ɂȂ�܂��B

1. ���L�̃R�}���h�Ńc�[�����N�����܂��B
	```cmd
	> JsonUrlSaver.exe /I
	```
2. �c�[���ɓ��͂����߂���̂ŁA���L�̈�����n���܂��B��s����͂���ƁA�����̎󂯕t�����~���܂��B
	```
	dir=<JSON �t�@�C�����܂ރf�B���N�g���ւ̊��S�p�X>
	json=<������ URL ���܂� JSON ����������p���t���œ\��t����>
	mode=openOnly

	```
3. ���L�̗l�ɁAJSON ������ł͂Ȃ� URL �𒼐ړ��͂��鎖���ł��܂��B
	```
	dir=<JSON �t�@�C�����܂ރf�B���N�g���ւ̊��S�p�X>
	url=<�\������t�@�C���� URL>
	mode=openOnly

	```

### �ݒ�t�@�C��
`appSettings.json` �����L�̗l�ɏ��������鎖�ŁA������ `dir=...` ���w�肷��K�v�������Ȃ�܂��B
```json
{
	"dir": "<JSON �t�@�C�����܂ރf�B���N�g���ւ̊��S�p�X>"
}
```

## ���s��
* **OS**: Microsoft Windows 10 �܂��͂���ȍ~
* **�����^�C��**: .NET 8.0 �܂��͂���ȍ~

## ���pSDK
* **Microsoft.NET.Sdk**
	* [.NET �v���W�F�N�g SDK](https://docs.microsoft.com/ja-jp/dotnet/core/project-sdk/overview)
	* ���쌠�\�L�FCopyright (c) .NET Foundation and Contributors
	* ���|�W�g���F<https://github.com/dotnet/sdk>
	* �g�p�����F[MIT���C�Z���X](https://github.com/dotnet/sdk/blob/main/LICENSE.TXT)
* **Microsoft.Extensions.Hosting**
	* ���쌠�\�L�FCopyright (c) .NET Foundation and Contributors
	* �g�p�����F[The MIT License](https://github.com/dotnet/runtime/blob/main/LICENSE.TXT)
	* ���|�W�g���F<https://github.com/dotnet/runtime>
	* �p�b�P�[�W�F<https://www.nuget.org/packages/Microsoft.Extensions.Hosting/>

## ���p�K��
���̃v���O������[MIT���C�Z���X](https://github.com/Takym/Gradexor/blob/master/LICENSE.md)�Ɋ�Â��Ĕz�z����Ă��܂��B
