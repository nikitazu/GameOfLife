using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    [TestClass]
    public class ThrorusFieldTests : BaseFieldTests<CellState>
    {
        protected override CellState GetEmptyValue()
        {
            return CellState.Dead;
        }

        protected override CellState GetSolidValue()
        {
            return CellState.Live;
        }

        protected override IField<CellState> GetSolidField()
        {
            return new ThorusField(new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });
        }

        protected override IField<CellState> GetFieldFromMatrix5x5()
        {
            return new ThorusField(Matrix5x5);
        }

        [TestInitialize]
        public void Init()
        {
            Field = new ThorusField(Size);
        }
        
        [TestMethod]
        public void TestForEach()
        {
            var matrixFromArray = new ThorusField(new int[2, 2] { { 1, 0 }, { 1, 0 } });
            var values = new List<CellState>();
            matrixFromArray.ForEach((i, j, value) => values.Add(value));

            Assert.AreEqual(CellState.Live, values[0]);
            Assert.AreEqual(CellState.Dead, values[1]);
            Assert.AreEqual(CellState.Live, values[2]);
            Assert.AreEqual(CellState.Dead, values[3]);
        }
    }
}
