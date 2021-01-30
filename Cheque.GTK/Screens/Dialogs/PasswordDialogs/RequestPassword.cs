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
	public partial class RequestPassword : Gtk.Dialog
	{
		public delegate void VerifyPasswordEventHandler (object sender, EventArgs e);

		public event VerifyPasswordEventHandler VerifiedPassword;

		protected virtual void OnVerifiedPassword (EventArgs e)
		{
			if (VerifiedPassword != null)
				VerifiedPassword (this, e);
		}

		public RequestPassword ()
		{
			this.Build ();
			entryPassword.Visibility = false;
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			BL.Password.Password savedPasswd = DAL.DataManager.GetPassword ();
			if (savedPasswd.DecriptedPasswd ().Equals (entryPassword.Text)) {
				OnVerifiedPassword (EventArgs.Empty);
				this.Destroy ();
			}
		}
	}
}