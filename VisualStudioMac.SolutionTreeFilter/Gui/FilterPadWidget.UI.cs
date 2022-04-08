using System;
using Xwt;

namespace VisualStudioMac.SolutionTreeFilter.Gui
{
    public partial class FilterPadWidget : Widget
    {

        private TextEntry filterEntry;

        private Button filterClearButton;

        private TextEntry projectsEntry;

        private Button applyButton;

        //private HBox hbox2;

        //private CheckButton oneClickCheckbutton;

        //private Button newReleaseAvailableButton;

        //private HBox hbox3;

        //private Label label2;

        //private Entry collapseEntry;

        //private Button collapseButton;

        //private HBox hbox4;

        //private Button ResetPinnedButton;

        //private Button ReloadPropertiesButton;

        //private Button PinOpenDocumentsButton;

        protected virtual void Build()
        {
            HeightRequest = 130;
            MinHeight = 130;
            CanGetFocus = true;
            this.Name = "VisualStudioMac.SolutionTreeFilter.Gui.FilterPadWidget";

            // Tree Filter Section
            var filterHBox = new HBox();
            var filterLabel = new Label("Tree Filter:")
            {
                MarginLeft = 2,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Start,
            };

            filterEntry = new TextEntry
            {
                TooltipText = "Separate by space, colon, semicolon",
                CanGetFocus = true,
                ExpandHorizontal = true,
                HorizontalPlacement = WidgetPlacement.Fill,
                VerticalPlacement = WidgetPlacement.Center,
                Margin = 2,
                MultiLine = true
            };

            filterClearButton = new Button("Clear")
            {
                MarginLeft = 2,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.End,
            };

            filterHBox.PackStart(filterLabel);
            filterHBox.PackStart(filterEntry, true);
            filterHBox.PackStart(filterClearButton);

            // Expand projects section
            var expandHBox = new HBox();
            var expandLabel = new Label("Projects to Expand:")
            {
                MarginLeft = 2,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Start,
            };

            projectsEntry = new TextEntry
            {
                TooltipText = "Separate by space, colon, semicolon",
                CanGetFocus = true,
                Name = "projectsEntry",
                ExpandHorizontal = true,
                HorizontalPlacement = WidgetPlacement.Fill,
                VerticalPlacement = WidgetPlacement.Center,
                Margin = 2,
                MultiLine = true
            };

            applyButton = new Button("Apply")
            {
                MarginLeft = 2,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.End,
            };

            expandHBox.PackStart(expandLabel);
            expandHBox.PackStart(projectsEntry, true);
            expandHBox.PackStart(applyButton);

            var mainVBox = new VBox();
            mainVBox.PackStart(filterHBox, false, false);
            mainVBox.PackStart(expandHBox, false, false);

            //			this.label1.Name = "label1";
            //this.hbox1.Placements.Add(new BoxPlacement() { Child = label1 });
            //Box.BoxChild w1 = ((Box.BoxChild)(this.hbox1[this.label1]));
            //w1.Position = 0;
            //w1.Expand = false;
            //w1.Fill = false;
            //w1.Padding = ((uint)(6));
            // Container child hbox1.Gtk.Box+BoxChild
            //this.filterEntry = new Entry();
            //this.filterEntry.TooltipMarkup = "Separate by space, colon, semicolon";
            //this.filterEntry.CanFocus = true;
            //this.filterEntry.Name = "filterEntry";
            //this.filterEntry.IsEditable = true;
            //this.filterEntry.HasFrame = false;
            //this.filterEntry.InvisibleChar = '●';
            //this.hbox1.Add(this.filterEntry);
            //Box.BoxChild w2 = ((Box.BoxChild)(this.hbox1[this.filterEntry]));
            //w2.Position = 2;
            //w2.Padding = ((uint)(6));
            //// Container child hbox1.Gtk.Box+BoxChild
            //this.button1 = new Button();
            //this.button1.CanFocus = true;
            //this.button1.Name = "button1";
            //this.button1.UseUnderline = true;
            //this.button1.Relief = ((ReliefStyle)(1));
            //this.button1.Label = global::Mono.Unix.Catalog.GetString("Clear");
            //this.hbox1.Add(this.button1);
            //Box.BoxChild w3 = ((Box.BoxChild)(this.hbox1[this.button1]));
            //w3.PackType = ((PackType)(1));
            //w3.Position = 3;
            //w3.Expand = false;
            //w3.Fill = false;
            //w3.Padding = ((uint)(2));
            //this.vbox1.Add(this.hbox1);
            //Box.BoxChild w4 = ((Box.BoxChild)(this.vbox1[this.hbox1]));
            //w4.Position = 0;
            //w4.Expand = false;
            //w4.Fill = false;
            //// Container child vbox1.Gtk.Box+BoxChild
            //this.hbox2 = new HBox();
            //this.hbox2.Name = "hbox2";
            //this.hbox2.Spacing = 6;
            //// Container child hbox2.Gtk.Box+BoxChild
            //this.oneClickCheckbutton = new CheckButton();
            //this.oneClickCheckbutton.CanFocus = true;
            //this.oneClickCheckbutton.Name = "oneClickCheckbutton";
            //this.oneClickCheckbutton.Label = global::Mono.Unix.Catalog.GetString("Use one click to show file");
            //this.oneClickCheckbutton.Active = true;
            //this.oneClickCheckbutton.DrawIndicator = true;
            //this.oneClickCheckbutton.UseUnderline = true;
            //this.hbox2.Add(this.oneClickCheckbutton);
            //Box.BoxChild w5 = ((Box.BoxChild)(this.hbox2[this.oneClickCheckbutton]));
            //w5.Position = 0;
            //// Container child hbox2.Gtk.Box+BoxChild
            //this.newReleaseAvailableButton = new Button();
            //this.newReleaseAvailableButton.CanFocus = true;
            //this.newReleaseAvailableButton.Name = "newReleaseAvailableButton";
            //this.newReleaseAvailableButton.UseUnderline = true;
            //this.newReleaseAvailableButton.Relief = ((ReliefStyle)(1));
            //this.newReleaseAvailableButton.Label = global::Mono.Unix.Catalog.GetString("A new release is available");
            //this.hbox2.Add(this.newReleaseAvailableButton);
            //Box.BoxChild w6 = ((Box.BoxChild)(this.hbox2[this.newReleaseAvailableButton]));
            //w6.Position = 2;
            //w6.Expand = false;
            //w6.Fill = false;
            //w6.Padding = ((uint)(2));
            //this.vbox1.Add(this.hbox2);
            //Box.BoxChild w7 = ((Box.BoxChild)(this.vbox1[this.hbox2]));
            //w7.Position = 1;
            //w7.Expand = false;
            //w7.Fill = false;
            //// Container child vbox1.Gtk.Box+BoxChild
            //this.hbox3 = new HBox();
            //this.hbox3.Name = "hbox3";
            //this.hbox3.Spacing = 6;
            //// Container child hbox3.Gtk.Box+BoxChild
            //this.label2 = new Label();
            //this.label2.Name = "label2";
            //this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Projects to Expand");
            //this.hbox3.Add(this.label2);
            //Box.BoxChild w8 = ((Box.BoxChild)(this.hbox3[this.label2]));
            //w8.Position = 0;
            //w8.Expand = false;
            //w8.Fill = false;
            //w8.Padding = ((uint)(6));
            //// Container child hbox3.Gtk.Box+BoxChild
            //this.collapseEntry = new Entry();
            //this.collapseEntry.TooltipMarkup = "Separate by space, colon, semicolon";
            //this.collapseEntry.CanFocus = true;
            //this.collapseEntry.Name = "collapseEntry";
            //this.collapseEntry.IsEditable = true;
            //this.collapseEntry.HasFrame = false;
            //this.collapseEntry.InvisibleChar = '●';
            //this.hbox3.Add(this.collapseEntry);
            //Box.BoxChild w9 = ((Box.BoxChild)(this.hbox3[this.collapseEntry]));
            //w9.Position = 1;
            //w9.Padding = ((uint)(6));
            //// Container child hbox3.Gtk.Box+BoxChild
            //this.collapseButton = new Button();
            //this.collapseButton.CanFocus = true;
            //this.collapseButton.Name = "collapseButton";
            //this.collapseButton.UseUnderline = true;
            //this.collapseButton.FocusOnClick = false;
            //this.collapseButton.Relief = ((ReliefStyle)(1));
            //this.collapseButton.Label = global::Mono.Unix.Catalog.GetString("Apply");
            //this.hbox3.Add(this.collapseButton);
            //Box.BoxChild w10 = ((Box.BoxChild)(this.hbox3[this.collapseButton]));
            //w10.Position = 2;
            //w10.Expand = false;
            //w10.Fill = false;
            //w10.Padding = ((uint)(2));
            //this.vbox1.Add(this.hbox3);
            //Box.BoxChild w11 = ((Box.BoxChild)(this.vbox1[this.hbox3]));
            //w11.Position = 3;
            //w11.Expand = false;
            //w11.Fill = false;
            //// Container child vbox1.Gtk.Box+BoxChild
            //this.hbox4 = new HBox();
            //this.hbox4.Name = "hbox4";
            //this.hbox4.Spacing = 6;
            //// Container child hbox4.Gtk.Box+BoxChild
            //this.ResetPinnedButton = new Button();
            //this.ResetPinnedButton.CanFocus = true;
            //this.ResetPinnedButton.Name = "ResetPinnedButton";
            //this.ResetPinnedButton.UseUnderline = true;
            //this.ResetPinnedButton.FocusOnClick = false;
            //this.ResetPinnedButton.Label = global::Mono.Unix.Catalog.GetString("Reset Pinned Doc\'s");
            //this.hbox4.Add(this.ResetPinnedButton);
            //Box.BoxChild w12 = ((Box.BoxChild)(this.hbox4[this.ResetPinnedButton]));
            //w12.Position = 0;
            //w12.Expand = false;
            //w12.Fill = false;
            //w12.Padding = ((uint)(2));
            //// Container child hbox4.Gtk.Box+BoxChild
            //this.ReloadPropertiesButton = new Button();
            //this.ReloadPropertiesButton.CanFocus = true;
            //this.ReloadPropertiesButton.Name = "ReloadPropertiesButton";
            //this.ReloadPropertiesButton.UseUnderline = true;
            //this.ReloadPropertiesButton.FocusOnClick = false;
            //this.ReloadPropertiesButton.Label = global::Mono.Unix.Catalog.GetString("Reload Properties");
            //this.hbox4.Add(this.ReloadPropertiesButton);
            //Box.BoxChild w13 = ((Box.BoxChild)(this.hbox4[this.ReloadPropertiesButton]));
            //w13.Position = 2;
            //w13.Expand = false;
            //w13.Fill = false;
            //w13.Padding = ((uint)(6));
            //// Container child hbox4.Gtk.Box+BoxChild
            //this.PinOpenDocumentsButton = new Button();
            //this.PinOpenDocumentsButton.TooltipMarkup = "Pin all open documents in the workbench";
            //this.PinOpenDocumentsButton.CanFocus = true;
            //this.PinOpenDocumentsButton.Name = "PinOpenDocumentsButton";
            //this.PinOpenDocumentsButton.UseUnderline = true;
            //this.PinOpenDocumentsButton.FocusOnClick = false;
            //this.PinOpenDocumentsButton.Label = global::Mono.Unix.Catalog.GetString("Pin All Open Doc\'s");
            //this.hbox4.Add(this.PinOpenDocumentsButton);
            //Box.BoxChild w14 = ((Box.BoxChild)(this.hbox4[this.PinOpenDocumentsButton]));
            //w14.PackType = ((PackType)(1));
            //w14.Position = 4;
            //w14.Expand = false;
            //w14.Fill = false;
            //w14.Padding = ((uint)(2));
            //this.vbox1.Add(this.hbox4);
            //Box.BoxChild w15 = ((Box.BoxChild)(this.vbox1[this.hbox4]));
            //w15.Position = 4;
            //w15.Expand = false;
            //w15.Fill = false;
            //this.Add(this.vbox1);
            //if ((this.Child != null))
            //{
            //	this.Child.ShowAll();
            //}


            //paned = new HPaned();
            ////var mainVBox = new VBox();
            //label = new Label("Hello world!");
            //paned.Panel1.Content = label;
            Content = mainVBox;

            //this.newReleaseAvailableButton.Hide();
            //this.Show();
            //this.filterEntry.Changed += new global::System.EventHandler(this.OnFilterEntryChanged);
            //this.button1.Clicked += new global::System.EventHandler(this.clearButton_Clicked);
            //this.oneClickCheckbutton.Toggled += new global::System.EventHandler(this.oneClickCheckbutton_Toggled);
            //this.newReleaseAvailableButton.Clicked += new global::System.EventHandler(this.NewReleaseAvailableButton_Clicked);
            //this.collapseButton.Clicked += new global::System.EventHandler(this.collapseButton_Clicked);
            //this.ResetPinnedButton.Clicked += new global::System.EventHandler(this.ResetPinnedDocuments_Clicked);
            //this.ReloadPropertiesButton.Clicked += new global::System.EventHandler(this.ReloadPropertiesButton_Clicked);
            //this.PinOpenDocumentsButton.Clicked += new global::System.EventHandler(this.PinOpenDocuments_Clicked);
        }
    }
}

