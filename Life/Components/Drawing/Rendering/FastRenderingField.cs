using Life.Components.Configuration;
using Life.Core;
using System;
using System.Windows.Controls;

namespace Life.Components.Drawing.Rendering
{
    public class FastRenderingField : TranslatingMatrix<IRenderingFieldItem>, IRenderingField
    {
        readonly AppConfig _config;
        readonly FastRenderer _renderer;

        public FastRenderingField(AppConfig config) : base(config.FieldSize)
        {
            _config = config;
            _renderer = new FastRenderer(_config);
        }

        public void Initialize(Canvas screen)
        {
            screen.Children.Add(_renderer);

            ForEach((i, j, value) =>
                this[i, j] = new FastRenderingFieldItem(i, j, _renderer));
        }
    }
}
