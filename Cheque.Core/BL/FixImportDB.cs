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

