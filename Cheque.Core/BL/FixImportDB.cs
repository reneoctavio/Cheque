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
				branch.BranchName = branch.BranchName.Trim ();
				branch.City = branch.City.Trim ();
				branch.State = branch.State.Trim ();
				branch.PartCNPJ = Formatter.ZeroPad (branch.PartCNPJ.Replace (".", ""), 8);

				foreach (Bank bank in bankList) {
					if (branch.PartCNPJ.Contains (bank.PartCNPJ)) {
						branch.BankNumber = bank.BankNumber;
						break;
					}
				}
			}

			DL.Database.SaveItems<Branch> (branchList);
		}
	}
}

