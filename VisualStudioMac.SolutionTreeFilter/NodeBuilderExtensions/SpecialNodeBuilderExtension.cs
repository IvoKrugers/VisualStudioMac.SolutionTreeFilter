﻿using System;
using MonoDevelop.Ide.Gui.Components;
using VisualStudioMac.SolutionTreeFilter.Helpers;
using VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings;

namespace VisualStudioMac.SolutionTreeFilter.NodeBuilderExtensions
{
    public class SpecialNodeBuilderExtension : NodeBuilderExtension
    {
        public override bool CanBuildNode(Type dataType)
        {
            var canBuild =
                    dataType.Name == "ConnectedServiceFolderNode" ||
                    dataType.Name == "ProjectReferenceCollection" ||
                    dataType.Name == "AssetFolder" ||
                    dataType.Name == "GettingStartedNode" ||
                    dataType.Name == "DependenciesNode" ||
                    dataType.Name == "ProjectPackagesFolderNode";
                    //||                    dataType.Name == "SolutionFolder";

            //Debug.WriteLine($"[CanBuildNode] {dataType}, canBuild: {canBuild}");
            return canBuild;
        }

        public override void GetNodeAttributes(ITreeNavigator parentNode, object dataObject, ref NodeAttributes attributes)
        {
            base.GetNodeAttributes(parentNode, dataObject, ref attributes);

            if (!string.IsNullOrEmpty(FilterSettings.SolutionFilter) && FilterSettings.Enabled)
            {
                attributes = NodeAttributes.Hidden;
            }
        }
    }
}
