#include <stdio.h>

CALLCONV unsigned int func1(unsigned int a, unsigned int b)
{
	return (a & b) ^ (a | b);
}

typedef struct _DATA_PACKET_ {
	unsigned int a, b, c, d;
} DataPacket;

CALLCONV DataPacket func2(DataPacket data)
{
	data.c = data.a & data.b;
	data.d = data.a | data.b;
	return data;
}

CALLCONV void func3(unsigned int a, unsigned int b, unsigned int *c, unsigned int *d)
{
	*c = a & b;
	*d = a | b;
}

CALLCONV int sub(int argc, char *argv[], char *envp[])
{
	printf("Command-line arguments:\r\n");
	for (int i = 0; i < argc; ++i) {
		printf("[%10d]: %s\r\n", i, argv[i]);
	}

	printf("\r\nEnvironment variables:\r\n");
	for (int i = 0; envp[i]; ++i) {
		printf("[%10d]: %s\r\n", i, envp[i]);
	}

	printf("\r\nValue: %d\r\n", func1(1234, 5678));

	DataPacket dp = { 1111, 2222, 0, 0 };
	dp = func2(dp);
	printf("Value: %d, %d\r\n", dp.c, dp.d);

	func3(dp.a, dp.b, &dp.c, &dp.d);
	printf("Value: %d, %d\r\n", dp.c, dp.d);

	return 0;
}

int main(int argc, char *argv[], char *envp[])
{
	return sub(argc, argv, envp);
}
