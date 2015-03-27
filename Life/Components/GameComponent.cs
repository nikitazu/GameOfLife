using Life.Components.Configuration;
using Life.Components.Drawing;
using Life.Core;
using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Life.Components
{
    public class GameComponent
    {
        readonly Random _random = new Random(123);
        readonly Painter _painter;
        readonly Game<CellState> _game;

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

            _currentState = currentState;
            _nextState = currentState.Copy();

            _currentState.ForEach((i, j, value) =>
            {
                _currentState[i, j] = _random.Next(3) == 0 ? CellState.Live : CellState.Dead;
            });
        }

        internal void AutoStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) => _painter.ToggleRectangle(i, j, value == CellState.Live));
            SwapState();
        }

        internal void ManualStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) => _painter.ToggleRectangle(i, j, value == CellState.Live));
            SwapState();
        }

        internal void PutGraphicOn(Canvas canvas)
        {
            _painter.Initialize(canvas);
        }

        void SwapState()
        {
            var temp = _currentState;
            _currentState = _nextState;
            _nextState = temp;
        }
    }
}
