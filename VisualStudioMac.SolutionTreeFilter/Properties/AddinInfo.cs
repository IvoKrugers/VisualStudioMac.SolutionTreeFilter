using Mono.Addins;
using Mono.Addins.Description;
using VisualStudioMac.SolutionTreeFilter;

[assembly: Addin(
    "SolutionTreeFilter",
    Namespace = "VisualStudioMac",
    Version = Constants.Version,
    Category = "IDE extensions"
)]

[assembly: AddinName("_SolutionTree Filter")]
[assembly: AddinDescription("This extension adds a Pad which allows you to filter the solution tree by multiple search terms.\n\nby Ivo Krugers")]
[assembly: AddinAuthor("Ivo Krugers")]
[assembly: AddinUrl("https://github.com/IvoKrugers")]

[assembly: AddinDependency("::MonoDevelop.Core", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.TextEditor", MonoDevelop.BuildInfo.Version)]
