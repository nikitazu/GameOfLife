﻿using Life.Components.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components.Drawing
{
    public class Painter
    {
        readonly AppConfig _config;
        readonly RenderingField _rectangles;

        public Painter(
            AppConfig config,
            RenderingField field)
        {
            _config = config;
            _rectangles = field;
        }

        public void Initialize(Canvas canvas)
        {
            canvas.Width = _config.FieldSize * _config.CellSize;
            canvas.Height = canvas.Width;

            InitializeLines(canvas);
            InitializeRectangles(canvas);
        }

        public void Reset()
        {
            _rectangles.ForEach((i, j, value) =>
            {
                value.Visibility = Visibility.Hidden;
            });
        }

        public void ToggleRectangle(int i, int j, bool visible, CellMetadata metadata)
        {
            var rectangle = _rectangles[i, j];
            rectangle.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            if (visible && metadata.Generation > 10)
            {
                rectangle.Fill = Brushes.Red;
            }
        }

        void InitializeRectangles(Canvas screen)
        {
            _rectangles.ForEach((i, j, value) =>
            {
                value = new Rectangle
                {
                    Stroke = Brushes.DarkKhaki,
                    Fill = Brushes.DarkKhaki,
                    Width = _config.CellSize,
                    Height = _config.CellSize,
                    Visibility = Visibility.Hidden
                };

                screen.Children.Add(value);
                Canvas.SetLeft(value, i * _config.CellSize);
                Canvas.SetTop(value, j * _config.CellSize);
                
                _rectangles[i, j] = value;
            });
        }

        void InitializeLines(Canvas screen)
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
