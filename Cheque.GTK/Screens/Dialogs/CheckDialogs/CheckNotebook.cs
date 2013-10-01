using System;

namespace Cheque.GTK.Dialogs
{
	public partial class CheckNotebook : Gtk.Window
	{
		public CheckNotebook (Cheque.BL.CheckClass check) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			notebook1.PrevPage ();
			checkinfo.UpdateInfo (check);

			checkinfo.ChangedCheck += (object sender, EventArgs e) => {
				customerinfo1.customer = DAL.DataManager.GetCustomer (check.CustomerID);
				customerinfo1.UpdateInfo ();
				customerinfo1.ShowAll ();
			};

			checkinfo.ShowAll ();

			customerinfo1.customer = DAL.DataManager.GetCustomer (check.CustomerID);
			customerinfo1.UpdateInfo ();
			customerinfo1.ShowAll ();
		}

		public NotebookPage.CheckInfo GetCheckInfo ()
		{
			return checkinfo;
		}
	}
}

