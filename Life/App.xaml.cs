using Life.Components;
using Life.Components.Configuration;
using Life.Components.Drawing;
using Life.Core;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Shapes;

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

            var container = new UnityContainer()
                .RegisterInstance<AppConfig>(new AppConfig())
                .RegisterType<IField<CellState>, GameField>()
                .RegisterType<IField<Rectangle>, RenderingField>()
                .RegisterType<Game<CellState>, ClassicGame>();

            GameComponent = container.Resolve<GameComponent>();

            Console.WriteLine("app initialize done");
        }
    }
}
