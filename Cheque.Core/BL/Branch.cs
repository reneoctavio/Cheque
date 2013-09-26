using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Cheque.BL
{
	public partial class Branch : Contracts.BusinessEntityBase
	{
		[XmlAttribute("brnu")]
		public virtual string BranchNumber { get; set; }

		[XmlAttribute("banu")]
		public virtual string BankNumber { get; set; }
		// These are only populated on the client-side, when a single branch is requested
		// Checks
		[XmlElement("cn")]
		[Cheque.DL.SQLite.Ignore]
		public List<string> CheckNumbers { get; set; }

		[XmlIgnore]
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

