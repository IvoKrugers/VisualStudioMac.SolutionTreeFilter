using System;
using System.IO;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.NodeCommandHandlers
{
    public class FileNodeCommandHandler : NodeCommandHandler
    {
        // Double-Clicked
        public override void ActivateItem()
        {
            base.ActivateItem();
            if (CurrentNode.DataItem is ProjectFile f && f.BuildAction != "InterfaceDefinition" && FilterSettings.DoubleClickToPin)
            {
                if (FilterSettings.IsPinned(f))
                    FilterSettings.RemovePinnedDocument(f);
                else
                    FilterSettings.AddPinnedDocument(f);

                var pad = SolutionPadExtensions.GetSolutionPad();
                pad?.RefreshSelectedNode();
            }
        }
    }
}
