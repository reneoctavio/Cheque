using System;
using System.Collections.Generic;
using Cheque.DL.SQLite;

namespace Cheque.BL
{
	public partial class Customer : Contracts.BusinessEntityBase
	{
		public enum TypeID
		{ 			
			CPF,
			CNPJ
		}

		public string Name { get; set; }

		[Indexed(Name = "IdentityInd", Order = 1, Unique = true)]
		public string Identity { get; set; }

		public TypeID IdentityType { get; set; }
		// These are only populated on the client-side, when a single customer is requested
		// Banks
		[Cheque.DL.SQLite.Ignore]
		public List<string> BankNumbers { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<Bank> Banks { get; set; }
		// Checks
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> Checks { get; set; }

		public Customer ()
		{
			// Initialize banks lists
			BankNumbers = new List<string> ();
			Banks = new List<Bank> ();

			// Initialize checks lists
			CheckNumbers = new List<string> ();
			Checks = new List<CheckClass> ();
		}
	}
}

