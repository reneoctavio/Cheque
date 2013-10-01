using System;
using System.Collections.Generic;
using Cheque.DL.SQLite;

namespace Cheque.BL
{
	public partial class Branch : Contracts.BusinessEntityBase
	{
		public string BranchNumber { get; set; }

		public string BankNumber { get; set; }

		public string PartCNPJ { get; set; }

		public string SeqCNPJ { get; set; }

		public string DV_CNPJ { get; set; }

		public string BranchName { get; set; }

		public string Address { get; set; }

		public string AddressComplement { get; set; }

		public string Neighborhood { get; set; }

		public string CEP { get; set; }

		public string City { get; set; }

		public string UF { get; set; }

		public string PhoneCode { get; set; }

		public string Telephone { get; set; }
		// These are only populated on the client-side, when a single branch is requested
		// Checks
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> Checks { get; set; }

		public Branch ()
		{
			// Initialize checks lists
			CheckNumbers = new List<string> ();
			Checks = new List<CheckClass> ();
		}

		public string FormattedBranchNumber {
			get {
				int num = Int32.Parse (BranchNumber);
				return num.ToString ("D4");
			}
		}
	}
}

