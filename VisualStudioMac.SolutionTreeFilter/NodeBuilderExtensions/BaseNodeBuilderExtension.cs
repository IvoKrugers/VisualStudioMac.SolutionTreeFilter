using System;
using System.IO;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Cache;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class BaseNodeBuilderExtension : NodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => false;

        public override void BuildNode(ITreeBuilder treeBuilder, object dataObject, NodeInfo nodeInfo)
        {
            if (FilterSettings.Initialized && !string.IsNullOrEmpty(FilterSettings.SolutionFilter))
            {
                if (!FilteredProjectCache.IsProjectItemEnabled(dataObject))
                    nodeInfo.Style = NodeInfo.LabelStyle.Disabled;
            }
        }

        protected bool ProjectHasChildNodesInFilter(Project project)
        {
            FilteredProjectCache.ScanProjectForFiles(project);
            return FilteredProjectCache.IsProjectItemVisible(project);
        }

        protected bool ProjectFolderHasChildNodesInFilter(ITreeBuilder builder, ProjectFolder dataObject)
        {
            Project project = builder.GetParentDataItem(typeof(Project), true) as Project;
            if (project == null)
                return false;

            FilteredProjectCache.ScanProjectForFiles(project);
            return FilteredProjectCache.IsProjectItemVisible(dataObject);
        }
    }
}
