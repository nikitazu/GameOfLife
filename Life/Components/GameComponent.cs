using Life.Components.Configuration;
using Life.Components.Drawing;
using Life.Core;
using Life.Core.Mathematics;
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
        readonly ICalculator _calc;

        IField<CellState> _currentState;
        IField<CellState> _nextState;
        IField<CellMetadata> _metadata;

        public AppConfig Config { get; private set; }

        public GameComponent(
            AppConfig config,
            Painter painter,
            Game<CellState> gameOfLife,
            AutoStepper autostepper,
            ICalculator calc,
            IField<Rectangle> rectangles,
            IField<CellState> currentState,
            IField<CellMetadata> metadata)
        {
            Config = config;

            _painter = painter;
            _game = gameOfLife;
            _autostepper = autostepper;
            _calc = calc;

            _currentState = currentState;
            _nextState = currentState.Copy();
            _metadata = metadata;

            RandomizeData();
        }

        internal void Initialize(Canvas canvas)
        {
            _painter.Initialize(canvas);
            _autostepper.Initialize(MakeStep, canvas.Dispatcher);
        }

        internal void MakeStep()
        {
            _game.Step(_currentState, _nextState);
            _nextState.ForEach((i, j, value) =>
            {
                var metadata = _metadata[i, j];

                if (_currentState[i, j] == CellState.Dead && value == CellState.Live)
                {
                    metadata.Generation += 1;
                }

                _painter.ToggleRectangle(i, j, value == CellState.Live, metadata);
            });
            SwapState();
        }

        internal void ToggleAutoStep()
        {
            _autostepper.Toggle();
        }

        internal void Reset()
        {
            RandomizeData();
            _painter.Reset();
        }

        void RandomizeData()
        {
            int randomMax = _calc.DencityToRandomMaximum(Config.Dencity);
            _currentState.ForEach((i, j, value) => _currentState[i, j] = _random.Next(randomMax) == 0 ? CellState.Live : CellState.Dead);
        }

        void SwapState()
        {
            var temp = _currentState;
            _currentState = _nextState;
            _nextState = temp;
        }
    }
}
