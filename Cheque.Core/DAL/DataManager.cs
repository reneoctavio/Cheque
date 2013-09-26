using System;
using Cheque.BL;

namespace Cheque.DAL
{
	public static class DataManager
	{
		#region Bank
		public static int AddBank (string bankNumber, string bankName)
		{
			return DL.Database.SaveItem<Bank> (new Bank (bankNumber, bankName));
		}

		public static Bank GetBank (string bankNumber)
		{
			return DL.Database.GetBank (bankNumber);
		}
		#endregion
		#region Branch
		public static int AddBranch (string branchNum, string bankNum)
		{
			Branch branch = new Branch ();
			branch.BranchNumber = branchNum;
			branch.BankNumber = bankNum;
			return DL.Database.SaveItem<Branch> (branch);
		}

		public static Branch GetBranch (string branchNum, string bankNum)
		{
			return DL.Database.GetBranch (branchNum, bankNum);
		}
		#endregion
		#region Customer
		public static int AddCustomer (string customerID, string customerName, Customer.TypeID type)
		{
			Customer customer = new Customer ();
			customer.Identity = customerID;
			customer.Name = customerName;
			customer.IdentityType = type;
			return DL.Database.SaveItem<Customer> (customer);
		}

		public static Customer GetCustomer (string customerID)
		{
			return DL.Database.GetCustomer (customerID);
		}
		#endregion
		#region Check
		public static int AddCheck (CheckClass check)
		{
			return DL.Database.SaveItem<CheckClass> (check);
		}
		#endregion
	}
}

