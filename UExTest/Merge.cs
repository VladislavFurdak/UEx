using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uex;
using System.Linq;

namespace UEx_tests
{
    [TestClass]
    public class Merge
    {
        [TestMethod]
        public void MergeTest()
        {
            var listA = new List<Item> {
               new Item { Key = 1, Field = "Item1"},
               new Item { Key = 2, Field = "Item2"},
               new Item {Key = 4, Field="Item3"}
           };

            var listB = new List<Item2> {
               new Item2 { Item2Key = 1,  ExtraField="Item1_2", Field = "John"},
               new Item2 { Item2Key = 2,  ExtraField="Item2_2",Field = "Doe"},
               new Item2 { Item2Key = 3,  ExtraField="Item3_3",Field = "Jim"},
           };

            var result = listA.Merge(
                listB,
                (source,dest) => source.Key == dest.Item2Key, //Compare
                FirstOnlyItem => { return new MergeResult { StrProp = string.Format("{0} single Item", FirstOnlyItem.Field) }; },
                SecondOnlyItem => { return new MergeResult { StrProp = string.Format("{0} single Item2", SecondOnlyItem.Field) }; }, 
                (First, Second) => { return new MergeResult { StrProp = string.Format("{0} - {1} Ids both", First.Key, Second.Item2Key) }; });

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(true, result.Count(x => x.StrProp == "Item3 single Item") == 1);
            Assert.AreEqual(true, result.Count(x => x.StrProp == "Jim single Item2") == 1);
            Assert.AreEqual(true, result.Count(x => x.StrProp == "1 - 1 Ids both") == 1);
            Assert.AreEqual(true, result.Count(x => x.StrProp == "2 - 2 Ids both") == 1);
        }
    }
}
