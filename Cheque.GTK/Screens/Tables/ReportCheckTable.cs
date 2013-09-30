using System;
using System.Collections.Generic;
using Cheque.BL;
using Cheque.DAL;

namespace Cheque.GTK.Tables
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ReportCheckTable : Gtk.Bin
	{
		// List Store
		private Gtk.ListStore checkPropsList;
		// Filter
		private Gtk.TreeModelFilter filter;
		// Sorter
		private Gtk.TreeModelSort sorter;
		// Check List
		private List<CheckClass> checkList;

		public ReportCheckTable ()
		{
			this.Build ();
			SetEventHandlers ();
			UpdateModel ();
			ShowAll ();
		}

		private void UpdateModel ()
		{
			// Create a list store with all the check information
			checkPropsList = new Gtk.ListStore (typeof(CheckClass), typeof(string));
			// Create a list with all checks objects
			checkList = DataManager.GetChecks ();
			// Add all checks and customer name to the list store
			foreach (CheckClass check in checkList) {
				string name = (DAL.DataManager.GetCustomer (check.CustomerID)).Name;
				checkPropsList.AppendValues (check, name);
			}
			// Add filter for searching
			filter = new Gtk.TreeModelFilter (checkPropsList, null);
			filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);

			// Configure sorter and the sort funcions
			sorter = new Gtk.TreeModelSort (filter);
			sorter.SetSortFunc (0, CompareCustomerIDFunc);
			sorter.SetSortFunc (1, CompareCustomerNameFunc);
			sorter.SetSortFunc (2, CompareCheckNumberFunc);
			sorter.SetSortFunc (3, CompareBankNumberFunc);
			sorter.SetSortFunc (4, CompareBranchNumberFunc);
			sorter.SetSortFunc (5, CompareSerialFunc);
			sorter.SetSortFunc (6, CompareDueDateFunc);
			sorter.SetSortFunc (7, CompareValueFunc);

			// Set the treeview model
			checktableview.Model = sorter;
		}
		//
		// EVENT HANDLERS
		//
		private void SetEventHandlers ()
		{
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
			radioBtnNotPastDate.Toggled += OnFilterEntryTextChanged;

			checktableview.RowActivated += OnDoubleClickedRow;
		}

		private void OnDoubleClickedRow (object o, Gtk.RowActivatedArgs args)
		{
			Gtk.TreeIter iter;
			sorter.GetIter (out iter, args.Path);

			CheckClass check = (CheckClass)sorter.GetValue (iter, 0);

			Dialogs.CheckNotebook chkBook = new Dialogs.CheckNotebook (check);
			chkBook.Show ();

			chkBook.GetCheckInfo ().DeletedCheck += (object sender, EventArgs e) => UpdateModel ();
		}
		//
		// Tree Iter Compare Functions
		//
		private int CompareCustomerIDFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.CustomerID, check2.CustomerID);
		}

		private int CompareCustomerNameFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			string name1 = (string)model.GetValue (a, 1);
			string name2 = (string)model.GetValue (b, 1);

			return String.Compare (name1, name2, true, System.Globalization.CultureInfo.CurrentCulture);
		}

		private int CompareCheckNumberFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.Number, check2.Number);
		}

		private int CompareBankNumberFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.BankNumber, check2.BankNumber);
		}

		private int CompareBranchNumberFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.BranchNumber, check2.BranchNumber);
		}

		private int CompareSerialFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return Formatter.NumericStringSort (check1.Serial, check2.Serial);
		}

		private int CompareDueDateFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return (check1.DueDate <= check2.DueDate) ? -1 : 1;
		}

		private int CompareValueFunc (Gtk.TreeModel model, Gtk.TreeIter a, Gtk.TreeIter b)
		{
			CheckClass check1 = (CheckClass)model.GetValue (a, 0);
			CheckClass check2 = (CheckClass)model.GetValue (b, 0);

			return (check1.Value <= check2.Value) ? -1 : 1;
		}
		//
		// Filter Functions
		//
		private void OnFilterEntryTextChanged (object o, System.EventArgs args)
		{
			// Since the filter text changed, tell the filter to re-determine which rows to display
			filter.Refilter ();
			// Update the total
			lblTotal.Text = "Total: " + String.Format ("{0:C}", CalculateTotal ());
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
			System.Globalization.DateTimeFormatInfo formatDate = new System.Globalization.DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			startDate = DateTime.Parse (Constants.MIN_CHECK_DATE, formatDate);
			endDate = DateTime.Now;
			bool allDates = true;

			if (!(entryStartDate.Text == "")) {
				if (!(DateTime.TryParse (entryStartDate.Text, formatDate, System.Globalization.DateTimeStyles.None, out startDate)))
					startDate = DateTime.Parse (Constants.MIN_CHECK_DATE, formatDate);
			}
			if (!(entryEndDate.Text == "")) {
				if ((DateTime.TryParse (entryEndDate.Text, formatDate, System.Globalization.DateTimeStyles.None, out endDate)))
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
									if (radioBtnNotPastDate.Active) {
										if (check.DueDate > DateTime.Now)
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
		private decimal CalculateTotal ()
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

