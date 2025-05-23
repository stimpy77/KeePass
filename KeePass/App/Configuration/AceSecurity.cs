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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KeePass.App.Configuration
{
	public sealed class AceSecurity
	{
		public AceSecurity()
		{
		}

		private AceWorkspaceLocking m_wsl = new AceWorkspaceLocking();
		public AceWorkspaceLocking WorkspaceLocking
		{
			get { return m_wsl; }
			set
			{
				if(value == null) throw new ArgumentNullException("value");
				m_wsl = value;
			}
		}

		private AppPolicyFlags m_appPolicy = new AppPolicyFlags();
		public AppPolicyFlags Policy
		{
			get { return m_appPolicy; }
			set
			{
				if(value == null) throw new ArgumentNullException("value");
				m_appPolicy = value;
			}
		}

		private AceMasterPassword m_mp = new AceMasterPassword();
		public AceMasterPassword MasterPassword
		{
			get { return m_mp; }
			set
			{
				if(value == null) throw new ArgumentNullException("value");
				m_mp = value;
			}
		}

		private int m_nMasterKeyTries = 3;
		[DefaultValue(3)]
		public int MasterKeyTries
		{
			get { return m_nMasterKeyTries; }
			set { m_nMasterKeyTries = value; }
		}

		private bool m_bSecureDesktop = false;
		[DefaultValue(false)]
		public bool MasterKeyOnSecureDesktop
		{
			get { return m_bSecureDesktop; }
			set { m_bSecureDesktop = value; }
		}

		private string m_strMasterKeyExpiryRec = string.Empty;
		[DefaultValue("")]
		public string MasterKeyExpiryRec
		{
			get { return m_strMasterKeyExpiryRec; }
			set
			{
				if(value == null) throw new ArgumentNullException("value");
				m_strMasterKeyExpiryRec = value;
			}
		}

		private string m_strMasterKeyExpiryForce = string.Empty;
		[DefaultValue("")]
		public string MasterKeyExpiryForce
		{
			get { return m_strMasterKeyExpiryForce; }
			set
			{
				if(value == null) throw new ArgumentNullException("value");
				m_strMasterKeyExpiryForce = value;
			}
		}

		private bool m_bKeyTrfWeakWarning = true;
		[DefaultValue(true)]
		public bool KeyTransformWeakWarning
		{
			get { return m_bKeyTrfWeakWarning; }
			set { m_bKeyTrfWeakWarning = value; }
		}

		private bool m_bClipClearOnExit = true;
		[DefaultValue(true)]
		public bool ClipboardClearOnExit
		{
			get { return m_bClipClearOnExit; }
			set { m_bClipClearOnExit = value; }
		}

		private int m_nClipClearSeconds = 12;
		[DefaultValue(12)]
		public int ClipboardClearAfterSeconds
		{
			get { return m_nClipClearSeconds; }
			set { m_nClipClearSeconds = value; }
		}

		private bool m_bClipNoPersist = true;
		[DefaultValue(true)]
		public bool ClipboardNoPersist
		{
			get { return m_bClipNoPersist; }
			set { m_bClipNoPersist = value; }
		}

		// The clipboard tools of old Office versions crash when
		// storing the 'Clipboard Viewer Ignore' format using the
		// OleSetClipboard function.
		// Therefore, the default value of the option to use this
		// format should be true if and only if KeePass uses the
		// SetClipboardData function only (i.e. no OLE).
		// Note that the .NET Framework and the UWP seem to use
		// OLE internally.
		private bool m_bUseClipboardViewerIgnoreFmt = true;
		[DefaultValue(true)]
		public bool UseClipboardViewerIgnoreFormat
		{
			get { return m_bUseClipboardViewerIgnoreFmt; }
			set { m_bUseClipboardViewerIgnoreFmt = value; }
		}

		private bool m_bClearKeyCmdLineOpt = true;
		[DefaultValue(true)]
		public bool ClearKeyCommandLineParams
		{
			get { return m_bClearKeyCmdLineOpt; }
			set { m_bClearKeyCmdLineOpt = value; }
		}

		private bool m_bSslCertsAcceptInvalid = false;
		[DefaultValue(false)]
		public bool SslCertsAcceptInvalid
		{
			get { return m_bSslCertsAcceptInvalid; }
			set { m_bSslCertsAcceptInvalid = value; }
		}

		private bool m_bPreventScreenCapture = false;
		[DefaultValue(false)]
		public bool PreventScreenCapture
		{
			get { return m_bPreventScreenCapture; }
			set { m_bPreventScreenCapture = value; }
		}

		// https://keepass.info/help/v2_dev/customize.html#opt
		private bool m_bProtectProcessWithDacl = false;
		[DefaultValue(false)]
		public bool ProtectProcessWithDacl
		{
			get { return m_bProtectProcessWithDacl; }
			set { m_bProtectProcessWithDacl = value; }
		}
	}

	public sealed class AceWorkspaceLocking
	{
		public AceWorkspaceLocking()
		{
		}

		private bool m_bOnMinimize = false;
		[DefaultValue(false)]
		public bool LockOnWindowMinimize
		{
			get { return m_bOnMinimize; }
			set { m_bOnMinimize = value; }
		}

		private bool m_bOnMinimizeToTray = false;
		[DefaultValue(false)]
		public bool LockOnWindowMinimizeToTray
		{
			get { return m_bOnMinimizeToTray; }
			set { m_bOnMinimizeToTray = value; }
		}

		private bool m_bOnSessionSwitch = false;
		[DefaultValue(false)]
		public bool LockOnSessionSwitch
		{
			get { return m_bOnSessionSwitch; }
			set { m_bOnSessionSwitch = value; }
		}

		private bool m_bOnSuspend = false;
		[DefaultValue(false)]
		public bool LockOnSuspend
		{
			get { return m_bOnSuspend; }
			set { m_bOnSuspend = value; }
		}

		private bool m_bOnRemoteControlChange = false;
		[DefaultValue(false)]
		public bool LockOnRemoteControlChange
		{
			get { return m_bOnRemoteControlChange; }
			set { m_bOnRemoteControlChange = value; }
		}

		private uint m_uLockAfterTime = 0;
		public uint LockAfterTime
		{
			get { return m_uLockAfterTime; }
			set { m_uLockAfterTime = value; }
		}

		private uint m_uLockAfterGlobalTime = 0;
		public uint LockAfterGlobalTime
		{
			get { return m_uLockAfterGlobalTime; }
			set { m_uLockAfterGlobalTime = value; }
		}

		private bool m_bExitInsteadOfLockingAfterTime = false;
		[DefaultValue(false)]
		public bool ExitInsteadOfLockingAfterTime
		{
			get { return m_bExitInsteadOfLockingAfterTime; }
			set { m_bExitInsteadOfLockingAfterTime = value; }
		}

		private bool m_bAlwaysExitInsteadOfLocking = false;
		[DefaultValue(false)]
		public bool AlwaysExitInsteadOfLocking
		{
			get { return m_bAlwaysExitInsteadOfLocking; }
			set { m_bAlwaysExitInsteadOfLocking = value; }
		}
	}

	public sealed class AceMasterPassword
	{
		public AceMasterPassword()
		{
		}

		private uint m_uMinLength = 0;
		public uint MinimumLength
		{
			get { return m_uMinLength; }
			set { m_uMinLength = value; }
		}

		private uint m_uMinQuality = 0;
		public uint MinimumQuality
		{
			get { return m_uMinQuality; }
			set { m_uMinQuality = value; }
		}

		private bool m_bRememberWhileOpen = true;
		[DefaultValue(true)]
		public bool RememberWhileOpen
		{
			get { return m_bRememberWhileOpen; }
			set { m_bRememberWhileOpen = value; }
		}
	}
}
