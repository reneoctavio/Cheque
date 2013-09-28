using System;
using Cheque.BL;
using Cheque.DAL;
using Gtk;

namespace Cheque.GTK.Screens
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CheckInfo : Gtk.Bin
	{
		private CheckClass check;

		public CheckInfo ()
		{
			this.Build ();
		}

		public void setInfos (CheckClass chk)
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
			if (check.CashDate <= DateTime.Now) {
				if (check.IsCashed) {
					entryCashDate.Text = check.CashDate.ToShortDateString ();
					checkBtnCashed.Activate ();
				}
				if (check.CashedOverdue) {
					checkBtnOverdue.Activate ();
				}
			}

			// Issue Date
			lblIssueDate.Text = "Data de Cadastro: " + check.IssueDate.ToString ();
		}

		protected void OnBtnSaveClicked (object sender, EventArgs e)
		{

		}

		protected void OnBtnCloseClicked (object sender, EventArgs e)
		{
			this.ParentWindow.Destroy ();
		}

		protected void OnBtnEditClicked (object sender, EventArgs e)
		{
			setSensitive (true);
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
					addCustomer ();
				} else {
					check.CustomerID = customer.Identity;
					SetCustomerName (customer.Name);
				}
			} else if (Formatter.IsCNPJ (Formatter.GetNumericID (entryID.Text))) {
				entryID.ModifyBase (Gtk.StateType.Normal, new Gdk.Color (0, 255, 0));
				lbl_ID.Text = "CNPJ";
				Customer customer = DataManager.GetCustomer (Formatter.GetNumericID (entryID.Text));
				if (customer == null) {
					addCustomer ();
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
		private void setSensitive (bool sensitivity)
		{
			entryID.IsEditable = sensitivity;
			entryNumber.IsEditable = sensitivity;
			entryBankNum.IsEditable = sensitivity;
			entryBranch.IsEditable = sensitivity;
			entrySerial.IsEditable = sensitivity;
			entryDueDate.IsEditable = sensitivity;
			entryValue.IsEditable = sensitivity;
			checkBtnCashed.Sensitive = sensitivity;
			checkBtnOverdue.Sensitive = sensitivity;
			btnSave.Sensitive = sensitivity;
			btnEdit.Sensitive = !sensitivity;
		}

		private void addCustomer ()
		{
			MessageDialog md = new MessageDialog (null, 
			                                      DialogFlags.DestroyWithParent,
			                                      MessageType.Question, 
			                                      ButtonsType.YesNo, "Cliente não existe! Quer adicioná-lo?");

			ResponseType result = (ResponseType)md.Run ();

			if (result == ResponseType.Yes) {
				md.Destroy ();
				AddCustomerDialog addCustDia = new AddCustomerDialog ();
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
			//throw new NotImplementedException ();
		}

		protected void OnCheckBtnOverdueToggled (object sender, EventArgs e)
		{
			//throw new NotImplementedException ();
		}
	}
}

