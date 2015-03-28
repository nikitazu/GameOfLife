using Life.Components.Configuration;
using System.Collections.Generic;
using System.Windows.Media;

namespace Life.Components.Drawing
{
    public class BrushesList
    {
        readonly List<Brush> _brushes;
        readonly AppConfig _config;

        public BrushesList(AppConfig config)
        {
            _config = config;
            _brushes = new List<Brush>(_config.BrushesCount);

            for (int i = 1; i <= _config.BrushesCount; i++)
            {
                byte b = (byte)(i * _config.BrushColorStep);
                byte r = (byte)(255 - b);
                var brush = new SolidColorBrush(Color.FromRgb(r, 150, b));
                brush.Freeze();
                _brushes.Add(brush);
            }
        }

        public Brush GetBrush(int index)
        {
            return index < _config.BrushesCount ? _brushes[index] : _brushes[_config.BrushesCount - 1];
        }
    }
}
