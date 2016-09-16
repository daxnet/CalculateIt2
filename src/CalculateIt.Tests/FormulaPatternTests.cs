using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using CalculateIt2.Engine.Generation;
using CalculateIt2.Engine;
using System.Text;
using CalculateIt2.Engine.Rules;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalculateIt.Tests
{
    [TestClass]
    public class FormulaPatternTests
    {
        private const string BasicArithmeticFormulaPattern = @"^{(?<min>\d+)}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors_min>\d+)(-(?<factors_max>\d+))?)?$";

        [TestMethod]
        public void BasicArithmeticFormulaPatternSingleLimitTest()
        {
            var input = "{20}+-*|2-3";
            var regex = new Regex(BasicArithmeticFormulaPattern);
            var match = regex.Match(input);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void BasicArithmeticFormulaPatternScopeLimitTest()
        {
            var input = "{20}+-";
            var regex = new Regex(BasicArithmeticFormulaPattern);
            var match = regex.Match(input);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void BasicArithmeticFomulaSingleLimitTest()
        {
            var input = "{20}+-*|10";
            var generator = new ArithmeticEquationGenerator(input);
        }

        [TestMethod]
        public void Test()
        {
            var input = "{25}+-*/|3";
            var generator = new ArithmeticEquationGenerator(input, new AvoidNegativeResultRule(), new DivisibilityEnsuranceRule());
            Calculation calculation = null;
            List<Calculation> calculations = new List<Calculation>();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            for (var i = 0; i < 100000; i++)
            {
                calculation = generator.Generate();
            }
            sw.Stop();
            
            var elapsed = sw.Elapsed;

        }
    }
}
