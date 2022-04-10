using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Helpers;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class ProjectNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => dataType.Name == "CSharpProject";

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (EssentialProperties.SolutionFilterArray.Length == 0)
                return;

            if (dataObject is DotNetProject project)
            {
                if (!ProjectHasChildNodesInFilter(project))
                {
                    attributes = NodeAttributes.Hidden;
                }
            }

        }
    }
}
