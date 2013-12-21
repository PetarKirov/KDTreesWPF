using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KDTrees.Utility;

namespace WpfPlayground
{
    public partial class DrawingCanvas : UserControl
    {
        public event LineDrawnEventHandler LineDrawn;

        public DrawingCanvas()
        {
            InitializeComponent();

            SetUpMouseInteractions();
        }

        private void SetUpMouseInteractions()
        {
            var mouseClicks = Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(
                h => canvas.MouseLeftButtonUp += h,
                h => canvas.MouseLeftButtonUp -= h)
                .Select(click => new { Position = click.EventArgs.GetPosition(canvas), TimeStamp = click.EventArgs.Timestamp })
                .Distinct(clickInfo => clickInfo.Position)
                .Buffer(3);

            mouseClicks.Subscribe(args =>
            {
                var first = args.First();
                var second = args.Skip(1).First();
                var third = args.Last();

                var p = new Polygon();
                p.Points.Add(first.Position);
                p.Points.Add(second.Position);
                p.Points.Add(third.Position);

                p.Stroke = Brushes.Red;
                p.StrokeThickness = 1;

                canvas.Children.Add(p);

                if (this.LineDrawn != null)
                    LineDrawn(this, new LineDrawnEventArgs(first.Position, second.Position, DateTime.Now));
            });
        }

        internal void DrawEllipse(Point pos, int size, Color color)
        {
            var e = new Ellipse() { Width = size, Height = size, Fill = new SolidColorBrush(color) };
            Canvas.SetLeft(e, pos.X);
            Canvas.SetTop(e, pos.Y);

            this.canvas.Children.Add(e);
        }
    }

    public class LineDrawnEventArgs : EventArgs
    {
        public LineDrawnEventArgs(Point start, Point end, DateTime timeStamp)
        {
            this.Start = start;
            this.End = end;
            this.TimeStamp = timeStamp;
        }

        /// <summary>
        /// Get the starting poiont of the line
        /// </summary>
        public Point Start { get; private set; }

        /// <summary>
        /// Gets the ending point of the line
        /// </summary>
        public Point End { get; private set; }

        /// <summary>
        /// Gets the time when the line was created
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        public override string ToString()
        {
            return "Start: {0}, {1} End: {2}, {3}, TimeStamp: {4}".Format(Start.X, Start.Y, End.X, End.Y, TimeStamp);
        }
    }

    public delegate void LineDrawnEventHandler(object sender, LineDrawnEventArgs e);
}
