/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using PortableGranuleAssembler.Instructions;

namespace PortableGranuleAssembler
{
	public class Emitter : IDisposable
	{
		private static readonly UTF8Encoding    _utf8    = new(       false);
		private static readonly UnicodeEncoding _utf16le = new(false, false);
		private static readonly UnicodeEncoding _utf16be = new(true,  false);
		private static readonly UTF32Encoding   _utf32le = new(false, false);
		private static readonly UTF32Encoding   _utf32be = new(true,  false);

		private          bool                                      _disposed;
		private readonly Stream                                    _stream;
		private readonly BinaryWriter                              _writer;
		private readonly TextWriter                                _logger;
		private readonly Dictionary<string, PseudoInstruction    > _insts;
		private readonly Dictionary<string, ReadOnlyMemory<Token>> _vars;
		private readonly StringBuilder                             _sb;
		private          int                                       _size;
		private          Encoding                                  _enc;

		public bool                                      IsDisposed   => _disposed;
		public Dictionary<string, PseudoInstruction    > Instructions => _insts;
		public Dictionary<string, ReadOnlyMemory<Token>> Variables    => _vars;

		public Emitter(
			Stream                                     stream,
			TextWriter?                                logger = null,
			Dictionary<string, PseudoInstruction    >? insts  = null,
			Dictionary<string, ReadOnlyMemory<Token>>? vars   = null)
		{
			ArgumentNullException.ThrowIfNull(stream);

			_disposed = false;
			_stream   = stream;
			_writer   = new(stream);
			_logger   = logger ?? Console.Out;
			_insts    = insts  ?? [];
			_vars     = vars   ?? [];
			_sb       = new();
			_size     = 1;
			_enc      = _utf8;

			this.AddInstruction(new SetInstruction    ());
			this.AddInstruction(new GetInstruction    ());
			this.AddInstruction(new IncludeInstruction());
			this.AddInstruction(new RepeatInstruction ());
		}

		~Emitter()
		{
			this.Dispose(false);
		}

