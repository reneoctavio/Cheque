// -------------------------------------------------------------------------
// Cheque - An Application to help you keep track of checks in your custody
// Copyright (C) 2013 Rene Octavio Queiroz Dias
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Source code at <https://github.com/reneoctavio/Cheque>.
// -------------------------------------------------------------------------
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

