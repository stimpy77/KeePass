﻿/*
  KeePass Password Safe - The Open-Source Password Manager
  Copyright (C) 2003-2025 Dominik Reichl <dominik.reichl@t-online.de>

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#if !KeePassUAP
using System.Security.Cryptography;
#endif

using KeePassLib.Utility;

namespace KeePassLib.Cryptography
{
	public sealed class HashingStreamEx : Stream
	{
		private readonly Stream m_sBaseStream;
		private readonly bool m_bWriting;
		private HashAlgorithm m_hash;

		private byte[] m_pbFinalHash = null;

		public byte[] Hash
		{
			get { return m_pbFinalHash; }
		}

		public override bool CanRead
		{
			get { return !m_bWriting; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return m_bWriting; }
		}

		public override long Length
		{
			get { return m_sBaseStream.Length; }
		}

		public override long Position
		{
			get { return m_sBaseStream.Position; }
			set { Debug.Assert(false); throw new NotSupportedException(); }
		}

		public HashingStreamEx(Stream sBaseStream, bool bWriting, HashAlgorithm hashAlgorithm)
		{
			if(sBaseStream == null) throw new ArgumentNullException("sBaseStream");

			m_sBaseStream = sBaseStream;
			m_bWriting = bWriting;

#if !KeePassLibSD
			m_hash = (hashAlgorithm ?? new SHA256Managed());
#else // KeePassLibSD
			try { m_hash = HashAlgorithm.Create("SHA256"); }
			catch(Exception) { m_hash = null; }
			try { if(m_hash == null) m_hash = HashAlgorithm.Create(); }
			catch(Exception) { }
			if(m_hash == null) { Debug.Assert(false); return; }
#endif

			// Validate hash algorithm
			if(!m_hash.CanTransformMultipleBlocks)
			{
				Debug.Assert(false);
				m_hash = null;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(m_hash != null)
				{
					try
					{
						m_hash.TransformFinalBlock(MemUtil.EmptyByteArray, 0, 0);
						m_pbFinalHash = m_hash.Hash;
						m_hash.Clear();
					}
					catch(Exception) { Debug.Assert(false); }

					m_hash = null;
				}

				m_sBaseStream.Dispose();
			}

			base.Dispose(disposing);
		}

		public override void Flush()
		{
			m_sBaseStream.Flush();
		}

		public override long Seek(long lOffset, SeekOrigin soOrigin)
		{
			Debug.Assert(false);
			throw new NotSupportedException();
		}

		public override void SetLength(long lValue)
		{
			Debug.Assert(false);
			throw new NotSupportedException();
		}

		public override int Read(byte[] pbBuffer, int nOffset, int nCount)
		{
			if(m_bWriting) { Debug.Assert(false); throw new InvalidOperationException(); }

			int nRead = m_sBaseStream.Read(pbBuffer, nOffset, nCount);
			int nPartialRead = nRead;
			while((nRead < nCount) && (nPartialRead != 0))
			{
				nPartialRead = m_sBaseStream.Read(pbBuffer, nOffset + nRead,
					nCount - nRead);
				nRead += nPartialRead;
			}

#if DEBUG
			byte[] pbOrg = new byte[pbBuffer.Length];
			Array.Copy(pbBuffer, pbOrg, pbBuffer.Length);
#endif

			if((m_hash != null) && (nRead > 0))
				m_hash.TransformBlock(pbBuffer, nOffset, nRead, pbBuffer, nOffset);

#if DEBUG
			Debug.Assert(MemUtil.ArraysEqual(pbBuffer, pbOrg));
#endif

			return nRead;
		}

		public override void Write(byte[] pbBuffer, int nOffset, int nCount)
		{
			if(!m_bWriting) { Debug.Assert(false); throw new InvalidOperationException(); }

#if DEBUG
			byte[] pbOrg = new byte[pbBuffer.Length];
			Array.Copy(pbBuffer, pbOrg, pbBuffer.Length);
#endif

			if((m_hash != null) && (nCount > 0))
				m_hash.TransformBlock(pbBuffer, nOffset, nCount, pbBuffer, nOffset);

#if DEBUG
			Debug.Assert(MemUtil.ArraysEqual(pbBuffer, pbOrg));
#endif

			m_sBaseStream.Write(pbBuffer, nOffset, nCount);
		}
	}
}
