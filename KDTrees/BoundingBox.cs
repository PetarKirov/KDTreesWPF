using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTrees
{
    public struct BoundingBox
    {
        public Point Min;
        public Point Max;

        public BoundingBox(BoundingBox other)
        {
            this.Min = new Point(other.Min);
            this.Max = new Point(other.Max);
        }

        public BoundingBox(Point min, Point max)
            : this()
        {
            if (min.X > max.X || min.Y > max.Y || min.Z > max.Z)
                throw new ArgumentException("min should be smaller than max!");

            this.Min = min;
            this.Max = max;
        }

        public BoundingBox(IEnumerable<Point> points)
            : this()
        {
            var min = new Point(double.MaxValue, double.MaxValue, double.MaxValue);
            var max = new Point(double.MinValue, double.MinValue, double.MinValue);

            foreach (var p in points)
            {
                min.X = Math.Min(min.X, p.X);
                min.Y = Math.Min(min.Y, p.Y);
                min.Z = Math.Min(min.Z, p.Z);

                max.X = Math.Max(max.X, p.X);
                max.Y = Math.Max(max.Y, p.Y);
                max.Z = Math.Max(max.Z, p.Z);
            }

            this.Min = min;
            this.Max = max;
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

        public Pair<BoundingBox> Split(double where, Axis axis)
        {
            var left = new BoundingBox(this);
            var right = new BoundingBox(this);

            left.Max[axis] = where;
            right.Min = left.Max;

            return new Pair<BoundingBox>(left, right);
        }

        public static BoundingBox MaxValue
        {
            get
            {
                return new BoundingBox(
                    new Point(0.0, 0.0, 0.0),
                    new Point(double.MaxValue, double.MaxValue, double.MaxValue));
            }
        }
    }
}
