using Life.Core;

namespace Life.Components.Configuration
{
    public class GameField : ThorusField
    {
        public GameField(AppConfig config) : base(config.FieldSize)
        {
            // empty
        }
    }
}
