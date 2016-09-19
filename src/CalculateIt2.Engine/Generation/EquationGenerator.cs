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

using System.Linq;
using CalculateIt2.Engine.Rules;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the base class for the equation generators.
    /// </summary>
    public abstract class EquationGenerator
    {
        #region Fields
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

        /// <summary>
        /// The local list for storing the error message texts.
        /// </summary>
        protected readonly List<string> errorMessages = new List<string>();

        /// <summary>
        /// The local list which contains all the registered rules.
        /// </summary>
        protected readonly List<IRule> rules = new List<IRule>();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>EquationGenerator</c> class.
        /// </summary>
        /// <param name="formation">The formation of the equation that is going to be generated.</param>
        /// <param name="rules">A list of <see cref="Rules.IRule"/> instances that is registered with current equation generator.</param>
        public EquationGenerator(string formation, params IRule[] rules)
        {
            // Adds the AvoidDivideByZeroRule, as it is a mandatory for a arithmetic calculation.
            this.rules.Add(new AvoidDivideByZeroRule());

            // Load additional rules and register to the current generator instance.
            if (rules != null)
            {
                foreach(var rule in rules)
                {
                    if (this.rules.Any(r => r.GetType() == rule.GetType())) continue;
                    this.rules.Add(rule);
                }
            }

            this.Formation = formation;
            errorMessages.Clear();

            // Firstly use the regular expression to validate the given formation.
            var regex = new Regex(this.FormationPattern);
            var match = regex.Match(this.Formation);
            IsValid = match.Success;
            if (IsValid)
            {
                // If the given formation can pass the regular expression matching, the matching groups
                // will be extracted from the formation, and be stored into the local dictionary as parameters.
                foreach (var groupName in regex.GetGroupNames())
                {
                    parameters.Add(groupName, match.Groups[groupName].Value);
                }

                // Passing the parameters to the ValidateParamters method for additional validation. For example,
                // check if the parameters are valid.
                IsValid = ValidateParameters(parameters);
            }
            else
            {
                // If the given formation cannot pass the regular expression matching, an error
                // message will be added to the local list.
                errorMessages.Add("The formation passed in cannot match the required pattern criteria.");
            }
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the current <c>EquationGenerator</c>
        /// is in a valid state.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current <c>EquationGenerator</c> is in a valid state; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the formation of the equations being generated.
        /// </summary>
        /// <value>
        /// The formation of the equations being generated.
        /// </value>
        public string Formation { get; }

        /// <summary>
        /// Gets the error messages being generated when doing the generator validation.
        /// </summary>
        /// <value>
        /// The error messages.
        /// </value>
        public IEnumerable<string> ErrorMessages => errorMessages;
        #endregion

        #region Protected Properties        
        /// <summary>
        /// Gets a <see cref="System.String"/> value which represents the regular expression pattern of the formation.
        /// </summary>
        /// <value>
        /// The formation pattern.
        /// </value>
        protected abstract string FormationPattern { get; }

        /// <summary>
        /// Gets the parameters that are extracted from the given formation.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        protected IDictionary<string, string> Parameters => parameters;
        #endregion

        #region Public Methods        
        /// <summary>
        /// Generates the calculation based on the given formation.
        /// </summary>
        /// <returns>The <see cref="Calculation"/> being generated.</returns>
        public abstract Calculation Generate();
        #endregion

        #region Protected Methods        
        /// <summary>
        /// Validates the parameters that are extracted from the given formation.
        /// </summary>
        /// <param name="parameters">The parameters to be validated.</param>
        /// <returns><c>true</c> if all the parameters are valid, otherwise, <c>false</c>.</returns>
        protected virtual bool ValidateParameters(IDictionary<string, string> parameters) => true;
        #endregion
    }
}
