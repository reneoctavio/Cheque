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
			checkinfo.setInfos (check);
			checkinfo.ShowAll ();
		}

		public NotebookPage.CheckInfo GetCheckInfo ()
		{
			return checkinfo;
		}
	}
}

