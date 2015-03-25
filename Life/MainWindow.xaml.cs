using Life.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ClassicGame _game = new ClassicGame();
        readonly Random _random = new Random(123);
        IField<CellState> _currentState = new ThorusField(80);
        IField<CellState> _nextState = new ThorusField(80);
        IField<Rectangle> _rectangles = new TranslatingMatrix<Rectangle>(80);

        DispatcherTimer _timer;


        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < Screen.Width; i+=10)
            {
                var myLine = VerticalLine(i);
                Screen.Children.Add(myLine);
            }

            for (int i = 0; i < Screen.Height; i += 10)
            {
                var myLine = HorizontalLine(i);
                Screen.Children.Add(myLine);
            }

            _rectangles.ForEach((i, j, value) =>
            {
                _currentState[i, j] = _random.Next(3) == 0 ? CellState.Live : CellState.Dead;

                value = new Rectangle();
                Screen.Children.Add(value);
                Canvas.SetLeft(value, i*10);
                Canvas.SetTop(value, j*10);
                value.Stroke = Brushes.DarkKhaki;
                value.Fill = Brushes.DarkKhaki;
                value.Width = 10;
                value.Height = 10;
                value.Visibility = Visibility.Hidden;
                _rectangles[i, j] = value;
            });


            EventHandler autoStep = (o, e) =>
            {
                _game.Step(_currentState, _nextState);

                _nextState.ForEach((i, j, value) =>
                {
                    ToggleRectangle(i, j, value == CellState.Live);
                });

                // redraw end
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;
            };

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(.1), DispatcherPriority.Render, autoStep, Dispatcher);
        }

        bool clicked = true;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleRectangle(0, 20, clicked);
            clicked = !clicked;

            _game.Step(_currentState, _nextState);
            // redraw

            _nextState.ForEach((i, j, value) =>
            {
                ToggleRectangle(i, j, value == CellState.Live);
            });

            // redraw end
            var temp = _currentState;
            _currentState = _nextState;
            _nextState = temp;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        void ToggleRectangle(int columnIndex, int rowIndex, bool visible)
        {
            _rectangles[columnIndex, rowIndex].Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        Line VerticalLine(int columnIndex)
        {
            return CreateLine(columnIndex, columnIndex, 0, Screen.Height);
        }

        Line HorizontalLine(int rowIndex)
        {
            return CreateLine(0, Screen.Width, rowIndex, rowIndex);
        }

        Line CreateLine(double x1, double x2, double y1, double y2)
        {
            return new Line
            {
                Stroke = Brushes.DarkBlue,
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = .2,
            };
        }

    }
}
