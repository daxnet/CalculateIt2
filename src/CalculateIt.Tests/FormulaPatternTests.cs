using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using CalculateIt2.Engine.Generation;

namespace CalculateIt.Tests
{
    [TestClass]
    public class FormulaPatternTests
    {
        private const string BasicArithmeticFormulaPattern = @"^{(?<min>\d+)(~(?<max>\d+))?}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors>\d+))?$";

        [TestMethod]
        public void BasicArithmeticFormulaPatternSingleLimitTest()
        {
            var input = "{20}+-*|2";
            var regex = new Regex(BasicArithmeticFormulaPattern);
            var match = regex.Match(input);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void BasicArithmeticFormulaPatternScopeLimitTest()
        {
            var input = "{20~30}+-";
            var regex = new Regex(BasicArithmeticFormulaPattern);
            var match = regex.Match(input);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void BasicArithmeticFomulaSingleLimitTest()
        {
            var input = "{20}+-*|10";
            var generator = new ArithmeticFormulaGenerator(input);

        }

        [TestMethod]
        public void Test()
        {
            var input = "{20}+-";
            var generator = new ArithmeticFormulaGenerator(input);
            var calculation = generator.Generate();
        }
    }
}
