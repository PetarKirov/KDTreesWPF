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
using WpfPlayground.Utility;

namespace WpfPlayground
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.drawingCanvas.LineDrawn += (s, args) => VM.Append(args.ToString());
        }

        private MainViewModel VM { get { return this.DataContext as MainViewModel; } }

        private void FillWithRandomPoints(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var pos = RG.NextPointInCoordinates(
                    new KDTrees.Point(0.0, 0.0),
                    new KDTrees.Point(this.drawingCanvas.ActualWidth, this.drawingCanvas.ActualHeight));

                this.drawingCanvas.DrawEllipse(pos.ToWpfPoint(), 5, RG.NextColor());
            }
        }

        private async void BuildTreeOnCanvas(object sender, RoutedEventArgs e)
        {
            await this.drawingCanvas.BuildTree();
        }

        private void ClearCanvas(object sender, RoutedEventArgs e)
        {
            this.drawingCanvas.Reset();
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
