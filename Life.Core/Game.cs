
namespace Life.Core
{
    public class Game
    {
        readonly IRules<CellState> _rules;

        public Game(IRules<CellState> rules)
        {
            _rules = rules;
        }

        public void Step(IField<CellState> currentState, IField<CellState> nextState)
        {
            currentState.ForEach((colId, rowId, currentValue) =>
            {
                int population = _rules.EvaluatePopulationForCoordinates(currentState, colId, rowId);
                var nextCellState = _rules.EvaluateCellStateFromPopulation(currentValue, population);
                nextState[colId, rowId] = nextCellState;
            });
        }
    }
}
