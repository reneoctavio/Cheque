using System;

namespace Cheque.GTK.Dialogs.NotebookPage
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CustomerInfo : Gtk.Bin
	{
		public BL.Customer customer{ get; set; }

		public CustomerInfo ()
		{
			this.Build ();
		}

		public void UpdateInfo ()
		{
			// Name
			lblName.UseMarkup = true;
			lblName.Markup = ("<span size='x-large'>" + customer.Name + "</span>");

			// Identification
			if (customer.IdentityType == BL.Customer.TypeID.CPF) {
				lbl_ID.Text = BL.Formatter.FormatCPF (customer.Identity);
			} else {
				lbl_ID.Text = BL.Formatter.FormatCNPJ (customer.Identity);
			}

			// Total # of checks
			lblTotal.Text = "Este cliente tem " 
				+ customer.CountChecks (customer.AllChecks)
				+ " cheques cadastrados no total de "
				+ String.Format ("{0:C}", customer.CheckListTotal (customer.AllChecks));
		
			// Overdue
			if (customer.CountChecks (customer.OverdueChecks) != 0) {
				lblOverdue.Text = "Este cliente está inadimplente, com "
					+ customer.CountChecks (customer.OverdueChecks)
					+ " cheques não pagos na data de vencimento, totalizando: "
					+ String.Format ("{0:C}", customer.CheckListTotal (customer.OverdueChecks));
			} else {
				lblOverdue.Text = "Este cliente não está inadimplente.";
			}
		
			// Paid due rate
			lblPaidDue.Text = "Este cliente paga "
				+ String.Format ("{0:P2}", customer.PercentagePaidWithinDueDate ())
				+ " de seus cheques até sua data de vencimento.";

			// Not Due checks
			lblNotDue.Text = "Este cliente tem ainda "
				+ customer.CountChecks (customer.NotDueChecks)
				+ " cheques a vencer, totalizando: "
				+ String.Format ("{0:C}", customer.CheckListTotal (customer.NotDueChecks));
		
			ShowAll ();
		}

		protected void OnBtnCloseClicked (object sender, EventArgs e)
		{
			this.ParentWindow.Destroy ();
		}
	}
}

