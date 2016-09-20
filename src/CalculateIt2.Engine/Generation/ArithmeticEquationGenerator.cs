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

using CalculateIt2.Engine.Rules;
using System;
using System.Collections.Generic;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the arithmetic equation generator.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.Generation.EquationGenerator" />
    public sealed class ArithmeticEquationGenerator : EquationGenerator
    {
        #region Private Fields
        private int max;
        private string acceptableOperators;
        private int numOfFactors_min;
        private int numOfFactors_max;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ArithmeticEquationGenerator"/> class.
        /// </summary>
        /// <param name="formation">The formation of the equation that is going to be generated.</param>
        /// <param name="rules">A list of <see cref="Rules.IRule" /> instances that is registered with current equation generator.</param>
        public ArithmeticEquationGenerator(string formation, params IRule[] rules) 
            : base(formation, rules)
        {
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Generates the calculation based on the given formation.
        /// </summary>
        /// <returns>
        /// The <see cref="Calculation" /> being generated.
        /// </returns>
        /// <exception cref="InvalidOperationException">Cannot generate the equation: the given equation generation formation is not valid, please see ErrorMessages property for details.</exception>
        public override Calculation Generate()
        {
            if (!this.IsValid)
            {
                throw new InvalidOperationException("Cannot generate the equation: the given equation generation formation is not valid, please see ErrorMessages property for details.");
            }
            Calculation result = null;
            var numOfFactors = numOfFactors_min;
            if (numOfFactors_max != 0)
            {
                numOfFactors = rnd.Next(numOfFactors_min, numOfFactors_max + 1);
            }

            for (var idx = 0; idx < numOfFactors; idx++)
            {
                long factor = rnd.Next(max + 1);
                var @operator = Utils.GenerateRandomOperator(this.acceptableOperators);
                Calculation left = result, right = new ConstantCalculation(factor);

                if (@operator == Operator.Add || @operator == Operator.Mul)
                {
                    var seed = rnd.Next(DateTime.Now.Millisecond);
                    if ((seed % 2) == 0)
                    {
                        left = new ConstantCalculation(factor);
                        right = result;
                    }
                }

                if (this.rules != null)
                {
                    foreach (var rule in rules)
                    {
                        rule.Apply(left, right, Parameters, ref @operator);
                    }
                }

                result = Calculation.Merge(left, right, @operator);
            }
            return result;
        }
        #endregion

        #region Protected Properties        
        /// <summary>
        /// Gets a <see cref="System.String" /> value which represents the regular expression pattern of the formation.
        /// </summary>
        /// <value>
        /// The formation pattern.
        /// </value>
        protected override string FormationPattern => @"^{(?<max>\d+)}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors_min>\d+)(-(?<factors_max>\d+))?)?$";
        #endregion

        #region Protected Methods        
        /// <summary>
        /// Validates the parameters that are extracted from the given formation.
        /// </summary>
        /// <param name="parameters">The parameters to be validated.</param>
        /// <returns>
        ///   <c>true</c> if all the parameters are valid, otherwise, <c>false</c>.
        /// </returns>
        protected override bool ValidateParameters(IDictionary<string, string> parameters)
        {
            max = Convert.ToInt32(parameters["max"]);
            if (max <= 0)
            {
                errorMessages.Add("Proposed maximum value should be larger than zero.");
            }

            acceptableOperators = parameters["operator"];
            if (!int.TryParse(parameters["factors_min"], out numOfFactors_min))
            {
                numOfFactors_min = 2;
            }

            if (!int.TryParse(parameters["factors_max"], out numOfFactors_max))
            {
                numOfFactors_max = 0;
            }

            if (numOfFactors_max != 0 && numOfFactors_min > numOfFactors_max)
            {
                errorMessages.Add("Maximum number of factors should be larger than or equal to the minimum value.");
            }

            if (string.IsNullOrEmpty(acceptableOperators))
            {
                errorMessages.Add("No acceptable operator has been specified.");
            }

            return errorMessages.Count == 0;
        }
        #endregion

    }
}
