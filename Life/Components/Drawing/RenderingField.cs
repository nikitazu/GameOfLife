using Life.Components.Configuration;
using Life.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components.Drawing
{
    public class RenderingField : TranslatingMatrix<IRenderingFieldItem>, IRenderingField
    {
        readonly AppConfig _config;

        public RenderingField(AppConfig config) : base(config.FieldSize)
        {
            _config = config;
        }

        public IRenderingFieldItem CreateItemAt(int i, int j, Canvas screen)
        {
            var rectangle = new Rectangle
            {
                Fill = Brushes.DarkKhaki,
                Width = _config.CellSize,
                Height = _config.CellSize,
                Visibility = Visibility.Hidden
            };

            screen.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, i * _config.CellSize);
            Canvas.SetTop(rectangle, j * _config.CellSize);

            return new RenderingFieldItem(rectangle);
        }
    }

    public class RenderingFieldItem : IRenderingFieldItem
    {
        readonly Rectangle _rectangle;

        public RenderingFieldItem(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }

        public void Hide()
        {
            _rectangle.Visibility = Visibility.Hidden;
        }

        public void Toggle(bool visible)
        {
            _rectangle.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        public void FillWith(Brush brush)
        {
            _rectangle.Fill = brush;
        }
    }
}
