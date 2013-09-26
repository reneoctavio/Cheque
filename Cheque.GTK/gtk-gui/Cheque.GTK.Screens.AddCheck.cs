
// This file has been generated by the GUI designer. Do not modify.
namespace Cheque.GTK.Screens
{
	public partial class AddCheck
	{
		private global::Gtk.VBox vbox4;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeview;
		private global::Gtk.Label lblTotal;
		private global::Gtk.HBox hbox5;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOK;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Cheque.GTK.Screens.AddCheck
			this.Name = "Cheque.GTK.Screens.AddCheck";
			this.Title = global::Mono.Unix.Catalog.GetString ("Adicionar Cheque");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.DefaultWidth = 1024;
			this.DefaultHeight = 768;
			// Container child Cheque.GTK.Screens.AddCheck.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeview = new global::Gtk.TreeView ();
			this.treeview.CanFocus = true;
			this.treeview.Name = "treeview";
			this.treeview.EnableSearch = false;
			this.GtkScrolledWindow.Add (this.treeview);
			this.vbox4.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.GtkScrolledWindow]));
			w2.Position = 0;
			// Container child vbox4.Gtk.Box+BoxChild
			this.lblTotal = new global::Gtk.Label ();
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Xalign = 1F;
			this.lblTotal.LabelProp = global::Mono.Unix.Catalog.GetString ("Total: R$ 0,00");
			this.lblTotal.Justify = ((global::Gtk.Justification)(1));
			this.vbox4.Add (this.lblTotal);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.lblTotal]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Cancel");
			this.hbox5.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.buttonCancel]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonOK = new global::Gtk.Button ();
			this.buttonOK.CanFocus = true;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseUnderline = true;
			this.buttonOK.Label = global::Mono.Unix.Catalog.GetString ("OK");
			this.hbox5.Add (this.buttonOK);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.buttonOK]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox4.Add (this.hbox5);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.hbox5]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			this.Add (this.vbox4);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.buttonOK.Clicked += new global::System.EventHandler (this.OnButtonOKClicked);
		}
	}
}