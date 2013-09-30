using System;
using Cheque.BL;
using Cheque.DAL;
using Gtk;
using System.Globalization;

namespace Cheque.GTK.Dialogs.NotebookPage
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CheckInfo : Gtk.Bin
	{
		private CheckClass check;

		private bool IsEditingActivated { get; set; }

		public MessageDialog deleteDialog;
		// A delegate type for hooking up change notifications.
		public delegate void ChangedCheckEventHandler (object sender, EventArgs e);

		public CheckInfo ()
		{
			createDeleteDialog ();
			this.Build ();
			IsEditingActivated = false;
		}
		// An event that clients can use to be notified whenever the
		// elements of the list change.
		public event ChangedCheckEventHandler ChangedCheck;
		// Invoke the Changed event; called whenever list changes
		protected virtual void OnChangedCheck (EventArgs e)
		{
			if (ChangedCheck != null)
				ChangedCheck (this, e);
		}

		public void UpdateInfo (CheckClass chk)
		{
			check = chk;

			Customer customer = DataManager.GetCustomer (check.CustomerID);

			// Name
			SetCustomerName (customer.Name);

			// Identification
			if (customer.IdentityType == Customer.TypeID.CPF) {
				entryID.Text = Formatter.FormatCPF (customer.Identity);
				lbl_ID.Text = "CPF";
			} else {
				entryID.Text = Formatter.FormatCNPJ (customer.Identity);
				lbl_ID.Text = "CNPJ";
			}

			// Number
			entryNumber.Text = check.Number;

			// Bank
			entryBankNum.Text = check.BankNumber;

			// Branch
			entryBranch.Text = check.BranchNumber;

			// Serial
			entrySerial.Text = check.Serial;

			// Due Date
			entryDueDate.Text = check.DueDate.ToShortDateString ();

			// Value
			entryValue.Text = String.Format ("{0:C}", check.Value);

			// Cash Date
			if (check.CashDate.Date <= DateTime.Now.Date) {
				if (check.IsCashed) {
					entryCashDate.Text = check.CashDate.ToShortDateString ();
					if (!checkBtnCashed.Active)
						checkBtnCashed.Activate ();
				} else {
					entryCashDate.Text = DateTime.Now.ToShortDateString ();
					if (checkBtnCashed.Active)
						checkBtnCashed.Active = false;
				}
				if (check.CashedOverdue) {
					if (!checkBtnOverdue.Active)
						checkBtnOverdue.Activate ();
				} else {
					if (checkBtnOverdue.Active)
						checkBtnOverdue.Active = false;
				}
			}

			// Issue Date
			lblIssueDate.Text = "Data de Cadastro: " + check.IssueDate.ToString ();
		}

		private Gtk.MessageDialog InvalidEntryDialog (string message)
		{
			return new Gtk.MessageDialog (null, 
			                              Gtk.DialogFlags.DestroyWithParent,
			                              Gtk.MessageType.Error, 
			                              Gtk.ButtonsType.Ok, message);
		}

		protected void OnBtnSaveClicked (object sender, EventArgs e)
		{
			bool isInputValid = true;
			// Check valid customer
			BL.Customer customer = DAL.DataManager.GetCustomer (BL.Formatter.GetNumericID (entryID.Text));
			if (customer == null) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Cliente inexistente!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.CustomerID = BL.Formatter.GetNumericID (entryID.Text);
			}

			// Check valid number
			string formattedNumber = BL.Formatter.ZeroPad (entryNumber.Text, BL.Constants.LENGTH_NUM_CHECK);
			if (formattedNumber == null) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Número de cheque inválido!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.Number = formattedNumber;
			}

			// Check valid bank number 
			formattedNumber = BL.Formatter.ZeroPad (entryBankNum.Text, BL.Constants.LENGTH_NUM_BANK);
			if (formattedNumber == null) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Número de banco inválido!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.BankNumber = formattedNumber;
			}

			// Check valid branch number
			formattedNumber = BL.Formatter.ZeroPad (entryBranch.Text, BL.Constants.LENGTH_NUM_BRANCH);
			if (formattedNumber == null) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Número de agência inválido!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.BranchNumber = formattedNumber;
			}

			// Check valid due date
			DateTime dt;
			DateTimeFormatInfo formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			if (!DateTime.TryParse (entryDueDate.Text, formatDate, DateTimeStyles.None, out dt)) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Data inválida!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.DueDate = dt;
			}

			// Check valid cash date
			if (!DateTime.TryParse (entryCashDate.Text, formatDate, DateTimeStyles.None, out dt)) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Data inválida!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				if (check.IsCashed) {
					check.CashDate = dt;
					if (check.CashDate.Date > check.DueDate.Date) {
						check.CashedOverdue = true;
					} else {
						check.CashedOverdue = false;
					}
				}
			}

			// Value
			Decimal value;
			if (!BL.Formatter.GetValueWithoutCurrency (entryValue.Text, out value)) {
				isInputValid = false;
				Gtk.MessageDialog dialog = InvalidEntryDialog ("Valor inválido!");
				Gtk.ResponseType result = (Gtk.ResponseType)dialog.Run ();
				if (result == Gtk.ResponseType.Ok)
					dialog.Destroy ();
			} else {
				check.Value = value;
			}

			if (isInputValid) {
				UpdateInfo (check);
				SetSensitive (false);
				DAL.DataManager.AddCheck (check);
				IsEditingActivated = false;
				ShowAll ();
				OnChangedCheck (EventArgs.Empty);
			}
		}

		protected void OnBtnEditClicked (object sender, EventArgs e)
		{
			SetSensitive (true);
			IsEditingActivated = true;
		}

		protected void OnEntryIDTextInserted (object o, Gtk.TextInsertedArgs args)
		{
			SetID ();
		}

		protected void OnEntryIDTextDeleted (object o, TextDeletedArgs args)
		{
			SetID ();
		}

		private void SetID ()
		{
			if (Formatter.IsCPF (Formatter.GetNumericID (entryID.Text))) {
				entryID.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (0, 255, 0));
				lbl_ID.Text = "CPF";
				Customer customer = DataManager.GetCustomer (Formatter.GetNumericID (entryID.Text));
				if (customer == null) {
					AddCustomer ();
				} else {
					check.CustomerID = customer.Identity;
					SetCustomerName (customer.Name);
				}
			} else if (Formatter.IsCNPJ (Formatter.GetNumericID (entryID.Text))) {
				entryID.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (0, 255, 0));
				lbl_ID.Text = "CNPJ";
				Customer customer = DataManager.GetCustomer (Formatter.GetNumericID (entryID.Text));
				if (customer == null) {
					AddCustomer ();
				} else {
					check.CustomerID = customer.Identity;
					SetCustomerName (customer.Name);
				}
			} else {
				entryID.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (255, 0, 0));
				lbl_ID.Text = "Inválido";
			}
		}

		private void SetCustomerName (string name)
		{
			lblName.UseMarkup = true;
			lblName.Markup = ("<span size='x-large'>" + name + "</span>");
			this.ShowAll ();
		}

		/// <summary>
		/// Sets the sensitive of the input entries
		/// </summary>
		/// <param name="sensitivity">If set to <c>true</c> sensitivity.</param>
		private void SetSensitive (bool sensitivity)
		{
			entryID.IsEditable = sensitivity;
			entryNumber.IsEditable = sensitivity;
			entryBankNum.IsEditable = sensitivity;
			entryBranch.IsEditable = sensitivity;
			entrySerial.IsEditable = sensitivity;
			entryDueDate.IsEditable = sensitivity;
			entryValue.IsEditable = sensitivity;
			entryCashDate.IsEditable = sensitivity;
			checkBtnCashed.Sensitive = sensitivity;
			btnSave.Sensitive = sensitivity;
			btnEdit.Sensitive = !sensitivity;
		}

		private void AddCustomer ()
		{
			MessageDialog md = new MessageDialog (null, 
			                                      DialogFlags.DestroyWithParent,
			                                      MessageType.Question, 
			                                      ButtonsType.YesNo, "Cliente não existe! Quer adicioná-lo?");

			ResponseType result = (ResponseType)md.Run ();

			if (result == ResponseType.Yes) {
				md.Destroy ();
				Dialogs.AddCustomerDialog addCustDia = new Dialogs.AddCustomerDialog ();
				addCustDia.GetEntryID ().Text = Formatter.GetNumericID (entryID.Text);
				ResponseType ans = (ResponseType)addCustDia.Run ();

				// If answer was OK, add customer
				if (ans == ResponseType.Ok) {
					Customer customer = DAL.DataManager.GetCustomer (Formatter.GetNumericID (entryID.Text));
					if (customer != null) {
						check.CustomerID = customer.Identity;
						SetCustomerName (customer.Name);
					}
				}
			} else {
				md.Destroy ();
			}
		}

		protected void OnCheckBtnCashedToggled (object sender, EventArgs e)
		{
			if (IsEditingActivated) {
				check.IsCashed = !check.IsCashed;
				if (!check.IsCashed)
					check.CashedOverdue = false;
			}
		}

		protected void OnBtnDeleteClicked (object sender, EventArgs e)
		{
			ResponseType result = (ResponseType)deleteDialog.Run ();

			if (result == ResponseType.Yes) {
				DataManager.DeleteCheck (check);
				OnChangedCheck (EventArgs.Empty);
				deleteDialog.Destroy ();
				this.ParentWindow.Destroy ();
			}
			deleteDialog.Destroy ();
		}

		protected void OnBtnCloseClicked (object sender, EventArgs e)
		{
			this.ParentWindow.Destroy ();
		}

		private void createDeleteDialog ()
		{
			deleteDialog = new MessageDialog (null, 
			                                  DialogFlags.DestroyWithParent,
			                                  MessageType.Question, 
			                                  ButtonsType.YesNo, "Quer mesmo deletar este cheque?");
		}
	}
}

