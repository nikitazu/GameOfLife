using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public interface IRules<T>
    {
        CellState EvaluateCellStateFromPopulation(CellState state, int population);

        int EvaluatePopulationForCoordinates(IField<T> field, int i, int j);
    }
}
