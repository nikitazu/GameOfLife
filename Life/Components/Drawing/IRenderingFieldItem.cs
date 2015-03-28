using System.Windows.Media;

namespace Life.Components.Drawing
{
    public interface IRenderingFieldItem
    {
        void Hide();
        void Toggle(bool visible);
        void FillWith(Brush brush);
    }
}
