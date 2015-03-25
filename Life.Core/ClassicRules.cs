using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class ClassicRules : IRules<int>
    {
        public CellState EvaluateCellStateFromPopulation(CellState state, int population)
        {
            return population == 3 || (population == 2 && state == CellState.Live) ? CellState.Live : CellState.Dead;
        }

        public int EvaluatePopulationForCoordinates(IField<int> field, int i, int j)
        {
            int population = 0;
            field.ForEachAround(i, j, (x, y, nearValue) => population += nearValue);
            return population;
        }
    }
}
