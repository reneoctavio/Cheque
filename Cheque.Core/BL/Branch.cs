using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Cheque.BL
{
	public partial class Branch : Contracts.BusinessEntityBase
	{
		[XmlAttribute("brnu")]
		public string BranchNumber { get; set; }

		[XmlAttribute("banu")]
		public string BankNumber { get; set; }

		[XmlAttribute("pCNPJ")]
		public string PartCNPJ { get; set; }

		[XmlAttribute("seqC")]
		public string SeqCNPJ { get; set; }

		[XmlAttribute("dvC")]
		public string DV_CNPJ { get; set; }

		[XmlAttribute("brna")]
		public string BranchName { get; set; }

		[XmlAttribute("cep")]
		public string CEP { get; set; }

		[XmlAttribute("cit")]
		public string City { get; set; }

		[XmlAttribute("sta")]
		public string State { get; set; }

		[XmlAttribute("ddd")]
		public string DDD { get; set; }

		[XmlAttribute("tel")]
		public string Telephone { get; set; }
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

