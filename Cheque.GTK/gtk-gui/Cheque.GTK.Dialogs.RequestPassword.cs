
// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Dialogs
{
	public partial class RequestPassword
	{
		private global::Gtk.Label lblEnterPasswd;
		private global::Gtk.Entry entryPassword;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Dialogs.RequestPassword
			this.Name = "Cheque.GTK.Dialogs.RequestPassword";
			this.Title = global::Mono.Unix.Catalog.GetString ("Digite a senha");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child Cheque.GTK.Dialogs.RequestPassword.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.lblEnterPasswd = new global::Gtk.Label ();
			this.lblEnterPasswd.Name = "lblEnterPasswd";
			this.lblEnterPasswd.LabelProp = global::Mono.Unix.Catalog.GetString ("Digite a senha abaixo:");
			w1.Add (this.lblEnterPasswd);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1 [this.lblEnterPasswd]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.InvisibleChar = '●';
			w1.Add (this.entryPassword);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(w1 [this.entryPassword]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Internal child Cheque.GTK.Dialogs.RequestPassword.ActionArea
			global::Gtk.HButtonBox w4 = this.ActionArea;
			w4.Name = "dialog1_ActionArea";
			w4.Spacing = 10;
			w4.BorderWidth = ((uint)(5));
			w4.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w5 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w4 [this.buttonOk]));
			w5.Expand = false;
			w5.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 88;
			this.Show ();
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}
