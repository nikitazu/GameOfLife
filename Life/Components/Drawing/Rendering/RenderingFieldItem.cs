using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components.Drawing.Rendering
{
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
