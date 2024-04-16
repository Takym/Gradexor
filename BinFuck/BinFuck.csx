///////                                         //
//////  The BinFuck Interpreter                ///
/////   Copyright (C) 2020-2024 Takym.        ////
////                                         /////
///     distributed under the MIT License.  //////
//                                         ///////

#nullable enable
#r "System.Console"
using static System.Console;

string version = "0.0.0.4";

if (Args.Count == 0) {
	ShowUsage();
	return;
}

switch (Args[0].ToLower()) {
case "a":
case "about":
case "v":
case "ver":
case "version":
	ShowVersion();
	break;
case "h":
case "help":
case "m":
case "man":
case "manual":
	ShowHelp();
	break;
case "repl":
	var runner = new Runner(new());
	while (true) {
		Write("> ");
		string? cmd = ReadLine();
		if (!string.IsNullOrEmpty(cmd)) {
			runner.Run(cmd);
			WriteLine();
		}
	}
case "r":
case "run":
	runner = new Runner(LoadSources(Args));
	runner.RunAll();
	break;
default:
	ShowUsage();
	break;
}

List<string> LoadSources(IList<string> args)
{
	var sources = new List<string>();
	for (int i = 1; i < args.Count; ++i) {
		sources.Add(File.ReadAllText(args[i]));
	}
	return sources;
}

void ShowUsage()
{
	WriteLine("Usage> csi.exe BinFuck.csx run <source-file.bfk>");
	WriteLine("Usage> csi.exe BinFuck.csx help");
	WriteLine();
}

void ShowHelp()
{
	WriteLine("The BinFuck Command-line Manual");
	WriteLine("===============================");
	WriteLine();
	WriteLine("Usage> csi.exe BinFuck.csx <command> [options...]");
	ShowUsage();
	WriteLine("Commands:");
	WriteLine("  about        Shows the version information of this interpreter.");
	WriteLine("  help         Shows this command-line manual.");
	WriteLine("  manual       Same as \'help\' command.");
	WriteLine("  version      Same as \'about\' command.");
	WriteLine("  repl         Runs in read-eval-print-loop mode.");
	WriteLine("  run          Runs a BinFuck script file.");
	WriteLine();
	WriteLine("Options for \'run\' command:");
	WriteLine("  File names for source files.");
	WriteLine();
}

void ShowVersion()
{
	WriteLine("The BinFuck Interpreter");
	WriteLine("=======================");
	WriteLine();
	WriteLine("Copyright (C) 2020-2023 Takym.");
	WriteLine();
	WriteLine("BinFuck is an esoteric programming language.");
	WriteLine("Try \'1[>rw<]\' in REPL mode!");
	WriteLine();
	WriteLine("Current Version: {0}", version);
	WriteLine("The Repo: https://github.com/Takym/Gradexor/tree/master/BinFuck/");
	WriteLine();
}

public class Runner
{
	private static readonly StringBuilder _sb = new();
	private        readonly List<string>  _sources;
	private        readonly List<string>  _funcs;
	private        readonly Encoding      _enc;
	private        readonly long[]        _memory;
	private                 int           _index;
	private                 int           _stack_point;
	private                 bool          _do_subtract;
	private                 bool          _rw_int;

	public Runner(List<string> sources, Encoding? enc = null, int memorySize = 2048)
	{
		_sources     = sources;
		_funcs       = new();
		_enc         = enc ?? Encoding.UTF8;
		_memory      = new long[memorySize];
		_stack_point = 0;
		_do_subtract = false;
		_rw_int      = false;
	}

	public void RunAll()
	{
		while (this.RunNext()) ;
	}

	public bool RunNext()
	{
		if (_index < _sources.Count) {
			this.Run(_sources[_index]);
			++_index;
			return true;
		}
		return false;
	}

