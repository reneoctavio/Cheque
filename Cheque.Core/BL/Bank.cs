using System.Xml;
using System.Xml.Serialization;
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
		[XmlAttribute("na")]
		public string Name { get; set; }

		[Unique]
		[XmlAttribute("banu")]
		public string BankNumber { get; set; }

		[Unique]
		[XmlAttribute("pCNPJ")]
		public string PartCNPJ { get; set; }
		// These are only populated on the client-side, when a single bank is requested
		// Branches
		[XmlElement("bn")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> BranchNumbers { get; set; }

		[XmlIgnore]
		[Cheque.DL.SQLite.Ignore]
		public List<Branch> Branches { get; set; }
		// Customers
		[XmlElement("cids")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> CustomerIDs { get; set; }

		[XmlIgnore]
		[Cheque.DL.SQLite.Ignore]
		public List<Customer> Customers { get; set; }
		// Checks
		[XmlElement("cn")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[XmlIgnore]
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
			// Initialize branches lists
			BranchNumbers = new List<string> ();
			Branches = new List<Branch> ();

			// Initialize customers lists
			CustomerIDs = new List<string> ();
			Customers = new List<Customer> ();

			// Initialize checks lists
			CheckNumbers = new List<string> ();
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

