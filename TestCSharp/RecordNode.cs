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
            // TODO: Make the node insert self so the list stays sorted in regard to node ID.
            children.Add(child);
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
            Console.WriteLine(output);// + " " + string.Join("-", containedIdsLookup.ToArray())) ;
            foreach(RecordNode c in children)
            {
                c.Print(indentation + 1);
            }
        }
        //
        public void RecursivelyUpdateIdLookup(RecordNode child, RecordNode parent)
        {
            // TODO: Add the values as if they were sets, not lists.
            parent.containedIdsLookup.AddRange(child.containedIdsLookup);
            parent.containedIdsLookup.Add(child.id);
            if (!(parent.parent == null))
            {
                RecursivelyUpdateIdLookup(parent, parent.parent);
            }
        }
    }
}
