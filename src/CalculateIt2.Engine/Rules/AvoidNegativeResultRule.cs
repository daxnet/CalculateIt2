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
    /// Represents that when the operator is <see cref="Operator.Sub"/>, the equation should ensure that
    /// the value of the left calculation is larger than or equal to the value of the right calculation,
    /// so that the generated equation will never has a negative value.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.Rules.IRule" />
    public sealed class AvoidNegativeResultRule : IRule
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
            if (left == null || right == null)
                return;

            var leftValue = left.Value;
            var rightValue = right.Value;

            if (@operator == Operator.Sub &&
                leftValue < rightValue)
            {
                var max = Convert.ToInt32(parameters["max"]);

                var leftConstant = left as ConstantCalculation;
                var rightConstant = right as ConstantCalculation;

                if (leftConstant == null &&
                    rightConstant == null)
                {
                    return;
                }

                if (rightConstant != null)
                {
                    rightConstant.SetValue(rnd.Next(Convert.ToInt32(leftValue) + 1));
                }
                else if (leftConstant != null)
                {
                    leftConstant.SetValue(rnd.Next(Convert.ToInt32(rightValue), max + 1));
                }

            }
        }
        #endregion
    }
}
