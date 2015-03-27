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
        readonly AutoStepper _autostepper;

        IField<CellState> _currentState;
        IField<CellState> _nextState;

        public AppConfig Config { get; private set; }

        public GameComponent(
            AppConfig config,
            Painter painter,
            Game<CellState> gameOfLife,
            AutoStepper autostepper,
            IField<Rectangle> rectangles,
            IField<CellState> currentState)
        {
            Config = config;

            _painter = painter;
            _game = gameOfLife;
            _autostepper = autostepper;

            _currentState = currentState;
            _nextState = currentState.Copy();

            _currentState.ForEach((i, j, value) =>
            {
                _currentState[i, j] = _random.Next(10) == 0 ? CellState.Live : CellState.Dead;
            });
        }

        internal void Initialize(Canvas canvas)
        {
            _painter.Initialize(canvas);
            _autostepper.Initialize(MakeStep, canvas.Dispatcher);
        }

        internal void MakeStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) => _painter.ToggleRectangle(i, j, value == CellState.Live));
            SwapState();
        }

        internal void ToggleAutoStep()
        {
            _autostepper.Toggle();
        }

        void SwapState()
        {
            var temp = _currentState;
            _currentState = _nextState;
            _nextState = temp;
        }
    }
}
