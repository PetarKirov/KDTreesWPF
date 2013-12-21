using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTrees.Utility;

namespace KDTrees.KDTree
{
    public struct Pair<T>
    {
        public Pair(T a, T b)
            : this()
        {
            this.A = a;
            this.B = b;
        }

        public T A { get; set; }
        public T B { get; set; }
    }

    public enum Axis
    {
        X,
        Y,
        Z,
        None
    }

    public class Point<T> : IEquatable<Point<T>> where T : IComparable<T>, IEquatable<T>
    {
        private T[] coordinates;

        public T this[Axis axis]
        {
            get { return this.coordinates[(int)axis]; }
            set { this.coordinates[(int)axis] = value; }
        }

        public T X
        {
            get { return this[Axis.X]; }
            set { this[Axis.X] = value; }
        }

        public T Y
        {
            get { return this[Axis.Y]; }
            set { this[Axis.Y] = value; }
        }

        public T Z
        {
            get { return this[Axis.Z]; }
            set { this[Axis.Z] = value; }
        }        

        public Point(T x, T y, T z)
        {
            this.coordinates = new T[3] { x, y, z };
        }

        public Point(Point<T> other) : this(other.X, other.Y, other.Z)
        {
        }

        public Point(T x, T y)
            : this(x, y, (default(T)))
        {
        }

        public Point(T x)
            : this(x, default(T), default(T))
        {
        }

        public bool Equals(Point<T> other)
        {
            return this.X.Equals(other.X) &&
                this.Y.Equals(other.Y) &&
                this.Z.Equals(other.Z);
        }

        public static bool operator ==(Point<T> a, Point<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point<T> a, Point<T> b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point<T>))
                return false;

            var other = (Point<T>)obj;

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public override string ToString()
        {
            return "{0}, {1}, {2}".Format(X, Y, Z);
        }
    }

    public class Point : Point<double>
    {
        public Point(Point other)
            : base(other)
        {
        }

        public Point(double x, double y, double z)
            : base(x, y, z)
        {
        }

        public Point(double x, double y)
            : base(x, y)
        {
        }

        public Point(double x)
            : base(x)
        {
        }

        /// <summary>
        /// Returns true if all of the coordinates of a are less then those of b
        /// </summary>
        public static bool operator <(Point a, Point b)
        {
            return a.X < b.X &&
                a.Y < b.Y &&
                a.Z < b.Z;
        }

        /// <summary>
        /// Returns true if all of the coordinates of a are greater then those of b
        /// </summary>
        public static bool operator >(Point a, Point b)
        {
            return a.X > b.X &&
                a.Y > b.Y &&
                a.Z > b.Z;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);
        }

        public static Point operator *(Point p, double a)
        {
            return new Point(
                p.X * a,
                p.Y * a,
                p.Z * a);
        }

        public static Point operator /(Point p, double a)
        {
            return new Point(
                p.X / a,
                p.Y / a,
                p.Z / a);
        }

        public static double DistanceBetween(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double dz = a.Z - b.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }

    public struct Triangle
    {
        public readonly Point[] points;

        public Triangle(Point a, Point b, Point c)
        {
            this.points = new Point[3];

            this.points[0] = a;
            this.points[1] = b;
            this.points[2] = c;
        }

        public Point A
        {
            get { return points[0]; }
            set { points[0] = value; }
        }

        public Point B
        {
            get { return points[1]; }
            set { points[1] = value; }
        }

        public Point C
        {
            get { return points[2]; }
            set { points[2] = value; }
        }
    }

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

    public abstract class KDNode
    {
        public BoundingBox Boundaries { get; set; }

        public abstract KDNode[] ChildNodes { get; }

        public KDNode Left { get { return this.ChildNodes[0]; } }

        public KDNode Right { get { return this.ChildNodes[1]; } }
    }

    public class KDNodeWithChildNodes : KDNode
    {
        private KDNode[] childNodes = new KDNode[2];
        public override KDNode[] ChildNodes
        {
            get { return this.childNodes; }
        }

        public Axis SeparationAxis { get; set; }

        public double separationPoint { get; set; }
    }

    public class KDNodeWithGeometry : KDNode
    {
        public override KDNode[] ChildNodes
        {
            get { throw new InvalidOperationException("No child nodes!"); }
        }

        public readonly List<Triangle> geometry;

        public static KDNode CreateTree()
        {
            var root = new KDNodeWithGeometry()
            {
                //geometry = 
            };


            return root;
        }
    }
}
