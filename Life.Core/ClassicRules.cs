using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class ClassicRules : IRules
    {
        public CellState Evaluate(CellState state, int population)
        {
            return population == 3 || (population == 2 && state == CellState.Live) ? CellState.Live : CellState.Dead;
        }
    }
}
