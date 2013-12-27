using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTrees
{
    public interface IGeometry
    {
        bool IsContainedIn(BoundingBox box);
    }

    public struct BoundingBox
    {
        public Point Min;
        public Point Max;

        public double Width
        {
            get { return Max.X - Min.X; }
        }

        public double Height
        {
            get { return this.Max.Y - this.Min.Y; }
        }

        public BoundingBox(Point min, Point max)
            : this()
        {
            if (min.X > max.X || min.Y > max.Y || min.Z > max.Z)
                throw new ArgumentException("min should be smaller than max!");

            this.Min = min;
            this.Max = max;
        }

        public BoundingBox(double width, double height)
            : this(new Point(0), new Point(width, height))
        { }

        public BoundingBox(BoundingBox other)
        {
            this.Min = new Point(other.Min);
            this.Max = new Point(other.Max);
        }

        public BoundingBox(IEnumerable<Point> points)
            : this()
        {
            this.Min = new Point(double.MaxValue, double.MaxValue, double.MaxValue);
            this.Max = new Point(double.MinValue, double.MinValue, double.MinValue);

            foreach (var p in points)
                this.Extend(p);
        }

        public BoundingBox(Triangle t)
            : this(t.points)
        {
        }

        public void Extend(Point p)
        {
            if (p.X < Min.X)
                Min.X = p.X;
            if (p.Y < Min.Y)
                Min.Y = p.Y;
            if (p.Z < Min.Z)
                Min.Z = p.Z;

            if (p.X > Max.X)
                Max.X = p.X;
            if (p.Y > Max.Y)
                Max.Y = p.Y;
            if (p.Z > Max.Z)
                Max.Z = p.Z;
        }

        public bool Contains(Point point, double delta = 1e-6)
        {
            return (Min.X - delta <= point.X && point.X <= Max.X + delta &&
                    Min.Y - delta <= point.Y && point.Y <= Max.Y + delta &&
                    Min.Z - delta <= point.Z && point.Z <= Max.Z + delta);
        }

        /// <summary>
        /// Naively checks if a triangle is containd in the bounding box.
        /// </summary>
        /// <remarks>
        /// Does not account for the other 2 cases: 
        /// A) One of the sides of the triangle intersects the bounding box.
        /// B) An edge of the bounding box intersects the triangle area.
        /// </remarks>
        /// <returns>
        /// true if its 3 vertices are inside.
        /// </returns>
        public bool Contains(Triangle t, double delta = 1e-6)
        {
            //TODO: Check the other 2 cases.
            return Contains(t.A, delta) || Contains(t.B, delta) || Contains(t.C, delta);
        }

        /// <summary>
        /// Checks if a triangle is containd in the bounding box.
        /// </summary>
        /// <returns>
        /// true if its 3 vertices are inside.
        /// </returns>
        public bool Contains2D(Triangle t, double delta = 1e-6)
        {
            return Contains(t.A, delta) || Contains(t.B, delta) || Contains(t.C, delta);
        }

        public Pair<BoundingBox> Split(double where, Axis axis)
        {
            if (axis == Axis.None)
                throw new ArgumentException("The splitting axis should be X, Y or Z!");

            var left = new BoundingBox(this);
            var right = new BoundingBox(this);

            left.Max[axis] = where;
            right.Min[axis] = where;

            return new Pair<BoundingBox>(left, right);
        }

        public Pair<BoundingBox> SplitInHalf(Axis axis)
        {
            double where = (this.Min[axis] + this.Max[axis]) / 2.0;

            return Split(where, axis);
        }

        public static readonly BoundingBox MaxValue = new BoundingBox(
                    new Point(0.0, 0.0, 0.0),
                    new Point(double.MaxValue, double.MaxValue, double.MaxValue));


        #region Splitted Box Comparer
        private static readonly SplittedBoxComparer comparer = new SplittedBoxComparer();

        public static IEqualityComparer<BoundingBox> GetSplittedBoxComparer()
        {
            return BoundingBox.comparer;
        }

        /// <summary>
        /// Equals returns true if the two boxes have a common side.
        /// </summary>
        class SplittedBoxComparer : IEqualityComparer<BoundingBox>
        {
            public bool Equals(BoundingBox x, BoundingBox y)
            {
                Axis ignore;
                return Point.AreParallelToAnAxis(x.Min, y.Max, out ignore) ||
                    Point.AreParallelToAnAxis(x.Max, y.Min, out ignore);
            }

            public int GetHashCode(BoundingBox obj)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
