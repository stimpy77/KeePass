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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using KeePass.App;
using KeePass.App.Configuration;
using KeePass.Resources;
using KeePass.UI;

using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Security;

namespace KeePass.Forms
{
	public partial class DuplicationForm : Form
	{
		// Copy data from controls to simple member variables, because
		// ApplyTo must work after the dialog has been destroyed.
		private bool m_bExtendTitle = true;
		private bool m_bFieldRefs = false;
		private bool m_bCopyHistory = true;

		public DuplicationForm()
		{
			InitializeComponent();
			GlobalWindowManager.InitializeForm(this);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			GlobalWindowManager.AddWindow(this);

			this.Icon = AppIcons.Default;

			FontUtil.AssignDefaultBold(m_cbAppendCopy);
			FontUtil.AssignDefaultBold(m_cbFieldRefs);
			FontUtil.AssignDefaultBold(m_cbCopyHistory);

			FormDataExchange fdx = new FormDataExchange(this, true, true, true);
			AceDuplication aceDup = Program.Config.Defaults.Duplication;
			fdx.Add(m_cbAppendCopy, aceDup, "ExtendTitle");
			fdx.Add(m_cbFieldRefs, aceDup, "CreateFieldReferences");
			fdx.Add(m_cbCopyHistory, aceDup, "CopyHistory");

			UIUtil.SetEnabledFast(m_cbFieldRefs.Enabled, m_lblFieldRefs, m_lnkFieldRefs);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			GlobalWindowManager.RemoveWindow(this);
		}

		private void OnFieldRefsLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			AppHelp.ShowHelp(AppDefs.HelpTopics.FieldRefs, null);
		}

		private void OnBtnOK(object sender, EventArgs e)
		{
			m_bExtendTitle = m_cbAppendCopy.Checked;
			m_bFieldRefs = m_cbFieldRefs.Checked;
			m_bCopyHistory = m_cbCopyHistory.Checked;
		}

		public void ApplyTo(PwEntry peNew, PwEntry pe, PwDatabase pd)
		{
			if((peNew == null) || (pe == null)) { Debug.Assert(false); return; }
			Debug.Assert(this.IsDisposed);

			Debug.Assert(peNew.Strings.ReadSafe(PwDefs.UserNameField) ==
				pe.Strings.ReadSafe(PwDefs.UserNameField));
			Debug.Assert(peNew.Strings.ReadSafe(PwDefs.PasswordField) ==
				pe.Strings.ReadSafe(PwDefs.PasswordField));

			if(m_bExtendTitle && (pd != null))
			{
				string strTitle = peNew.Strings.ReadSafe(PwDefs.TitleField);
				peNew.Strings.Set(PwDefs.TitleField, new ProtectedString(
					pd.MemoryProtection.ProtectTitle, strTitle + " - " +
					KPRes.CopyOfItem));
			}

			if(m_bFieldRefs && (pd != null))
			{
				string strUser = "{REF:U@I:" + pe.Uuid.ToHexString() + "}";
				peNew.Strings.Set(PwDefs.UserNameField, new ProtectedString(
					pd.MemoryProtection.ProtectUserName, strUser));

				string strPw = "{REF:P@I:" + pe.Uuid.ToHexString() + "}";
				peNew.Strings.Set(PwDefs.PasswordField, new ProtectedString(
					pd.MemoryProtection.ProtectPassword, strPw));
			}

			if(!m_bCopyHistory)
				peNew.History = new PwObjectList<PwEntry>();
		}
	}
}
