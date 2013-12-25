using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTrees.Utility
{
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
