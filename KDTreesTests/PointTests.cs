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

        [TestMethod]
        public void AxisEnum_Test()
        {
            Assert.AreEqual(0L, (uint)Axis.None);
            Assert.AreEqual(1L, (uint)Axis.X);
            Assert.AreEqual(2L, (uint)Axis.Y);
            Assert.AreEqual(4L, (uint)Axis.Z);
            Assert.AreEqual(3L, (uint)Axis.XY);
            Assert.AreEqual(5L, (uint)Axis.XZ);
            Assert.AreEqual(6L, (uint)Axis.YZ);
            Assert.AreEqual(7L, (uint)Axis.XYZ);
        }

        [TestMethod]
        public void ParallelToAnAxis_WithEqualPoints_ShouldReturn_True_AndPassOut_XYZ()
        {
            Point eqA = new Point(3, 0, 4);
            Point eqB = new Point(3, 0, 4);

            Axis axis;
            bool result = Point.AreParallelToAnAxis(eqA, eqB, out axis);

            Assert.AreEqual(eqA, eqB);
            Assert.AreEqual(Axis.XYZ, axis);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ParallelToAnAxis_Test_X_Y_Z()
        {
            //Test X:
            Point x1 = new Point(4, -45, 2);
            Point x2 = new Point(4, -999, -82);
            Axis axis;
            bool result = Point.AreParallelToAnAxis(x1, x2, out axis);
            Assert.AreEqual(Axis.X, axis);
            Assert.AreEqual(true, result);

            //Test Y:
            Point y1 = new Point(4, 455, -88);
            Point y2 = new Point(5, 455, double.MaxValue);
            result = Point.AreParallelToAnAxis(y1, y2, out axis);
            Assert.AreEqual(Axis.Y, axis);
            Assert.AreEqual(true, result);

            //Test Z:
            Point z1 = new Point(float.MinValue, 23, int.MaxValue);
            Point z2 = new Point(5, float.MaxValue, int.MaxValue);
            result = Point.AreParallelToAnAxis(z1, z2, out axis);
            Assert.AreEqual(Axis.Z, axis);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ParallelToAnAxis_TestXY()
        {
            Point a = new Point(4, -999, 2);
            Point b = new Point(4, -999, -82);

            Axis axis;
            bool result = Point.AreParallelToAnAxis(a, b, out axis);

            Assert.AreEqual(Axis.XY, axis);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ParallelToAnAxis_TestXZ()
        {
            Point a = new Point(89, 7, 34);
            Point b = new Point(89, -5, 34);

            Axis axis;
            bool result = Point.AreParallelToAnAxis(a, b, out axis);

            Assert.AreEqual(Axis.XZ, axis);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ParallelToAnAxis_TestYZ()
        {
            Point a = new Point(-9, 7, 2);
            Point b = new Point(6, 7, 2);

            Axis axis;
            bool result = Point.AreParallelToAnAxis(a, b, out axis);

            Assert.AreEqual(Axis.YZ, axis);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ParallelToAnAxis_TestNone()
        {
            Point a = new Point(short.MaxValue, ulong.MinValue, float.MinValue);
            Point b = new Point(int.MinValue, float.MaxValue, double.MaxValue);

            Axis axis;
            bool result = Point.AreParallelToAnAxis(a, b, out axis);

            Assert.AreEqual(Axis.None, axis);
            Assert.AreEqual(false, result);
        }
    }
}
