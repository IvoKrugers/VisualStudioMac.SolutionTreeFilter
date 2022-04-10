using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Cache;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.NodeCommandHandlers;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class FileNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => typeof(ProjectFile).IsAssignableFrom(dataType);

        public override Type CommandHandlerType
            => typeof(FileNodeCommandHandler);

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (EssentialProperties.SolutionFilterArray.Length == 0)
                return;

            if (dataObject is ProjectFile file)
            {
                if (!FilteredProjectCache.IsProjectItemVisible(file))
                    attributes = NodeAttributes.Hidden;
            }
        }

        public override void BuildNode(ITreeBuilder treeBuilder, object dataObject, NodeInfo nodeInfo)
        {
            base.BuildNode(treeBuilder, dataObject, nodeInfo);
           
            if (dataObject is ProjectFile file)
            {
                var isObjectPinned = EssentialProperties.IsPinned(file);

                if (isObjectPinned)
                {
                    nodeInfo.Label = $"{nodeInfo.Label}{" [Pinned]"}";
                }
            }
        }
    }
}