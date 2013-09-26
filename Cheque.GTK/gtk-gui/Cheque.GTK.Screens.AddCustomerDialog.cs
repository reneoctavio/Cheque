
// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Screens
{
	public partial class AddCustomerDialog
	{
		private global::Gtk.VBox vbox3;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label lblName;
		private global::Gtk.Entry entryName;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Label lbl_ID;
		private global::Gtk.Entry entryID;
		private global::Gtk.Label lblValid;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Screens.AddCustomerDialog
			this.Name = "Cheque.GTK.Screens.AddCustomerDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Adicionar Cliente");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Cheque.GTK.Screens.AddCustomerDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.lblName = new global::Gtk.Label ();
			this.lblName.Name = "lblName";
			this.lblName.LabelProp = global::Mono.Unix.Catalog.GetString ("Nome:");
			this.hbox3.Add (this.lblName);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.lblName]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entryName = new global::Gtk.Entry ();
			this.entryName.CanFocus = true;
			this.entryName.Name = "entryName";
			this.entryName.IsEditable = true;
			this.entryName.InvisibleChar = '●';
			this.hbox3.Add (this.entryName);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.entryName]));
			w3.Position = 1;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox3]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.lbl_ID = new global::Gtk.Label ();
			this.lbl_ID.Name = "lbl_ID";
			this.lbl_ID.LabelProp = global::Mono.Unix.Catalog.GetString ("CPF/CNPJ:");
			this.hbox4.Add (this.lbl_ID);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.lbl_ID]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.entryID = new global::Gtk.Entry ();
			this.entryID.CanFocus = true;
			this.entryID.Name = "entryID";
			this.entryID.IsEditable = true;
			this.entryID.InvisibleChar = '●';
			this.hbox4.Add (this.entryID);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.entryID]));
			w6.Position = 1;
			// Container child hbox4.Gtk.Box+BoxChild
			this.lblValid = new global::Gtk.Label ();
			this.lblValid.Name = "lblValid";
			this.lblValid.LabelProp = global::Mono.Unix.Catalog.GetString ("Inválido");
			this.hbox4.Add (this.lblValid);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.lblValid]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox3.Add (this.hbox4);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox4]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			w1.Add (this.vbox3);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox3]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Internal child Cheque.GTK.Screens.AddCustomerDialog.ActionArea
			global::Gtk.HButtonBox w10 = this.ActionArea;
			w10.Name = "dialog1_ActionArea";
			w10.Spacing = 10;
			w10.BorderWidth = ((uint)(5));
			w10.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w10 [this.buttonCancel]));
			w11.Expand = false;
			w11.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w12 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w10 [this.buttonOk]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 115;
			this.Show ();
			this.entryID.Changed += new global::System.EventHandler (this.OnEntryIDChanged);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}