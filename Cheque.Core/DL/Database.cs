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
using System.IO;
using System.Linq;
using Cheque.DL.SQLite;
using Cheque.BL;

namespace Cheque.DL
{
	public class Database : SQLiteConnection
	{
		protected static Database me = null;
		protected static string dbLocation;
		static object locker = new object ();

		protected Database (string path) : base(path)
		{
			CreateTable<Bank> ();
			CreateTable<Branch> ();
			CreateTable<CheckClass> ();
			CreateTable<Customer> ();
			CreateTable<BL.Password.Password> ();
		}

		static Database ()
		{
			dbLocation = DatabaseFilePath;
			me = new Database (dbLocation);
		}

		public static string DatabaseFilePath {
			get { 
				// TODO : Add compatibilty for other platforms
				var path = "ChequeDB.db3";
				return path;	
			}
		}

		public static IEnumerable<T> GetItems<T> () where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {
				return (from i in me.Table<T> () select i).ToList ();
			}
		}

		public static T GetItem<T> (int id) where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {

				// ---
				//return (from i in me.Table<T> ()
				//        where i.ID == id
				//        select i).FirstOrDefault ();

				// +++ To properly use Generic version and eliminate NotSupportedException
				// ("Cannot compile: " + expr.NodeType.ToString ()); in SQLite.cs
				return me.Table<T> ().FirstOrDefault (x => x.ID == id);
			}
		}

		public static int SaveItem<T> (T item) where T : BL.Contracts.IBusinessEntity
		{
			lock (locker) {
				if (item.ID != 0) {
					me.Update (item);
					return item.ID;
				} else {
					return me.Insert (item);
				}
			}
		}

		public static void SaveItems<T> (IEnumerable<T> items) where T : BL.Contracts.IBusinessEntity
		{
			lock (locker) {
				me.BeginTransaction ();

				foreach (T item in items) {
					SaveItem<T> (item);
				}

				me.Commit ();
			}
		}
		/*
		public static int DeleteItem<T> (int id) where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {
				return me.Delete<T> (new T () { ID = id });
			}
		}
		*/
		public static int DeleteItem<T> (T item) where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {
				return me.Delete (item);
			}
		}

		public static void ClearTable<T> () where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {
				me.Execute (string.Format ("delete from \"{0}\"", typeof(T).Name));
			}
		}
		// helper for checking if database has been populated
		public static int CountTable<T> () where T : BL.Contracts.IBusinessEntity, new()
		{
			lock (locker) {
				string sql = string.Format ("select count (*) from \"{0}\"", typeof(T).Name);
				var c = me.CreateCommand (sql, new object[0]);
				return c.ExecuteScalar<int> ();
			}
		}

		/// <summary>
		/// Gets the Bank and populate it with branches, customers and checks
		/// </summary>
		/// <returns>The bank.</returns>
		/// <param name="bankNumber">Bank number.</param>
		public static Bank GetBank (string bankNumber)
		{
			lock (locker) {
				// Find Bank in the Database
				Bank bank = (from ba in me.Table<Bank> ()
				             where ba.BankNumber == bankNumber
				             select ba).FirstOrDefault ();

				if (bank != null) {
					// Add branches to this bank object
					bank.Branches = (from br in me.Table<Branch> ()
				                 where br.BankNumber == bankNumber
				                 select br).ToList ();

					// Add checks to this bank object
					bank.Checks = (from chk in me.Table<CheckClass> ()
				               where chk.BankNumber == bankNumber
				               select chk).ToList ();
				}
				return bank;
			}
		}

		/// <summary>
		/// Gets the branch given its number and its bank number
		/// </summary>
		/// <returns>The branch.</returns>
		/// <param name="branchNumber">Branch number.</param>
		/// <param name="bankNumber">Bank number.</param>
		public static Branch GetBranch (string branchNumber, string bankNumber)
		{
			if ((branchNumber == null) || (branchNumber == "")) {
				return null;
			}

			if ((bankNumber == null) || (bankNumber == "")) {
				return null;
			}

			lock (locker) {
				// Find branch in the database
				Branch branch = (from bch in me.Table<Branch> ()
				                 where ((bch.BranchNumber == branchNumber) && (bch.BankNumber == bankNumber))
				                 select bch).FirstOrDefault ();

				if (branch != null) {
					// Add checks to this branch object
					branch.Checks = (from chk in me.Table<CheckClass> ()
				                 where ((chk.BranchNumber == branchNumber) && (chk.BankNumber == bankNumber))
				                 select chk).ToList ();
				}
				return branch;
			}
		}

		/// <summary>
		/// Gets the check based on its number, bank, branch and customer
		/// </summary>
		/// <returns>The check.</returns>
		/// <param name="checkNumber">Check number.</param>
		/// <param name="bankNumber">Bank number.</param>
		/// <param name="branchNumber">Branch number.</param>
		/// <param name="customerID">Customer ID.</param>
		public static CheckClass GetCheck (string checkNumber, string bankNumber, string branchNumber, string serial, string customerID)
		{
			lock (locker) {
				CheckClass check = (from chk in me.Table<CheckClass> ()
				               where ((chk.Number == checkNumber)
					&& (chk.BankNumber == bankNumber)
					&& (chk.BranchNumber == branchNumber)
					&& (chk.Serial == serial)
					&& (chk.CustomerID == customerID))
				               select chk).FirstOrDefault ();

				return check;
			}
		}

		/// <summary>
		/// Gets the checks by due date.
		/// </summary>
		/// <returns>The checks by due date.</returns>
		/// <param name="dateMin">Date minimum.</param>
		/// <param name="dateMax">Date max.</param>
		public static IEnumerable<CheckClass> GetChecksByDueDate (DateTime dateMin, DateTime dateMax)
		{
			lock (locker) {
				return (from i in me.Table<CheckClass> ()
				        where i.DueDate >= dateMin && i.DueDate <= dateMax
				        select i).ToList ();
			}
		}

		/// <summary>
		/// Gets the overdue checks.
		/// </summary>
		/// <returns>The overdue checks.</returns>
		public static IEnumerable<CheckClass> GetOverdueChecks ()
		{
			lock (locker) {
				return (from i in me.Table<CheckClass> ()
				        where ((i.DueDate <= DateTime.Now) && (!i.IsCashed))
				        select i).ToList ();
			}
		}

		/// <summary>
		/// Gets the customer.
		/// </summary>
		/// <returns>The customer.</returns>
		/// <param name="id">Identifier.</param>
		public static Customer GetCustomer (string id)
		{
			lock (locker) {
				Customer customer = (from i in me.Table<Customer> ()
				        where (i.Identity == id)
				        select i).FirstOrDefault ();

				if (customer != null) {
					customer.AllChecks = (from chk in me.Table<CheckClass> ()
					                      where chk.CustomerID.Equals (customer.Identity)
					                      select chk).ToList (); 
					customer.PaidChecks = (from chk in me.Table<CheckClass> ()
					                       where (chk.CustomerID.Equals (customer.Identity) && (chk.IsCashed))
					                       select chk).ToList (); 

					customer.OverdueChecks = (from chk in me.Table<CheckClass> ()
					                         where (chk.CustomerID.Equals (customer.Identity) && (chk.IsCashed == false))
					                         select chk).ToList (); 
					customer.PaidOverdueChecks = (from chk in me.Table<CheckClass> ()
					                          where (chk.CustomerID.Equals (customer.Identity) && (chk.CashedOverdue))
					                          select chk).ToList (); 
					customer.NotDueChecks = (from chk in me.Table<CheckClass> ()
					                              where (chk.CustomerID.Equals (customer.Identity) && (chk.DueDate > DateTime.Now))
					                              select chk).ToList (); 
				}
				return customer;
			}
		}
	}
}



