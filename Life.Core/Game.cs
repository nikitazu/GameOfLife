using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class Game
    {
        readonly IRules<int> _rules;

        public Game(IRules<int> rules)
        {
            _rules = rules;
        }

        public void Step(IField<int> currentState, IField<int> nextState)
        {
            currentState.ForEach((colId, rowId, currentValue) =>
            {
                int population = _rules.EvaluatePopulationForCoordinates(currentState, colId, rowId);
                var currentCellState = currentValue == 0 ? CellState.Dead : CellState.Live;
                var nextCellState = _rules.EvaluateCellStateFromPopulation(currentCellState, population);
                var nextValue = nextCellState == CellState.Dead ? 0 : 1;
                nextState[colId, rowId] = nextValue;
            });
        }
    }
}
