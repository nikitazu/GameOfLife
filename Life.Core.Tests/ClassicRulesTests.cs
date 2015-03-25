using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    //Any live cell with fewer than two live neighbours dies, as if caused by under-population.
    //Any live cell with two or three live neighbours lives on to the next generation.
    //Any live cell with more than three live neighbours dies, as if by overcrowding.
    //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

    [TestClass]
    public class ClassicRulesTests
    {
        private IRules<int> _rules;

        [TestInitialize]
        public void Init()
        {
            _rules = new ClassicRules();
        }

        [TestMethod]
        public void TestDeadCellEvaluation()
        {
            AssertRule(CellState.Live, CellState.Dead, 0);
            AssertRule(CellState.Live, CellState.Dead, 1);
            AssertRule(CellState.Live, CellState.Live, 2);
            AssertRule(CellState.Live, CellState.Live, 3);
            AssertRule(CellState.Live, CellState.Dead, 4);
            AssertRule(CellState.Live, CellState.Dead, 5);
            AssertRule(CellState.Live, CellState.Dead, 6);
            AssertRule(CellState.Live, CellState.Dead, 7);
            AssertRule(CellState.Live, CellState.Dead, 8);
        }


        [TestMethod]
        public void TestLiveCellEvaluation()
        {
            AssertRule(CellState.Dead, CellState.Dead, 0);
            AssertRule(CellState.Dead, CellState.Dead, 1);
            AssertRule(CellState.Dead, CellState.Dead, 2);
            AssertRule(CellState.Dead, CellState.Live, 3);
            AssertRule(CellState.Dead, CellState.Dead, 4);
            AssertRule(CellState.Dead, CellState.Dead, 5);
            AssertRule(CellState.Dead, CellState.Dead, 6);
            AssertRule(CellState.Dead, CellState.Dead, 7);
            AssertRule(CellState.Dead, CellState.Dead, 8);
        }

        [TestMethod]
        public void TestEvaluatePopulationForCoordinates()
        {
            IField<int> field = new TranslatingMatrix<int>(new int[6, 6]
            { 
                { 0, 0, 0, 0, 0, 0 }, 
                { 0, 1, 1, 1, 0, 0 }, 
                { 0, 1, 0, 1, 0, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
            });

            Assert.AreEqual(0, _rules.EvaluatePopulationForCoordinates(field, 5, 5));
            Assert.AreEqual(1, _rules.EvaluatePopulationForCoordinates(field, 0, 0));
            Assert.AreEqual(8, _rules.EvaluatePopulationForCoordinates(field, 2, 2));
            Assert.AreEqual(3, _rules.EvaluatePopulationForCoordinates(field, 2, 0));
            Assert.AreEqual(2, _rules.EvaluatePopulationForCoordinates(field, 3, 3));
        }

        private void AssertRule(CellState initial, CellState target, int population)
        {
            Assert.AreEqual(
                target,
                _rules.EvaluateCellStateFromPopulation(initial, population),
                initial + " rule broken for population of " + population);
        }
    }
}
