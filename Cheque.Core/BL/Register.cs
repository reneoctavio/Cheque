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
using System.Xml.Serialization;

namespace Cheque.BL
{
	public class Register
	{
		public Register ()
		{
			Banks = new List<Bank> ();
			Branches = new List<Branch> ();
			Customers = new List<Customer> ();
			Checks = new List<CheckClass> ();
		}

		[XmlElement("ba")]
		public List<Bank> Banks { get; set; }

		[XmlElement("br")]
		public List<Branch> Branches { get; set; }

		[XmlElement("cs")]
		public List<Customer> Customers { get; set; }

		[XmlElement("ch")]
		public List<CheckClass> Checks { get; set; }
	}
}

