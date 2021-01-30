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
using System.Collections.Generic;

namespace Cheque.BL
{
	public static class FixImportDB
	{
		public static void FixBankTable ()
		{
			List<Bank> bankList = (List<Bank>)DL.Database.GetItems<Bank> ();

			foreach (Bank bank in bankList) {
				bank.BankNumber = Formatter.ZeroPad (bank.BankNumber, BL.Constants.LENGTH_NUM_BANK);
				bank.PartCNPJ = Formatter.ZeroPad (bank.PartCNPJ, 8);
			}

			DL.Database.SaveItems<Bank> (bankList);
		}

		public static void FixBranchTable ()
		{
			List<Branch> branchList = (List<Branch>)DL.Database.GetItems<Branch> ();
			List<Bank> bankList = (List<Bank>)DL.Database.GetItems<Bank> ();

			foreach (Branch branch in branchList) {
				branch.BranchNumber = Formatter.ZeroPad (branch.BranchNumber, Constants.LENGTH_NUM_BRANCH);
				branch.BranchName = branch.BranchName.Trim ();
				branch.Address = branch.Address.Trim ();
				branch.AddressComplement = branch.AddressComplement.Trim ();
				branch.Neighborhood = branch.Neighborhood.Trim ();
				branch.City = branch.City.Trim ();
				branch.UF = branch.UF.Trim ();
				branch.PartCNPJ = Formatter.ZeroPad (branch.PartCNPJ.Replace (".", ""), 8);
				branch.SeqCNPJ = Formatter.ZeroPad (branch.SeqCNPJ, 4);
				branch.DV_CNPJ = Formatter.ZeroPad (branch.DV_CNPJ, 2);

				foreach (Bank bank in bankList) {
					if (branch.PartCNPJ.Equals (bank.PartCNPJ)) {
						branch.BankNumber = bank.BankNumber;
						break;
					}
				}

				if (branch.BankNumber == null) {
					Console.Write ("\n" + branch.ID);
					DL.Database.DeleteItem<Branch> (branch);
				} else {
					//Branch br = DAL.DataManager.GetBranch (branch.BranchNumber, branch.BankNumber);
					try {
						DL.Database.SaveItem<Branch> (branch);
					} catch {
						Console.Write ("\n" + branch.ID);
						DL.Database.DeleteItem<Branch> (branch);
					}
				}

				/*
				if (br != null) {
					Console.Write (branch.ID);
					Console.Write ("\n");
					Console.Write (br.ID);
					Console.Write ("\n");
				}*/
			}
			//DL.Database.SaveItems<Branch> (branchList);
		}
	}
}

