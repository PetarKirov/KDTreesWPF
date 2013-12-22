using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KDTrees;

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

            Assert.AreEqual(new Point(0.0, 0.0), 
                a.Min);
            Assert.AreEqual(new Point(5, 13), 
                a.Max);

            Assert.AreEqual(new Point(5, 0), 
                b.Min);
            Assert.AreEqual(new Point(10, 13), 
                b.Max);
        }

        [TestMethod]
        public void TesSplitByY1()
        {
            BoundingBox bb = new BoundingBox(new Point(0.0, 0.0), new Point(10, 13));

            var pair = bb.Split(5, Axis.Y);

            var a = pair.A;
            var b = pair.B;

            Assert.AreEqual(new Point(0.0, 0.0), 
                a.Min);
            Assert.AreEqual(new Point(10, 5), 
                a.Max);

            Assert.AreEqual(new Point(0, 5), 
                b.Min);
            Assert.AreEqual(new Point(10, 13), 
                b.Max);
        }

        [TestMethod]
        public void TesSplitByZ1()
        {
            BoundingBox bb = new BoundingBox(new Point(3.4, 7.0, 8.0), new Point(20.0, 13.0, 10.0));

            var pair = bb.Split(9, Axis.Z);

            var a = pair.A;
            var b = pair.B;

            Assert.AreEqual(new Point(3.4, 7.0, 8.0), 
                a.Min);
            Assert.AreEqual( new Point(20.0, 13.0, 9.0), 
                a.Max);

            Assert.AreEqual(new Point(3.4, 7.0, 9.0), 
                b.Min);
            Assert.AreEqual(new Point(20.0, 13.0, 10.0), 
                b.Max);
        }

        [TestMethod]
        public void TestMaxValue()
        {
            var max = BoundingBox.MaxValue;

            Assert.AreEqual(new Point(0, 0, 0), 
                max.Min);
            Assert.AreEqual(new Point(double.MaxValue, double.MaxValue, double.MaxValue), 
                max.Max);
        }
    }
}

