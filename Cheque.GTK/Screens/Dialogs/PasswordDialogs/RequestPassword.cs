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