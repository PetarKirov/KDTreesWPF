using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KDTrees;

namespace KDTreesTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void TestPerimeter2D()
        {
            var t = new Triangle(new Point(0.0, 0.0), new Point(4.0, 0.0), new Point(0.0, 3.0));
            Assert.AreEqual(12.0, t.Peremiter2D);

            var t2 = new Triangle(new Point(0.0, 0.0), new Point(0.0, 0.0), new Point(0.0, 0.0));
            Assert.AreEqual(0.0, t2.Peremiter2D);
        }

        [TestMethod]
        public void TestArea2D()
        {
            var t = new Triangle(new Point(0.0, 0.0), new Point(4.0, 0.0), new Point(0.0, 3.0));
            double s = t.Area2D;
            double expectedS = (3 * 4) / 2;
            Assert.AreEqual(expectedS, s);

            var t2 = new Triangle(new Point(0.0, 0.0), new Point(0.0, 0.0), new Point(0.0, 0.0));

            Assert.AreEqual(0.0, t2.Area2D);
        }
    }
}
