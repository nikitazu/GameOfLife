using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public interface IField<T>
    {
        T this[int x, int y] { get; set; }
        void ForEach(Action<int, int, T> handleCell);
        void ForEachAround(int i, int j, Action<int, int, T> handleCell);
    }
}
