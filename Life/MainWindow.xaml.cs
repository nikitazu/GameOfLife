using Life.Components;
using Life.Core;
using System;
using System.Windows;
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
            _component.PutLinesOn(Screen);
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
    }
}
