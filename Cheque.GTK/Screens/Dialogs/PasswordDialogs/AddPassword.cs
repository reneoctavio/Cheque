// -------------------------------------------------------------------------
// Cheque - An Application to help you keep track of checks in your custody
// Copyright (C) 2013 Rene Octavio Queiroz Dias
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Source code at <https://github.com/reneoctavio/Cheque>.
// -------------------------------------------------------------------------
using System;

namespace Cheque.GTK.Dialogs
{
	public partial class AddPassword : Gtk.Window
	{
		public delegate void SavedPasswordEventHandler (object sender, EventArgs e);

		public event SavedPasswordEventHandler SavedPassword;

		public AddPassword () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entryPasswd.Visibility = false;
			entryPasswdAgain.Visibility = false;
		}

		protected virtual void OnSavedPassword (EventArgs e)
		{
			if (SavedPassword != null)
				SavedPassword (this, e);
		}

		private void SetPassword ()
		{
			if (entryPasswd.Text.Equals (entryPasswdAgain.Text)) {
				if (!btnAccept.Sensitive) {
					btnAccept.Sensitive = true;
				}
				if (!checkBtnSamePass.Active) {
					checkBtnSamePass.Active = true;
				}
			} else {
				if (btnAccept.Sensitive) {
					btnAccept.Sensitive = false;
				}
				if (checkBtnSamePass.Active) {
					checkBtnSamePass.Active = false;
				}
			}
		}

		protected void OnEntryPasswdAgainTextDeleted (object o, Gtk.TextDeletedArgs args)
		{
			SetPassword ();
		}

		protected void OnEntryPasswdAgainTextInserted (object o, Gtk.TextInsertedArgs args)
		{
			SetPassword ();
		}

		protected void OnBtnAcceptClicked (object sender, EventArgs e)
		{
			BL.Password.Password passwd = new Cheque.BL.Password.Password (entryPasswdAgain.Text);
			DAL.DataManager.SavePassword (passwd);
			OnSavedPassword (EventArgs.Empty);
			this.Destroy ();
		}
	}
}

