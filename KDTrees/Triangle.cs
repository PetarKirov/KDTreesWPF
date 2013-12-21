using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTrees
{
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

        /// <summary>
        /// Calculates the perimeter of the triangle in 3D
        /// </summary>
        public double Peremiter
        {
            get
            {
                return Point.DistanceBetween(A, B) +
                    Point.DistanceBetween(B, C) +
                    Point.DistanceBetween(C, A);
            }
        }

        /// <summary>
        /// Calculates the perimeter of the triangle in 2D (without accounting for Z)
        /// </summary>
        public double Peremiter2D
        {
            get
            {
                return Point.DistanceBetween2D(A, B) +
                    Point.DistanceBetween2D(B, C) +
                    Point.DistanceBetween2D(C, A);
            }
        }

        /// <summary>
        /// Calculates the area of the triangle in 3D using Heron's formula
        /// </summary>
        public double Area
        {
            get
            {
                double a = B.DistanceTo(C);
                double b = A.DistanceTo(C);
                double c = A.DistanceTo(B);

                return 0.25 * Math.Sqrt(
                    (-a + b + c) *
                    (a - b + c) *
                    (a + b - c) *
                    (a + b + c));
            }
        }

        /// <summary>
        /// Calculates the area of the triangle in 2D using Heron's formula
        /// </summary>
        public double Area2D
        {
            get
            {
                double a = B.DistanceTo2D(C);
                double b = A.DistanceTo2D(C);
                double c = A.DistanceTo2D(B);

                return 0.25 * Math.Sqrt(
                    (-a + b + c) *
                    (a - b + c) *
                    (a + b - c) *
                    (a + b + c));
            }
        }
    }
}
