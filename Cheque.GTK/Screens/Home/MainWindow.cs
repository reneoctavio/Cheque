using System;
using Gtk;

namespace Cheque.GTK.Screens
{
	public partial class MainWindow: Gtk.Window
	{	
		public MainWindow (): base (Gtk.WindowType.Toplevel)
		{
			Build ();
			this.Maximize ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnBankActionActivated (object sender, EventArgs e)
		{
			AddBankDialog addBankDialog = new AddBankDialog ();
			addBankDialog.Show ();
		}

		protected void OnCheckActionActivated (object sender, EventArgs e)
		{
			AddCheck addCheck = new AddCheck ();
			addCheck.Show ();
		}

		protected void OnCustomerActionActivated (object sender, EventArgs e)
		{
			AddCustomerDialog addCustomerDialog = new AddCustomerDialog ();
			addCustomerDialog.Show ();
		}

		protected void OnChecksActionActivated (object sender, EventArgs e)
		{
			CheckReport chkReport = new CheckReport ();
			chkReport.Show ();
		}
	}
}
