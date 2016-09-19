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

namespace CalculateIt2.Engine.Rules
{
    /// <summary>
    /// Represents the class that can modify the constant calculations
    /// in a given composite calculation instance.
    /// </summary>
    internal sealed class RandomizedCalculationValueAdjustment : CalculationVisitor
    {
        #region Private Fields
        private readonly int min;
        private readonly int max;
        private readonly int totalNumberOfConstantCalculations;
        private readonly Func<int, bool> exclusionExpectation;
        private int currentIdx = 0;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        private int hitIndex;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomizedCalculationValueAdjustment"/> class.
        /// </summary>
        /// <param name="min">The minimum of the value that can be generated and being used by the adjustment.</param>
        /// <param name="max">The maximum of the value that can be generated and being used by the adjustment.</param>
        /// <param name="totalNumberOfConstantCalculations">The total number of constant calculations.</param>
        /// <param name="exclusionExpectation">The exclusion expectation.</param>
        public RandomizedCalculationValueAdjustment(int min, int max, int totalNumberOfConstantCalculations, Func<int, bool> exclusionExpectation = null)
        {
            this.min = min;
            this.max = max;
            this.exclusionExpectation = exclusionExpectation;
            this.totalNumberOfConstantCalculations = totalNumberOfConstantCalculations;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Resets the counters of the calculation adjustment.
        /// </summary>
        internal void Reset()
        {
            currentIdx = 0;
            hitIndex = rnd.Next(totalNumberOfConstantCalculations);
        }
        #endregion

        #region Protected Methods        
        /// <summary>
        /// Visits the constant calculation.
        /// </summary>
        /// <param name="constantCalculation">The constant calculation.</param>
        protected override void VisitConstantCalculation(ConstantCalculation constantCalculation)
        {
            if (currentIdx == hitIndex)
            {
                constantCalculation.SetValue(GetValue());
            }
            currentIdx++;
        }
        #endregion

        #region Private Methods
        private int GetValue()
        {
            int value;
            if (exclusionExpectation != null)
            {
                do
                {
                    if (max == 0)
                    {
                        value = rnd.Next(0, min + 1);
                    }
                    else
                    {
                        value = rnd.Next(min, max + 1);
                    }
                } while (exclusionExpectation(value));
            }
            else
            {
                if (max == 0)
                {
                    value = rnd.Next(0, min + 1);
                }
                else
                {
                    value = rnd.Next(min, max + 1);
                }
            }
            return value;
        }
        #endregion
    }
}
