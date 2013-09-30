using System;

namespace Cheque.GTK.Tables
{
	public class TableColumn
	{
		public int ModelColumnNumber { get; private set; }

		public Gtk.CellRenderer Cell { get; private set; }

		public Gtk.TreeViewColumn Column { get; private set; }

		public TableColumn (string name, int sortColumnId, Gtk.TreeCellDataFunc dataFunc)
		{
			Cell = new Gtk.CellRendererText ();
			Column = new Gtk.TreeViewColumn ();

			Column.Title = name;
			Column.SortColumnId = sortColumnId;

			Column.PackStart (Cell, true);
			Column.Expand = true;
			Column.SetCellDataFunc (Cell, dataFunc);
		}
	}
}

