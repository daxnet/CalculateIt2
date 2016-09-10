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
        private const string BasicArithmeticFormulaPattern = @"^{(?<min>\d+)}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors>\d+))?$";

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
            var input = "{30}+-*/|4";
            var generator = new ArithmeticEquationGenerator(input, new AvoidNegativeResultRule(), new DivisibilityEnsuranceRule());
            Calculation calculation = null;
            List<Calculation> calculations = new List<Calculation>();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            for (var i = 0; i < 1000; i++)
            {
                calculation = generator.Generate();
                //calculations.Add(calculation);
            }
            sw.Stop();
            

            //var minusResults = calculations.Where(x => x.Value < 0).ToList();
            //var divByZeroResults = calculations.Where(x =>
            //{
            //    try
            //    {
            //        var y = x.Value;
            //        return false;
            //    }
            //    catch(DivideByZeroException)
            //    {
            //        return true;
            //    }
            //});
            var elapsed = sw.Elapsed;

            //Assert.AreEqual(0, minusResults.Count);
            //Assert.AreEqual(0, divByZeroResults.ToList().Count);
        }
    }
}
