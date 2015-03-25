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
            for (int i = 0; i < _size; i++)
            {
                var row = new List<T>(_size);
                for (int j = 0; j < _size; j++)
                {
                    row.Add(default(T));
                }
                _matrix.Add(row);
            }
        }

        public TranslatingMatrix(T[,] array)
        {
            _size = array.GetLength(0);
            _matrix = new List<List<T>>(_size);
            for (int i = 0; i < _size; i++)
            {
                var row = new List<T>(_size);
                for (int j = 0; j < _size; j++)
                {
                    row.Add(array[i, j]);
                }
                _matrix.Add(row);
            }
        }

        public T this[int x, int y]
        {
            get
            {
                return _matrix[Translate(x)][Translate(y)];
            }
            set
            {
                _matrix[Translate(x)][Translate(y)] = value;
            }
        }

        private int Translate(int coordinate)
        {
            return coordinate > 0 ? coordinate % _size : Math.Abs(_size + coordinate) % _size;
        }

        public void ForEach(Action<int, int, T> proc)
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    proc(i, j, this[i, j]);
                }
            }
        }

        public void ForEachAround(int i, int j, Action<int, int, T> proc)
        {
            proc(i - 1, j - 1, this[i - 1, j - 1]);
            proc(i - 1, j + 1, this[i - 1, j + 1]);
            proc(i - 1, j, this[i - 1, j]);

            proc(i + 1, j - 1, this[i + 1, j - 1]);
            proc(i + 1, j + 1, this[i + 1, j + 1]);
            proc(i + 1, j, this[i + 1, j]);

            proc(i, j - 1, this[i, j - 1]);
            proc(i, j + 1, this[i, j + 1]);
        }
    }
}
