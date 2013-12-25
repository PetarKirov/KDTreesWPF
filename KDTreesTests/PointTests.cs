using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KDTrees;

namespace KDTreesTests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void DistanceBetween2D_ShouldNotAccountForZ()
        {
            Point a = new Point(3, 0, 1000);
            Point b = new Point(0, 4, 5000);

            var distance = Point.DistanceBetween2D(a, b);

            Assert.AreEqual(5.0, distance);

            Point x = new Point(0, 0, 99953.445);
            Point y = new Point(0, 0, 753.88);

            distance = Point.DistanceBetween2D(x, y);

            Assert.AreEqual(0.0, distance);
        }

        [TestMethod]
        public void DistanceBetween_Test()
        {
            Point a = new Point(3, 0, 0);
            Point b = new Point(0, 0, 4);
            var distance = Point.DistanceBetween(a, b);
            Assert.AreEqual(5.0, distance);

            Point x = new Point(0, 0, 0);
            Point y = new Point(0, 0, 1);
            distance = Point.DistanceBetween(x, y);
            Assert.AreEqual(1.0, distance);
        }
    }
}
