using System;

namespace Cheque.GTK.Dialogs.NotebookPage
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class BranchInfo : Gtk.Bin
	{
		public BL.Branch Branch{ get; set; }

		public BL.Bank Bank{ get; set; }

		public BranchInfo ()
		{
			this.Build ();
		}

		public void UpdateInfo ()
		{
			if (Bank != null) {
				// Name
				lblBankName.UseMarkup = true;
				lblBankName.Markup = ("<span size='x-large'>" + Bank.ShortName + "</span>");
		
				// Full Name
				lblFullBankName.Text = Bank.Name;
			} else {
				lblFullBankName.Text = "Banco não encontrado";
			}

			if (Branch != null) {
				// Branch Name
				lblBranchName.Text = Branch.BranchName;

				// CNPJ
				lblBranchCNPJ.Text = BL.Formatter.FormatCNPJ (Branch.PartCNPJ + Branch.SeqCNPJ + Branch.DV_CNPJ);

				// Address
				lblAddress.Text = Branch.Address + " " + Branch.AddressComplement +
					"\n" + Branch.Neighborhood +
					"\n" + Branch.City + " - " + Branch.UF +
					"\n" + Branch.CEP;

				// Phone
				lblPhone.Text = "+ 55 (" + Branch.PhoneCode + ") " + Branch.Telephone;
			} else {
				lblBranchName.Text = "Agência não encontrada.";
				lblBranchCNPJ.Text = "";
				lblAddress.Text = "";
				lblPhone.Text = "";
			}
		}

		protected void OnBtnCloseClicked (object sender, EventArgs e)
		{
			this.ParentWindow.Destroy ();
		}
	}
}

