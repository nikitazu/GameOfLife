using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    [TestClass]
    public class TranslatingMatrixTests : BaseFieldTests<int>
    {
        protected override int GetEmptyValue()
        {
            return 0;
        }

        protected override int GetSolidValue()
        {
            return 1;
        }

        protected override IField<int> GetSolidField()
        {
            return new TranslatingMatrix<int>(new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });
        }

        protected override IField<int> GetFieldFromMatrix5x5()
        {
            return new TranslatingMatrix<int>(Matrix5x5);
        }

        [TestInitialize]
        public void Init()
        {
            Field = new TranslatingMatrix<int>(Size);
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
    }
}
