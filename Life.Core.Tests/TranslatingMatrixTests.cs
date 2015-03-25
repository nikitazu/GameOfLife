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
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Assert.AreEqual(0, _matrix[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSetter()
        {
            _matrix[2, 2] = 1;
            Assert.AreEqual(1, _matrix[2, 2]);
        }

        [TestMethod]
        public void TestTranslateIndex()
        {
            _matrix[3, 3] = 1;
            Assert.AreEqual(1, _matrix[0, 0]);
            Assert.AreEqual(1, _matrix[3, 3]);
        }
    }
}
