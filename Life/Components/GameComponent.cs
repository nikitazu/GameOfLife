using Life.Components.Configuration;
using Life.Components.Drawing;
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
        readonly Painter _painter;
        readonly Game<CellState> _game;
        readonly IField<Rectangle> _rectangles;

        IField<CellState> _currentState;
        IField<CellState> _nextState;

        public AppConfig Config { get; private set; }

        public GameComponent(
            AppConfig config,
            Painter painter,
            Game<CellState> gameOfLife,
            IField<Rectangle> rectangles,
            IField<CellState> currentState)
        {
            Config = config;

            _painter = painter;
            _game = gameOfLife;
            _rectangles = rectangles;

            _currentState = currentState;
            _nextState = currentState.Copy();
        }

        internal void AutoStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) => ToggleRectangle(i, j, value == CellState.Live));
            SwapState();
        }

        internal void ManualStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) => ToggleRectangle(i, j, value == CellState.Live));
            SwapState();
        }

        internal void PutLinesOn(Canvas screen)
        {
            for (int i = 0; i < Config.FieldSize; i++)
            {
                var index = i * Config.CellSize;
                screen.Children.Add(_painter.VerticalLine(index));
                screen.Children.Add(_painter.HorizontalLine(index));
            }
        }

        internal void PutRectanglesOn(Canvas screen)
        {
            _rectangles.ForEach((i, j, value) =>
            {
                _currentState[i, j] = _random.Next(3) == 0 ? CellState.Live : CellState.Dead;

                value = new Rectangle();
                screen.Children.Add(value);
                Canvas.SetLeft(value, i * Config.CellSize);
                Canvas.SetTop(value, j * Config.CellSize);
                value.Stroke = Brushes.DarkKhaki;
                value.Fill = Brushes.DarkKhaki;
                value.Width = Config.CellSize;
                value.Height = Config.CellSize;
                value.Visibility = Visibility.Hidden;

                _rectangles[i, j] = value;
            });
        }

        void ToggleRectangle(int i, int j, bool visible)
        {
            _rectangles[i, j].Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        void SwapState()
        {
            var temp = _currentState;
            _currentState = _nextState;
            _nextState = temp;
        }
    }
}
