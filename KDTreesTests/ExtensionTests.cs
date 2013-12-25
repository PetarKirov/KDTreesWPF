using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KDTrees.Utility;

namespace KDTreesTests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void IListEquals()
        {
            var a = new List<int>() { 3, 67, 5, 7, 2, 8 };
            var b = new List<int>() { 3, 67, 5, 7, 2, 8 };
            var c = new List<int>() { 3, 67, 5, 7, 2 };

            Assert.IsTrue(Extensions.IListEquals(a, b));
            Assert.IsTrue(Extensions.IListEquals(a, b, (x, y) => x == y));
            Assert.IsFalse(Extensions.IListEquals(a, b, (x, y) => x != y));

            Assert.IsFalse(Extensions.IListEquals(a, c));
        }

        private static IEnumerable<int> GetTestSequence()
        {
            yield return 3;
            yield return 67;
            yield return 5;
            yield return 7;
            yield return 2;
            yield return 8;
        }

        [TestMethod]
        public void IEnumerableEquals()
        {
            var a = new List<int>() { 3, 67, 5, 7, 2, 8 }.Select(x => x);
            var b = GetTestSequence();
            var c = new int[] { 3, 67, 5, 7, 2 }.Select(x => x);

            Assert.IsTrue(Extensions.IEnumerableEquals(a, b));
            Assert.IsTrue(Extensions.IEnumerableEquals(a, b, (x, y) => x == y));
            Assert.IsFalse(Extensions.IEnumerableEquals(a, b, (x, y) => x != y));

            Assert.IsFalse(Extensions.IEnumerableEquals(a, c));
        }

        [TestMethod]
        public void TakeIf_Test()
        {
            var originalList = new List<char> { 'A', 'b', 'c', 'D', 'e', 'f', 'g', 'H' };

            var result = originalList.TakeIf(c => Char.IsUpper(c));

            var expectedResult = new List<char> { 'A', 'D', 'H' };

            Assert.IsTrue(Extensions.IListEquals(result, expectedResult));
        }

        [TestMethod]
        public void TakeAll_WithNotEmptyList()
        {
            var a = new List<int> { 3, 56, 7, 4, 8, 5 };
            var b = new List<int> { 3, 56, 7, 4, 8, 5 };

            var copyOfA = a.TakeAll();

            Assert.IsTrue(a.Count == 0);
            Assert.IsTrue(Extensions.IListEquals(copyOfA, b));
        }

        [TestMethod]
        public void TakeAll_WithEmptyList()
        {
            var a = new List<int> { };
            var b = new List<int> { };

            var copyOfA = a.TakeAll();

            Assert.IsTrue(a.Count == 0);
            Assert.IsTrue(Extensions.IListEquals(copyOfA, b));
        }
    }
}
