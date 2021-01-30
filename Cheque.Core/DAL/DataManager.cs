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
using Cheque.BL;
using System.Collections.Generic;

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

		public static List<Customer> GetCustomers ()
		{
			return (List<Customer>)DL.Database.GetItems<Customer> ();
		}
		#endregion
		#region Check
		public static int AddCheck (CheckClass check)
		{
			return DL.Database.SaveItem<CheckClass> (check);
		}

		public static List<CheckClass> GetChecks ()
		{
			return (List<CheckClass>)DL.Database.GetItems<CheckClass> ();

		}

		public static CheckClass GetCheck (CheckClass check)
		{
			return DL.Database.GetCheck (check.Number, check.BankNumber, check.BranchNumber, check.Serial, check.CustomerID);
		}

		public static int DeleteCheck (CheckClass check)
		{
			return DL.Database.DeleteItem<CheckClass> (check);
		}
		#endregion
		#region Password
		public static int SavePassword (BL.Password.Password passwd)
		{
			return DL.Database.SaveItem<BL.Password.Password> (passwd);
		}

		public static BL.Password.Password GetPassword ()
		{
			return DL.Database.GetItem<BL.Password.Password> (1);
		}
		#endregion
	}
}

