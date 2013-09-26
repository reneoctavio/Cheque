
// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Screens
{
	public partial class AddBankDialog
	{
		private global::Gtk.VBox propVbox;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label lblName;
		private global::Gtk.Entry entryName;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label lblBankNumber;
		private global::Gtk.Entry entryBankNumber;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Screens.AddBankDialog
			this.Name = "Cheque.GTK.Screens.AddBankDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Adicionar Banco");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Cheque.GTK.Screens.AddBankDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.propVbox = new global::Gtk.VBox ();
			this.propVbox.Name = "propVbox";
			this.propVbox.Spacing = 6;
			// Container child propVbox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.lblName = new global::Gtk.Label ();
			this.lblName.Name = "lblName";
			this.lblName.LabelProp = global::Mono.Unix.Catalog.GetString ("Nome:");
			this.hbox1.Add (this.lblName);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.lblName]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryName = new global::Gtk.Entry ();
			this.entryName.CanFocus = true;
			this.entryName.Name = "entryName";
			this.entryName.IsEditable = true;
			this.entryName.InvisibleChar = '●';
			this.hbox1.Add (this.entryName);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryName]));
			w3.Position = 1;
			this.propVbox.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.propVbox [this.hbox1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child propVbox.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.lblBankNumber = new global::Gtk.Label ();
			this.lblBankNumber.Name = "lblBankNumber";
			this.lblBankNumber.LabelProp = global::Mono.Unix.Catalog.GetString ("Número do Banco:");
			this.hbox2.Add (this.lblBankNumber);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.lblBankNumber]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.entryBankNumber = new global::Gtk.Entry ();
			this.entryBankNumber.CanFocus = true;
			this.entryBankNumber.Name = "entryBankNumber";
			this.entryBankNumber.IsEditable = true;
			this.entryBankNumber.WidthChars = 3;
			this.entryBankNumber.InvisibleChar = '●';
			this.hbox2.Add (this.entryBankNumber);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.entryBankNumber]));
			w6.Position = 1;
			this.propVbox.Add (this.hbox2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.propVbox [this.hbox2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			w1.Add (this.propVbox);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(w1 [this.propVbox]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Internal child Cheque.GTK.Screens.AddBankDialog.ActionArea
			global::Gtk.HButtonBox w9 = this.ActionArea;
			w9.Name = "dialog1_ActionArea";
			w9.Spacing = 10;
			w9.BorderWidth = ((uint)(5));
			w9.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w10 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9 [this.buttonCancel]));
			w10.Expand = false;
			w10.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9 [this.buttonOk]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 446;
			this.DefaultHeight = 112;
			this.Show ();
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}
