using Life.Components;
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
            _component.InitializeGraphics(Screen);

            EventHandler autoStep = (o, e) =>
            {
                _component.MakeStep();
            };

            _timer = new DispatcherTimer(_component.Config.AnimationSpeed, DispatcherPriority.Render, autoStep, Dispatcher);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _component.MakeStep();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }
    }
}
