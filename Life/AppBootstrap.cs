using Life.Components;
using Life.Components.Configuration;
using Life.Components.Drawing.Rendering;
using Life.Core;
using Life.Core.Mathematics;
using Microsoft.Practices.Unity;

namespace Life
{
    static class AppBootstrap
    {
        internal static GameComponent InitializeGameComponent()
        {
            var container = new UnityContainer()
                .RegisterInstance<AppConfig>(new AppConfig())
                .RegisterType<ICalculator, Calculator>()
                .RegisterType<IField<CellState>, GameField>()
                .RegisterType<IField<CellMetadata>, MetadataField>()
                .RegisterType<IRenderingField, FastRenderingField>()
                .RegisterType<Game<CellState>, ClassicGame>();

            return container.Resolve<GameComponent>();

        }
    }
}
