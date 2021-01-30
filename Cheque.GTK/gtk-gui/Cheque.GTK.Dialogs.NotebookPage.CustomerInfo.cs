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

// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Dialogs.NotebookPage
{
	public partial class CustomerInfo
	{
		private global::Gtk.VBox vbox3;
		private global::Gtk.Label lblName;
		private global::Gtk.Label lbl_ID;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.Label lblTotal;
		private global::Gtk.Label lblOverdue;
		private global::Gtk.Label lblPaidDue;
		private global::Gtk.Label lblNotDue;
		private global::Gtk.Button btnClose;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Dialogs.NotebookPage.CustomerInfo
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Cheque.GTK.Dialogs.NotebookPage.CustomerInfo";
			// Container child Cheque.GTK.Dialogs.NotebookPage.CustomerInfo.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lblName = new global::Gtk.Label ();
			this.lblName.Name = "lblName";
			this.lblName.LabelProp = global::Mono.Unix.Catalog.GetString ("Nome");
			this.lblName.UseMarkup = true;
			this.vbox3.Add (this.lblName);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lblName]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lbl_ID = new global::Gtk.Label ();
			this.lbl_ID.Name = "lbl_ID";
			this.lbl_ID.LabelProp = global::Mono.Unix.Catalog.GetString ("ID");
			this.vbox3.Add (this.lbl_ID);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lbl_ID]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox3.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hseparator1]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lblTotal = new global::Gtk.Label ();
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.LabelProp = global::Mono.Unix.Catalog.GetString ("Este cliente tem # cheques cadastrados no total de R$ 0,00.");
			this.vbox3.Add (this.lblTotal);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lblTotal]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lblOverdue = new global::Gtk.Label ();
			this.lblOverdue.Name = "lblOverdue";
			this.lblOverdue.LabelProp = global::Mono.Unix.Catalog.GetString ("Este cliente está inadimplente, com # cheques não pagos na data de vencimento.");
			this.vbox3.Add (this.lblOverdue);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lblOverdue]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lblPaidDue = new global::Gtk.Label ();
			this.lblPaidDue.Name = "lblPaidDue";
			this.lblPaidDue.LabelProp = global::Mono.Unix.Catalog.GetString ("Este cliente paga #% de seus cheques até sua data de vencimento.");
			this.vbox3.Add (this.lblPaidDue);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lblPaidDue]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.lblNotDue = new global::Gtk.Label ();
			this.lblNotDue.Name = "lblNotDue";
			this.lblNotDue.LabelProp = global::Mono.Unix.Catalog.GetString ("Este cliente tem ainda # cheques a vencer, totalizando: R$ 0,00");
			this.vbox3.Add (this.lblNotDue);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.lblNotDue]));
			w7.Position = 6;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.btnClose = new global::Gtk.Button ();
			this.btnClose.CanFocus = true;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseUnderline = true;
			this.btnClose.Label = global::Mono.Unix.Catalog.GetString ("Fechar");
			this.vbox3.Add (this.btnClose);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.btnClose]));
			w8.Position = 7;
			w8.Expand = false;
			w8.Fill = false;
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.btnClose.Clicked += new global::System.EventHandler (this.OnBtnCloseClicked);
		}
	}
}