using KDTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayground.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Implicitly converts *to* a WPF Point by using only the X and Y components.
        /// </summary>
        public static System.Windows.Point ToWpfPoint(this Point point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }

        /// <summary>
        /// Implicitly converts *from* a WPF Point *to* a KDTrees.Point.
        /// </summary>
        public static Point ToPoint(this System.Windows.Point wpfPoint)
        {
            return new Point(wpfPoint.X, wpfPoint.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Triangle ToTriangle(this System.Windows.Shapes.Polygon wpfPolygon)
        {
            if (wpfPolygon.Points.Count != 3)
                throw new ArgumentException("A WPF Polygon needs to have 3 points in order to be convrted to a Triangle");

            return new Triangle(
                wpfPolygon.Points[0].ToPoint(),
                wpfPolygon.Points[1].ToPoint(),
                wpfPolygon.Points[2].ToPoint());
        }
    }
}
