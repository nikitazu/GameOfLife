using Life.Components.Configuration;
using Life.Core;
using System.Windows;
using System.Windows.Media;

namespace Life.Components.Drawing.Rendering
{
    public class FastRenderer : UIElement
    {
        readonly TranslatingMatrix<MyRenderingBrush> _cells;
        readonly AppConfig _config;

        public FastRenderer(AppConfig config)
        {
            _config = config;
            _cells = new TranslatingMatrix<MyRenderingBrush>(config.FieldSize);

            _cells.ForEach((i, j, visible) =>
            {
                _cells[i, j] = new MyRenderingBrush();
            });
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            _cells.ForEach((i, j, brush) =>
            {
                if (brush.Visible)
                {
                    drawingContext.DrawRectangle(
                        brush.Brush,
                        null,
                        new Rect(i * _config.CellSize, j * _config.CellSize, _config.CellSize, _config.CellSize));
                }
            });
        }

        internal void Toggle(int i, int j, bool visible)
        {
            var cell = _cells[i, j];
            if (cell.Visible != visible)
            {
                cell.Visible = visible;
                InvalidateVisual();
            }
        }

        internal void FillWith(int i, int j, Brush brush)
        {
            _cells[i, j].Brush = brush;
        }

        class MyRenderingBrush
        {
            public Brush Brush { get; set; }
            public bool Visible { get; set; }

            public MyRenderingBrush()
            {
                Brush = Brushes.DarkKhaki;
                Visible = false;
            }
        }
    }
}
