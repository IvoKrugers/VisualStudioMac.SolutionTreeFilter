using System;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class ProjectNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => dataType.Name == "CSharpProject";
    }
}
