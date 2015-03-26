using Life.Components.Configuration;
using Life.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life.Components
{
    public class GameComponent
    {
        readonly Random _random = new Random(123);

        public AppConfig Config { get; private set; }
        public Game<CellState> GameOfLife { get; private set; }
        public IField<CellState> CurrentState { get; private set; }
        public IField<CellState> NextState { get; private set; }
        public IField<Rectangle> Rectangles { get; private set; }

        public GameComponent(
            AppConfig config,
            Game<CellState> gameOfLife,
            IField<CellState> currentState,
            IField<Rectangle> rectangles)
        {
            Config = config;
            GameOfLife = gameOfLife;
            CurrentState = currentState;
            NextState = currentState.Copy();
            Rectangles = rectangles;
        }

        internal void AutoStep(Action<int, int, CellState> afterStepCallback)
        {
            GameOfLife.Step(CurrentState, NextState);
            NextState.ForEach(afterStepCallback);
            SwapState();
        }

        internal void SwapState()
        {
            var temp = CurrentState;
            CurrentState = NextState;
            NextState = temp;
        }

        internal void PutLinesOn(Canvas Screen)
        {
            for (int i = 0; i < Config.FieldSize; i++)
            {
                var index = i * Config.CellSize;
                Screen.Children.Add(VerticalLine(index));
                Screen.Children.Add(HorizontalLine(index));
            }
        }

        internal void PutRectanglesOn(System.Windows.Controls.Canvas Screen)
        {
            Rectangles.ForEach((i, j, value) =>
            {
                CurrentState[i, j] = _random.Next(3) == 0 ? CellState.Live : CellState.Dead;

                value = new Rectangle();
                Screen.Children.Add(value);
                Canvas.SetLeft(value, i * Config.CellSize);
                Canvas.SetTop(value, j * Config.CellSize);
                value.Stroke = Brushes.DarkKhaki;
                value.Fill = Brushes.DarkKhaki;
                value.Width = Config.CellSize;
                value.Height = Config.CellSize;
                value.Visibility = Visibility.Hidden;

                Rectangles[i, j] = value;
            });
        }

        Line VerticalLine(int columnIndex)
        {
            return CreateLine(columnIndex, columnIndex, 0, Config.CellSize * Config.FieldSize);
        }

        Line HorizontalLine(int rowIndex)
        {
            return CreateLine(0, Config.CellSize * Config.FieldSize, rowIndex, rowIndex);
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
