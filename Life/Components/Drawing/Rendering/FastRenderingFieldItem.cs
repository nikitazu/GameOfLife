using System.Windows.Media;

namespace Life.Components.Drawing.Rendering
{
    public class FastRenderingFieldItem : IRenderingFieldItem
    {
        readonly FastRenderer _renderer;
        readonly int _i;
        readonly int _j;

        public FastRenderingFieldItem(int i, int j, FastRenderer renderer)
        {
            _i = i;
            _j = j;
            _renderer = renderer;
        }

        public void Hide()
        {
            Toggle(false);
        }

        public void Toggle(bool visible)
        {
            _renderer.Toggle(_i, _j, visible);
        }

        public void FillWith(Brush brush)
        {
            _renderer.FillWith(_i, _j, brush);
        }
    }
}
