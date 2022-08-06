using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSharp
{
    // A simple class to help order the organogram by record IDs.
    class Sorter
    {
        // Perform a binary search to find where a given record so that the list remains sorted.
        public static int GetInsertId(int recordId, List<RecordNode> list)
        {
            int a = 0;
            int b = list.Count;
            int i = 0;
            int previousI; 

            // An empty list is considered to be sorted.
            if( b == 0 )
            {
                return 0;
            }
            // In other case, find the fitting index.
            do
            {
                previousI = i;
                i = (a + b) / 2;
                if (recordId > list[i].id)
                {
                    a = i;
                }
                // Since ID is unique, it will never be the case that recordId == list[i].id.
                else
                {
                    b = i;
                }
            } while (!(previousI == i));
            // When the search stops, make a final comparison to check if the record should be inserted at i or i+1.
            return recordId > list[i].id ? i + 1 : i;
        }
    }
}
