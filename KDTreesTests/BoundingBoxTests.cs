using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KDTrees;

namespace KDTreesTests
{
    [TestClass]
    public class BoundingBoxTests
    {
        [TestMethod]
        public void Constructor_WithValidArgs_ShouldNotThrow()
        {
            try
            {
                BoundingBox b1 = new BoundingBox(new Point(0.0), new Point(0.0));
                BoundingBox b2 = new BoundingBox(new Point(0.0, 0.0), new Point(0.0, 0.0));
                BoundingBox b3 = new BoundingBox(new Point(0.0, 0.0, 0.0), new Point(0.0, 0.0, 0.0));

                BoundingBox b4 = new BoundingBox(new Point(17.8), new Point(24.7));
                BoundingBox b5 = new BoundingBox(new Point(17.8, 9.5), new Point(24.7, 10.3));
                BoundingBox b6 = new BoundingBox(new Point(17.8, 9.5, 13.2), new Point(24.7, 10.3, 18.7));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Constructor_WithTriangle_ShoudExtendProperly()
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
        public void Constructor_WithReversedArguments_ShouldThrowArgumentException()
        {
            try
            {
                BoundingBox b1 = new BoundingBox(new Point(5.0, 0.0), new Point(0.0, 0.0));
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("min should be smaller than max!", ex.Message);
            }

            try
            {
                BoundingBox b1 = new BoundingBox(new Point(0.0, 5.0), new Point(0.0, 0.0));
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("min should be smaller than max!", ex.Message);
            }
        }

        [TestMethod]
        public void Split_ByX()
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
        public void Split_ByY()
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
        public void Split_ByZ()
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
        public void Split_ByNone_ShouldThrowArgumentException()
        {
            BoundingBox bb = new BoundingBox(new Point(3.4, 7.0, 8.0), new Point(20.0, 13.0, 10.0));
            
            try
            {
                var pair = bb.Split(9, Axis.None);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("The splitting axis should be X, Y or Z!", ex.Message);
            }
        }

        [TestMethod]
        public void MaxValue_ShouldBeFrom_0_To_Inf()
        {
            var max = BoundingBox.MaxValue;

            Assert.AreEqual(new Point(0, 0, 0), 
                max.Min);
            Assert.AreEqual(new Point(double.MaxValue, double.MaxValue, double.MaxValue), 
                max.Max);
        }
    }
}

