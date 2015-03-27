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

        App CurrentApp
        {
            get { return (App)Application.Current; }
        }

        public MainWindow()
        {
            InitializeComponent();

            _component = CurrentApp.GameComponent;
            _component.Initialize(Screen);
        }

        void StepButtonClick(object sender, RoutedEventArgs e)
        {
            _component.MakeStep();
        }

        void AutoButtonClick(object sender, RoutedEventArgs e)
        {
            _component.ToggleAutoStep();
        }
    }
}
