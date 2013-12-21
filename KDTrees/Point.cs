using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTrees.Utility;

namespace KDTrees
{
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

        //public Point(System.Windows.Point wpfPoint)
        //    : base(wpfPoint.X, wpfPoint.Y)
        //{ }

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

        public static double DistanceBetween(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double dz = a.Z - b.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static double DistanceBetween2D(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double DistanceTo(Point other)
        {
            return DistanceBetween(this, other);
        }

        public double DistanceTo2D(Point other)
        {
            return DistanceBetween2D(this, other);
        }
    }
}
