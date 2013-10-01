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
		// Checks
		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> AllChecks { get; set; }
		// Paid Checks
		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> PaidChecks { get; set; }
		// Not paid check
		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> OverdueChecks { get; set; }
		// Paid but overdue
		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> PaidOverdueChecks { get; set; }
		// Not cashed yet
		[Cheque.DL.SQLite.Ignore]
		public List<CheckClass> NotDueChecks { get; set; }

		public Customer ()
		{
			// Initialize checks lists
			AllChecks = new List<CheckClass> ();
			PaidChecks = new List<CheckClass> ();
			OverdueChecks = new List<CheckClass> ();
			PaidOverdueChecks = new List<CheckClass> ();
			NotDueChecks = new List<CheckClass> ();
		}

		public bool IsThereAnyCheck (List<CheckClass> checkList)
		{
			return ((checkList != null) || (checkList.Count != 0));
		}

		public int CountChecks (List<CheckClass> checkList)
		{
			if (IsThereAnyCheck (checkList)) {
				return checkList.Count;
			} else {
				return 0;
			}
		}

		public decimal PercentagePaidWithinDueDate ()
		{
			int overdue = 0;
			int paidOverdue = 0;
			int paid = 0;
			if (IsThereAnyCheck (PaidOverdueChecks))
				paidOverdue = CountChecks (PaidOverdueChecks);
			if (IsThereAnyCheck (AllChecks))
				paid = CountChecks (AllChecks);
			if (IsThereAnyCheck (NotDueChecks)) 
				paid -= CountChecks (NotDueChecks);
			if (IsThereAnyCheck (OverdueChecks)) 
				overdue = CountChecks (OverdueChecks);

			int paidDue = paid - paidOverdue - overdue;

			if (paid != 0) 
				return (decimal)((decimal)paidDue) / ((decimal)paid);
			else if (IsThereAnyCheck (OverdueChecks))
				return 0.0m;
			else
				return 1.0m;
		}

		public decimal CheckListTotal (List<CheckClass> checkList)
		{
			decimal total = 0.0m;
			if (IsThereAnyCheck (checkList)) {
				foreach (CheckClass check in checkList) {
					total += check.Value;
				}
			}
			return total;
		}
	}
}

