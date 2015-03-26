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

        public void AutoStep(Action<int, int, CellState> afterStepCallback)
        {
            GameOfLife.Step(CurrentState, NextState);
            NextState.ForEach(afterStepCallback);
            SwapState();
        }

        public void SwapState()
        {
            var temp = CurrentState;
            CurrentState = NextState;
            NextState = temp;
        }

        public void PutRectanglesOn(System.Windows.Controls.Canvas Screen)
        {
            Rectangles.ForEach((i, j, value) =>
            {
                CurrentState[i, j] = _random.Next(3) == 0 ? CellState.Live : CellState.Dead;

                value = new Rectangle();
                Screen.Children.Add(value);
                Canvas.SetLeft(value, i * 10);
                Canvas.SetTop(value, j * 10);
                value.Stroke = Brushes.DarkKhaki;
                value.Fill = Brushes.DarkKhaki;
                value.Width = 10;
                value.Height = 10;
                value.Visibility = Visibility.Hidden;

                Rectangles[i, j] = value;
            });
        }
    }
}
