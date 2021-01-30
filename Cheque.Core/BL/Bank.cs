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

