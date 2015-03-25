using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Life.Core.Tests
{
    public abstract class BaseFieldTests<T>
    {
        protected const int Size = 3;
        protected IField<T> Field;

        protected readonly int[,] Matrix5x5 = new int[5, 5]
        { 
            { 0, 0, 0, 0, 0 }, 
            { 0, 1, 1, 1, 0 }, 
            { 0, 1, 0, 1, 0 },
            { 0, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0 },
        };

        protected abstract T GetEmptyValue();
        protected abstract T GetSolidValue();
        protected abstract IField<T> GetSolidField();
        protected abstract IField<T> GetFieldFromMatrix5x5();


        [TestMethod]
        public void TestDefault()
        {
            AssertFieldAllValues(GetEmptyValue(), Field);
        }

        [TestMethod]
        public void TestSetter()
        {
            Field[2, 2] = GetSolidValue();
            AssertFieldValue(GetSolidValue(), Field, 2, 2);
        }

        [TestMethod]
        public void TestTranslateIndex()
        {
            Field[3, 3] = GetSolidValue();
            AssertFieldValue(GetSolidValue(), Field, 0, 0);
            AssertFieldValue(GetSolidValue(), Field, 3, 3);
        }

        [TestMethod]
        public void TestTranslateIndexNegative()
        {
            Field[-1, -1] = GetSolidValue();
            AssertFieldValue(GetSolidValue(), Field, 2, 2);
            AssertFieldValue(GetSolidValue(), Field, -1, -1);
        }

        [TestMethod]
        public void TestCreateFromArray()
        {
            AssertFieldAllValues(GetSolidValue(), GetSolidField());
        }

        [TestMethod]
        public void TestForEachAround()
        {
            var field5x5 = GetFieldFromMatrix5x5();
            field5x5.ForEachAround(2, 2, (i, j, value) => Assert.AreEqual(GetSolidValue(), value));
        }

        protected void AssertFieldAllValues(T expected, IField<T> field)
        {
            field.ForEach((i, j, value) => AssertFieldValue(expected, field, i, j));
        }

        protected void AssertFieldValue(T expected, IField<T> field, int i, int j)
        {
            Assert.AreEqual(expected, field[i, j], "Values differ at index {0}:{1}", i, j);
        }
    }
}
