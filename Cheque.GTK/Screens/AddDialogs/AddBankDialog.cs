using System;
using Cheque;
using Gtk;

namespace Cheque.GTK.Screens
{
	public partial class AddBankDialog : Gtk.Dialog
	{
		public AddBankDialog ()
		{
			this.Build ();
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			// Standard Bank Number
			string standardNumber = BL.Formatter.ZeroPad (this.entryBankNumber.Text, BL.Constants.LENGTH_NUM_BANK);

			// If Bank number exceeds its maximum length, give an error
			if (standardNumber == null) {
				MessageDialog md = new MessageDialog (this, 
				                                      DialogFlags.DestroyWithParent,
				                                      MessageType.Error, 
				                                      ButtonsType.Close, "Número inválido!");
				md.Run ();
				md.Destroy ();

			} else {
				// If number is valid then
				// Verify if Bank exists and add if not
				if (DAL.DataManager.GetBank (standardNumber) == null) {
					DAL.DataManager.AddBank (standardNumber, this.entryName.Text);
					this.Destroy ();
				} else {
					// Error message if bank already exists
					MessageDialog md = new MessageDialog (this, 
					                                      DialogFlags.DestroyWithParent,
					                                      MessageType.Error, 
					                                      ButtonsType.Close, "Banco existente!");
					md.Run ();
					md.Destroy ();
				}
			}
		}

		protected void OnButtonCancelClicked (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