		public void AddInstruction(PseudoInstruction inst)
		{
			ObjectDisposedException.ThrowIf(_disposed, this);

			foreach (string name in inst.EnumerateNames()) {
				_insts[name] = inst;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual int GetDataSize() => _size;

		protected virtual void SetDataSize(int size, Token token)
		{
			ObjectDisposedException.ThrowIf    (_disposed, this);
			ArgumentNullException  .ThrowIfNull(token          );

			_size = size;

			if (_size == 1) {
				this.LogInfo(token, $"The data size mode is set to 1 byte.");
			} else {
				this.LogInfo(token, $"The data size mode is set to {size} bytes.");
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDataSizeToByte(Token token)
			=> this.SetDataSize(1, token);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDataSizeToWord(Token token)
			=> this.SetDataSize(2, token);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDataSizeToDWord(Token token)
			=> this.SetDataSize(4, token);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDataSizeToQWord(Token token)
			=> this.SetDataSize(8, token);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual Encoding GetTextEncoding() => _enc;

		protected virtual void SetTextEncoding(Encoding enc, Token token, string? encName = null)
		{
			ObjectDisposedException.ThrowIf    (_disposed, this);
			ArgumentNullException  .ThrowIfNull(token          );

			_enc = enc;

			this.LogInfo(token, $"Info: The text encoding mode is set to {encName ?? enc.EncodingName}.");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTextEncodingToUTF8(Token token)
			=> this.SetTextEncoding(_utf8, token, "UTF-8");

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTextEncodingToUTF16LE(Token token)
			=> this.SetTextEncoding(_utf16le, token, "UTF-16 LE");

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTextEncodingToUTF16BE(Token token)
			=> this.SetTextEncoding(_utf16be, token, "UTF-16 BE");

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTextEncodingToUTF32LE(Token token)
			=> this.SetTextEncoding(_utf32le, token, "UTF-32 LE");

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetTextEncodingToUTF32BE(Token token)
			=> this.SetTextEncoding(_utf32be, token, "UTF-32 BE");

		public virtual void Emit(Token token)
		{
			ObjectDisposedException.ThrowIf(_disposed, this);

			switch (token) {
			case IntegerToken it:
				ulong value = it.Value;
				int   size  = this.GetDataSize();

				switch (size) {
				case 1:
					byte v1 = unchecked((byte)(value));

					_writer.Write(v1);
					this.LogOut(it, $"{v1:X2} = {v1}");

					if (value > byte.MaxValue) {
						this.LogWarn(it, $"The original value \'{value:X16} = {value}\' is too large for 1 byte.");
					}
					break;
				case 2:
					ushort v2 = unchecked((ushort)(value));

					_writer.Write(v2);
					this.LogOut(it, $"{v2:X4} = {v2}");

					if (value > ushort.MaxValue) {
						this.LogWarn(it, $"The original value \'{value:X16} = {value}\' is too large for 2 bytes.");
					}
					break;
				case 4:
					uint v4 = unchecked((uint)(value));

					_writer.Write(v4);
					this.LogOut(it, $"{v4:X8} = {v4}");

					if (value > uint.MaxValue) {
						this.LogWarn(it, $"The original value \'{value:X16} = {value}\' is too large for 4 bytes.");
					}
					break;
				case 8:
					_writer.Write(value);
					this.LogOut(it, $"{value:X16} = {value}");
					break;
				default:
					this.LogInternalError(it, $"The specified byte size \'{size}\' is invalid.");
					break;
				}
				break;
			case StringToken st:
				var enc = this.GetTextEncoding();

				if (enc is null) {
					this.LogInternalError(st, $"No text encoding format is specified.");
				} else {
					string text = st.Value;
					byte[] buf  = enc.GetBytes(text);

					_writer.Write(buf);
					this.LogOut(st, $"{this.FromBinaryToString(buf)} = {text}");
				}
				break;
			default:
				this.LogUnexpectedToken(token);
				break;
			}
		}

		public virtual void Log(Token token, string level, FormattableString msg)
		{
			ObjectDisposedException.ThrowIf           (_disposed, this);
			ArgumentNullException  .ThrowIfNull       (token          );
			ArgumentException      .ThrowIfNullOrEmpty(level          );
			ArgumentNullException  .ThrowIfNull       (msg            );

			_logger.WriteLine("{0}:({1},{2}) [{3}] {4}", token.FileName, token.Row, token.Column, level, msg);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogOut(Token token, FormattableString msg)
			=> this.Log(token, "Out", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogNote(Token token, FormattableString msg)
			=> this.Log(token, "Note", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogInfo(Token token, FormattableString msg)
			=> this.Log(token, "Info", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogWarn(Token token, FormattableString msg)
			=> this.Log(token, "Warn", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogError(Token token, FormattableString msg)
			=> this.Log(token, "Error", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogSystemError(Token token, FormattableString msg)
			=> this.Log(token, "System Error", msg);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void LogInternalError(Token token, FormattableString msg)
			=> this.Log(token, "Internal Error", msg);

		public void LogUnexpectedToken(Token token, FormattableString? msg = null)
		{
			if (msg is null) {
				this.LogInternalError(token, $"An unexpected token ({token.DisplayText}) appeared.");
			} else {
				this.LogError(token, $"An unexpected token ({token.DisplayText}) appeared. {msg}");
			}
		}

		public virtual string FromBinaryToString(byte[] buf)
		{
			ObjectDisposedException.ThrowIf    (_disposed, this);
			ArgumentNullException  .ThrowIfNull(buf            );

			_sb.Clear();
			for (int i = 0; i < buf.Length; ++i) {
				_sb.Append(buf[i].ToString("X2"));
			}

			string result = _sb.ToString();
			_sb.Clear();
			return result;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) {
				return;
			}

			if (disposing) {
				_writer.Dispose();
				_stream.Dispose();
				_logger.Dispose();
			}

			_insts.Clear();
			_vars .Clear();
			_sb   .Clear();

			_enc = null!;

			_disposed = true;
		}
	}
}
