
namespace Life.Core
{
    public class Game<T>
    {
        readonly IRules<T> _rules;

        public Game(IRules<T> rules)
        {
            _rules = rules;
        }

        public void Step(IField<T> currentState, IField<T> nextState)
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
