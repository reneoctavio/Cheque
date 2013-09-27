using System;
using Cheque.BL;
using Gtk;
using System.Globalization;

namespace Cheque.GTK.Screens
{
	public partial class AddCheck : Gtk.Window
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

		public AddCheck () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.Maximize ();

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

			// Initialize first check
			CheckClass check = new CheckClass ();
			checkPropsList.AppendValues (check, "");

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

			this.treeview.Model = checkPropsList;

			idCell.Editable = true;
			numCell.Editable = true;
			bankCell.Editable = true;
			branchCell.Editable = true;
			serialCell.Editable = true;
			dueDateCell.Editable = true;
			valueCell.Editable = true;

			idCell.Edited += CustID_Edited;
			numCell.Edited += Number_Edited;
			bankCell.Edited += BankNumber_Edited;
			branchCell.Edited += BranchNumber_Edited;
			serialCell.Edited += Serial_Edited;
			dueDateCell.Edited += DueDate_Edited;
			valueCell.Edited += Value_Edited;

			idCol.Expand = true;
			custNameCol.Expand = true;
			numCol.Expand = true;
			bankCol.Expand = true;
			branchCol.Expand = true;
			serialCol.Expand = true;
			dueDateCol.Expand = true;
			valueCol.Expand = true;
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

		private void CustID_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

			// Find customer
			Customer customer = DAL.DataManager.GetCustomer (args.NewText);
			if (customer != null) {
				check.CustomerID = customer.Identity;
				checkPropsList.SetValue (iter, 1, customer.Name);
				// Move to next cell
				this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), numCol, numCell, true);
			} else if (!check.isBlank () || (!args.NewText.Equals (""))) {
				// Verify if check is blank before showing error dialog
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Question, 
				                                      ButtonsType.YesNo, "Cliente não existe! Quer adicioná-lo?");

				ResponseType result = (ResponseType)md.Run ();

				if (result == ResponseType.Yes) {
					md.Destroy ();
					AddCustomerDialog addCustDia = new AddCustomerDialog ();
					addCustDia.GetEntryID ().Text = args.NewText;
					ResponseType ans = (ResponseType)addCustDia.Run ();

					// If answer was OK, add customer
					if (ans == ResponseType.Ok) {
						customer = DAL.DataManager.GetCustomer (args.NewText);
						if (customer != null) {
							check.CustomerID = customer.Identity;
							checkPropsList.SetValue (iter, 1, customer.Name);
							// Move to next cell
							this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), numCol, numCell, true);
						}
					}
				} else {
					md.Destroy ();
				}
			}
		}

		private void Number_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (args.NewText, BL.Constants.LENGTH_NUM_CHECK);

			if (formattedNumber != null) {
				check.Number = formattedNumber;
				// Move to next cell
				this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), bankCol, bankCell, true);
			} else {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
			}
		}

		private void BankNumber_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (args.NewText, BL.Constants.LENGTH_NUM_BANK);

			if (formattedNumber != null) {
				check.BankNumber = formattedNumber;
				// Move to next cell
				this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), branchCol, branchCell, true);
			} else if (!check.isBlank ()) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
			}
		}

		private void BranchNumber_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);
			String formattedNumber = BL.Formatter.ZeroPad (args.NewText, BL.Constants.LENGTH_NUM_BRANCH);

			if (formattedNumber != null) {
				check.BranchNumber = formattedNumber;
				// Move to next cell
				this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), serialCol, serialCell, true);
			} else if (!check.isBlank ()) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
			}
		}

		private void Serial_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);
			check.Serial = args.NewText;
			// Move to next cell
			this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), dueDateCol, dueDateCell, true);
		}

		private void DueDate_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, new Gtk.TreePath (args.Path));

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

			// Parsing the date
			DateTime dt;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			bool isParsed = DateTime.TryParse (args.NewText, formatDate, DateTimeStyles.None, out dt);

			if (isParsed) {
				check.DueDate = dt;
				check.CashDate = dt;
				// Move to next cell
				this.treeview.SetCursorOnCell (new Gtk.TreePath (args.Path), valueCol, valueCell, true);
			} else {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Data inválida!");

				md.Run ();
				md.Destroy ();
			}

		}

		private void Value_Edited (object o, Gtk.EditedArgs args)
		{
			Gtk.TreeIter iter;
			Gtk.TreePath path = new Gtk.TreePath (args.Path);
			checkPropsList.GetIter (out iter, path);

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

			decimal value;
			bool isParsed = false;
			string[] splittedString = args.NewText.Split ();

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
				if (isRowFilled (path)) {

					if (!checkPropsList.IterNext (ref iter)) {
						// Update Total
						checkPropsList.GetIter (out iter, path);
						lblTotal.Text = "Total: " + String.Format ("{0:C}", calcTotal ());

						// Move to next cell
						checkPropsList.AppendValues (new BL.CheckClass (), "");
						checkPropsList.IterNext (ref iter);
						this.treeview.SetCursorOnCell (checkPropsList.GetPath (iter), idCol, idCell, true);
					}
				}

			} else {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
			}
		}

		private bool isRowFilled (Gtk.TreePath treePath)
		{
			bool isFilled = true;
			Gtk.TreeIter iter;
			checkPropsList.GetIter (out iter, treePath);

			CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

			// Check any blank field
			if (check.isBlank ()) {
				return false;
			}

			// Check valid customer
			Customer customer = DAL.DataManager.GetCustomer (check.CustomerID);
			if (customer == null) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Question, 
				                                      ButtonsType.YesNo, "Cliente não existe! Quer adicioná-lo?");
				md.Run ();
				md.Destroy ();
				isFilled = false;
			}

			// Check valid number
			String formattedNumber = BL.Formatter.ZeroPad (check.Number, BL.Constants.LENGTH_NUM_CHECK);
			if (formattedNumber == null) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
				isFilled = false;
			}

			// Check valid bank number 
			formattedNumber = BL.Formatter.ZeroPad (check.BankNumber, BL.Constants.LENGTH_NUM_BANK);
			if (formattedNumber == null) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
				isFilled = false;
			}

			// Check valid branch number
			formattedNumber = BL.Formatter.ZeroPad (check.BranchNumber, BL.Constants.LENGTH_NUM_BRANCH);
			if (formattedNumber == null) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Número inválido!");

				md.Run ();
				md.Destroy ();
				isFilled = false;
			}

			// Check valid due date
			DateTime dt;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			DateTime.TryParse (BL.Constants.MIN_CHECK_DATE, formatDate, DateTimeStyles.None, out dt);
			if (check.DueDate < dt) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Ok, "Data inválida! Data tem que ser maior que " + BL.Constants.MIN_CHECK_DATE + ".");

				md.Run ();
				md.Destroy ();
				isFilled = false;
			}

			return isFilled;
		}

		private decimal calcTotal ()
		{
			TreeIter iter;
			decimal total = 0.00m;

			checkPropsList.GetIterFirst (out iter);

			do {
				CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

				if ((check != null) && (!check.isBlank ())) {
					total += check.Value;
				}

			} while (checkPropsList.IterNext(ref iter));

			return total;
		}

		protected void OnButtonOKClicked (object sender, EventArgs e)
		{
			TreeIter iter;

			checkPropsList.GetIterFirst (out iter);

			do {
				CheckClass check = (CheckClass)checkPropsList.GetValue (iter, 0);

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

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}