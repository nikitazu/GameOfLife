using Life.Components;
using System;
using System.Windows;

namespace Life
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public GameComponent GameComponent { get; private set; }

        public App()
        {
            Console.WriteLine("app initialize");
            GameComponent = AppBootstrap.InitializeGameComponent();
            Console.WriteLine("app initialize done");
        }
    }
}
