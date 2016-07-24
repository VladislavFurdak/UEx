using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uex;
using System.Linq;

namespace UEx_tests
{
    [TestClass]
    public class LeftJoin
    {
        [TestMethod]
        public void LeftJoinTest()
        {
            var listA = new List<Item> {
               new Item { Key = 1, Field = "Holla"},
               new Item { Key = 1, Field = "Holla"},
               new Item { Key = 2, Field = "Privet"},
               new Item { Key = 3, Field = "Vitau"},
               new Item { Key = 5, Field = "Czenkuyu"},
           };

            var listB = new List<Item> {
               new Item { Key = 1, Field = "John"},
               new Item { Key = 2, Field = "Doe"},
               new Item { Key = 2, Field = "Jim"},
               new Item { Key = 2, Field = "Steve"},
           };

            var result = listA.LeftJoin(source => source.Key, listB, joinField => joinField.Key);

            Assert.AreEqual(7, result.Count());
            Assert.AreEqual(3, result.Count(x => x.Item1.Key == 2));
            Assert.AreEqual(5, result.Single(x => x.Item1.Key == 5).Item1.Key);
        }
    }
}
