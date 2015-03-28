using System;

namespace Life.Components.Configuration
{
    public class AppConfig
    {
        public int FieldSize { get; private set; }
        public int CellSize { get; private set; }
        public TimeSpan AnimationSpeed { get; private set; }
        public double Dencity { get; private set; }
        public bool ColorCodeGenerations { get; private set; }
        public bool ShowGridLines { get; private set; }
        public int BrushColorStep { get; set; }
        public int BrushesCount { get; set; }

        public AppConfig()
        {
            FieldSize = 80 * 5;
            CellSize = 10 / 5;
            
            AnimationSpeed = TimeSpan.FromSeconds(.01);
            Dencity = .2;

            ColorCodeGenerations = true;
            ShowGridLines = false;

            BrushColorStep = 5;
            BrushesCount = 40;
        }
    }
}
