
using System;

namespace Life.Core.Mathematics
{
    public class Calculator : ICalculator
    {
        public int DencityToRandomMaximum(double dencity)
        {
            return Convert.ToInt32(Math.Round(1.0d / dencity, 0));
        }
    }
}
