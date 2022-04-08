using System;
using System.IO;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Helpers;

namespace VisualStudioMac.SolutionTreeFilter.NodeCommandHandlers
{
    public class FileNodeCommandHandler : NodeCommandHandler
    {
        // Double-Clicked
        public override void ActivateItem()
        {
            base.ActivateItem();
            if (CurrentNode.DataItem is ProjectFile f && f.BuildAction != "InterfaceDefinition")
            {
                if (EssentialProperties.IsPinned(f))
                    EssentialProperties.RemovePinnedDocument(f);
                else
                    EssentialProperties.AddPinnedDocument(f);

                var pad = SolutionPadExtensions.GetSolutionPad();
                if (pad == null)
                    return;
                pad.RefreshSelectedNode();
            }
        }

        // Single-Clicked
        public override void OnItemSelected()
        {
            base.OnItemSelected();

            //if (EssentialProperties.IsRefreshingTree)
            //    return;

            //if (CurrentNode.DataItem is ProjectFile f)
            //{
            //    string ext = Path.GetExtension(f.FilePath);
            //    if (EssentialProperties.ExcludedExtensionsFromOneClick.FindIndex((s) => s == ext) == -1)
            //    {
            //        if (IdeApp.Workbench.ActiveDocument == null || IdeApp.Workbench.ActiveDocument.Name != f.FilePath.FileName)
            //            IdeApp.Workbench.OpenDocument(f.FilePath, project: null);
            //    }
            //}
        }

        public override void RefreshItem()
        {
            base.RefreshItem();
        }
    }
}
