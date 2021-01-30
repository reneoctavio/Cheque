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
	public partial class ChangePassword : Gtk.Dialog
	{
		private BL.Password.Password currentPasswd;

		public ChangePassword ()
		{
			this.Build ();
			currentPasswd = DAL.DataManager.GetPassword ();
			SetSensitivity (false);
			buttonOk.Sensitive = false;

			entryOldPasswd.Visibility = false;
			entryNewPasswd1.Visibility = false;
			entryNewPasswd2.Visibility = false;
		}

		private void SetSensitivity (bool sensitivity)
		{
			entryNewPasswd1.Sensitive = sensitivity;
			entryNewPasswd2.Sensitive = sensitivity;
		}

		private void HandleOldPassword ()
		{
			if (currentPasswd.DecriptedPasswd ().Equals (entryOldPasswd.Text)) {
				SetSensitivity (true);
				entryOldPasswd.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (0, 255, 0));
			} else {
				SetSensitivity (false);
				entryOldPasswd.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (255, 0, 0));
			}
		}

		protected void OnEntryOldPasswdTextInserted (object o, Gtk.TextInsertedArgs args)
		{
			HandleOldPassword ();
		}

		protected void OnEntryOldPasswdTextDeleted (object o, Gtk.TextDeletedArgs args)
		{
			HandleOldPassword ();
		}

		private void HandleNewPassword ()
		{
			if (entryNewPasswd1.Text.Equals (entryNewPasswd2.Text)) {
				buttonOk.Sensitive = true;
				entryNewPasswd2.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (0, 255, 0));
			} else {
				buttonOk.Sensitive = false;
				entryNewPasswd2.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (255, 0, 0));
			}
		}

		protected void OnEntryNewPasswd2TextInserted (object o, Gtk.TextInsertedArgs args)
		{
			HandleNewPassword ();
		}

		protected void OnEntryNewPasswd2TextDeleted (object o, Gtk.TextDeletedArgs args)
		{
			HandleNewPassword ();
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			currentPasswd.SetPassword (entryNewPasswd2.Text);
			DAL.DataManager.SavePassword (currentPasswd);
			Destroy ();
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			Destroy ();
		}
	}
}

