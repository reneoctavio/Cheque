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
using Cheque.DL.SQLite;

namespace Cheque.BL
{
	public partial class CheckClass : Contracts.BusinessEntityBase
	{
		[Indexed(Name = "ListingCheck", Order = 5, Unique = true)]
		public string Number { get; set; }

		[Indexed(Name = "ListingCheck", Order = 2, Unique = true)]
		public string BankNumber { get; set; }

		[Indexed(Name = "ListingCheck", Order = 3, Unique = true)]
		public string BranchNumber { get; set; }

		[Indexed(Name = "ListingCheck", Order = 4, Unique = true)]
		public string Serial { get; set; }

		[Indexed(Name = "ListingCheck", Order = 1, Unique = true)]
		public string CustomerID { get; set; }

		public DateTime IssueDate { get; set; }

		public DateTime DueDate { get; set; }

		public DateTime CashDate { get; set; }

		public bool IsCashed { get; set; }

		public bool CashedOverdue { get; set; }

		public decimal Value { get; set; }

		public CheckClass ()
		{
			IssueDate = DateTime.Now;
			IsCashed = true;
			CashedOverdue = false;
		}

		public string FormattedCheckNumber {
			get {
				int num = Int32.Parse (Number);
				return num.ToString ("D5");
			}
		}

		/// <summary>
		/// Verify if check is blank
		/// </summary>
		/// <returns><c>true</c>, if the check is blank, <c>false</c> otherwise.</returns>
		public bool isBlank ()
		{
			bool isNull = ((Number == null)
				|| (BankNumber == null)
				|| (BranchNumber == null)
				|| (Serial == null)
				|| (CustomerID == null));

			bool isEmpty = ((Number == "")
				|| (BankNumber == "")
				|| (BranchNumber == "")
				|| (Serial == "")
				|| (CustomerID == ""));

			return isEmpty || isNull;
		}
	}
}

