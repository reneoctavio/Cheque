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

