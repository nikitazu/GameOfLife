using Life.Components.Configuration;
using Life.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Life.Components.Drawing
{
    public class RenderingFieldFast : TranslatingMatrix<IRenderingFieldItem>, IRenderingField
    {
        readonly AppConfig _config;
        readonly Lazy<MyRenderer> _renderer;

        public RenderingFieldFast(AppConfig config) : base(config.FieldSize)
        {
            _config = config;
            _renderer = new Lazy<MyRenderer>(() => new MyRenderer(_config));
        }

        public IRenderingFieldItem CreateItemAt(int i, int j, Canvas screen)
        {
            if (!_renderer.IsValueCreated)
            {
                screen.Children.Add(_renderer.Value);
            }

            return new RenderingFieldFastItem(i, j, _renderer.Value);
        }
    }

    public class RenderingFieldFastItem : IRenderingFieldItem
    {
        readonly MyRenderer _renderer;
        readonly int _i;
        readonly int _j;

        public RenderingFieldFastItem(int i, int j, MyRenderer renderer)
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

    public class MyRenderer : UIElement
    {
        readonly TranslatingMatrix<MyRenderingBrush> _cells;
        readonly AppConfig _config;

        public MyRenderer(AppConfig config)
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
