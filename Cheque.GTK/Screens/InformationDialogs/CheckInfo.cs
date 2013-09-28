using System;
using Cheque.BL;
using Cheque.DAL;

namespace Cheque.GTK.Screens
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CheckInfo : Gtk.Bin
	{
		public CheckInfo ()
		{
			this.Build ();
		}

		public void setInfos (CheckClass check)
		{
			Customer customer = DataManager.GetCustomer (check.CustomerID);

			// Name
			lblName.UseMarkup = true;
			lblName.Markup = ("<span size='x-large'>" + customer.Name + "</span>");

			// Identification
			if (customer.IdentityType == Customer.TypeID.CPF) {
				entryID.Text = Formatter.FormatCPF (customer.Identity);
				lblTypeID.Text = "CPF";
			} else {
				entryID.Text = Formatter.FormatCNPJ (customer.Identity);
				lblTypeID.Text = "CNPJ";
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

		protected void OnBtnEditClicked (object sender, EventArgs e)
		{
			setSensitive (true);
		}

		/// <summary>
		/// Sets the sensitive of the input entries
		/// </summary>
		/// <param name="sensitivity">If set to <c>true</c> sensitivity.</param>
		private void setSensitive (bool sensitivity)
		{
			entryID.Sensitive = sensitivity;
			entryNumber.Sensitive = sensitivity;
			entryBankNum.Sensitive = sensitivity;
			entryBranch.Sensitive = sensitivity;
			entrySerial.Sensitive = sensitivity;
			entryDueDate.Sensitive = sensitivity;
			entryValue.Sensitive = sensitivity;
		}
	}
}

