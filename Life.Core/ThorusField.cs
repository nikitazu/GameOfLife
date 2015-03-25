using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class ThorusField : TranslatingMatrix<CellState>, IField<CellState>
    {
        public ThorusField(int size) : base(size)
        {
            // empty
        }

        public ThorusField(CellState[,] cells) : base(cells)
        {
            // empty
        }

        public ThorusField(int[,] array) : base(array.GetLength(0))
        {
            this.ForEach((i, j, _) =>
            {
                this[i, j] = array[i, j] > 0 ? CellState.Live : CellState.Dead;
            });
        }
    }
}
