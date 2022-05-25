using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Pads;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Gui;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.CommandHandlers.Editor
{
    public class PinCommandHandler : CommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            base.Update(info);
            info.Enabled = false;
            if (IdeApp.Workbench.ActiveDocument != null && IdeApp.Workbench.ActiveDocument.IsFile)
            {
                info.Enabled = true;
                var isPinned = FilterSettings.IsPinned(IdeApp.Workbench.ActiveDocument);
                info.Text = isPinned ? "Unpin from Solution Tree" : "Pin on Solution Tree";
            }
        }

        protected override void Run()
        {
            if (IdeApp.Workbench.ActiveDocument != null && IdeApp.Workbench.ActiveDocument.IsFile)
            {
                var isPinned = FilterSettings.IsPinned(IdeApp.Workbench.ActiveDocument);
                if (isPinned)
                    FilterSettings.RemovePinnedDocument(IdeApp.Workbench.ActiveDocument);
                else
                    FilterSettings.AddPinnedDocument(IdeApp.Workbench.ActiveDocument);

                var pad = (SolutionPad)IdeApp.Workbench.Pads.SolutionPad.Content;
                if (pad == null)
                    return;

                var node = pad.Controller.GetSelectedNode();
                if (node != null && node.DataItem is ProjectFile projectfile)
                {
                    if (projectfile.FilePath.FullPath == IdeApp.Workbench.ActiveDocument.FilePath.FullPath)
                    {
                        pad.RefreshSelectedNode();
                    }
                }
                else
                {
                    var SolutionPad = (FilterPad)IdeApp.Workbench.GetPad<FilterPad>().Content;
                    if (SolutionPad != null)
                    {
                        ((FilterPadWidget)SolutionPad.Control).FilterSolutionPad();
                    }
                }
            }
        }
    }
}