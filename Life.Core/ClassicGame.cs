
namespace Life.Core
{
    public class ClassicGame : Game<CellState>
    {
        public ClassicGame() : base(new ClassicRules())
        {
            // empty
        }
    }
}
