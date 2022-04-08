using System;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class FolderNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => typeof(ProjectFolder).IsAssignableFrom(dataType);
    }
}
