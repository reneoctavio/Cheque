using System;
using System.Xml.Serialization;

namespace Cheque.BL
{
	public partial class CheckClass : Contracts.BusinessEntityBase
	{
		[XmlElement("n")]
		public string Number { get; set; }

		[XmlElement("banu")]
		public string BankNumber { get; set; }

		[XmlElement("brnu")]
		public string BranchNumber { get; set; }

		[XmlElement("ser")]
		public string Serial { get; set; }

		[XmlElement("cid")]
		public string CustomerID { get; set; }

		[XmlElement("isd")]
		public DateTime IssueDate { get; set; }

		[XmlElement("dd")]
		public DateTime DueDate { get; set; }

		[XmlElement("cad")]
		public DateTime CashDate { get; set; }

		[XmlElement("csh")]
		public bool IsCashed { get; set; }

		[XmlElement("csho")]
		public bool CashedOverdue { get; set; }

		[XmlElement("va")]
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

