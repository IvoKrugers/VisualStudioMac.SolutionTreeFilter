﻿using System;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Projects;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class ProjectNodeBuilderExtension : BaseNodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
            => dataType.Name == "CSharpProject";

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (!FilterSettings.Initialized || FilterSettings.SolutionFilterArray.Length == 0 || !FilterSettings.Enabled)
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
