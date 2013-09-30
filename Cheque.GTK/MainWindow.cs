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
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnBankActionActivated (object sender, EventArgs e)
		{
			//AddBankDialog addBankDialog = new AddBankDialog ();
			//addBankDialog.Show ();
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
			//AddCustomerDialog addCustomerDialog = new AddCustomerDialog ();
			//addCustomerDialog.Show ();
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
