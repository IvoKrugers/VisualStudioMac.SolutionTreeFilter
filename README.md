# VisualStudioMac.SolutionTreeFilter
Visual Studio Mac 2022 extension that adds a Pad to filter the solution tree by multiple search terms.


## Solution tree filtering

The filtering is done by partial file or foldername comparing. Its real power can be found in its ability to filter by multiple search terms seperated by a space. This way you can specify a subset of files/folder to work on. Oh, did I mention that it is not necessary to write file/foldernames out in full.

![Filtering the solution tree.](/Art/Filtering.gif)

## Projects to expand

Is your filtering still showing a lot of solutiontree entries, mayby you can filter those projects that you only care about. Like if you're only working on iOS, you only want to see the core and iOS project. Then specify: `core ios` and hit apply or change the tree filter.


## Document pinning

Documents can be pinned so that they are always shown in the Solution tree regardless of if they match with any search term. This can be done from a file node in the solution tree or from the context menu in the TextEditor. A refresh or change in search terms is sometimes required for this to be reflected in the solution tree.


## Local storage
The search terms, pinned documents and projects to expand are stored in a json file in the solutionfolder. These settings are stored per branchname. This way these values are per solution and branch which makes switching between solutions/branches easier. 

## Author

**Ivo Krugers** - Author


## License

This project is licensed under the MIT License - [full details](LICENSE).
