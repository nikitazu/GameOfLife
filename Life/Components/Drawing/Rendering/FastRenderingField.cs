using Life.Components.Configuration;
using Life.Core;
using System;
using System.Windows.Controls;

namespace Life.Components.Drawing.Rendering
{
    public class FastRenderingField : TranslatingMatrix<IRenderingFieldItem>, IRenderingField
    {
        readonly AppConfig _config;
        readonly Lazy<FastRenderer> _renderer;

        public FastRenderingField(AppConfig config) : base(config.FieldSize)
        {
            _config = config;
            _renderer = new Lazy<FastRenderer>(() => new FastRenderer(_config));
        }

        public IRenderingFieldItem CreateItemAt(int i, int j, Canvas screen)
        {
            if (!_renderer.IsValueCreated)
            {
                screen.Children.Add(_renderer.Value);
            }

            return new FastRenderingFieldItem(i, j, _renderer.Value);
        }
    }
}
