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

