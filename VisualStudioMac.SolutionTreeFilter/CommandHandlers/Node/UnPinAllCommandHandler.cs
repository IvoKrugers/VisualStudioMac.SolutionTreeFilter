using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using VisualStudioMac.SolutionTreeFilter.Gui;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.CommandHandlers.Node
{
    public class UnPinAllCommandHandler : CommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            base.Update(info);
            info.Enabled = true;
        }

        protected override void Run()
        {
            FilterSettings.ClearPinnedDocuments();
            var pad = (FilterPad)IdeApp.Workbench.GetPad<FilterPad>().Content;
            if (pad == null)
                return;

            Runtime.RunInMainThread(((FilterPadWidget)pad.Control).FilterSolutionPad);
            ;
        }
    }
}