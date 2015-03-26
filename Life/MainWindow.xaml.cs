using Life.Components;
using Life.Core;
using System;
using System.Windows;
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
        readonly GameComponent _component;
        readonly DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _component = (Application.Current as App).GameComponent;

            for (int i = 0; i < _component.Config.FieldSize; i++)
            {
                var myLine = VerticalLine(i*10);
                Screen.Children.Add(myLine);
            }

            for (int i = 0; i < _component.Config.FieldSize; i++)
            {
                var myLine = HorizontalLine(i*10);
                Screen.Children.Add(myLine);
            }

            _component.PutRectanglesOn(Screen);

            EventHandler autoStep = (o, e) =>
            {
                _component.AutoStep((i, j, value) => ToggleRectangle(i, j, value == CellState.Live));
            };

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(.1), DispatcherPriority.Render, autoStep, Dispatcher);
        }

        bool clicked = true;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleRectangle(0, 20, clicked);
            clicked = !clicked;

            _component.GameOfLife.Step(_component.CurrentState, _component.NextState);
            // redraw

            _component.NextState.ForEach((i, j, value) =>
            {
                ToggleRectangle(i, j, value == CellState.Live);
            });

            _component.SwapState();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        void ToggleRectangle(int columnIndex, int rowIndex, bool visible)
        {
            _component.Rectangles[columnIndex, rowIndex].Visibility = visible ? Visibility.Visible : Visibility.Hidden;
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
