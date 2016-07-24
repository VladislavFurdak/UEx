using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uex;
using System.Linq;

namespace UEx_tests
{
    [TestClass]
    public class InnerJoin
    {
        [TestMethod]
        public void InnerJoinTest()
        {
            var listA = new List<Item> {
               new Item { Key = 1, Field = "HollItem"},
               new Item { Key = 1, Field = "HollItem"},
               new Item { Key = 2, Field = "Privet"},
               new Item { Key = 3, Field = "Vitau"},
           };

            var listB = new List<Item> {
               new Item { Key = 1, Field = "John"},
               new Item { Key = 2, Field = "Doe"},
               new Item { Key = 2, Field = "Jim"},
               new Item { Key = 2, Field = "Steve"},
           };

            var result = listA.InnerJoin(source => source.Key, listB, joinField => joinField.Key);

            Assert.AreEqual(5, result.Count());
            Assert.AreEqual(3, result.Count(x => x.Item1.Key == 2));
        }
    }
}
