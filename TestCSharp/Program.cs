using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {
        // A list of records, arranged in an organogram.
        public static List<RecordNode> nodeList = new List<RecordNode>();
        static void Main(string[] args)
        {
            
            // Get the data from the csv file specified by the user.
            string inputFileName = args[0];
            string text = System.IO.File.ReadAllText(inputFileName);
            // Split the text file's content into individual lines to be processed,
            List<string> lineList = new List<string>(text.Split('\n'));

            // Populate the nodeList.
            foreach(string l in lineList)
            {
                // Parse each line into a record object.
                RecordNode rn = new RecordNode(l);
                // Iterate through the list to claim the children of this node.
                for(int i=0; i<nodeList.Count; i++)
                {
                    // Add the node as own child and remove it from the global list.
                    if (nodeList[i].parentId == rn.id)
                    {
                        rn.AddChild(nodeList[i]);
                        nodeList.RemoveAt(i);
                        i--;
                    }
                }
                // Attempt to find own parent.
                bool parentFound = false;
                for(int i=0; i<nodeList.Count && !parentFound; i++)
                {
                    // Case 1: the parent is currently parentless and exists in the global list.
                    if (nodeList[i].id == rn.parentId)
                    {
                        nodeList[i].AddChild(rn);
                        parentFound = true;
                    }
                    // Case 2: the parent is already in one of the trees and needs to be found.
                   
                    else if(nodeList[i].containedIdsLookup.Contains(rn.parentId)) {
                        RecordNode parent = FindParentNode(rn, nodeList[i]);
                        if(!(parent == null))
                        {
                            parent.AddChild(rn);
                            parentFound = true;
                        }
                    }
                }

                // If unsuccessful, add itself to the global list.
                // TODO: make it insert self so that the list stays sorted.
                if (!parentFound)
                {
                    nodeList.Add(rn);
                }
                
            }
            // Once the tree is complete, print it.
            foreach(RecordNode n in nodeList)
            {
                n.Print(0);
            }
        }

        // Search the tree and return the specified node's parent.
        public static RecordNode FindParentNode(RecordNode rn, RecordNode root)
        {
            foreach(RecordNode c in root.children)
            {
                // Case 1: the parent is currently parentless and exists in the global list.
                if (c.id == rn.parentId)
                {
                    return c;
                }
                // Case 2: the parent is already in one of the trees and needs to be found.
                else if(c.containedIdsLookup.Contains(rn.parentId))
                {
                    // Continue searching within the subtree of node c.
                    return FindParentNode(rn, c);
                }
            }
            return null;
        }
    }
}
