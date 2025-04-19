![KeePass Icon](trans.png)

# ⚠️ NOTICE ⚠️

> **This repository was created on 2025-04-18 by Jon Davis ([stimpy77](https://github.com/stimpy77)) as a downloaded snapshot of KeePass v2.58 source from the official zip archive.**
> 
> The icon `trans.png` was downloaded from the KeePass website.

---

# KeePass Password Safe

KeePass is a free open source password manager, which helps you to manage your passwords in a secure way. You can store all your passwords in one database, which is locked with a master key. So you only have to remember one single master key to unlock the whole database. Database files are encrypted using the best and most secure encryption algorithms currently known (AES-256, ChaCha20 and Twofish).

## Why KeePass?
Today, you have to remember many passwords. You need a password for a lot of websites, your e-mail account, your webserver, network logins, etc. The list is endless. Also, you should use a different password for each account, because if you would use only one password everywhere and someone gets this password, you would have a problem: the thief would have access to all of your accounts.

KeePass is really free, and more than that: it is open source (OSI certified). You can have a look at its full source code and check whether the security features are implemented correctly.

---

# Features

## Strong Security
- Supports AES (Rijndael), ChaCha20, and Twofish encryption for password databases.
- The complete database is encrypted, not only the password fields.
- SHA-256 is used to hash master key components.
- Protection against dictionary and guessing attacks using key derivation functions (AES-KDF, Argon2, ...).
- Process memory protection: passwords are encrypted while KeePass is running.
- Security-enhanced password edit controls.
- Option to prevent certain screen captures.
- [2.x] Master key dialog can be shown on a secure desktop.

## Multiple User Keys
- One master password decrypts the complete database.
- Alternatively, use key files for better security.
- Combine master password and key file for even more security.
- [2.x] Optionally lock the database to the current Windows user account.

## Portable and No Installation Required
- KeePass is portable: can be carried on a USB stick and runs on Windows systems without installation.
- Installer packages are available for those who prefer shortcuts in the Start menu/desktop.
- KeePass doesn't store anything on your system (no registry keys or system INI files).
- Ports for Android, iOS, Linux, MacOS, etc. are available. See the [downloads](https://keepass.info/download.html) page.
- [2.x] Accessibility options for screen readers.

## Import/Export
- Export password list to TXT, HTML, XML, and CSV.
- Import from many file formats, including CSV, TXT, and more than 35 formats out of the box ([see details](https://keepass.info/help/base/importexport.html)).
- Many other file formats supported via plugins.

## Database Management
- A password database consists of only one file that can be transferred easily.
- Support for password groups (organized as trees with subgroups).
- Time fields: creation, last modification, last access, expiration.
- Attach files to password entries (e.g., PGP signatures).
- [2.x] Built-in viewer/editor for attachments.

## Auto-Type, Drag&Drop, Clipboard
- Auto-Type: can type information of the selected entry into dialogs/webforms (fully customizable sequence).
- Global auto-type hot key.
- Drag&drop fields (title, username, password, URL, notes) into other windows.
- Double-click to copy field value to clipboard; timed clipboard clearing.

## Search, Sorting, Languages, Plugins
- Search for specific entries; sort by any column.
- Over 45 languages available ([translations](https://keepass.info/translations.html)).
- Strong random password generator ([details](https://keepass.info/help/base/pwgenerator.html)).
- Plugin architecture for extended functionality ([plugins](https://keepass.info/plugins.html)).
- 100% open source for transparency and extensibility.

---

# Documentation
- [User Guide & Help Center](https://keepass.info/help/base/index.html)
- [Technical FAQ](https://keepass.info/help/base/faq_tech.html)
- [Security Information](https://keepass.info/help/base/security.html)
- [Import/Export Formats](https://keepass.info/help/base/importexport.html)
- [Auto-Type Feature](https://keepass.info/help/base/autotype.html)
- [Accessibility](https://keepass.info/help/base/accessibility.html)

---

# Technical FAQ (Selected)

## Why doesn't KeePass 2.x run on my computer?
KeePass 2.x requires Microsoft .NET Framework ≥ 2.0. Install it from Microsoft or via Windows Update. KeePass 1.x does not require this framework.

## Why does KeePass 2.x crash when starting from a network drive/share?
.NET security policy may disallow running .NET apps from network drives. Copy KeePass to a local disk or adjust security policy (admin rights required).

## Does KeePass 2.x use FIPS-validated algorithm implementations?
KeePass uses .NET-provided cryptographic classes. For FIPS-validated algorithms, ensure .NET Framework 4.8+ and enable FIPS mode in `KeePass.exe.config`.

## Why does KeePass try to connect to the Internet?
KeePass can automatically check for updates. No personal info is sent. Update checks can be enabled/disabled in Options.

## Does the GUI support dark themes?
KeePass supports all system themes, including dark ones, by following the system settings. For custom appearance, see the [KeeTheme plugin](https://keepass.info/plugins.html).

---

# Links and Resources

## Official
- [KeePass Website](https://keepass.info/)
- [Downloads](https://keepass.info/download.html)
- [Plugins](https://keepass.info/plugins.html)
- [Translations](https://keepass.info/translations.html)
- [Help Center](https://keepass.info/help/base/index.html)
- [Screenshots](https://keepass.info/screenshots.html)

## Articles & Reviews
- [First Steps Tutorial at HowToAnswer](https://www.howtoanswer.com/keepass-review-one-of-the-best-password-managers--153.html)
- [Locking Down Your Passwords with KeePass (Tom's Hardware)](https://www.tomshardware.com/reviews/locking-passwords-keepass,806.html)
- [KeePass Review (ProPrivacy)](https://proprivacy.com/password-manager/review/keepass)
- [KeePass Review (VPN Critic)](https://vpncritic.com/keepass-review/)
- [KeePass Review (Slant)](https://www.slant.co/options/2824/~keepass-review)
- [KeePass Review (SuperbWebsiteBuilders)](https://superbwebsitebuilders.com/keepass-review-how-to-keep-your-passwords-safe/)

## Guides & Tutorials
- [KeePass - Getting Started on Windows (PDF)](https://keepass.info/extensions/v2/docs/KeePass2-GS.pdf)
- [Steps to Set Up KeePass (Jacob Egner)](https://jacobegner.blogspot.com/2016/12/steps-to-set-up-keepass.html)
- [How To Install KeePass On Ubuntu And Use It To Manage Passwords](https://www.usessionbuddy.com/post/how-to-install-keepass-on-ubuntu-and-use-it-to-manage-passwords/)

## Videos
- [KeePass Tutorial - Detailed Step by Step Guide](https://www.youtube.com/watch?v=rB-VqKJGHsg)
- [7 KeePass Features You Should Know About](https://www.youtube.com/watch?v=AtWvLodBey8)
- [9 Plugins/Extensions That Will Make Your Life Easier](https://www.youtube.com/watch?v=5JD6bBDCs2A)
- [How To Sync KeePass Across Devices](https://www.youtube.com/watch?v=mbZU_NEtXNU)
- [Best Free Password Manager 2022 - Is KeePass Still the King?](https://www.youtube.com/watch?v=lK1A9Iu3KZU)
- [KeePass Tutorial for the Absolute Newbie](https://www.youtube.com/watch?v=KQuDrKSZkck)

## More
- [Awards and Ratings](https://keepass.info/ratings.html)
- [Legal Contact / Imprint](https://keepass.info/contact.html)
- [Terms & Privacy](https://keepass.info/help/base/terms.html)
- [Acknowledgements](https://keepass.info/help/base/credits.html)
- [Donate](https://keepass.info/donate.html)

---

# About This Repository

This repository is **not** the official KeePass repository. It is a snapshot of the KeePass v2.58 source code, downloaded as a zip from the official website on 2025-04-18 by Jon Davis (GitHub: stimpy77).

For the latest version, support, and official releases, visit the [KeePass website](https://keepass.info/).

---

# License

KeePass is licensed under the [GNU General Public License v2](LICENSE.txt).

---

*This README was generated to closely resemble the structure and content of the KeePass website for clarity and reference, and includes substantial content directly adapted from official KeePass web pages as of April 2025.*
