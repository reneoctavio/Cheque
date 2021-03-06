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

		/// <summary>
		/// Compare two numeric strings
		/// </summary>
		/// <returns>Greater than 0 if s1 is greater than s2. 0 if they are equal. Less than zero otherwise.</returns>
		/// <param name="s1">String 1.</param>
		/// <param name="s2">String 2.</param>
		public static int NumericStringSort (string s1, string s2)
		{
			if (s1.Length > s2.Length) {
				return 1;
			} else if (s1.Length < s2.Length) {
				return -1;
			} else {
				return s1.CompareTo (s2);
			}
		}

		/// <summary>
		/// Given a numeric CPF, format it.
		/// </summary>
		/// <returns>The Formatted CPF.</returns>
		/// <param name="cpf">Numeric CPF.</param>
		public static string FormatCPF (string cpf)
		{
			return cpf.Substring (0, 3) + "." + cpf.Substring (3, 3) + "." + cpf.Substring (6, 3) + "-" + cpf.Substring (9, 2);
		}

		/// <summary>
		/// Given a numeric CNPJ, format it.
		/// </summary>
		/// <returns>The Formatted CNPJ.</returns>
		/// <param name="cpf">Numeric CNPJ.</param>
		public static string FormatCNPJ (string cnpj)
		{
			return cnpj.Substring (0, 2) + "." + cnpj.Substring (2, 3) + "." + cnpj.Substring (5, 3) + "/" + cnpj.Substring (8, 4) + "-" + cnpj.Substring (12, 2);
		}

		/// <summary>
		/// Given a formatted CNPJ or CPF, return a numeric one.
		/// </summary>
		/// <returns>The numeric CNPJ or CPF.</returns>
		/// <param name="idString">Formatted CNPJ or CPF.</param>
		public static string GetNumericID (string idString)
		{
			return idString.Replace (".", "").Replace ("-", "").Replace ("/", "");
		}

		/// <summary>
		/// Gets the value without currency.
		/// </summary>
		/// <returns><c>true</c>, if value without currency was gotten, <c>false</c> otherwise.</returns>
		/// <param name="valueWithCurrency">Value with currency.</param>
		/// <param name="value">Value.</param>
		public static bool GetValueWithoutCurrency (string valueWithCurrency, out Decimal value)
		{
			bool isParsed = false;
			string[] splittedString = valueWithCurrency.Split ();

			// Splitting for removing the $ string and getting the number only
			if (splittedString.Length == 2) {
				isParsed = Decimal.TryParse (splittedString [1], out value);
				if (!isParsed)
					isParsed = Decimal.TryParse (splittedString [0], out value);
			} else {
				isParsed = Decimal.TryParse (splittedString [0], out value);
			}

			return isParsed;
		}
	}
}

