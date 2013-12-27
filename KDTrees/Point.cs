using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTrees.Utility;

namespace KDTrees
{
    public enum Axis : uint
    {
        None = 0,
        X = 1,
        Y = X << 1,
        Z = Y << 1,
        XY = X | Y,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z
    }

    public static class AxisExtensions
    {
        /// <summary>
        /// Cycles through X, Y and Z
        /// </summary>
        public static Axis Next(this Axis a)
        {
            int i = (int)a;
            return (Axis)((++i) % 3);
        }

        /// <summary>
        /// Cycles through X and Y
        /// </summary>
        public static Axis Next2D(this Axis a)
        {
            int i = (int)a;
            return (Axis)((++i) % 2);
        }

        /// <summary>
        /// Returns 0, 1 or 2 for values of X, Y or Z, respectively.
        /// Otherwise, returns 3.
        /// </summary>
        public static int ToPointComponentIndex(this Axis axis)
        {
            switch (axis)
            {               
                case Axis.X:
                    return 0;
                case Axis.Y:
                    return 1;
                case Axis.Z:
                    return 2;
                default:
                    return 3; //should throw OutOfRangeException if used as p[axis] = val;
            }
        }
    }

    public class Point<T> : IEquatable<Point<T>> where T : IComparable<T>, IEquatable<T>
    {
        private T[] coordinates;

        public T this[Axis axis]
        {
            get { return this.coordinates[axis.ToPointComponentIndex()]; }
            set { this.coordinates[axis.ToPointComponentIndex()] = value; }
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

        public Point(Point<T> other)
            : this(other.X, other.Y, other.Z)
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

        /// <summary>
        /// Checks if the points *a* and *b* lie on a line parallel to an axis. This is 
        /// true if one of their components (X, Y or Z) match.
        /// </summary>
        /// <returns></returns>
        public static bool AreParallelToAnAxis(Point<T> a, Point<T> b, out Axis parallelAxis)
        {
            parallelAxis = Axis.None;

            if (a.X.Equals(b.X))
                parallelAxis |= Axis.X;

            if (a.Y.Equals(b.Y))
                parallelAxis |= Axis.Y;

            if (a.Z.Equals(b.Z))
                parallelAxis |= Axis.Z;

            return parallelAxis != Axis.None;
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

    public class Point : Point<double>, IGeometry
    {
        public Point(Point other)
            : base(other)
        { }

        public Point(double x, double y, double z)
            : base(x, y, z)
        { }

        public Point(double x, double y)
            : base(x, y)
        { }

        public Point(double x)
            : base(x)
        { }

        /// <summary>
        /// Returns true if all of the coordinates of first Point are less then those of second
        /// </summary>
        public static bool operator <(Point a, Point b)
        {
            return a.X < b.X &&
                a.Y < b.Y &&
                a.Z < b.Z;
        }

        /// <summary>
        /// Returns true if all of the coordinates of first Point are greater then those of second
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

        /// <summary>
        /// Calculates the distance between two points in 3D.
        /// </summary>
        public static double DistanceBetween(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double dz = a.Z - b.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Calculates the distance between two points in 2D (without accounting for the Z dimension).
        /// </summary>
        public static double DistanceBetween2D(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Calculates the distance between *this* and the *other* point in 3D.
        /// </summary>
        public double DistanceTo(Point other)
        {
            return DistanceBetween(this, other);
        }

        /// <summary>
        /// Calculates the distance between *this* and the *other* point in 2D
        /// (without accounting for the Z dimension).
        /// </summary>
        public double DistanceTo2D(Point other)
        {
            return DistanceBetween2D(this, other);
        }

        public bool IsContainedIn(BoundingBox box)
        {
            return box.Contains(this);
        }
    }
}
