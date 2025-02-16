///////                                         //
//////  List Prime Factorizations              ///
/////   Copyright (C) 2025 Takym.             ////
////                                         /////
///     distributed under the MIT License.  //////
//                                         ///////

#nullable enable

#r "System.Console"
using static System.Console;

var fsCsv = new FileStream("primes.csv", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
var srCsv = new StreamReader(fsCsv);
var swCsv = new StreamWriter(fsCsv);

var primes = new List<ulong>();
for (ulong i = 2; i <= 20000 /* ulong.MaxValue */; ++i) {
	ulong n       = i;
	bool  isPrime = true;

	swCsv.Write(n);

	int pCount = primes.Count;
	for (int j = 0; j < pCount; ++j) {
		ulong p       = primes[j];
		int   factors = 0;

		while (n > 0 && (n % p) == 0) {
			++factors;
			n /= p;
		}

		if (factors != 0) {
			isPrime = false;
		}

		swCsv.Write(",{0}", factors);
	}

	if (isPrime) {
		swCsv.Write(",1");
		primes.Add(n);
		WriteLine("new prime found: {0}", n);
	}

	swCsv.WriteLine();
}

swCsv.Flush();
fsCsv.Position = 0;

string buf = srCsv.ReadToEnd();
fsCsv.Position = 0;

int pCount = primes.Count;
for (int j = 0; j < pCount; ++j) {
	swCsv.Write(",{0}", primes[j]);
}
swCsv.WriteLine();
swCsv.WriteLine(buf);

swCsv.Close();
srCsv.Close();
fsCsv.Close();
