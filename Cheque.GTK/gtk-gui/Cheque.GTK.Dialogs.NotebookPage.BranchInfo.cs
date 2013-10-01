
// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Dialogs.NotebookPage
{
	public partial class BranchInfo
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Label lblBankName;
		private global::Gtk.Label lblFullBankName;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.Label lblBranchName;
		private global::Gtk.Label lblBranchCNPJ;
		private global::Gtk.HSeparator hseparator2;
		private global::Gtk.Label lblAddress;
		private global::Gtk.HSeparator hseparator3;
		private global::Gtk.Label lblPhone;
		private global::Gtk.HSeparator hseparator4;
		private global::Gtk.Button btnClose;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Dialogs.NotebookPage.BranchInfo
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Cheque.GTK.Dialogs.NotebookPage.BranchInfo";
			// Container child Cheque.GTK.Dialogs.NotebookPage.BranchInfo.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblBankName = new global::Gtk.Label ();
			this.lblBankName.Name = "lblBankName";
			this.lblBankName.LabelProp = global::Mono.Unix.Catalog.GetString ("Banco");
			this.lblBankName.UseMarkup = true;
			this.vbox2.Add (this.lblBankName);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblBankName]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblFullBankName = new global::Gtk.Label ();
			this.lblFullBankName.Name = "lblFullBankName";
			this.lblFullBankName.LabelProp = global::Mono.Unix.Catalog.GetString ("Banco S.A.");
			this.vbox2.Add (this.lblFullBankName);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblFullBankName]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox2.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator1]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblBranchName = new global::Gtk.Label ();
			this.lblBranchName.Name = "lblBranchName";
			this.lblBranchName.LabelProp = global::Mono.Unix.Catalog.GetString ("Ag. Esquina");
			this.vbox2.Add (this.lblBranchName);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblBranchName]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblBranchCNPJ = new global::Gtk.Label ();
			this.lblBranchCNPJ.Name = "lblBranchCNPJ";
			this.lblBranchCNPJ.LabelProp = global::Mono.Unix.Catalog.GetString ("00.000.000/0000-00");
			this.vbox2.Add (this.lblBranchCNPJ);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblBranchCNPJ]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator ();
			this.hseparator2.Name = "hseparator2";
			this.vbox2.Add (this.hseparator2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator2]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblAddress = new global::Gtk.Label ();
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.LabelProp = global::Mono.Unix.Catalog.GetString ("Avenida Brasil, 2908\nCentro\nRio de Janeiro - RJ\n00.000-000");
			this.vbox2.Add (this.lblAddress);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblAddress]));
			w7.Position = 6;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator3 = new global::Gtk.HSeparator ();
			this.hseparator3.Name = "hseparator3";
			this.vbox2.Add (this.hseparator3);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator3]));
			w8.Position = 7;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblPhone = new global::Gtk.Label ();
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.LabelProp = global::Mono.Unix.Catalog.GetString ("+55 (00) 0000-0000");
			this.vbox2.Add (this.lblPhone);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.lblPhone]));
			w9.Position = 8;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hseparator4 = new global::Gtk.HSeparator ();
			this.hseparator4.Name = "hseparator4";
			this.vbox2.Add (this.hseparator4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hseparator4]));
			w10.Position = 9;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.btnClose = new global::Gtk.Button ();
			this.btnClose.CanFocus = true;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseUnderline = true;
			this.btnClose.Label = global::Mono.Unix.Catalog.GetString ("Fechar");
			this.vbox2.Add (this.btnClose);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.btnClose]));
			w11.Position = 10;
			w11.Expand = false;
			w11.Fill = false;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.btnClose.Clicked += new global::System.EventHandler (this.OnBtnCloseClicked);
		}
	}
}
