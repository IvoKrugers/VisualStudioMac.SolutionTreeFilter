using Mono.Addins;
using Mono.Addins.Description;
using VisualStudioMac.SolutionTreeFilter;
using System.Runtime.Versioning;

[assembly: Addin(
    "SolutionTreeFilter",
    Namespace = "VisualStudioMac",
    Version = Constants.Version,
    Category = "IDE extensions"
)]

[assembly: AddinName("SolutionTree Filter")]
[assembly: AddinDescription("This extension adds a Pad which allows you to filter the solution tree by multiple search terms.\n\nby Ivo Krugers")]
[assembly: AddinAuthor("Ivo Krugers")]
[assembly: AddinUrl("https://github.com/IvoKrugers/VisualStudioMac.SolutionTreeFilter")]

[assembly: AddinDependency("::MonoDevelop.Core", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.TextEditor", MonoDevelop.BuildInfo.Version)]

[assembly: SupportedOSPlatform("macos10.15")]