using System;
using System.Globalization;

namespace Cheque.GTK.Tables
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class AddCheckTable : Gtk.Bin
	{
		enum ColumnType
		{
			CUSTOMER_ID,
			CUSTOMER_NAME,
			CHECK_NUMBER,
			BANK_NUMBER,
			BRANCH_NUMBER,
			CHECK_SERIAL,
			CHECK_DUEDATE,
			CHECK_VALUE
		}

		private Gtk.ListStore checkPropsList;

		public AddCheckTable ()
		{
			this.Build ();
			CreateListStore ();
			SetCellsEditable ();
			AddEventHandlers ();
			ShowAll ();
		}

		private void CreateListStore ()
		{
			// Create a list store with all the check information
			checkPropsList = new Gtk.ListStore (typeof(BL.CheckClass), typeof(string));
			// Initialize first check
			checkPropsList.AppendValues (new BL.CheckClass (), "");
			checktableview.Model = checkPropsList;
		}

		private void SetCellsEditable ()
		{
			foreach (TableColumn col in checktableview.columnList) {
				if (col.Cell is Gtk.CellRendererText) {
					((Gtk.CellRendererText)col.Cell).Editable = true;
				}
			}
		}

		private void AddEventHandlers ()
		{
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CUSTOMER_ID].Cell).Edited += CustID_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_NUMBER].Cell).Edited += Number_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.BANK_NUMBER].Cell).Edited += BankNumber_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.BRANCH_NUMBER].Cell).Edited += BranchNumber_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_SERIAL].Cell).Edited += Serial_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_DUEDATE].Cell).Edited += DueDate_Edited;
			((Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_VALUE].Cell).Edited += Value_Edited;
		}
		//
		// EDITED EVENT HANDLERS
		//
		private void CustID_Edited (object o, Gtk.EditedArgs args)
		{
			// Set and Get data
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_NUMBER].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.CHECK_NUMBER].Column;
			string text = BL.Formatter.GetNumericID (args.NewText);

			// Get iter
			checkPropsList.GetIter (out iter, treepath);
			// Get Check 
			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

			// Find customer
			BL.Customer customer = DAL.DataManager.GetCustomer (text);

			if (customer != null) {
				check.CustomerID = customer.Identity;
				checkPropsList.SetValue (iter, 1, customer.Name);

				// Move to next cell
				checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
			
			} else if (!text.Equals ("")) {
				Gtk.MessageDialog dialog = YesNoDialog ("Cliente não existente!\nQuer adicioná-lo?");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Yes) {
					dialog.Destroy ();
					Dialogs.AddCustomerDialog addCustDia = new Dialogs.AddCustomerDialog ();
					addCustDia.GetEntryID ().Text = text;
					addCustDia.Run ();
				}
				dialog.Destroy ();
			}
		}

		private void Number_Edited (object o, Gtk.EditedArgs args)
		{
			// Set and Get data
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.BANK_NUMBER].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.BANK_NUMBER].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (text, BL.Constants.LENGTH_NUM_CHECK);

			if (formattedNumber != null) {
				check.Number = formattedNumber;
				// Move to next cell
				checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
			} else if (!text.Equals ("")) {
				InvalidEntryDialog ("Número de cheque inválido!").Run ();
			}
		}

		private void BankNumber_Edited (object o, Gtk.EditedArgs args)
		{
			// Set and Get data
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.BRANCH_NUMBER].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.BRANCH_NUMBER].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (text, BL.Constants.LENGTH_NUM_BANK);

			if (formattedNumber != null) {
				check.BankNumber = formattedNumber;
				// Move to next cell
				checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
			} else if (!text.Equals ("")) {
				InvalidEntryDialog ("Número de banco inválido!").Run ();
			}
		}

		private void BranchNumber_Edited (object o, Gtk.EditedArgs args)
		{
			// Set and Get data
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_SERIAL].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.CHECK_SERIAL].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (text, BL.Constants.LENGTH_NUM_BRANCH);

			if (formattedNumber != null) {
				check.BranchNumber = formattedNumber;
				// Move to next cell
				checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
			} else if (!text.Equals ("")) {
				InvalidEntryDialog ("Número de agência inválido!").Run ();
			}
		}

		private void Serial_Edited (object o, Gtk.EditedArgs args)
		{
			// Set and Get data
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_DUEDATE].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.CHECK_DUEDATE].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);
			check.Serial = text;
			// Move to next cell
			checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
		}

		private void DueDate_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CHECK_VALUE].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.CHECK_VALUE].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

			// Parsing the date
			DateTime dt;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			bool isParsed = DateTime.TryParse (text, formatDate, DateTimeStyles.None, out dt);

			if (isParsed) {
				check.DueDate = dt;
				check.CashDate = dt;
				// Move to next cell
				checktableview.SetCursorOnCell (treepath, nextColumn, nextCell, true);
			} else {
				InvalidEntryDialog ("Data inválida!").Run ();
			}
		}

		private void Value_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			Gtk.TreePath treepath = new Gtk.TreePath (args.Path);
			Gtk.CellRendererText nextCell = (Gtk.CellRendererText)checktableview.columnList [(int)ColumnType.CUSTOMER_ID].Cell;
			Gtk.TreeViewColumn nextColumn = checktableview.columnList [(int)ColumnType.CUSTOMER_ID].Column;
			string text = args.NewText;

			checkPropsList.GetIter (out iter, treepath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

			decimal value;
			bool isParsed = false;
			string[] splittedString = text.Split ();

			// Splitting for removing the $ string and getting the number only
			if (splittedString.Length == 2) {
				isParsed = Decimal.TryParse (splittedString [1], out value);
				if (!isParsed)
					isParsed = Decimal.TryParse (splittedString [0], out value);
			} else {
				isParsed = Decimal.TryParse (splittedString [0], out value);
			}

			if (isParsed) {
				check.Value = value;

				// If row is filled and there is no other row below, add one
				if (isRowFilled (treepath)) {

					if (!checkPropsList.IterNext (ref iter)) {
						// Update Total
						checkPropsList.GetIter (out iter, treepath);
						lblTotal.Text = "Total: " + String.Format ("{0:C}", CalculateTotal ());

						// Move to next cell
						checkPropsList.AppendValues (new BL.CheckClass (), "");
						checkPropsList.IterNext (ref iter);
						checktableview.SetCursorOnCell (checkPropsList.GetPath (iter), nextColumn, nextCell, true);
					}
				}

			} else {
				InvalidEntryDialog ("Valor inválido!").Run ();
			}
		}
		//
		// DIALOGS
		//
		private Gtk.MessageDialog InvalidEntryDialog (string message)
		{
			return new Gtk.MessageDialog (null, 
			                              Gtk.DialogFlags.DestroyWithParent,
			                              Gtk.MessageType.Error, 
			                              Gtk.ButtonsType.Ok, message);
		}

		private Gtk.MessageDialog YesNoDialog (string message)
		{
			return new Gtk.MessageDialog (null, 
			                              Gtk.DialogFlags.DestroyWithParent,
			                              Gtk.MessageType.Question, 
			                              Gtk.ButtonsType.YesNo, message);
		}

		private bool isRowFilled (Gtk.TreePath treePath)
		{
			bool isFilled = true;
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, treePath);

			BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

			// Check any blank field
			if (check.isBlank ()) {
				return false;
			}

			// Check valid customer
			BL.Customer customer = DAL.DataManager.GetCustomer (check.CustomerID);
			if (customer == null) {
				InvalidEntryDialog ("Cliente inexistente!").Run ();
				isFilled = false;
			}

			// Check valid number
			String formattedNumber = BL.Formatter.ZeroPad (check.Number, BL.Constants.LENGTH_NUM_CHECK);
			if (formattedNumber == null) {
				InvalidEntryDialog ("Número de cheque inválido!").Run ();
				isFilled = false;
			}

			// Check valid bank number 
			formattedNumber = BL.Formatter.ZeroPad (check.BankNumber, BL.Constants.LENGTH_NUM_BANK);
			if (formattedNumber == null) {
				InvalidEntryDialog ("Número de banco inválido!").Run ();
				isFilled = false;
			}

			// Check valid branch number
			formattedNumber = BL.Formatter.ZeroPad (check.BranchNumber, BL.Constants.LENGTH_NUM_BRANCH);
			if (formattedNumber == null) {
				InvalidEntryDialog ("Número de agência inválido!").Run ();
				isFilled = false;
			}

			// Check valid due date
			DateTime dt;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			DateTime.TryParse (BL.Constants.MIN_CHECK_DATE, formatDate, DateTimeStyles.None, out dt);
			if (check.DueDate < dt) {
				InvalidEntryDialog ("Data inválida!").Run ();
				isFilled = false;
			}

			return isFilled;
		}

		private decimal CalculateTotal ()
		{
			Gtk.TreeIter iter;
			decimal total = 0.00m;

			checkPropsList.GetIterFirst (out iter);

			do {
				BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

				if ((check != null) && (!check.isBlank ())) {
					total += check.Value;
				}

			} while (checkPropsList.IterNext(ref iter));

			return total;
		}

		protected void OnBtnSaveClicked (object sender, EventArgs e)
		{
			Gtk.TreeIter iter;

			checkPropsList.GetIterFirst (out iter);

			do {
				BL.CheckClass check = (BL.CheckClass)checkPropsList.GetValue (iter, 0);

				if ((check != null) && (!check.isBlank ())) {
					DAL.DataManager.AddCheck (check);

					// Add bank if it does not exist
					if (DAL.DataManager.GetBank (check.BankNumber) == null) {
						DAL.DataManager.AddBank (check.BankNumber, null);
					}

					// Add branch if it does not exist
					if (DAL.DataManager.GetBranch (check.BranchNumber, check.BankNumber) == null) {
						DAL.DataManager.AddBranch (check.BranchNumber, check.BankNumber);
					}
				}

			} while (checkPropsList.IterNext(ref iter));
			this.Destroy ();
		}

		protected void OnBtnCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}
