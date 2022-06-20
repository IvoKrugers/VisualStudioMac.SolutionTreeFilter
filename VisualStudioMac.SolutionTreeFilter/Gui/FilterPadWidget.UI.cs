using System;
using AppKit;
using CoreGraphics;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using Xwt;
using Xwt.Drawing;

namespace VisualStudioMac.SolutionTreeFilter.Gui
{
    public partial class FilterPadWidget : Widget
    {

        private TextEntry filterEntry;

        private Button filterClearButton;

        private TextEntry projectsEntry;

        private Button applyButton;

        private CheckBox doubleClickToPinCheckbutton;

        private Button resetPinnedButton;
        
        private Button pinOpenDocumentsButton;

        protected virtual void Build()
        {
           
            CanGetFocus = true;

            //BackgroundColor = Color.FromBytes(37,40,45);
            //BackgroundColor = Color.FromBytes(30, 30, 30);

            // Tree Filter Section
            var filterHBox = new HBox();
            var filterLabel = new Label("Tree Filter:")
            {
                MarginLeft = 6,
                MarginRight = 0,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Fill,
                VerticalPlacement = WidgetPlacement.Center,
            };

            filterClearButton = new Button(ImageService.GetIcon("gtk-delete", Xwt.IconSize.Small))
            {
                MarginLeft = 0,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.End,
                VerticalPlacement = WidgetPlacement.Center,
                Style = ButtonStyle.Borderless,
                TooltipText = "Clear"
            };
            var refreshButton = new Button(ImageService.GetIcon("gtk-refresh", Xwt.IconSize.Small))
            {
                MarginLeft = 0,
                MarginRight = 6,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.End,
                VerticalPlacement = WidgetPlacement.Center,
                Style = ButtonStyle.Borderless,
                TooltipText = "Apply filter"
            };

            filterHBox.PackStart(filterLabel);
            filterHBox.PackEnd(refreshButton);
            filterHBox.PackEnd(filterClearButton);


            filterEntry = new TextEntry
            {
                TooltipText = "Search terms separated by space, colon or semicolon",
                CanGetFocus = true,
                ExpandHorizontal = true,
                HorizontalPlacement = WidgetPlacement.Fill,
                VerticalPlacement = WidgetPlacement.Fill,
                MarginLeft = 8,
                MarginRight = 8,
                Sensitive = true,
                MultiLine = true,
                PlaceholderText = "",
            };

            // Expand projects section
            var expandHBox = new HBox();
            var expandLabel = new Label("Projects to Expand:")
            {
                MarginLeft = 6,
                MarginRight = 0,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Start,
            };

            projectsEntry = new TextEntry
            {
                TooltipText = "Project search terms separate by space, colon or semicolon",
                CanGetFocus = true,
                Name = "projectsEntry",
                ExpandHorizontal = true,
                HorizontalPlacement = WidgetPlacement.Fill,
                VerticalPlacement = WidgetPlacement.Center,
                MarginLeft = 2,
                MarginRight = 2,
                MultiLine = false,
                PlaceholderText = ""
            };

            applyButton = new Button(ImageService.GetIcon("gtk-refresh"))
            {
                TooltipText = "Apply the Project to expand",
                MarginLeft = 0,
                MarginRight = 6,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.End,
                VerticalPlacement = WidgetPlacement.Center,
                Style = ButtonStyle.Borderless
            };

            expandHBox.PackStart(expandLabel);
            expandHBox.PackStart(projectsEntry, true, true);
            expandHBox.PackEnd(applyButton);

            // Expand projects section
            var buttonsHBox = new HBox()
            {
                MarginBottom = 6
            };

            pinOpenDocumentsButton = new Button(ImageService.GetIcon(Stock.PinDown, Xwt.IconSize.Small), "Pin All Open Docs")
            {
                MarginLeft = 6,
                MarginRight = 2,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Center,
            };

            resetPinnedButton = new Button(ImageService.GetIcon(Stock.PinUp, Xwt.IconSize.Small), "Unpin All")
            {
                MarginLeft = 2,
                MarginRight = 6,
                ExpandHorizontal = false,
                HorizontalPlacement = WidgetPlacement.Center,
            };

            doubleClickToPinCheckbutton = new CheckBox("Double-Click to Pin")
            {
                MarginLeft = 6,
                MarginRight = 6,
                ExpandHorizontal = true,
                HorizontalPlacement = WidgetPlacement.Center,
            };

            buttonsHBox.PackStart(pinOpenDocumentsButton);
            //buttonsHBox.PackStart(new FrameBox(), true, true);
            buttonsHBox.PackStart(doubleClickToPinCheckbutton, true, true);
            buttonsHBox.PackEnd(resetPinnedButton);

            var mainVBox = new VBox();
            mainVBox.PackStart(filterHBox, false, false);
            mainVBox.PackStart(filterEntry, true, true);
            mainVBox.PackStart(expandHBox, false, false);
            mainVBox.PackStart(buttonsHBox, false, false);
            //mainVBox.PackStart(projectsEntry, false, true);

            // Force a height
            var mainFrame = new FrameBox(mainVBox) { MinHeight = 250 };

            var view = mainFrame.Surface.NativeWidget as NSView;
            if (view != null)
            {

                view.SetFrameSize(new CGSize(260/*view.Frame.Size.Width*/, 130));
                view.Identifier = "VisualStudio.SolutionTreeFilter.FilterPad.MainVBox";
            }

            Content = mainFrame;
        }
    }
}