using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using AppKit;
using MonoDevelop.Components;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;
using Xwt.Mac;

namespace VisualStudioMac.SolutionTreeFilter.Gui
{
    public class FilterPad : PadContent
    {
        private FilterPadWidget widget;
        private Control control;

        public override Control Control
        {
            get
            {
                if (control == null)
                {
                    widget = new FilterPadWidget() ;
                    control = new XwtControl(widget);
                }
                // Returning control does not work.
                return control.GetNativeWidget<NSView>();
            }
        }

        public override string Id => Constants.SolutionFilterPadId;

        public override void Dispose()
        {
            if (widget != null)
            {
                widget.Dispose();
                widget = null;
            }
            base.Dispose();
        }

        protected override void Initialize(IPadWindow window)
        {
            base.Initialize(window);

            Initialize();

            StartListeningForWorkspaceChanges();

            this.Window.Title = $"Solution Filter";
        }

        void StartListeningForWorkspaceChanges()
        {
            IdeApp.Workbench.ActiveDocumentChanged += (sender, e) => StorePinnedDocuments(sender);
            IdeApp.Workspace.SolutionLoaded += (sender, e) => Initialize();
            IdeApp.Workspace.CurrentSelectedSolutionChanged += (sender, e) => Initialize();
            IdeApp.FocusIn += (sender, e) => { log("FocusIn"); Initialize(true); };
        }

        private void log([CallerMemberName] string memberName = "", [CallerLineNumber] int ln = 0)
        {
            Debug.WriteLine($"{memberName}:ln {ln}: event fired (CurrentBranch: {{GitHub.GitHelper.GetCurrentBranch()}})");
        }

        SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private void StorePinnedDocuments(object sender)
        {

            if (sender is null)
                return;

            if (_semaphore.CurrentCount <= 0)
                return;


            System.Threading.Tasks.Task.Run(() =>
            {
                _semaphore.Wait();
                try
                {
                    if (!FilterSettings.Initialized)
                        return;

                    if (sender is Workbench wb)
                        sender = wb.RootWindow;

                    var activeWorkbenchWindowProp = sender.GetType().GetProperty("ActiveWorkbenchWindow");
                    var activeWorkbenchWindow = activeWorkbenchWindowProp.GetValue(sender, null);

                    var tabControlProp = activeWorkbenchWindow?.GetType().GetProperty("TabControl");
                    object tabControl = tabControlProp?.GetValue(activeWorkbenchWindow, null);

                    var tabsProp = tabControl?.GetType().GetProperty("Tabs");
                    object tabs = tabsProp?.GetValue(tabControl, null);

                    if (tabControl != null)
                    {
                        var index = 0;
                        foreach (var tab in (IEnumerable)tabs)
                        {
                            var isPinnedProp = tab.GetType().GetProperty("IsPinned");
                            bool isPinned = (bool)isPinnedProp.GetValue(tab, null);
                            if (isPinned)
                                FilterSettings.AddPinnedDocument(IdeApp.Workbench.Documents[index]);
                            index++;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                }
                _semaphore.Release();
            });
        }

        internal void Initialize(bool forceReload = false)
        {
            if (IdeApp.Workspace.CurrentSelectedSolution is null)
                return;


            SolutionExtensionSettings.Instance.Init(IdeApp.Workspace.CurrentSelectedSolution);

            if (this.widget is null)
                return;

            var filterChanged =
                widget.FilterText != FilterSettings.SolutionFilter
                || widget.ExpandText != FilterSettings.ExpandFilter;

            widget.LoadProperties();

            if (forceReload || filterChanged)
                widget.StartTimer();
        }

    }
}