using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KDTrees.KDTree;

namespace KDTreesTests
{
    [TestClass]
    public class BoundingBoxTests
    {
        [TestMethod]
        public void TestConstructor1()
        {
            BoundingBox b1 = new BoundingBox(new Point(0.0), new Point(0.0));
            BoundingBox b2 = new BoundingBox(new Point(0.0, 0.0), new Point(0.0, 0.0));
            BoundingBox b3 = new BoundingBox(new Point(0.0, 0.0, 0.0), new Point(0.0, 0.0, 0.0));
        }

        [TestMethod]
        public void TestConstructor3()
        {
            BoundingBox b1 = new BoundingBox(new Point(17.8), new Point(24.7));
            BoundingBox b2 = new BoundingBox(new Point(17.8, 9.5), new Point(24.7, 10.3));
            BoundingBox b3 = new BoundingBox(new Point(17.8, 9.5, 13.2), new Point(24.7, 10.3, 18.7));
        }

        [TestMethod]
        public void TestConstructor4()
        {
            BoundingBox b1 = new BoundingBox(
                new Triangle(
                    new Point(5.0, 2.9),
                    new Point(3.17, 6.5),
                    new Point(7.12, 3.12)));

            Assert.AreEqual(new Point(3.17, 2.9), b1.Min);
            Assert.AreEqual(new Point(7.12, 6.5), b1.Max);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorException1()
        {
            BoundingBox b1 = new BoundingBox(new Point(5.0, 0.0), new Point(0.0, 0.0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorException2()
        {
            BoundingBox b1 = new BoundingBox(new Point(0.0, 5.0), new Point(0.0, 0.0));
        }

        [TestMethod]
        public void TesSplitByX1()
        {
            BoundingBox bb = new BoundingBox(new Point(0.0, 0.0), new Point(10, 13));

            var pair = bb.Split(5, Axis.X);

            var a = pair.A;
            var b = pair.B;

            Assert.AreEqual(a.Min, new Point(0.0, 0.0));
            Assert.AreEqual(a.Max, new Point(5, 13));

            Assert.AreEqual(b.Min, new Point(5, 13));
            Assert.AreEqual(b.Max, new Point(10, 13));
        }

        [TestMethod]
        public void TesSplitByY1()
        {
            BoundingBox bb = new BoundingBox(new Point(0.0, 0.0), new Point(10, 13));

            var pair = bb.Split(5, Axis.Y);

            var a = pair.A;
            var b = pair.B;

            Assert.AreEqual(a.Min, new Point(0.0, 0.0));
            Assert.AreEqual(a.Max, new Point(10, 5));

            Assert.AreEqual(b.Min, new Point(10, 5));
            Assert.AreEqual(b.Max, new Point(10, 13));
        }

        [TestMethod]
        public void TestMaxValue()
        {
            var max = BoundingBox.MaxValue;

            Assert.AreEqual(max.Min, new Point(0, 0, 0));
            Assert.AreEqual(max.Max, new Point(double.MaxValue, double.MaxValue, double.MaxValue));
        }
    }
}

