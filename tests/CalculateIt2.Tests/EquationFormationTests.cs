// ==================================================================================================
//
//   _____           _                  _           _            _____   _     ___  
//  / ____|         | |                | |         | |          |_   _| | |   |__ \ 
// | |        __ _  | |   ___   _   _  | |   __ _  | |_    ___    | |   | |_     ) |
// | |       / _` | | |  / __| | | | | | |  / _` | | __|  / _ \   | |   | __|   / / 
// | |____  | (_| | | | | (__  | |_| | | | | (_| | | |_  |  __/  _| |_  | |_   / /_ 
//  \_____|  \__,_| |_|  \___|  \__,_| |_|  \__,_|  \__|  \___| |_____|  \__| |____|
//                                                                                  
// An Arithmetic Equation Generator with Question Generation Capacity
// Copyright © 2016 by daxnet (Sunny Chen)
// https://github.com/daxnet/CalculateIt2
//
// MIT License
// 
// Copyright(c) 2016 Sunny Chen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ==================================================================================================   

using CalculateIt2.Engine.Generation;
using Xunit;

namespace CalculateIt2.Tests
{
    public class EquationFormationTests
    {
        [Fact(DisplayName = "TestTest")]
        public void TestTest()
        {
            Assert.True(1 == 1);
        }

        [Fact(DisplayName = "NoOperatorSpecifiedTest")]
        public void NoOperatorSpecifiedTest()
        {
            var formation = "{20}";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.False(generator.IsValid);
        }

        [Fact(DisplayName = "OnlyAddOperatorTest")]
        public void OnlyAddOperatorTest()
        {
            var formation = "{20}+";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "OnlySubOperatorTest")]
        public void OnlySubOperatorTest()
        {
            var formation = "{20}-";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "OnlyMulOperatorTest")]
        public void OnlyMulOperatorTest()
        {
            var formation = "{20}*";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "OnlyDivOperatorTest")]
        public void OnlyDivOperatorTest()
        {
            var formation = "{20}/";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "CombinedOperator1Test")]
        public void CombinedOperator1Test()
        {
            var formation = "{20}+-";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "CombinedOperator2Test")]
        public void CombinedOperator2Test()
        {
            var formation = "{20}*/";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "CombinedOperator3Test")]
        public void CombinedOperator3Test()
        {
            var formation = "{20}+-*/";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "DuplicatedOperatorTest")]
        public void DuplicatedOperatorTest()
        {
            var formation = "{20}++";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.False(generator.IsValid);
        }

        [Fact(DisplayName = "ExactFactorNumberTest")]
        public void ExactFactorNumberTest()
        {
            var formation = "{20}+|2";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "RangeFactorNumberTest")]
        public void RangeFactorNumberTest()
        {
            var formation = "{20}+|2-3";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.True(generator.IsValid);
        }

        [Fact(DisplayName = "InvalidFactorNumberRangeTest")]
        public void InvalidFactorNumberRangeTest()
        {
            var formation = "{20}+|3-2";
            var generator = new ArithmeticEquationGenerator(formation);
            Assert.False(generator.IsValid);
        }
    }
}
