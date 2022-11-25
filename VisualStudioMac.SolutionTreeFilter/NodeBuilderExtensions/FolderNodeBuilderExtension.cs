using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class FolderNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => typeof(ProjectFolder).IsAssignableFrom(dataType);

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (!FilterSettings.Initialized || FilterSettings.SolutionFilterArray.Length == 0 || !FilterSettings.Enabled)
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
