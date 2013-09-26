using System;
using System.Xml.Serialization;
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

		[XmlAttribute("na")]
		public string Name { get; set; }

		[Unique]
		[XmlAttribute("id")]
		public string Identity { get; set; }

		[XmlAttribute("idt")]
		public TypeID IdentityType { get; set; }
		// These are only populated on the client-side, when a single customer is requested
		// Banks
		[XmlElement("bn")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> BankNumbers { get; set; }

		[XmlIgnore]
		[Cheque.DL.SQLite.Ignore]
		public List<Bank> Banks { get; set; }
		// Checks
		[XmlElement("cn")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[XmlIgnore]
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

