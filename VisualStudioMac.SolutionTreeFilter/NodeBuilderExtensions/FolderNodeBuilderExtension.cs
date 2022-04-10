using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using VisualStudioMac.SolutionTreeFilter.Helpers;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class FolderNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => typeof(ProjectFolder).IsAssignableFrom(dataType);

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (EssentialProperties.SolutionFilterArray.Length == 0)
                return;

            if (dataObject is ProjectFolder pf)
            {
                if (!ProjectFolderHasChildNodesInFilter((ITreeBuilder)parentNode, pf))
                {
                    attributes = NodeAttributes.Hidden;
                }
            }
        }
    }
}
