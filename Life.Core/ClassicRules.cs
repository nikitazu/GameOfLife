
namespace Life.Core
{
    /// <summary>
    /// Any live cell with fewer than two live neighbours dies, as if caused by under-population.
    /// Any live cell with two or three live neighbours lives on to the next generation.
    /// Any live cell with more than three live neighbours dies, as if by overcrowding.
    /// Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
    /// </summary>
    public class ClassicRules : IRules<CellState>
    {
        public CellState EvaluateCellStateFromPopulation(CellState state, int population)
        {
            return population == 3 || (population == 2 && state == CellState.Live) ? CellState.Live : CellState.Dead;
        }

        public int EvaluatePopulationForCoordinates(IField<CellState> field, int i, int j)
        {
            int population = 0;
            field.ForEachAround(i, j, (x, y, nearValue) => population += nearValue == CellState.Live ? 1 : 0);
            return population;
        }
    }
}
