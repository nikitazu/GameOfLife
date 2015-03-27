using Life.Core.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests.Mathematics
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator _calc;

        [TestInitialize]
        public void Init()
        {
            _calc = new Calculator();
        }

        [TestMethod]
        public void TestDencityToRandomMaximum()
        {
            // 1 -> 0 -> 100%
            // 2 -> 0 1 -> 50%
            // 3 -> 0 1 2 -> 30%
            // 4 -> 0 1 2 3 -> 25%
            // 5 -> 0 1 2 3 4 -> 20%
            // 6 -> 0 1 2 3 4 5 -> 15%


            // 1    -> 1
            // 0.5  -> 2
            // 0.3  -> 3
            // 0.25 -> 4
            // 0.2  -> 5
            // 0.15 -> 6

            Assert.AreEqual(1, _calc.DencityToRandomMaximum(1.00));
            Assert.AreEqual(2, _calc.DencityToRandomMaximum(0.50));
            Assert.AreEqual(3, _calc.DencityToRandomMaximum(0.30));
            Assert.AreEqual(4, _calc.DencityToRandomMaximum(0.25));
            Assert.AreEqual(5, _calc.DencityToRandomMaximum(0.20));
            Assert.AreEqual(6, _calc.DencityToRandomMaximum(0.16));
        }
    }
}
