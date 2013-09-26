using System;
using Gtk;

namespace Cheque.GTK
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Screens.MainWindow win = new Screens.MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
