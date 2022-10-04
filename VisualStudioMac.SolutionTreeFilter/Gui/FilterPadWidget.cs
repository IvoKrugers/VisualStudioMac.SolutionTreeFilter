using System;
using System.Threading;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;
using Xwt;

namespace VisualStudioMac.SolutionTreeFilter.Gui
{
    public partial class FilterPadWidget : Widget
    {
        private Timer timer;
        public string FilterText => filterEntry.Text;
        public string ExpandText => projectsEntry.Text;

        public FilterPadWidget()
        {
            this.Name = "VisualStudioMac.SolutionTreeFilter.Gui.FilterPadWidget";

            Build();
            SetFocus();
            MinHeight = 130;

            enabledCheckBox.Active = FilterSettings.Enabled;

            filterEntry.Changed += FilterEntry_Changed;
            enabledCheckBox.Clicked += EnabledCheckBox_Clicked;
            filterClearButton.Clicked += FilterClearButton_Clicked;
            applyButton.Clicked += ApplyButton_Clicked;
            pinOpenDocumentsButton.Clicked += PinOpenDocumentsButton_Clicked;
            resetPinnedButton.Clicked += ResetPinnedButton_Clicked;
            doubleClickToPinCheckBox.Clicked += DoubleClickToPinCheckbutton_Clicked;

        }

        private void EnabledCheckBox_Clicked(object sender, EventArgs e)
        {
            FilterSettings.Enabled = enabledCheckBox.Active;
            FilterSolutionPad();
        }

        private void DoubleClickToPinCheckbutton_Clicked(object sender, EventArgs e)
        {
            FilterSettings.DoubleClickToPin = doubleClickToPinCheckBox.Active;
        }

        private void ResetPinnedButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                FilterSettings.ClearPinnedDocuments();
                FilterSolutionPad();
            }
            catch (Exception ex)
            {
                string msg = $"Error {ex.Message}";
                MessageService.ShowError(msg);
            }
        }

        private void PinOpenDocumentsButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                foreach (var doc in IdeApp.Workbench.Documents)
                {
                    FilterSettings.AddPinnedDocument(doc);
                }
                FilterSolutionPad();
            }
            catch (Exception ex)
            {
                string msg = $"Error {ex.Message}";
                MessageService.ShowError(msg);
            }
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                filterEntry.Changed -= FilterEntry_Changed; ;
                filterClearButton.Clicked -= FilterClearButton_Clicked;
                applyButton.Clicked -= ApplyButton_Clicked;
            }
            base.Dispose(disposing);
        }

        private void ApplyButton_Clicked(object sender, EventArgs e)
        {
            var pad = (SolutionPad)IdeApp.Workbench.Pads.SolutionPad.Content;
            if (pad != null)
            {
                FilterSettings.IsRefreshingTree = true;
                pad.CollapseTree();
                var root = pad.GetRootNode();
                if (root != null)
                {
                    root.Expanded = false;
                    pad.GetTreeView().RefreshNode(root);
                    root.Expanded = true;
                    SolutionTreeExtensions.ExpandAll(root);
                }
                FilterSettings.IsRefreshingTree = false;
            }
            ExpandOnlyCSharpProjects();
        }

        private void FilterClearButton_Clicked(object sender, EventArgs e)
        {
            filterEntry.Text = String.Empty;
        }

        private void FilterEntry_Changed(object sender, EventArgs e)
        {
            StartTimer();
        }

        public void LoadProperties()
        {
            filterEntry.Text = FilterSettings.SolutionFilter;
            projectsEntry.Text = FilterSettings.ExpandFilter;
            doubleClickToPinCheckBox.Active = FilterSettings.DoubleClickToPin;
        }

        protected void collapseButton_Clicked(object sender, EventArgs e)
        {
            
        }

        protected void clearButton_Clicked(object sender, EventArgs e)
        {
            filterEntry.Text = "";
            FilterSolutionPad();
        }

        internal void StartTimer()
        {
            StopTimer();
            timer = new Timer(OnTimerElapsed, null, 1000, System.Threading.Timeout.Infinite); // dueTime in miliseconds
        }

        private void StopTimer()
        {
            timer?.Dispose();
            timer = null;
        }

        object refreshLock = new object();
        private void OnTimerElapsed(object state)
        {
            lock (refreshLock)
            {
                StopTimer();
                Runtime.RunInMainThread(FilterSolutionPad);
            }
        }

        internal void FilterSolutionPad()
        {
            var SolutionPad = (FilterPad)IdeApp.Workbench.GetPad<FilterPad>()?.Content;
            if (SolutionPad != null)
                SolutionPad.Window.IsWorking = true;

            var ctx = IdeApp.Workbench.StatusBar.CreateContext();

            using (ctx)
            {
                ctx.AutoPulse = true;
                ctx.ShowMessage("Filtering...");
                ctx.Pulse();
                ctx.BeginProgress("Tree filtering...");

                //IdeApp.Workbench.StatusBar.ShowMessage("Filtering......");

                FilterSettings.SolutionFilter = filterEntry.Text;

                if (string.IsNullOrEmpty(filterEntry.Text) || !enabledCheckBox.Active)
                {
                    ExpandOnlyCSharpProjects();
                    return;
                }
                ctx.SetProgressFraction(0.25);
                var pad = (SolutionPad)IdeApp.Workbench.Pads.SolutionPad.Content;
                if (pad == null)
                    return;

                FilterSettings.IsRefreshingTree = true;
                pad.CollapseTree();
                ctx.SetProgressFraction(0.35);
                var root = pad.GetRootNode();
                if (root != null)
                {
                    root.Expanded = false;
                    pad.GetTreeView().RefreshNode(root);
                    ctx.SetProgressFraction(0.75);
                    root.Expanded = true;
                    SolutionTreeExtensions.ExpandAll(root);
                    ctx.SetProgressFraction(1);
                }
                FilterSettings.IsRefreshingTree = false;
            }
            ctx.EndProgress();
            IdeApp.Workbench.StatusBar.ShowReady();
            //if (SolutionPad != null)
            //   SolutionPad.Window.IsWorking = false;
        }

        private void ExpandOnlyCSharpProjects()
        {
            FilterSettings.ExpandFilter = projectsEntry.Text;

            var pad = IdeApp.Workbench.Pads.SolutionPad.Content as SolutionPad;
            if (pad == null)
                return;

            FilterSettings.IsRefreshingTree = true;
            pad.CollapseTree();
            var root = pad.GetRootNode();
            if (root != null)
            {
                root.Expanded = false;
                pad.GetTreeView().RefreshNode(root);
                root.Expanded = true;
                SolutionTreeExtensions.ExpandOnlyCSharpProjects(root);
            }
            FilterSettings.IsRefreshingTree = false;
        }
    }
}