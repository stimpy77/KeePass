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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using KeePassLib.Interfaces;
using KeePassLib.Utility;

#if KeePassLibSD
using KeePassLibSD;
#endif

namespace KeePassLib.Collections
{
	public sealed class StringDictionaryEx : IDeepCloneable<StringDictionaryEx>,
		IEnumerable<KeyValuePair<string, string>>, IEquatable<StringDictionaryEx>
	{
		private readonly SortedDictionary<string, string> m_d =
			new SortedDictionary<string, string>();

		// Non-null if and only if last mod. times should be remembered
		private Dictionary<string, DateTime> m_dLastMod = null;

		public int Count
		{
			get { return m_d.Count; }
		}

		public StringDictionaryEx()
		{
		}

		internal StringDictionaryEx(bool bRememberLastMod)
		{
			if(bRememberLastMod) m_dLastMod = new Dictionary<string, DateTime>();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_d.GetEnumerator();
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return m_d.GetEnumerator();
		}

		public StringDictionaryEx CloneDeep()
		{
			StringDictionaryEx sdNew = new StringDictionaryEx();

			foreach(KeyValuePair<string, string> kvp in m_d)
				sdNew.m_d[kvp.Key] = kvp.Value;

			if(m_dLastMod != null)
				sdNew.m_dLastMod = new Dictionary<string, DateTime>(m_dLastMod);

			Debug.Assert(Equals(sdNew));
			return sdNew;
		}

		public override int GetHashCode()
		{
			int h = ((m_dLastMod != null) ? 0x208F391C : 0);
			foreach(KeyValuePair<string, string> kvp in m_d)
				h += kvp.Key.GetHashCode() + kvp.Value.GetHashCode();
			return h;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as StringDictionaryEx);
		}

		public bool Equals(StringDictionaryEx sdOther)
		{
			if(object.ReferenceEquals(sdOther, this)) return true;
			if(object.ReferenceEquals(sdOther, null)) { Debug.Assert(false); return false; }

			if(m_d.Count != sdOther.m_d.Count) return false;

			foreach(KeyValuePair<string, string> kvp in sdOther.m_d)
			{
				string str = Get(kvp.Key);
				if((str == null) || (str != kvp.Value)) return false;
			}

			int cLastModT = ((m_dLastMod != null) ? m_dLastMod.Count : -1);
			int cLastModO = ((sdOther.m_dLastMod != null) ? sdOther.m_dLastMod.Count : -1);
			if(cLastModT != cLastModO) return false;

			if(m_dLastMod != null)
			{
				foreach(KeyValuePair<string, DateTime> kvp in sdOther.m_dLastMod)
				{
					DateTime? odt = GetLastModificationTime(kvp.Key);
					if(!odt.HasValue) return false;
					if(!TimeUtil.EqualsFloor(odt.Value, kvp.Value)) return false;
				}
			}

			return true;
		}

		public string Get(string strName)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }

			string str;
			m_d.TryGetValue(strName, out str);
			return str;
		}

		internal DateTime? GetLastModificationTime(string strName)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }

			if(m_dLastMod == null) return null;

			DateTime dt;
			if(m_dLastMod.TryGetValue(strName, out dt)) return dt;
			return null;
		}

		public bool Exists(string strName)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }

			return m_d.ContainsKey(strName);
		}

		public void Set(string strName, string strValue)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }
			if(strValue == null) { Debug.Assert(false); throw new ArgumentNullException("strValue"); }

			m_d[strName] = strValue;

			if(m_dLastMod != null) m_dLastMod[strName] = DateTime.UtcNow;
		}

		internal void Set(string strName, string strValue, DateTime? odtLastMod)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }
			if(strValue == null) { Debug.Assert(false); throw new ArgumentNullException("strValue"); }

			m_d[strName] = strValue;

			if(m_dLastMod != null)
			{
				if(odtLastMod.HasValue) m_dLastMod[strName] = odtLastMod.Value;
				else m_dLastMod.Remove(strName);
			}
		}

		public bool Remove(string strName)
		{
			if(strName == null) { Debug.Assert(false); throw new ArgumentNullException("strName"); }

			if(m_dLastMod != null) m_dLastMod.Remove(strName);

			return m_d.Remove(strName);
		}
	}
}
