/*
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
using System.Drawing;
using System.IO;

using KeePass.Resources;

using KeePassLib;
using KeePassLib.Interfaces;

namespace KeePass.DataExchange
{
	public abstract class FileFormatProvider
	{
		public abstract bool SupportsImport { get; }
		public abstract bool SupportsExport { get; }

		public abstract string FormatName { get; }

		public virtual string DisplayName
		{
			get { return this.FormatName; }
		}

		/// <summary>
		/// Default file name extension, without a leading dot.
		/// If there are multiple default extensions (e.g. "html" and "htm"),
		/// specify all of them separated by a '|' (e.g. "html|htm").
		/// </summary>
		public virtual string DefaultExtension
		{
			get { return string.Empty; }
		}

		/// <summary>
		/// Type of the original application using this format (e.g.
		/// <c>KPRes.PasswordManagers</c> or <c>KPRes.Browser</c>).
		/// </summary>
		public virtual string ApplicationGroup
		{
			get { return KPRes.General; }
		}

		/// <summary>
		/// This property indicates whether entries are only appended
		/// to the end of the root group. This is true for example if
		/// the file format does not support groups (i.e. no hierarchy).
		/// </summary>
		public virtual bool ImportAppendsToRootGroupOnly
		{
			get { return false; }
		}

		public virtual bool RequiresFile
		{
			get { return true; }
		}

		public virtual bool RequiresKey
		{
			get { return false; }
		}

		public virtual bool SupportsUuids
		{
			get { return false; }
		}

		public virtual Image SmallIcon
		{
			get { return null; }
		}

		/// <summary>
		/// If the importer is implemented as a profile for the generic
		/// XML importer, return the profile using this property (in
		/// this case the <c>Import</c> method must not be overridden!).
		/// </summary>
		internal virtual GxiProfile XmlProfile
		{
			get { return null; }
		}

		/// <summary>
		/// Called before the <c>Import</c> method is invoked.
		/// </summary>
		/// <returns>Returns <c>true</c>, if the <c>Import</c> method
		/// can be invoked. If it returns <c>false</c>, something has
		/// failed and the import process should be aborted.</returns>
		public virtual bool TryBeginImport()
		{
			return true;
		}

		/// <summary>
		/// Called before the <c>Export</c> method is invoked.
		/// </summary>
		/// <returns>Returns <c>true</c>, if the <c>Export</c> method
		/// can be invoked. If it returns <c>false</c>, something has
		/// failed and the export process should be aborted.</returns>
		public virtual bool TryBeginExport()
		{
			return true;
		}

		/// <summary>
		/// Import a stream into a database. Throws an exception when an error
		/// occurs. Do not call the base class method when overriding it.
		/// </summary>
		/// <param name="pdStorage">Database into which the data will be imported.</param>
		/// <param name="sInput">Input stream to read the data from.</param>
		/// <param name="slLogger">Status logger. May be <c>null</c>.</param>
		public virtual void Import(PwDatabase pdStorage, Stream sInput,
			IStatusLogger slLogger)
		{
			GxiProfile p = this.XmlProfile;
			if(p != null)
			{
				if(pdStorage == null) throw new ArgumentNullException("pdStorage");

				GxiImporter.Import(pdStorage.RootGroup, sInput, p, pdStorage, slLogger);
				return;
			}

			throw new NotSupportedException();
		}

		/// <summary>
		/// Export data into a stream. Throws an exception when an error
		/// occurs. Do not call the base class method when overriding it.
		/// </summary>
		/// <param name="pwExportInfo">Contains the data source and detailed
		/// information about which entries should be exported.</param>
		/// <param name="sOutput">Output stream to write the data to.</param>
		/// <param name="slLogger">Status logger. May be <c>null</c>.</param>
		/// <returns>Returns <c>true</c>, if the export was successful.
		/// Returns <c>false</c>, if the user has aborted the export process
		/// (like clicking 'Cancel' in an additional export settings dialog).</returns>
		public virtual bool Export(PwExportInfo pwExportInfo, Stream sOutput,
			IStatusLogger slLogger)
		{
			throw new NotSupportedException();
		}
	}
}
