using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    //Any live cell with fewer than two live neighbours dies, as if caused by under-population.
    //Any live cell with two or three live neighbours lives on to the next generation.
    //Any live cell with more than three live neighbours dies, as if by overcrowding.
    //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

    [TestClass]
    public class GameTests
    {
        private Game _game;

        [TestInitialize]
        public void Init()
        {
            _game = new Game(new ClassicRules());
        }

        [TestMethod]
        public void TestStep()
        {
            var state0 = new TranslatingMatrix<int>(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 0, 1, 0, 0 }, 
                { 0, 1, 1, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
            });

            var state1 = new TranslatingMatrix<int>(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 1, 1, 1, 0 }, 
                { 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 },
            });

            var actualState1 = new TranslatingMatrix<int>(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 0, 0, 0, 0 }, 
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
            });

            _game.Step(state0, actualState1);
            AssertGameState(state1, actualState1);
        }

        void AssertGameState(TranslatingMatrix<int> expectedState, TranslatingMatrix<int> actualState)
        {
            expectedState.ForEach((i, j, expectedValue) =>
            {
                Assert.AreEqual(expectedValue, actualState[i, j], "States differ at index {0}:{1}", i, j);
            });
        }
    }
}
