using Life.Components.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components.Drawing
{
    public class Painter
    {
        AppConfig _config;

        public Painter(AppConfig config)
        {
            _config = config;
        }

        public Line VerticalLine(int columnIndex)
        {
            return CreateLine(columnIndex, columnIndex, 0, _config.CellSize * _config.FieldSize);
        }

        public Line HorizontalLine(int rowIndex)
        {
            return CreateLine(0, _config.CellSize * _config.FieldSize, rowIndex, rowIndex);
        }

        Line CreateLine(double x1, double x2, double y1, double y2)
        {
            return new Line
            {
                Stroke = Brushes.DarkBlue,
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = .2,
            };
        }
    }
}
