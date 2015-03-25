using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    [TestClass]
    public class GameTests
    {
        private Game<CellState> _game;

        [TestInitialize]
        public void Init()
        {
            _game = new ClassicGame();
        }

        [TestMethod]
        public void TestStep()
        {
            IField<CellState> state0 = new ThorusField(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 0, 1, 0, 0 }, 
                { 0, 1, 1, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
            });

            IField<CellState> state1 = new ThorusField(new int[5, 5]
            { 
                { 0, 0, 0, 0, 0 }, 
                { 0, 1, 1, 1, 0 }, 
                { 0, 1, 0, 1, 0 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 },
            });

            IField<CellState> actualState1 = new ThorusField(new int[5, 5]
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

        void AssertGameState(IField<CellState> expectedState, IField<CellState> actualState)
        {
            expectedState.ForEach((i, j, expectedValue) =>
            {
                Assert.AreEqual(expectedValue, actualState[i, j], "States differ at index {0}:{1}", i, j);
            });
        }
    }
}
