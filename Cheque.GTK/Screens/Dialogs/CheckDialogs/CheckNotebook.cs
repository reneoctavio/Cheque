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

namespace Cheque.GTK.Dialogs
{
	public partial class CheckNotebook : Gtk.Window
	{
		public CheckNotebook (Cheque.BL.CheckClass check) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			notebook1.PrevPage ();
			checkinfo.UpdateInfo (check);

			checkinfo.ChangedCheck += (object sender, EventArgs e) => {
				customerinfo1.customer = DAL.DataManager.GetCustomer (check.CustomerID);
				customerinfo1.UpdateInfo ();
				customerinfo1.ShowAll ();

				branchinfo1.Branch = DAL.DataManager.GetBranch (check.BranchNumber, check.BankNumber);
				branchinfo1.UpdateInfo ();
				branchinfo1.ShowAll ();
			};

			checkinfo.ShowAll ();

			customerinfo1.customer = DAL.DataManager.GetCustomer (check.CustomerID);
			customerinfo1.UpdateInfo ();
			customerinfo1.ShowAll ();

			branchinfo1.Bank = DAL.DataManager.GetBank (check.BankNumber);
			branchinfo1.Branch = DAL.DataManager.GetBranch (check.BranchNumber, check.BankNumber);
			branchinfo1.UpdateInfo ();
			branchinfo1.ShowAll ();
		}

		public NotebookPage.CheckInfo GetCheckInfo ()
		{
			return checkinfo;
		}
	}
}

