using KDTrees.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfPlayground
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.drawingCanvas.LineDrawn += (s, args) => VM.Append(args.ToString());
        }

        public MainViewModel VM { get { return this.DataContext as MainViewModel; } }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < 100000; i++)
            //{
            //    var pos = RandomGenerator.GetRandomPointInCoordinates(
            //        new Point(0.0, 0.0),
            //        new Point(this.drawingCanvas.ActualWidth, this.drawingCanvas.ActualHeight));

            //    this.drawingCanvas.DrawEllipse(pos, 1, RandomGenerator.GetRandomColor());
            //}

            int n = 10;

            var d = RG.CreateArray(n, (x) => 0);

            for (int i = 0; i < n * 1000000; i++)
            {
                d[RG.Next(0, n)]++;
            }

            var s = Stats.FindFrom(d);
        }
    }

    public class MainViewModel : ReactiveUI.ReactiveObject
    {
        public ObservableCollection<LogMessage> LogStream { get; private set; }

        public MainViewModel()
        {
            this.LogStream = new ObservableCollection<LogMessage>();
        }

        public void Append(string message)
        {
            this.LogStream.Add(new LogMessage(message, DateTime.Now));
        }
    }

    public class LogMessage
    {
        public LogMessage(string message, DateTime time)
        {
            this.Message = message;
            this.TimeStamp = time;
        }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
