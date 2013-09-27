using System;
using Cheque.BL;
using Cheque.DAL;
using System.Collections.Generic;
using System.Globalization;

namespace Cheque.GTK.Screens
{
	public partial class CheckReport : Gtk.Window
	{
		private Gtk.ListStore checkPropsList;
		// Cell
		private Gtk.CellRendererText idCell;
		private Gtk.CellRendererText custNameCell;
		private Gtk.CellRendererText numCell;
		private Gtk.CellRendererText bankCell;
		private Gtk.CellRendererText branchCell;
		private Gtk.CellRendererText serialCell;
		private Gtk.CellRendererText dueDateCell;
		private Gtk.CellRendererText valueCell;
		// Column
		private Gtk.TreeViewColumn idCol;
		private Gtk.TreeViewColumn custNameCol;
		private Gtk.TreeViewColumn numCol;
		private Gtk.TreeViewColumn bankCol;
		private Gtk.TreeViewColumn branchCol;
		private Gtk.TreeViewColumn serialCol;
		private Gtk.TreeViewColumn dueDateCol;
		private Gtk.TreeViewColumn valueCol;
		// Lists
		private List<Customer> customerList;
		private List<CheckClass> checkList;
		// Filter
		private Gtk.TreeModelFilter filter;

		public CheckReport () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.Maximize ();

			entryName.Changed += OnFilterEntryTextChanged;
			entryID.Changed += OnFilterEntryTextChanged;
			entryStartDate.Changed += OnFilterEntryTextChanged;
			entryEndDate.Changed += OnFilterEntryTextChanged;
			entryBankNum.Changed += OnFilterEntryTextChanged;
			entryBranchNum.Changed += OnFilterEntryTextChanged;

			radioBtnAll.Active = true;
			radioBtnCashed.Toggled += OnFilterEntryTextChanged;
			radioBtnReturned.Toggled += OnFilterEntryTextChanged;
			radioBtnAll.Toggled += OnFilterEntryTextChanged;


			idCell = new Gtk.CellRendererText ();
			custNameCell = new Gtk.CellRendererText ();
			numCell = new Gtk.CellRendererText ();
			bankCell = new Gtk.CellRendererText ();
			branchCell = new Gtk.CellRendererText ();
			serialCell = new Gtk.CellRendererText ();
			dueDateCell = new Gtk.CellRendererText ();
			valueCell = new Gtk.CellRendererText ();

			idCol = new Gtk.TreeViewColumn ();
			custNameCol = new Gtk.TreeViewColumn ();
			numCol = new Gtk.TreeViewColumn ();
			bankCol = new Gtk.TreeViewColumn ();
			branchCol = new Gtk.TreeViewColumn ();
			serialCol = new Gtk.TreeViewColumn ();
			dueDateCol = new Gtk.TreeViewColumn ();
			valueCol = new Gtk.TreeViewColumn ();

			idCol.Title = "Identificação";
			custNameCol.Title = "Nome do Cliente";
			numCol.Title = "Número";
			bankCol.Title = "Banco";
			branchCol.Title = "Agência";
			serialCol.Title = "Série";
			dueDateCol.Title = "Vencimento";
			valueCol.Title = "Valor";

			idCol.PackStart (idCell, true);
			custNameCol.PackStart (custNameCell, true);
			numCol.PackStart (numCell, true);
			bankCol.PackStart (bankCell, true);
			branchCol.PackStart (branchCell, true);
			serialCol.PackStart (serialCell, true);
			dueDateCol.PackStart (dueDateCell, true);
			valueCol.PackStart (valueCell, true);

			checkPropsList = new Gtk.ListStore (typeof(CheckClass), typeof(string));

			//customerList = DataManager.GetCustomers ();
			checkList = DataManager.GetChecks ();

			foreach (CheckClass check in checkList) {
				string name = (DAL.DataManager.GetCustomer (check.CustomerID)).Name;
				checkPropsList.AppendValues (check, name);
			}

