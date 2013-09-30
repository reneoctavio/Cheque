using System;
using System.Collections.Generic;
using Cheque.DL.SQLite;

namespace Cheque.BL
{
	/// <summary>
	/// Represents a Bank
	/// </summary>
	public partial class Bank : Contracts.BusinessEntityBase
	{
		public string Name { get; set; }

		[Indexed(Name = "BankNumInd", Order = 1, Unique = true)]
		public string BankNumber { get; set; }

		[Unique]
		public string PartCNPJ { get; set; }
		// These are only populated on the client-side, when a single bank is requested
		// Branches
		[Cheque.DL.SQLite.Ignore]
		public List<string> BranchNumbers { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<Branch> Branches { get; set; }
		// Customers
		[Cheque.DL.SQLite.Ignore]
		public List<string> CustomerIDs { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<Customer> Customers { get; set; }
		// Checks
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> Checks { get; set; }

		public Bank ()
		{
			initializeLists ();
		}

		public Bank (string number, string name)
		{
			initializeLists ();
			BankNumber = number;
			Name = name;
		}

		private void initializeLists ()
		{
			// Initialize branches lists
			BranchNumbers = new List<string> ();
			Branches = new List<Branch> ();

			// Initialize customers lists
			CustomerIDs = new List<string> ();
			Customers = new List<Customer> ();

			// Initialize checks lists
			CheckNumbers = new List<string> ();
			Checks = new List<CheckClass> ();
		}

		public string FormattedBankNumber {
			get {
				int num = Int32.Parse (BankNumber);
				return num.ToString ("D3");
			}
		}
	}
}

