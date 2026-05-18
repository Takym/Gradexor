/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;

namespace PortableGranuleAssembler.Instructions
{
	public abstract class PseudoInstruction
	{
		public abstract IEnumerable<string> EnumerateNames();

		public bool VisitNameToken(NameToken nt, Emitter em, string name)
		{
			ArgumentNullException.ThrowIfNull       (nt  );
			ArgumentNullException.ThrowIfNull       (em  );
			ArgumentException    .ThrowIfNullOrEmpty(name);

			return this.VisitNameTokenCore(nt, em, name);
		}

		protected abstract bool VisitNameTokenCore(NameToken nt, Emitter em, string name);

		public bool VisitNextToken(Token token, Emitter em)
		{
			ArgumentNullException.ThrowIfNull(token);
			ArgumentNullException.ThrowIfNull(em   );

			return this.VisitNextTokenCore(token, em);
		}

		protected abstract bool VisitNextTokenCore(Token token, Emitter em);
	}

	public abstract class CompoundInstruction : PseudoInstruction
	{
		protected List<Token> TokenList           { get;      }
		protected bool        DoesEscapeNextToken { get; set; }

		protected CompoundInstruction()
		{
			this.TokenList = [];
		}

		protected sealed override bool VisitNameTokenCore(NameToken nt, Emitter em, string name)
		{
			this.TokenList.Clear();
			this.DoesEscapeNextToken = false;

			this.InitializeCore(nt, em, name);

			return true;
		}

		protected abstract void InitializeCore(NameToken nt, Emitter em, string name);

		protected sealed override bool VisitNextTokenCore(Token token, Emitter em)
		{
			if (this.NeedArgument()) {
				this.InputArgument(token, em);
			} else if (this.DoesEscapeNextToken) {
				this.DoesEscapeNextToken = false;

				em.LogInfo(token, $"* [{this.TokenList.Count}] = {token.DisplayText}; The next token will be unescaped.");

				this.TokenList.Add(token);
			} else if (token is SeparatorToken) {
				this.Invoke(token, em);

				return false;
			} else if (token is EscapeToken) {
				this.DoesEscapeNextToken = true;

				em.LogInfo(token, $"The next token will be escaped.");
			} else {
				em.LogInfo(token, $"* [{this.TokenList.Count}] = {token.DisplayText}");

				this.TokenList.Add(token);
			}

			return true;
		}

		protected abstract bool NeedArgument (                       );
		protected abstract void InputArgument(Token token, Emitter em);
		protected abstract void Invoke       (Token token, Emitter em);
	}
}
