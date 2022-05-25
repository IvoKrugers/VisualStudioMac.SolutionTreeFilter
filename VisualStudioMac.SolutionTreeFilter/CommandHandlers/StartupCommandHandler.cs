using System.Diagnostics;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using VisualStudioMac.SolutionTreeFilter.Gui;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.CommandHandlers
{
    public class StartupCommandHandler : CommandHandler
    {
        protected override void Run()
        {
            if (FilterSettings.IsFirstTime)
            {
                if (IdeApp.Workspace.CurrentSelectedSolution is null)
                {
                    IdeApp.Workspace.SolutionLoaded += Workspace_SolutionLoaded;
                }
            }
        }

        private void Workspace_SolutionLoaded(object sender, MonoDevelop.Projects.SolutionEventArgs e)
        {
            IdeApp.Workspace.SolutionLoaded -= Workspace_SolutionLoaded;
            if (e.Solution != null)
            {
                ShowMessageAndOpenPad();
            }
        }

        void ShowMessageAndOpenPad()
        {
            MessageService.ShowMessage(
                      "SolutionTree Filter Extension",
                      $"Thank you for installing this Free extension. It adds a Pad which allows you to filter the solution tree by multiple search terms.\n\nEnjoy!\n\nby Ivo Krugers");

            // Open the pad
            var pad = IdeApp.Workbench.GetPad<FilterPad>();
            if (pad != null)
            {
                pad.Visible = true;
                pad.IsOpenedAutomatically = true;
                pad.BringToFront(true);
            }

            FilterSettings.IsFirstTime = false;
        }
    }
}
