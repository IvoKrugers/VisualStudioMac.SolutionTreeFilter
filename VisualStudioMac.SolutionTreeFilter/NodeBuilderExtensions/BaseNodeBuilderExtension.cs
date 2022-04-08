using System;
using System.IO;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads.ProjectPad;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Cache;
using VisualStudioMac.SolutionTreeFilter.Helpers;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class BaseNodeBuilderExtension : NodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => false;

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            var filter = EssentialProperties.SolutionFilterArray;
            if (filter.Length == 0)
                return;

            if (dataObject is DotNetProject project)
            {
                if (!ProjectHasChildNodesInFilter(project))
                {
                    attributes = NodeAttributes.Hidden;
                }
            }

            if (dataObject is ProjectFolder pf)
            {
                if (!ProjectFolderHasChildNodesInFilter((ITreeBuilder)parentNode, pf))
                {
                    attributes = NodeAttributes.Hidden;
                }
            }

            if (dataObject is ProjectFile file)
            {
                if (!FilteredProjectCache.IsProjectItemVisible(file))
                    attributes = NodeAttributes.Hidden;
            }
        }

        private bool ProjectHasChildNodesInFilter(Project project)
        {
            FilteredProjectCache.ScanProjectForFiles(project);
            return FilteredProjectCache.IsProjectItemVisible(project);
        }

        private bool ProjectFolderHasChildNodesInFilter(ITreeBuilder builder, ProjectFolder dataObject)
        {
            Project project = builder.GetParentDataItem(typeof(Project), true) as Project;
            if (project == null)
                return false;

            FilteredProjectCache.ScanProjectForFiles(project);
            return FilteredProjectCache.IsProjectItemVisible(dataObject);
        }

        public override void BuildNode(ITreeBuilder treeBuilder, object dataObject, NodeInfo nodeInfo)
        {
            if (!string.IsNullOrEmpty(EssentialProperties.SolutionFilter))
            {
                if (!FilteredProjectCache.IsProjectItemEnabled(dataObject))
                    nodeInfo.Style = NodeInfo.LabelStyle.Disabled;
            }

            if (!(dataObject is ProjectFile))
                return;

            ProjectFile file = (ProjectFile)dataObject;

            var ext = Path.GetExtension(file.FilePath);

            var isObjectPinned = EssentialProperties.IsPinned(file);

            //var addOneClickText = EssentialProperties.OneClickShowFile && EssentialProperties.ExcludedExtensionsFromOneClick.FindIndex((s) => s == ext) == -1;
            var addOneClickText = false;

            if (isObjectPinned || addOneClickText)
            {
                nodeInfo.Label = $"{Path.GetFileName(file.FilePath)}{(isObjectPinned ? " [Pinned]" : "")}{(addOneClickText ? $" ->" : "")}";
            }
        }
    }
}
