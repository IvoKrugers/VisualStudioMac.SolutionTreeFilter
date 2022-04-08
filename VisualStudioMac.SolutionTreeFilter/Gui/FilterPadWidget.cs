using System;
using System.Threading;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads;
using VisualStudioMac.SolutionTreeFilter.Helpers;
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
            Build();
            Show();

            filterEntry.Changed += FilterEntry_Changed; ;
            filterClearButton.Clicked += FilterClearButton_Clicked;
            applyButton.Clicked += ApplyButton_Clicked;
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
                EssentialProperties.IsRefreshingTree = true;
                pad.CollapseTree();
                var root = pad.GetRootNode();
                if (root != null)
                {
                    root.Expanded = false;
                    pad.GetTreeView().RefreshNode(root);
                    root.Expanded = true;
                    SolutionTreeExtensions.ExpandAll(root);
                }
                EssentialProperties.IsRefreshingTree = false;
            }
            ExpandOnlyCSharpProjects();
        }

        private void FilterClearButton_Clicked(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void FilterEntry_Changed(object sender, EventArgs e)
        {
            StartTimer();
        }

        public void LoadProperties()
        {
            filterEntry.Text = EssentialProperties.SolutionFilter;
            projectsEntry.Text = EssentialProperties.ExpandFilter;
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
                IdeApp.Workbench.StatusBar.ShowMessage("Filtering...");

                EssentialProperties.SolutionFilter = filterEntry.Text;

                if (string.IsNullOrEmpty(filterEntry.Text))
                {
                    ExpandOnlyCSharpProjects();
                    return;
                }

                var pad = (SolutionPad)IdeApp.Workbench.Pads.SolutionPad.Content;
                if (pad == null)
                    return;

                EssentialProperties.IsRefreshingTree = true;
                pad.CollapseTree();
                var root = pad.GetRootNode();
                if (root != null)
                {
                    root.Expanded = false;
                    pad.GetTreeView().RefreshNode(root);
                    root.Expanded = true;
                    SolutionTreeExtensions.ExpandAll(root);
                }
                EssentialProperties.IsRefreshingTree = false;
            }
            IdeApp.Workbench.StatusBar.ShowReady();
            //if (SolutionPad != null)
            //   SolutionPad.Window.IsWorking = false;
        }

        private void ExpandOnlyCSharpProjects()
        {
            EssentialProperties.ExpandFilter = projectsEntry.Text;

            var pad = IdeApp.Workbench.Pads.SolutionPad.Content as SolutionPad;
            if (pad == null)
                return;

            EssentialProperties.IsRefreshingTree = true;
            pad.CollapseTree();
            var root = pad.GetRootNode();
            if (root != null)
            {
                root.Expanded = false;
                pad.GetTreeView().RefreshNode(root);
                root.Expanded = true;
                SolutionTreeExtensions.ExpandOnlyCSharpProjects(root);
            }
            EssentialProperties.IsRefreshingTree = false;
        }

        public void OnDocumentClosed()
        {
            if (IdeApp.Workbench.Documents is null || IdeApp.Workbench.Documents.Count == 0)
            {
                FilterSolutionPad();
            }
        }
    }
}