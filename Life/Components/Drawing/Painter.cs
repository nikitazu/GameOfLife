﻿using Life.Components.Configuration;
using System.Collections.Generic;
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
        readonly List<Brush> _brushes;

        public Painter(
            AppConfig config,
            RenderingField field)
        {
            _config = config;
            _rectangles = field;

            _brushes = new List<Brush>();
            for (int i = 1; i <= 10; i++)
            {
                byte b = (byte)(i * 20);
                byte r = (byte)(255 - b);
                _brushes.Add(new SolidColorBrush(Color.FromRgb(r, 150, b)));
            }
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
            _rectangles.ForEach((i, j, value) =>
            {
                value.Visibility = Visibility.Hidden;
            });
        }

        public void ToggleRectangle(int i, int j, bool visible, CellMetadata metadata)
        {
            var rectangle = _rectangles[i, j];
            rectangle.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            if (_config.ColorCodeGenerations && visible)
            {
                rectangle.Fill =
                    metadata.Generation < _brushes.Count 
                    ? _brushes[metadata.Generation] 
                    : _brushes[_brushes.Count - 1];
            }
        }

        void InitializeRectangles(Canvas screen)
        {
            _rectangles.ForEach((i, j, value) =>
            {
                value = new Rectangle
                {
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
