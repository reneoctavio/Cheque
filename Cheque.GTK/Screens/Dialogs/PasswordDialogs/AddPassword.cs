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

