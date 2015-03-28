using Life.Components.Configuration;
using Life.Components.Drawing.Rendering;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components.Drawing
{
    public class Painter
    {
        readonly AppConfig _config;
        readonly IRenderingField _renderingField;
        readonly BrushesList _brushes;

        public Painter(
            AppConfig config,
            IRenderingField renderingField,
            BrushesList brushes)
        {
            _config = config;
            _renderingField = renderingField;
            _brushes = brushes;
        }

        public void Initialize(Canvas canvas)
        {
            canvas.Width = _config.FieldSize * _config.CellSize;
            canvas.Height = canvas.Width;

            if (_config.ShowGridLines)
            {
                InitializeGridLines(canvas);
            }
            InitializeRectangles(canvas);
        }

        public void Reset()
        {
            _renderingField.ForEach((i, j, value) =>
            {
                value.Hide();
            });
        }

        public void ToggleRectangle(int i, int j, bool visible, CellMetadata metadata)
        {
            var renderingItem = _renderingField[i, j];
            renderingItem.Toggle(visible);
            if (_config.ColorCodeGenerations && visible)
            {
                var brush = _brushes.GetBrush(metadata.Generation);

                renderingItem.FillWith(brush);
            }
        }

        void InitializeRectangles(Canvas screen)
        {
            _renderingField.Initialize(screen);
        }

        void InitializeGridLines(Canvas screen)
        {
            for (int i = 0; i <= _config.FieldSize; i++)
            {
                var index = i * _config.CellSize;
                screen.Children.Add(VerticalLine(index));
                screen.Children.Add(HorizontalLine(index));
            }
        }

        Line VerticalLine(int columnIndex)
        {
            return CreateLine(columnIndex, columnIndex, 0, _config.CellSize * _config.FieldSize);
        }

        Line HorizontalLine(int rowIndex)
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
