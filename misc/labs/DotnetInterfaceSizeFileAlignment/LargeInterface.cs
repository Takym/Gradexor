namespace DotnetInterfaceSize
{
#if INCLUDE_LARGE
	public interface LargeInterface
	{
		public int   Version { get; }
		public ulong Counter { get; }

		public IEmpty GetObject  ();
		public void   DoSomething();
		public string Convert    (string original);

		public LargeInterface HogeFugaPiyo(
			int numberArg,
			bool flagArg,
			string optionalArg1 = "this is a default value",
			string optionalArg2 = "あああいうえお",
			params object[] manyValues
		);
	}
#endif
}
