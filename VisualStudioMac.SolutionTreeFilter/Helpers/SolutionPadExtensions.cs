using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui.Components;
using MonoDevelop.Ide.Gui.Pads;

namespace VisualStudioMac.SolutionTreeFilter.Helpers
{
    public static class SolutionPadExtensions
    {
        public static SolutionPad GetSolutionPad()
            =>(SolutionPad) IdeApp.Workbench.Pads.SolutionPad.Content;

        public static ExtensibleTreeViewController GetTreeView(this SolutionPad pad)
            => pad.Controller;
        
        public static ITreeNavigator GetRootNode(this SolutionPad pad)
            => pad.GetTreeView().GetRootNode();

        public static void CollapseTree(this SolutionPad pad)
        {
            if (pad is null)
                return;

            //if (pad.Control is ExtensibleTreeView tree)
            //{
            //    tree.CollapseTree();
            //}
            
            var root = pad.GetRootNode();
            if (root != null && pad.Control is ExtensibleTreeView tree)
            {
                root.CollapseAll();
            }
        }

        public static ITreeNavigator GetRootNode(this ExtensibleTreeViewController treeview)
        {
            var pos = treeview.GetRootPosition();
            return treeview.GetNodeAtPosition(pos);
        }

        public static void RefreshSelectedNode(this SolutionPad pad)
        {
            var node = pad.Controller.GetSelectedNode();
            pad.GetTreeView().RefreshNode(node);
            pad.Controller.RefreshTree();
        }
    }
}
