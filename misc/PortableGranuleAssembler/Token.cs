/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

namespace PortableGranuleAssembler
{
	public abstract record class Token
	{
		public required int     Row      { get; init; }
		public required int     Column   { get; init; }
		public          string? FileName { get; init; }
	}

	public sealed record class NameToken : Token
	{
		public required string Name { get; init; }
	}

	public abstract class LiteralToken<T> : Token
	{
		public required T Value { get; init; }
	}

	public sealed record class IntegerToken : LiteralToken<ulong >;
	public sealed record class StringToken  : LiteralToken<string>;

	public sealed record class UnexpectedToken : Token
	{
		public required char Actual { get; init; }
	}
}
