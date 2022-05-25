using System;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.Core;

namespace VisualStudioMac.SolutionTreeFilter.Helpers.ExtensionSettings
{
    public static class FilterSettings
    {
        private const string FIRSTTIME_KEY = "SolutionFilter_FirstTime";
        private const string DOUBLECLICKTOPIN_KEY = "SolutionFilter_DoubleClickToPin";

        private const string SOLUTIONFILTER_KEY = "Filter";
        private const string SOLUTIONPINNEDDOCUMENTS_KEY = "PinnedDocs";
        private const string SOLUTIONEXPANDFILTER_KEY = "ProjectsToExpand";

        private static char[] _delimiterChars = { ' ', ';', ':', '\t', '\n' };

        public static bool Initialized => SolutionExtensionSettings.Instance.Initialized;

        public static bool IsFirstTime
        {
            get => PropertyService.GlobalInstance.Get(FIRSTTIME_KEY, true);
            set => PropertyService.GlobalInstance.Set(FIRSTTIME_KEY, value);
        }

        public static bool DoubleClickToPin
        {
            get => PropertyService.GlobalInstance.Get(DOUBLECLICKTOPIN_KEY, false);
            set => PropertyService.GlobalInstance.Set(DOUBLECLICKTOPIN_KEY, value);
        }

        public static string SolutionFilter
        {
            get => Get(SOLUTIONFILTER_KEY, String.Empty);
            set => Set(SOLUTIONFILTER_KEY, value.ToLower());
        }

        public static void ClearPinnedDocuments()
        {
            PinnedDocuments = new List<string>();
        }

        public static bool AddPinnedDocument(MonoDevelop.Ide.Gui.Document document)
        => AddPinnedDocument(document.FilePath.FullPath);

        public static bool AddPinnedDocument(MonoDevelop.Projects.ProjectFile projectFile)
        => AddPinnedDocument(projectFile.FilePath.FullPath);

        private static bool AddPinnedDocument(string fullFilePath)
        {
            var filenames = PinnedDocuments;

            if (!filenames.Contains(fullFilePath))
            {
                filenames.Add(fullFilePath);
                PinnedDocuments = filenames;
                return true;
            }
            return false;
        }

        public static bool RemovePinnedDocument(MonoDevelop.Ide.Gui.Document document)
         => RemovePinnedDocument(document.FilePath.FullPath);

        public static bool RemovePinnedDocument(MonoDevelop.Projects.ProjectFile projectFile)
        => RemovePinnedDocument(projectFile.FilePath.FullPath);

        private static bool RemovePinnedDocument(string fullFilePath)
        {
            var filenames = PinnedDocuments;

            if (filenames.Contains(fullFilePath))
            {
                filenames.Remove(fullFilePath);
                PinnedDocuments = filenames;
                return true;
            }
            return false;
        }

        public static List<string> PinnedDocuments
        {
            get
            {
                var commaSepString = Get(SOLUTIONPINNEDDOCUMENTS_KEY, "");
                if (string.IsNullOrEmpty(commaSepString))
                    return new List<string>();
                char[] _delimiter = { ';' };
                return commaSepString.Split(_delimiter).ToList();
            }
            set => Set(SOLUTIONPINNEDDOCUMENTS_KEY, string.Join(";",value));
        }

        public static bool IsPinned(MonoDevelop.Ide.Gui.Document document)
        => PinnedDocuments.Contains(document.FilePath.FullPath);

        public static bool IsPinned(MonoDevelop.Projects.ProjectFile projectFile)
        => PinnedDocuments.Contains(projectFile.FilePath.FullPath);


        private static string BranchnameToKey(string branchName)
        {
            return branchName.Trim()
                            .Replace("  ", " ")
                            .Replace(" ", "-")
                            .Replace("/", "_");
        }

        private static string ConcatGitBranchName(string key)
        {
            var branchName = GitHelper.GetCurrentBranch() ?? "";
            branchName = BranchnameToKey(branchName);

            return $"{key}{(string.IsNullOrEmpty(branchName) ? "" : $"_{ branchName}")}";
        }

        private static void Set(string key, string value)
        {
            SolutionExtensionSettings.Instance.Set(ConcatGitBranchName(key), value);
            SolutionExtensionSettings.Instance.Set(key, value);
        }

        private static string Get(string key, string defaultValue)
        {
            var result = SolutionExtensionSettings.Instance.Get(ConcatGitBranchName(key), defaultValue);
            if (result == defaultValue)
                result = SolutionExtensionSettings.Instance.Get(key, defaultValue);
            return result;
        }

        public static string[] SolutionFilterArray
        {
            get
            {
                var filterText = SolutionFilter;
                if (string.IsNullOrWhiteSpace(filterText))
                    return new string[0];

                filterText = filterText.Trim();
                while (filterText.IndexOf("  ") >= 0)
                {
                    filterText = filterText.Replace("  ", " ");
                }

                return filterText.Split(_delimiterChars);
            }
        }

        public static string ExpandFilter
        {
            get => Get(SOLUTIONEXPANDFILTER_KEY, string.Empty);
            set => Set(SOLUTIONEXPANDFILTER_KEY, value.ToLower());
        }

        public static string[] ExpandFilterArray
        {
            get
            {
                var filterText = ExpandFilter;
                if (string.IsNullOrEmpty(filterText))
                    return new string[0];

                return filterText.Split(_delimiterChars);
            }
        }

        public static string[] ExcludedExtensionsFromExpanding = { ".xaml.cs", ".designer.cs" };

        public static bool IsRefreshingTree { get; set; }

        public static void PurgeProperties()
        {
            var keys = SolutionExtensionSettings.Instance.GetAllKeys() ?? new List<string>();
            var branches = GitHelper.GetLocalBranches() ?? new List<string>();
            branches = branches.Select(b => BranchnameToKey(b)).ToList();

            foreach (var key in keys)
            {
                if (branches.FirstOrDefault(b => key.Contains(b)) is null)
                {
                    SolutionExtensionSettings.Instance.RemoveKey(key);
                }
            }
            SolutionExtensionSettings.Instance.WriteProperties();
        }
    }
}