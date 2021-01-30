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

