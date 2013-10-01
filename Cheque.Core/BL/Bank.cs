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

		public string ShortName { get; set; }
		// These are only populated on the client-side, when a single bank is requested
		[Cheque.DL.SQLite.Ignore]
		public List<Branch> Branches { get; set; }

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
			Branches = new List<Branch> ();
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

