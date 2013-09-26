using System;
using Cheque;
using Gtk;

namespace Cheque.GTK.Screens
{
	public partial class AddCustomerDialog : Gtk.Dialog
	{
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
}