			idCol.SetCellDataFunc (idCell, new Gtk.TreeCellDataFunc (RenderCustID));
			custNameCol.SetCellDataFunc (custNameCell, new Gtk.TreeCellDataFunc (RenderCustName));
			numCol.SetCellDataFunc (numCell, new Gtk.TreeCellDataFunc (RenderNumber));
			bankCol.SetCellDataFunc (bankCell, new Gtk.TreeCellDataFunc (RenderBankNumber));
			branchCol.SetCellDataFunc (branchCell, new Gtk.TreeCellDataFunc (RenderBranchNumber));
			serialCol.SetCellDataFunc (serialCell, new Gtk.TreeCellDataFunc (RenderSerial));
			dueDateCol.SetCellDataFunc (dueDateCell, new Gtk.TreeCellDataFunc (RenderDueDate));
			valueCol.SetCellDataFunc (valueCell, new Gtk.TreeCellDataFunc (RenderValue));

			this.treeview.AppendColumn (idCol);
			this.treeview.AppendColumn (custNameCol);
			this.treeview.AppendColumn (numCol);
			this.treeview.AppendColumn (bankCol);
			this.treeview.AppendColumn (branchCol);
			this.treeview.AppendColumn (serialCol);
			this.treeview.AppendColumn (dueDateCol);
			this.treeview.AppendColumn (valueCol);

			idCol.Expand = true;
			custNameCol.Expand = true;
			numCol.Expand = true;
			bankCol.Expand = true;
			branchCol.Expand = true;
			serialCol.Expand = true;
			dueDateCol.Expand = true;
			valueCol.Expand = true;

			filter = new Gtk.TreeModelFilter (checkPropsList, null);

			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);

			treeview.Model = filter;
			//treeview.Model = checkPropsList;
		}

		private void RenderCustID (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = check.CustomerID;
			}
		}

		private void RenderCustName (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			String name = (String)model.GetValue (iter, 1);
			if (name != null) {
				(cell as Gtk.CellRendererText).Text = name;
			}
		}

		private void RenderNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = check.Number;
			}
		}

		private void RenderBankNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = check.BankNumber;
			}
		}

		private void RenderBranchNumber (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = check.BranchNumber;
			}
		}

		private void RenderSerial (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			if (check != null) {
				(cell as Gtk.CellRendererText).Text = check.Serial;
			}
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

		private void OnFilterEntryTextChanged (object o, System.EventArgs args)
		{
			// Since the filter text changed, tell the filter to re-determine which rows to display
			filter.Refilter ();
		}

		private bool FilterTree (Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			DateTime startDate, endDate;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			startDate = DateTime.Parse (Constants.MIN_CHECK_DATE, formatDate);
			endDate = DateTime.Now;

			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			string name = (string)model.GetValue (iter, 1);
			string entryNameFormatted = "";
			if ((name != null) || (name == "")) {
				name = Formatter.RemoveDiacritics (name.ToLower ());
				entryNameFormatted = Formatter.RemoveDiacritics (entryName.Text.ToLower ());
			}

			// Parsing the date
			if (!(entryStartDate.Text == "")) {
				startDate = DateTime.Parse (entryStartDate.Text, formatDate);
			}
			if (!(entryEndDate.Text == "")) {
				endDate = DateTime.Parse (entryEndDate.Text, formatDate);
			}

			if ((entryName.Text == "") || (name.Contains (entryNameFormatted))) {
				if ((entryID.Text == "") || (check.CustomerID.Contains (entryID.Text))) {
					if ((entryStartDate.Text == "") || (check.DueDate >= startDate)) {
						if ((entryEndDate.Text == "") || (check.DueDate <= endDate)) {
							if ((entryBankNum.Text == "") || (check.BankNumber.Contains (entryBankNum.Text))) {
								if ((entryBranchNum.Text == "") || (check.BranchNumber.Contains (entryBranchNum.Text))) {
									if (radioBtnAll.Active) {
										return true;
									}
									if (radioBtnCashed.Active) {
										if ((check.IsCashed) && (DateTime.Now >= check.DueDate))
											return true;
									}
									if (radioBtnReturned.Active) {
										if (!check.IsCashed)
											return true;
									}
								}
							}
						}
					}
				}
			}
			return false;
		}
	}
}

