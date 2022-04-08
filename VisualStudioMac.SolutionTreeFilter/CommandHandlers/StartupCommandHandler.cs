using System.Diagnostics;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using VisualStudioMac.SolutionTreeFilter.Gui;

namespace VisualStudioMac.SolutionTreeFilter.CommandHandlers
{
    public class StartupCommandHandler : CommandHandler
    {
        protected override void Run()
        {
            Debug.WriteLine("### STARTUP!!! ###");

            //var pad = IdeApp.Workbench.GetPad<FilterPad>();
            //if (pad != null)
            //{
            //    pad.Visible = true;
            //    pad.IsOpenedAutomatically = true;
            //    pad.BringToFront(true);
            //}
        }
    }
}
