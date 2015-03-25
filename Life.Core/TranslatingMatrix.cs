using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Core
{
    public class TranslatingMatrix<T>
    {
        readonly List<List<T>> _matrix;
        readonly int _size;

        public TranslatingMatrix(int size)
        {
            _size = size;
            _matrix = new List<List<T>>(size);
            for (int i = 0; i < size; i++)
            {
                var row = new List<T>(size);
                for (int j = 0; j < size; j++)
                {
                    row.Add(default(T));
                }
                _matrix.Add(row);
            }
        }

        public T this[int x, int y]
        {
            get
            {
                return _matrix[x % _size][y % _size];
            }
            set
            {
                _matrix[x % _size][y % _size] = value;
            }
        }
    }
}
