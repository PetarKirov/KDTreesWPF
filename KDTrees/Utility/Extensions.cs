using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;

namespace KDTrees.Utility
{
    public static class Extensions
    {
        public static string Format(this string formatString, params object[] args)
        {
            return string.Format(formatString, args);
        }
    }

    /// <summary>
    /// A handy wrapper around System.Random.
    /// </summary>
    public static class RG
    {
        private static readonly Random rnd = new Random(123);

        public static int Next()
        {
            return rnd.Next();
        }

        public static int Next(int max)
        {
            return rnd.Next(max);
        }

        public static int Next(int min, int max)
        {
            return rnd.Next(min, max);
        }

        //public static Color NextColor()
        //{
        //    byte a = 255;
        //    byte r = (byte)rnd.Next(0, 256);
        //    byte g = (byte)rnd.Next(0, 256);
        //    byte b = (byte)rnd.Next(0, 256);

        //    return Color.FromArgb(a, r, g, b);
        //}

        public static Point NextPointNormalized()
        {
            return new Point(rnd.NextDouble(), rnd.NextDouble());
        }

        public static Point NextPoint()
        {
            return new Point(rnd.Next(), rnd.Next());
        }

        public static Point NextPointInCoordinates(Point min, Point max)
        {
            return NextPointInCoordinates((int)min.X, (int)min.Y, (int)max.X, (int)max.Y);
        }

        public static Point NextPointInCoordinates(int minX, int minY, int maxX, int maxY)
        {
            return new Point(rnd.Next(minX, maxX), rnd.Next(minY, maxY));
        }

        public static T[] CreateArray<T>(int count, Func<int, T> func)
        {
            var arr = new T[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = func(i);
            }

            return arr;
        }
    }

    public struct Stats
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Avg { get; set; }

        public double Delta { get; set; }

        public Stats(double min, double max, double avg, double delta)
            : this()
        {
            this.Min = min;
            this.Max = max;
            this.Avg = avg;
            this.Delta = delta;
        }

        public static Stats FindFrom(IEnumerable<double> values)
        {
            double min = Double.MaxValue;
            double max = Double.MinValue;
            double avg = 0.0;
            double delta = 0.0;

            foreach (var x in values)
            {
                min = Math.Min(min, x);
                max = Math.Max(max, x);
            }

            avg = (min + max) / 2;
            delta = max - min;

            return new Stats(min, max, avg, delta);
        }

        public static Stats FindFrom(IEnumerable<float> values)
        {
            return FindFrom(values.Select(x => (double)x));
        }

        public static Stats FindFrom(IEnumerable<long> values)
        {
            return FindFrom(values.Select(x => (double)x));
        }

        public static Stats FindFrom(IEnumerable<int> values)
        {
            return FindFrom(values.Select(x => (double)x));
        }
    }
}
