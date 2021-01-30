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
using Cheque;
using Gtk;

namespace Cheque.GTK.Dialogs
{
	public partial class AddCustomerDialog : Gtk.Dialog
	{
		// A delegate type for hooking up new customer notification
		public delegate void AddedCustomerEventHandler (object sender, AddCustomerEventArgs e);
		// An event that clients can use to be notified whenever a new customer is added
		public event AddedCustomerEventHandler AddedCustomer;
		// Invoke the event; called whenever a customer is added
		protected virtual void OnAdditionCustomer (AddCustomerEventArgs e)
		{
			if (AddedCustomer != null)
				AddedCustomer (this, e);
		}

		public AddCustomerDialog ()
		{
			this.Build ();
		}

		public Gtk.Entry GetEntryID ()
		{
			return this.entryID;
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			// Check if is a valid id
			if (BL.Formatter.IsCPF (this.entryID.Text)) {

				// Check if customer exists
				if (DAL.DataManager.GetCustomer (this.entryID.Text) != null) {
					MessageDialog md = new MessageDialog (this, 
					                                      DialogFlags.DestroyWithParent,
					                                      MessageType.Error, 
					                                      ButtonsType.Close, "Número existente!");
					md.Run ();
					md.Destroy ();
				} else {
					DAL.DataManager.AddCustomer (this.entryID.Text, this.entryName.Text, BL.Customer.TypeID.CPF);
					OnAdditionCustomer (new AddCustomerEventArgs (this.entryID.Text));
					this.Destroy ();
				}
			} else if (BL.Formatter.IsCNPJ (this.entryID.Text)) {
				// Check if customer exists
				if (DAL.DataManager.GetCustomer (this.entryID.Text) != null) {
					MessageDialog md = new MessageDialog (this, 
					                                      DialogFlags.DestroyWithParent,
					                                      MessageType.Error, 
					                                      ButtonsType.Close, "Número existente!");
					md.Run ();
					md.Destroy ();
				} else {
					DAL.DataManager.AddCustomer (this.entryID.Text, this.entryName.Text, BL.Customer.TypeID.CNPJ);
					OnAdditionCustomer (new AddCustomerEventArgs (this.entryID.Text));
					this.Destroy ();
				}
			} else {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Close, "Número inválido!");
				md.Run ();
				md.Destroy ();
			}
		}

		protected void OnEntryIDChanged (object sender, EventArgs e)
		{
			if (BL.Formatter.IsCPF (this.entryID.Text)) {
				this.lblValid.Text = "CPF Válido";
			} else if (BL.Formatter.IsCNPJ (this.entryID.Text)) {
				this.lblValid.Text = "CNPJ Válido";
			} else {
				this.lblValid.Text = "Inválido";
			}
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}

	public class AddCustomerEventArgs : EventArgs
	{
		public string CustomerID { get; private set; }

		public AddCustomerEventArgs (string customer)
		{
			CustomerID = customer;
		}
	}
}

