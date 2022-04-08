using System;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.NodeCommandHandlers;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class FileNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => typeof(ProjectFile).IsAssignableFrom(dataType);

        public override Type CommandHandlerType
            => typeof(FileNodeCommandHandler);
    }
}
