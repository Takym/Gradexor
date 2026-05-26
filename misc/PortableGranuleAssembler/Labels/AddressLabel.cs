/****
 * PortableGranuleAssembler
 * Copyright (C) 2026 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;

namespace PortableGranuleAssembler.Labels
{
	public abstract record class AddressLabel
	{
		public required string Name { get; init; }

		public void Emit(BinaryWriter bw, Emitter em, LabelDereferenceToken ldt)
		{
			ArgumentNullException.ThrowIfNull(bw );
			ArgumentNullException.ThrowIfNull(em );
			ArgumentNullException.ThrowIfNull(ldt);

			this.EmitCore(bw, em, ldt);
		}

		protected abstract void EmitCore(BinaryWriter bw, Emitter em, LabelDereferenceToken ldt);

		protected void EmitCore(BinaryWriter bw, Emitter em, LabelDereferenceToken ldt, long addr)
		{
			long pos  = bw.BaseStream.Position;
			int  size = em.GetDataSize();
			bool isbe = em.IsBigEndian();

			switch (size) {
			case 1:
				sbyte v1 = unchecked((sbyte)(addr));

				bw.Write(v1);
				em.LogOut(ldt, $"0x{v1:X2} = {v1} (label: {this.Name}, pos: 0x{pos:X16} = {pos})");

				if (addr is < sbyte.MinValue or > sbyte.MaxValue) {
					em.LogWarn(ldt, $"The original value \'0x{addr:X16} = {addr}\' is too large or small for 1 byte.");
				}
				break;
			case 2:
				short v2 = unchecked((short)(addr));

				if (isbe) {
					byte[] buf = BitConverter.GetBytes(v2);

					if (BitConverter.IsLittleEndian) {
						buf.Reverse();
					}

					bw.Write(buf);
					em.LogOut(ldt, $"0x{v2:X4} = {v2} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, BE)");
				} else {
					bw.Write(v2);
					em.LogOut(ldt, $"0x{v2:X4} = {v2} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, LE)");
				}

				if (addr is < short.MinValue or > short.MaxValue) {
					em.LogWarn(ldt, $"The original value \'0x{addr:X16} = {addr}\' is too large or small for 2 bytes.");
				}
				break;
			case 4:
				int v4 = unchecked((int)(addr));

				if (isbe) {
					byte[] buf = BitConverter.GetBytes(v4);

					if (BitConverter.IsLittleEndian) {
						buf.Reverse();
					}

					bw.Write(buf);
					em.LogOut(ldt, $"0x{v4:X8} = {v4} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, BE)");
				} else {
					bw.Write(v4);
					em.LogOut(ldt, $"0x{v4:X8} = {v4} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, LE)");
				}

				if (addr is < int.MinValue or > int.MaxValue) {
					em.LogWarn(ldt, $"The original value \'0x{addr:X16} = {addr}\' is too large or small for 4 bytes.");
				}
				break;
			case 8:
				if (isbe) {
					byte[] buf = BitConverter.GetBytes(addr);

					if (BitConverter.IsLittleEndian) {
						buf.Reverse();
					}

					bw.Write(buf);
					em.LogOut(ldt, $"0x{addr:X16} = {addr} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, BE)");
				} else {
					bw.Write(addr);
					em.LogOut(ldt, $"0x{addr:X16} = {addr} (label: {this.Name}, pos: 0x{pos:X16} = {pos}, LE)");
				}
				break;
			default:
				em.LogInternalError(ldt, $"The specified byte size \'{size}\' is invalid.");
				break;
			}
		}
	}
}
