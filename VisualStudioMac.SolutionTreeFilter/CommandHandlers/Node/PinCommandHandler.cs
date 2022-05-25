using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.CommandHandlers.Node
{
    public class PinCommandHandler : CommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            base.Update(info);
            info.Enabled = false;
            if (IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFile projectFile)
            {
                info.Enabled = true;
                var isPinned = FilterSettings.IsPinned(projectFile);
                info.Text = isPinned ? "Unpin document" : "Pin document";
            }
        }

        protected override void Run()
        {
            if (IdeApp.ProjectOperations.CurrentSelectedItem is ProjectFile projectFile
                && projectFile.Subtype == Subtype.Code)
            {

                var isPinned = FilterSettings.IsPinned(projectFile);
                if (isPinned)
                    FilterSettings.RemovePinnedDocument(projectFile);
                else
                    FilterSettings.AddPinnedDocument(projectFile);


                var pad = (SolutionPad)IdeApp.Workbench.Pads.SolutionPad.Content;
                if (pad == null)
                    return;

                pad.RefreshSelectedNode();
            }
        }
    }
}
