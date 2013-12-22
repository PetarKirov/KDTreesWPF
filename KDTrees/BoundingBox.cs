using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTrees
{
    public struct BoundingBox
    {
        public Point Min;
        public Point Max;

        public BoundingBox(Point min, Point max)
            : this()
        {
            if (min.X > max.X || min.Y > max.Y || min.Z > max.Z)
                throw new ArgumentException("min should be smaller than max!");

            this.Min = min;
            this.Max = max;
        }

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

        public Pair<BoundingBox> Split(double where, Axis axis)
        {
            var left = new BoundingBox(this);
            var right = new BoundingBox(this);

            left.Max[axis] = where;
            right.Min[axis] = where;

            return new Pair<BoundingBox>(left, right);
        }

        public static readonly BoundingBox MaxValue = new BoundingBox(
                    new Point(0.0, 0.0, 0.0),
                    new Point(double.MaxValue, double.MaxValue, double.MaxValue));
    }
}
