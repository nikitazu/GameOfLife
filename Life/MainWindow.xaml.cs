using Life.Components;
using System.Windows;

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly GameComponent _component;

        public MainWindow()
        {
            InitializeComponent();

            _component = (Application.Current as App).GameComponent;
            _component.Initialize(Screen);
        }

        private void StepButtonClick(object sender, RoutedEventArgs e)
        {
            _component.MakeStep();
        }

        private void AutoButtonClick(object sender, RoutedEventArgs e)
        {
            _component.ToggleAutoStep();
        }
    }
}
