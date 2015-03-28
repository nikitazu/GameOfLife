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

        public AppConfig()
        {
            FieldSize = 80 * 1;
            CellSize = 10 / 1;
            AnimationSpeed = TimeSpan.FromSeconds(.1);
            Dencity = .2;
            ColorCodeGenerations = true;
            ShowGridLines = true;
        }
    }
}
