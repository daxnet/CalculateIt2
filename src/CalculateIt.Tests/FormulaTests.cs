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
        public void MultipleCompositeCalculationFormulaTest()
        {
            // (1+(2+3))*5
            var calculation = new CompositeCalculation(new CompositeCalculation(1.ToCalculation(), 
                    new CompositeCalculation(2.ToCalculation(), 3.ToCalculation(), Operator.Add), 
                    Operator.Add), 
                5.ToCalculation(), 
                Operator.Mul);
            Assert.AreEqual("(1+2+3)*5", calculation.ToString());
        }
    }
}
