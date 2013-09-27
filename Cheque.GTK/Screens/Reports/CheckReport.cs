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
		// Sorter
		private Gtk.TreeModelSort sorter;

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

			// Create cell renderer
			idCell = new Gtk.CellRendererText ();
			custNameCell = new Gtk.CellRendererText ();
			numCell = new Gtk.CellRendererText ();
			bankCell = new Gtk.CellRendererText ();
			branchCell = new Gtk.CellRendererText ();
			serialCell = new Gtk.CellRendererText ();
			dueDateCell = new Gtk.CellRendererText ();
			valueCell = new Gtk.CellRendererText ();

			// Create treeview columns
			idCol = new Gtk.TreeViewColumn ();
			custNameCol = new Gtk.TreeViewColumn ();
			numCol = new Gtk.TreeViewColumn ();
			bankCol = new Gtk.TreeViewColumn ();
			branchCol = new Gtk.TreeViewColumn ();
			serialCol = new Gtk.TreeViewColumn ();
			dueDateCol = new Gtk.TreeViewColumn ();
			valueCol = new Gtk.TreeViewColumn ();

			// Column titles
			idCol.Title = "Identificação";
			custNameCol.Title = "Nome do Cliente";
			numCol.Title = "Número";
			bankCol.Title = "Banco";
			branchCol.Title = "Agência";
			serialCol.Title = "Série";
			dueDateCol.Title = "Vencimento";
			valueCol.Title = "Valor";

			// Put column in the beginning
			idCol.PackStart (idCell, true);
			custNameCol.PackStart (custNameCell, true);
			numCol.PackStart (numCell, true);
			bankCol.PackStart (bankCell, true);
			branchCol.PackStart (branchCell, true);
			serialCol.PackStart (serialCell, true);
			dueDateCol.PackStart (dueDateCell, true);
			valueCol.PackStart (valueCell, true);

			// Create a list store with all the check information
			checkPropsList = new Gtk.ListStore (typeof(CheckClass), typeof(string));
			// Create a list with all checks objects
			checkList = DataManager.GetChecks ();
			// Add all checks and customer name to the list store
			foreach (CheckClass check in checkList) {
				string name = (DAL.DataManager.GetCustomer (check.CustomerID)).Name;
				checkPropsList.AppendValues (check, name);
			}

			// Set renderers while data changes
			idCol.SetCellDataFunc (idCell, new Gtk.TreeCellDataFunc (RenderCustID));
			custNameCol.SetCellDataFunc (custNameCell, new Gtk.TreeCellDataFunc (RenderCustName));
			numCol.SetCellDataFunc (numCell, new Gtk.TreeCellDataFunc (RenderNumber));
			bankCol.SetCellDataFunc (bankCell, new Gtk.TreeCellDataFunc (RenderBankNumber));
			branchCol.SetCellDataFunc (branchCell, new Gtk.TreeCellDataFunc (RenderBranchNumber));
			serialCol.SetCellDataFunc (serialCell, new Gtk.TreeCellDataFunc (RenderSerial));
			dueDateCol.SetCellDataFunc (dueDateCell, new Gtk.TreeCellDataFunc (RenderDueDate));
			valueCol.SetCellDataFunc (valueCell, new Gtk.TreeCellDataFunc (RenderValue));

			// Add columns to the treeview 
			this.treeview.AppendColumn (idCol);
			this.treeview.AppendColumn (custNameCol);
			this.treeview.AppendColumn (numCol);
			this.treeview.AppendColumn (bankCol);
			this.treeview.AppendColumn (branchCol);
			this.treeview.AppendColumn (serialCol);
			this.treeview.AppendColumn (dueDateCol);
			this.treeview.AppendColumn (valueCol);

			// Expand all columns to fill the screen
			idCol.Expand = true;
			custNameCol.Expand = true;
			numCol.Expand = true;
			bankCol.Expand = true;
			branchCol.Expand = true;
			serialCol.Expand = true;
			dueDateCol.Expand = true;
			valueCol.Expand = true;

			// Add sorting option to columns
			idCol.SortColumnId = 0;
			custNameCol.SortColumnId = 1;
			numCol.SortColumnId = 2;
			bankCol.SortColumnId = 3;
			branchCol.SortColumnId = 4;
			serialCol.SortColumnId = 5;
			dueDateCol.SortColumnId = 6;
			valueCol.SortColumnId = 7;

			// Add filter for searching
			filter = new Gtk.TreeModelFilter (checkPropsList, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);

			// Configure sorter and the sort funcions
			sorter = new Gtk.TreeModelSort (filter);
			sorter.SetSortFunc (0, IDSort);
			sorter.SetSortFunc (1, CustNameSort);
			sorter.SetSortFunc (2, NumSort);
			sorter.SetSortFunc (3, BankSort);
			sorter.SetSortFunc (4, BranchSort);
			sorter.SetSortFunc (5, SerialSort);
			sorter.SetSortFunc (6, DueDateSort);
			sorter.SetSortFunc (7, ValueSort);

			// Set the treeview model
			treeview.Model = sorter;

			// Update Total
			lblTotal.Text = "Total: " + String.Format ("{0:C}", calcTotal ());
		}

		private int IDSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.CustomerID, check2.CustomerID);
		}

		private int CustNameSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			string name1 = (string)model.GetValue (a, 1);
			string name2 = (string)model.GetValue (b, 1);

			return String.Compare (name1, name2, true, CultureInfo.CurrentCulture);
		}

		private int NumSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.Number, check2.Number);
		}

		private int BankSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.BankNumber, check2.BankNumber);
		}

		private int BranchSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.BranchNumber, check2.BranchNumber);
		}

		private int SerialSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.Serial, check2.Serial);
		}

		private int DueDateSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return (check1.DueDate <= check2.DueDate) ? -1 : 1;
		}

		private int ValueSort (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return (check1.Value <= check2.Value) ? -1 : 1;
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
			// Update the total
			lblTotal.Text = "Total: " + String.Format ("{0:C}", calcTotal ());
		}

		private bool FilterTree (Gtk.TreeModel model, Gtk.TreeIter iter)
		{
			CheckClass check = (CheckClass)model.GetValue (iter, 0);
			string name = (string)model.GetValue (iter, 1);

			// Parsing customer name
			string entryNameFormatted = "";
			if ((name != null) || (name == "")) {
				name = Formatter.RemoveDiacritics (name.ToLower ());
				entryNameFormatted = Formatter.RemoveDiacritics (entryName.Text.ToLower ());
			}

			// Parsing the date
			DateTime startDate, endDate;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			startDate = DateTime.Parse (Constants.MIN_CHECK_DATE, formatDate);
			endDate = DateTime.Now;
			bool allDates = true;

			if (!(entryStartDate.Text == "")) {
				if (!(DateTime.TryParse (entryStartDate.Text, formatDate, DateTimeStyles.None, out startDate)))
					startDate = DateTime.Parse (Constants.MIN_CHECK_DATE, formatDate);
			}
			if (!(entryEndDate.Text == "")) {
				if ((DateTime.TryParse (entryEndDate.Text, formatDate, DateTimeStyles.None, out endDate)))
					allDates = false;
			}

			// Selecting valid entries
			if ((entryName.Text == "") || (name.Contains (entryNameFormatted))) {
				if ((entryID.Text == "") || (check.CustomerID.Contains (entryID.Text))) {
					if (check.DueDate >= startDate) {
						if (allDates || check.DueDate <= endDate) {
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

		/// <summary>
		/// Calculates the total based on the filter
		/// </summary>
		/// <returns>The total.</returns>
		private decimal calcTotal ()
		{
			Gtk.TreeIter iter;
			decimal total = 0.00m;

			sorter.GetIterFirst (out iter);

			do {
				CheckClass check = (CheckClass)sorter.GetValue (iter, 0);

				if ((check != null) && (!check.isBlank ())) {
					total += check.Value;
				}

			} while (sorter.IterNext(ref iter));

			return total;
		}
	}
}

