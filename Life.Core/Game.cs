using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class Game
    {
        readonly IRules _rules;

        public Game(IRules rules)
        {
            _rules = rules;
        }

        public void Step(TranslatingMatrix<int> currentState, TranslatingMatrix<int> nextState)
        {
            currentState.ForEach((colId, rowId, currentValue) =>
            {
                int population = 0;
                currentState.ForEachAround(colId, rowId, (i, j, nearValue) => population += nearValue);

                var currentCellState = currentValue == 0 ? CellState.Dead : CellState.Live;
                var nextCellState = _rules.Evaluate(currentCellState, population);
                var nextValue = nextCellState == CellState.Dead ? 0 : 1;
                nextState[colId, rowId] = nextValue;
            });
        }
    }
}
