using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    [TestClass]
    public class TranslatingMatrixTests
    {
        const int _size = 3;
        TranslatingMatrix<int> _matrix;

        [TestInitialize]
        public void Init()
        {
            _matrix = new TranslatingMatrix<int>(_size);
        }

        [TestMethod]
        public void TestDefault()
        {
            AssertMatrixAllValues(0, _matrix);
        }

        [TestMethod]
        public void TestSetter()
        {
            _matrix[2, 2] = 1;
            AssertMatrixValue(1, _matrix, 2, 2);
        }

        [TestMethod]
        public void TestTranslateIndex()
        {
            _matrix[3, 3] = 1;
            AssertMatrixValue(1, _matrix, 0, 0);
            AssertMatrixValue(1, _matrix, 3, 3);
        }

        [TestMethod]
        public void TestTranslateIndexNegative()
        {
            _matrix[-1, -1] = 1;
            AssertMatrixValue(1, _matrix, 2, 2);
            AssertMatrixValue(1, _matrix, -1, -1);
        }

        [TestMethod]
        public void TestCreateFromArray()
        {
            var matrixFromArray = new TranslatingMatrix<int>(new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });
            AssertMatrixAllValues(1, matrixFromArray);
        }

        [TestMethod]
        public void TestForEach()
        {
            var matrixFromArray = new TranslatingMatrix<int>(new int[2, 2] { { 1, 2 }, { 3, 4 } });
            var values = new List<int>();
            matrixFromArray.ForEach((i, j, value) => values.Add(value));

            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(2, values[1]);
            Assert.AreEqual(3, values[2]);
            Assert.AreEqual(4, values[3]);
        }

        [TestMethod]
        public void TestForEachAround()
        {
            var matrixFromArray = new TranslatingMatrix<int>(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 1, 1, 1, 0 }, 
                { 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 },
            });

            matrixFromArray.ForEachAround(2, 2, (i, j, value) => Assert.AreEqual(1, value));
        }

        void AssertMatrixAllValues(int expected, TranslatingMatrix<int> matrix)
        {
            matrix.ForEach((i, j, value) => AssertMatrixValue(expected, matrix, i, j));
        }

        void AssertMatrixValue(int expected, TranslatingMatrix<int> matrix, int i, int j)
        {
            Assert.AreEqual(expected, matrix[i, j], "Values differ at index {0}:{1}", i, j);
        }
    }
}