	public void Run(string s)
	{
		for (int i = 0; i < s.Length; ++i) {
			char ch = s[i];
			switch (ch) {
			case '#': // Comment
				++i;
				while (i < s.Length && s[i] != '#') {
					++i;
				}
				break;
			case '(': // Define a function
				++i;
				int depth = 1;
				while (i < s.Length) {
					ch = s[i];
					if (ch == '(') {
						++depth;
					} else if (ch == ')') {
						--depth;
						if (depth <= 0) {
							break;
						}
					}
					++i;
					_sb.Append(ch);
				}
				int funcid = _funcs.Count;
				_funcs.Add(_sb.ToString());
				_sb.Clear();
				_memory[_stack_point] = funcid;
				break;
			case '@': // Call a function
				int funcCount = _funcs.Count;
				if (funcCount > 0) {
					int funcIdx = unchecked((int)(_memory[_stack_point]));
					if (funcIdx < funcCount) {
						this.Run(_funcs[funcIdx]);
					}
				}
				break;
			case '.': // Clear memory
				_memory[_stack_point] = 0;
				break;
			case >= '0' and <= '9': // Add or subtract a value
				if (_do_subtract) {
					unchecked { _memory[_stack_point] -= ch - '0'; }
				} else {
					unchecked { _memory[_stack_point] += ch - '0'; }
				}
				break;
			case 's': // Set do subtract flag to true
				_do_subtract = true;
				break;
			case 'a': // Set do subtract flag to false
				_do_subtract = false;
				break;
			case 'i': // Set read/write integer flag to true
				_rw_int = true;
				break;
			case 'c': // Set read/write integer flag to false
				_rw_int = false;
				break;
			case 'w': // Write a character or an integer
				if (_rw_int) {
					Write("{0:D}", _memory[_stack_point]);
				} else {
					this.TryWriteChar(0);
				}
				break;
			case 'W': // Write a string
				for (int j = 0; this.TryWriteChar(j); ++j) ;
				break;
			case 'r': // Read a character
				if (_rw_int) {
					_memory[_stack_point] = long.TryParse(ReadLine(), out long result) ? result : 0;
				} else {
					_memory[_stack_point] = Read();
				}
				break;
			case 'R': // Read a string
				byte[] input1 = _enc.GetBytes(ReadLine() ?? string.Empty);
				byte[] input2 = new byte[input1.Length + 8 - (input1.Length & 0b111)];
				Array.Copy(input1, input2, input1.Length);
				for (int j = 0; j < input2.Length; j += 8) {
					int sp = _stack_point + (j >> 3);
					if (sp >= _memory.Length) {
						break;
					}
					_memory[sp] = BitConverter.ToInt64(input2, j);
				}
				break;
			case 'D': // Dump the runner information
				WriteLine("Encoding   : {0}", _enc.EncodingName);
				WriteLine("Memory Size: {0}", _memory.Length);
				WriteLine("Memory Type: {0}", _memory[0].GetType());
				WriteLine("Stack Point: {0}", _stack_point);
				WriteLine("Do Subtract: {0}", _do_subtract);
				WriteLine("R/W Integer: {0}", _rw_int);
				WriteLine("Use the \'d\' instruction to dump the memory.");
				WriteLine("Use the \'_\' instruction to dump the list of functions.");
				break;
			case 'd': // Dump the memory
				Write("Memory:");
				for (int j = 0; j < _memory.Length; ++j) {
					if ((j & 0b11) == 0) {
						WriteLine();
					}
					Write("{0:X16} ", _memory[j]);
				}
				WriteLine();
				break;
			case '_': // Dump the list of functions
				WriteLine("Functions:");
				for (int j = 0; j < _funcs.Count; ++j) {
					WriteLine("#{0}#", j);
					WriteLine(_funcs[j]);
					WriteLine();
				}
				break;
			case '+': // Increment
				unchecked { ++_memory[_stack_point]; }
				break;
			case '-': // Decrement
				unchecked { --_memory[_stack_point]; }
				break;
			case '*': // Twice
				unchecked { _memory[_stack_point] <<= 1; }
				break;
			case '/': // Half
				unchecked { _memory[_stack_point] >>= 1; }
				break;
			case '=': // Copy a value from a left memory to a right memory
				int right = _stack_point + 1;
				if (right >= _memory.Length) {
					right = 0;
				}
				_memory[right] = _memory[_stack_point];
				break;
			case '>': // Go next stack point
				++_stack_point;
				if (i >= _memory.Length) {
					_stack_point = 0;
				}
				break;
			case '<': // Go previous stack point
				--_stack_point;
				if (i < 0) {
					_stack_point = _memory.Length - 1;
				}
				break;
			case '!': // Jump
				i += unchecked((int)(_memory[_stack_point]));
				--i;
				break;
			case '?': // Jump if not zero
				if (_memory[_stack_point] != 0) {
					int j = _stack_point + 1;
					if (j >= _memory.Length) {
						j = 0;
					}
					i += unchecked((int)(_memory[j]));
					--i;
				}
				break;
			case '[': // Loop start
				if (_memory[_stack_point] == 0) {
					do {
						++i;
					} while (i < s.Length && s[i] != ']');
				}
				break;
			case ']': // Loop end
				if (_memory[_stack_point] != 0) {
					do {
						--i;
					} while (i >= 0 && s[i] != '[');
				}
				break;
			// default: /* ignore */ break;
			}
		}
	}

	private bool TryWriteChar(int pos)
	{
		int i = _stack_point + pos;
		if (i >= _memory.Length) {
			return false;
		}
		long ch = _memory[i];
		if (ch == 0) {
			return false;
		}
		Write(_enc.GetString(BitConverter.GetBytes(ch)));
		return true;
	}
}
