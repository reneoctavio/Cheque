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
using System.Collections.Generic;
using Gtk;
using Cheque.BL;

namespace Cheque.GTK.Tables
{
	[System.ComponentModel.ToolboxItem(true)]
	public class CheckTableView : TreeView
	{
		// Column List
		public List<TableColumn> columnList { get; private set; }

		public CheckTableView ()
		{
			CreateColumns ();
			AppendColumns ();
			ShowAll ();
		}

		private void CreateColumns ()
		{
			columnList = new List<TableColumn> ();
			columnList.Add (new TableColumn ("Identificação", 0, RenderCustID));
			columnList.Add (new TableColumn ("Nome do Cliente", 1, RenderCustName));
			columnList.Add (new TableColumn ("Número", 2, RenderNumber));
			columnList.Add (new TableColumn ("Banco", 3, RenderBankNumber));
			columnList.Add (new TableColumn ("Agência", 4, RenderBranchNumber));
			columnList.Add (new TableColumn ("Série", 5, RenderSerial));
			columnList.Add (new TableColumn ("Vencimento", 6, RenderDueDate));
			columnList.Add (new TableColumn ("Valor", 7, RenderValue));
		}

		private void AppendColumns ()
		{
			foreach (TableColumn col in columnList) {
				AppendColumn (col.Column);
			}
		}
		//
		// RENDERERS
		//
		private void RenderCustID (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				if (check.CustomerID != null) {
					if (Formatter.IsCPF (check.CustomerID)) {
						(cell as Gtk.CellRendererText).Text = Formatter.FormatCPF (check.CustomerID);
						return;
					} else if (Formatter.IsCNPJ (check.CustomerID)) {
						(cell as Gtk.CellRendererText).Text = Formatter.FormatCNPJ (check.CustomerID);
						return;
					} 
				} 
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderCustName (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			String name = (String)model.GetValue (iter, 1);
			if (name != null) {
				(cell as Gtk.CellRendererText).Text = name;
				return;
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				if (check.Number != null) {
					(cell as Gtk.CellRendererText).Text = check.Number;
					return;
				}
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderBankNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				if (check.BankNumber != null) {
					(cell as Gtk.CellRendererText).Text = check.BankNumber;
					return;
				}
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderBranchNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				if (check.BankNumber != null) {
					(cell as Gtk.CellRendererText).Text = check.BranchNumber;
					return;
				}
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderSerial (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				if (check.Serial != null) {
					(cell as Gtk.CellRendererText).Text = check.Serial;
					return;
				}
			}
			(cell as Gtk.CellRendererText).Text = "";
		}

		private void RenderDueDate (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = BL.Formatter.ConvertDateToString (check.DueDate);
			}
		}

		private void RenderValue (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = String.Format ("{0:C}", check.Value);
			}
		}
	}
}

