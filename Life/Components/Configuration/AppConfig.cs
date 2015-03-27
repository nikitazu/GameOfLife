using System;

namespace Life.Components.Configuration
{
    public class AppConfig
    {
        public int FieldSize { get; private set; }
        public int CellSize { get; private set; }
        public TimeSpan AnimationSpeed { get; private set; }

        public AppConfig()
        {
            FieldSize = 80;
            CellSize = 10;
            AnimationSpeed = TimeSpan.FromSeconds(.1);
        }
    }
}
