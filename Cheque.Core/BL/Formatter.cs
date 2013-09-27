using System;
using System.Globalization;
using System.Text;

namespace Cheque.BL
{
	public static class Formatter
	{
		/// <summary>
		/// Pad an integer with leading zeros to a specific length
		/// Return null if number exceeds the maximum length
		/// </summary>
		/// <returns>The padded number or null if given number has a length greater than given length</returns>
		/// <param name="number">Number.</param>
		/// <param name="length">Length.</param>
		public static string ZeroPad (string number, int length)
		{
			// Check lenght
			if ((number.Length > length) || (number.Length <= 0)) {
				return null;
			}
			//Pad number
			int num = Int32.Parse (number);
			return num.ToString ("D" + length.ToString ());
		}

		/// <summary>
		/// Determines if the CNPJ is valid
		/// </summary>
		/// <returns>Whether the CNPJ is valid</returns>
		/// <param name="cnpj">CNPJ.</param>
		/// <seealso cref="http://www.macoratti.net/11/09/c_val1.htm"/>
		public static bool IsCNPJ (string cnpj)
		{
			int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma;
			int resto;
			string digito;
			string tempCnpj;
			cnpj = cnpj.Trim ();
			cnpj = cnpj.Replace (".", "").Replace ("-", "").Replace ("/", "");
			if (cnpj.Length != 14)
				return false;
			tempCnpj = cnpj.Substring (0, 12);
			soma = 0;
			for (int i = 0; i < 12; i++)
				soma += int.Parse (tempCnpj [i].ToString ()) * multiplicador1 [i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString ();
			tempCnpj = tempCnpj + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
				soma += int.Parse (tempCnpj [i].ToString ()) * multiplicador2 [i];
			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString ();
			return cnpj.EndsWith (digito);
		}

		/// <summary>
		/// Determines if the CPF is valid
		/// </summary>
		/// <returns><c>true</c> if the CPF is valid; otherwise, <c>false</c>.</returns>
		/// <param name="cpf">CPF.</param>
		public static bool IsCPF (string cpf)
		{
			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim ();
			cpf = cpf.Replace (".", "").Replace ("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring (0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse (tempCpf [i].ToString ()) * multiplicador1 [i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString ();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse (tempCpf [i].ToString ()) * multiplicador2 [i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString ();
			return cpf.EndsWith (digito);
		}

		/// <summary>
		/// Converts the date from a string
		/// </summary>
		/// <returns>The date.</returns>
		/// <param name="date">Date.</param>
		public static DateTime ConvertDateFromString (string date)
		{
			var formatDate = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy" };
			return DateTime.Parse (date, formatDate);
		}

		/// <summary>
		/// Converts the date to a string.
		/// </summary>
		/// <returns>The date in a string form of dd/MM/yyyy.</returns>
		/// <param name="date">Date.</param>
		public static string ConvertDateToString (DateTime date)
		{
			return date.ToString ("dd/MM/yyyy");
		}

		/// <summary>
		/// Remove diacritics from strings 
		/// </summary>
		/// <example>
		/// input: "Příliš žluťoučký kůň úpěl ďábelské ódy."
		/// result: "Prilis zlutoucky kun upel dabelske ody."
		/// </example>
		/// <param name="s">String containg diacritics</param>
		/// <remarks>found at http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net</remarks>
		/// <returns>string without accents</returns>

		public static string RemoveDiacritics (this string s)
		{
			string stFormD = s.Normalize (NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder ();

			for (int ich = 0; ich < stFormD.Length; ich++) {
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory (stFormD [ich]);
				if (uc != UnicodeCategory.NonSpacingMark) {
					sb.Append (stFormD [ich]);
				}
			}
			return (sb.ToString ().Normalize (NormalizationForm.FormC));
		}
	}
}

