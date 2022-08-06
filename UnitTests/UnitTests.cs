using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCSharp;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        // 
        [TestMethod]
        public void AddChildTest()
        {
            RecordNode parent = new RecordNode(1, 0, "", "", "");
            RecordNode child = new RecordNode(2, 1, "", "", "");

            parent.AddChild(child);

            Assert.AreEqual(parent.children[0], child);

        }
        [TestMethod]
        public void IdLookupTest()
        {
            int parentId = 1;
            int childId = 2;
            RecordNode parent = new RecordNode(parentId, 0, "", "", "");
            RecordNode child = new RecordNode(childId, 1, "", "", "");

            parent.AddChild(child);

            Assert.IsTrue(parent.containedIdsLookup.Contains(childId));
        }
    }
}
