using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    public class RecordNode
    {
        // Record data fields.
        public int id;
        public int parentId;

        public string name;
        public string company;
        public string position;

        // This node's children.
        public List<RecordNode> children = new List<RecordNode>();
        // Node's current parent, needed to pass containedIdsLookup upward.
        public RecordNode parent = null;

        // A list of all record IDs in this tree to help to populate it with new nodes.
        public List<int> containedIdsLookup = new List<int>();

        public RecordNode(string recordString)
        {
            // Each record is expected to be in format:
            // id, parentId, first name, last name, company, city, position, ...
            List<string> record = new List<string>(recordString.Split(','));
            id = int.Parse(record[0]);
            parentId = int.Parse(record[1]);
            name = record[2] + " " + record[3];
            company = record[4];
            position = record[5];
        }

        // Compile the node's data into a line of text.
        public string GetData()
        {
            return name + ", " + company + ", " + position;
        }

        // Return only the node's ID and its parent's ID.
        public string GetIdData()
        {
            return id + " " + parentId;
        }

        // Add the specified node to the list of this node's children and update its lookup list.
        public void AddChild(RecordNode child)
        {
            children.Insert(Sorter.GetInsertId(child.id, children), child);
            child.parent = this;
            RecursivelyUpdateIdLookup(child, this);
        }

        // Recursively print data about self and all children.
        public void Print(int indentation)
        {
            string output = "";
            // Avoid printing the '->' arrows for root nodes.
            if(indentation > 0)
            {
                output += new string(' ', indentation<<1) + "-> ";
            }
            output += GetData();
            Console.WriteLine(output);
            foreach(RecordNode c in children)
            {
                c.Print(indentation + 1);
            }
        }

        // Push the contained nodes upward so next records can find their parents easier.
        public void RecursivelyUpdateIdLookup(RecordNode child, RecordNode parent)
        {
            // First consider the lookup list of the child, then own ID.
            // Add only new IDs to the list.
            foreach(int id in child.containedIdsLookup)
            {
                if (!(parent.containedIdsLookup.Contains(id)))
                {
                    parent.containedIdsLookup.Add(id);
                }
            }
            if (!(parent.containedIdsLookup.Contains(child.id)))
            {
                parent.containedIdsLookup.Add(child.id);
            }
            // Do the same for the parent node.
            if (!(parent.parent == null))
            {
                RecursivelyUpdateIdLookup(parent, parent.parent);
            }
        }
    }
}
