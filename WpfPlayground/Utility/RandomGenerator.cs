using KDTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayground.Utility
{
    /// <summary>
    /// A handy wrapper around System.Random.
    /// </summary>
    /// <remarks>
    /// Probably *not* thread-safe.
    /// </remarks>
    public static class RG
    {
        private static Random rnd = new Random(123);

        /// <summary>
        /// Re-initializes the random generator, using the specified seed value.
        /// </summary>
        public static void Reset(int seed)
        {
            rnd = new Random(seed);
        }

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

        public static System.Windows.Media.Color NextColor()
        {
            byte a = 255;
            byte r = (byte)rnd.Next(0, 256);
            byte g = (byte)rnd.Next(0, 256);
            byte b = (byte)rnd.Next(0, 256);

            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }

        public static Point NextPointNormalized()
        {
            return new Point(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble());
        }

        public static Point NextPointNormalized2D()
        {
            return new Point(rnd.NextDouble(), rnd.NextDouble());
        }

        public static Point NextPoint()
        {
            return new Point(rnd.Next(), rnd.Next(), rnd.Next());
        }

        public static Point NextPoint2D()
        {
            return new Point(rnd.Next(), rnd.Next());
        }

        public static Point NextPointInCoordinates(Point min, Point max)
        {
            return NextPointInCoordinates(
                (int)min.X, (int)min.Y, (int)min.Z,
                (int)max.X, (int)max.Y, (int)max.Z);
        }

        public static Point NextPointInCoordinates(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
        {
            return new Point(
                rnd.Next(minX, maxX),
                rnd.Next(minY, maxY),
                rnd.Next(minZ, maxZ));
        }

        public static Point NextPointInCoordinates2D(int minX, int minY, int maxX, int maxY)
        {
            return new Point(
                rnd.Next(minX, maxX),
                rnd.Next(minY, maxY));
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
}
