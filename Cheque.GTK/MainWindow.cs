using System;
using Gtk;

namespace Cheque.GTK
{
	public partial class MainWindow: Gtk.Window
	{	
		private Frame mainFrame;

		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			this.Maximize ();
			mainFrame = new Frame ();
			containerVbox.Add (mainFrame);

			SetMenuSensitivity (false);
			BL.Password.Password passwd = DAL.DataManager.GetPassword ();
			if (passwd == null) {
				Dialogs.AddPassword dialog = new Cheque.GTK.Dialogs.AddPassword ();
				dialog.SavedPassword += (object sender, EventArgs e) => {
					SetMenuSensitivity (true);
				};
				dialog.Show ();
			} else {
				Dialogs.RequestPassword dialog = new Cheque.GTK.Dialogs.RequestPassword ();
				dialog.VerifiedPassword += (object sender, EventArgs e) => {
					SetMenuSensitivity (true);
				};
				dialog.Show ();
			}
		}

		private void SetMenuSensitivity (bool sensitivity)
		{
			menubar.Sensitive = sensitivity;
			ShowAll ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnCheckActionActivated (object sender, EventArgs e)
		{
			if (mainFrame.Child != null) {
				mainFrame.Remove (mainFrame.Child);
			}
			Tables.AddCheckTable addCheckTable = new Tables.AddCheckTable ();
			mainFrame.Add (addCheckTable);
			ShowAll ();
		}

		protected void OnCustomerActionActivated (object sender, EventArgs e)
		{
			Dialogs.AddCustomerDialog addCustomerDialog = new Dialogs.AddCustomerDialog ();
			addCustomerDialog.Show ();
		}

		protected void OnChecksActionActivated (object sender, EventArgs e)
		{
			if (mainFrame.Child != null) {
				mainFrame.Remove (mainFrame.Child);
			}
			Tables.ReportCheckTable reportTable = new Tables.ReportCheckTable ();
			mainFrame.Add (reportTable);
			ShowAll ();
		}

		protected void OnPasswordActionActivated (object sender, EventArgs e)
		{
			Dialogs.ChangePassword passwdDialog = new Cheque.GTK.Dialogs.ChangePassword ();
			passwdDialog.Show ();
		}
	}

	public class OpenedWidgetEventArgs : EventArgs
	{
		public Gtk.Bin Widget { get; set; }

		public OpenedWidgetEventArgs (Gtk.Bin widg)
		{
			Widget = widg;
		}
	}
}
