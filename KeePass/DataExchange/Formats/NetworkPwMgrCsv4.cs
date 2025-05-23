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

using KeePass.Resources;

using KeePassLib;
using KeePassLib.Interfaces;
using KeePassLib.Utility;

namespace KeePass.DataExchange.Formats
{
	// 4.0+
	internal sealed class NetworkPwMgrCsv4 : FileFormatProvider
	{
		public override bool SupportsImport { get { return true; } }
		public override bool SupportsExport { get { return false; } }

		public override string FormatName { get { return "Network Password Manager CSV"; } }
		public override string DefaultExtension { get { return "csv"; } }
		public override string ApplicationGroup { get { return KPRes.PasswordManagers; } }

		public override void Import(PwDatabase pdStorage, Stream sInput,
			IStatusLogger slLogger)
		{
			string strData = MemUtil.ReadString(sInput, Encoding.Default);

			CsvOptions opt = new CsvOptions();
			opt.BackslashIsEscape = false;
			opt.TextQualifier = char.MaxValue; // No text qualifier

			CsvStreamReaderEx csv = new CsvStreamReaderEx(strData, opt);
			PwGroup pg = pdStorage.RootGroup;
			char[] vGroupSplit = new char[] { '\\' };

			while(true)
			{
				string[] v = csv.ReadLine();
				if(v == null) break;
				if(v.Length < 1) continue;

				for(int i = 0; i < v.Length; ++i)
					v[i] = ParseString(v[i]);

				if(v[0].StartsWith("\\")) // Group
				{
					string strGroup = v[0].Trim(vGroupSplit); // Also from end
					if(strGroup.Length > 0)
					{
						pg = pdStorage.RootGroup.FindCreateSubTree(strGroup,
							vGroupSplit);

						if(v.Length >= 6) pg.Notes = v[5].Trim();
						if((v.Length >= 5) && (v[4].Trim().Length > 0))
						{
							if(pg.Notes.Length > 0)
								pg.Notes += Environment.NewLine + Environment.NewLine;

							pg.Notes += v[4].Trim();
						}
					}
				}
				else // Entry
				{
					PwEntry pe = new PwEntry(true, true);
					pg.AddEntry(pe, true);

					List<string> l = new List<string>(v);
					while(l.Count < 8)
					{
						Debug.Assert(false);
						l.Add(string.Empty);
					}

					ImportUtil.Add(pe, PwDefs.TitleField, l[0], pdStorage);
					ImportUtil.Add(pe, PwDefs.UserNameField, l[1], pdStorage);
					ImportUtil.Add(pe, PwDefs.PasswordField, l[2], pdStorage);
					ImportUtil.Add(pe, PwDefs.UrlField, l[3], pdStorage);
					ImportUtil.Add(pe, PwDefs.NotesField, l[4], pdStorage);

					if(l[5].Length > 0)
						ImportUtil.Add(pe, "Custom 1", l[5], pdStorage);
					if(l[6].Length > 0)
						ImportUtil.Add(pe, "Custom 2", l[6], pdStorage);
					if(l[7].Length > 0)
						ImportUtil.Add(pe, "Custom 3", l[7], pdStorage);
				}
			}
		}

		private static string ParseString(string str)
		{
			if(str == null) { Debug.Assert(false); return string.Empty; }

			str = str.Replace(@"#44", ",");
			str = str.Replace(@"#13", Environment.NewLine);

			return str;
		}
	}
}
