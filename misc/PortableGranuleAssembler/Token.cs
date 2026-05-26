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
		public required int     Row         { get; init; }
		public required int     Column      { get; init; }
		public          string? FileName    { get; init; }
		public abstract string  DisplayText { get;       }
	}

	public sealed record class SeparatorToken : Token
	{
		public override string DisplayText => "separator";
	}

	public sealed record class EscapeToken : Token
	{
		public override string DisplayText => "escape";
	}

	public sealed record class BlockIndicatorToken : Token
	{
		public required bool IsBegin { get; init; }
		public          bool IsEnd   => !this.IsBegin;

		public override string DisplayText => $"block_indicator: \'{(this.IsBegin ? '{' : '}')}\'";
	}

	public abstract record class NameLikeToken : Token
	{
		public required string Name { get; init; }
	}

	public sealed record class NameToken : NameLikeToken
	{
		public override string DisplayText => $"name: {this.Name}";
	}

	public abstract record class LabelToken : NameLikeToken;

	public sealed record class LabelDeclarationToken : LabelToken
	{
		public override string DisplayText => $"label_declaration: {this.Name}";
	}

	public sealed record class LabelDereferenceToken : LabelToken
	{
		public override string DisplayText => $"label_dereference: {this.Name}";
	}

	public sealed record class UnexpectedToken : Token
	{
		public required char Actual { get; init; }

		public override string DisplayText => $"unexpected: {this.Actual}";
	}

	public abstract record class LiteralToken<T> : Token
	{
		public required T Value { get; init; }
	}

	public sealed record class IntegerToken : LiteralToken<ulong>
	{
		public override string DisplayText => $"int: 0x{this.Value:X16} = {this.Value}";
	}

	public sealed record class StringToken : LiteralToken<string>
	{
		public override string DisplayText => $"str: {this.Value}";
	}
}
