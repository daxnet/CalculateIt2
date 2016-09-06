using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculateIt2.Engine;

namespace CalculateIt.Tests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        public void ConstantCalculationFormulaTest()
        {
            var calculation = new ConstantCalculation(6);
            Assert.AreEqual("6", calculation.ToString());
        }

        [TestMethod]
        public void CompositeCalculationFormulaTest()
        {
            var calc5 = new ConstantCalculation(5);
            var calc6 = new ConstantCalculation(6);
            var calculation = new CompositeCalculation(calc5, calc6, Operator.Add);
            Assert.AreEqual("5+6", calculation.ToString());
        }

        [TestMethod]
        public void MultipleCompositeCalculationFormulaTest1()
        {
            // (1+(2+3))*5
            var calculation = new CompositeCalculation(new CompositeCalculation(1L.ToCalculation(), 
                    new CompositeCalculation(2L.ToCalculation(), 3L.ToCalculation(), Operator.Add), 
                    Operator.Add), 
                5L.ToCalculation(), 
                Operator.Mul);
            Assert.AreEqual("(1+2+3)*5", calculation.ToString());
        }

        [TestMethod]
        public void MultipleCompositeCalculationFormulaTest2()
        {
            // (2+3)*(4-5)
            var left = new CompositeCalculation(2L.ToCalculation(), 3L.ToCalculation(), Operator.Add);
            var right = new CompositeCalculation(4L.ToCalculation(), 5L.ToCalculation(), Operator.Sub);
            var calculation = new CompositeCalculation(left, right, Operator.Mul);
            Assert.AreEqual("(2+3)*(4-5)", calculation.ToString());

        }
    }
}
