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

using System;
using System.Collections.Generic;

namespace CalculateIt2.Engine.Rules
{
    /// <summary>
    /// Represents that when the operator is <see cref="Operator.Div"/>, the equation should ensure that
    /// the value of the right side calculation should not be zero, so that the division by zero would never occur.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.Rules.IRule" />
    internal sealed class AvoidDivideByZeroRule : IRule
    {
        #region Private Fields
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Public Methods        
        /// <summary>
        /// Applies the current rule to the calculation, by fixing the values on either
        /// left or right hand side of the calculation. The operator will also has the chance
        /// to be changed when applying the rule.
        /// </summary>
        /// <param name="left">The left hand side of the calculation.</param>
        /// <param name="right">The right hand side of the calculation.</param>
        /// <param name="parameters">The calculation generation parameters that are extracted from the equation formation.</param>
        /// <param name="operator">The operator to join the two calculations.</param>
        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (right == null)
            {
                return;
            }

            if (@operator == Operator.Div &&
                right.Value == 0)
            {
                var max = Convert.ToInt32(parameters["max"]);

                var counter = new ConstantCalculationCounter();
                right.Accept(counter);

                var adjustment = new RandomizedCalculationValueAdjustment(1, max, counter.NumOfConstantCalculations, x => x == 0);
                while (right.Value == 0)
                {
                    adjustment.Reset();
                    right.Accept(adjustment);
                }
            }
        }
        #endregion
    }
}
