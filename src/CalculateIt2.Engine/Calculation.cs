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

namespace CalculateIt2.Engine
{
    /// <summary>
    /// Represents an arithmetic calculation.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.IVisitorAcceptor" />
    public abstract class Calculation : IVisitorAcceptor
    {
        #region Public Properties        
        /// <summary>
        /// Gets the value of the current calculation.
        /// </summary>
        /// <value>
        /// The value of the calculation.
        /// </value>
        public abstract long Value { get; }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor instance to be accepted.</param>
        public abstract void Accept(IVisitor visitor);

        /// <summary>
        /// Returns a more human-readable string that represents the current calculation instance.
        /// </summary>
        /// <param name="option">The spacing option.</param>
        /// <returns>A string with better readability.</returns>
        /// <seealso cref="SpacingOption"/>
        public abstract string ToFormattedString(SpacingOption option = SpacingOption.None);

        /// <summary>
        /// Merges the specified calculations with a given <see cref="Operator"/>
        /// </summary>
        /// <param name="left">The left side of the calculation.</param>
        /// <param name="right">The right side of the calculation.</param>
        /// <param name="operator">The operator to merge the two operations.</param>
        /// <returns>The merged operation.</returns>
        /// <exception cref="System.ArgumentNullException">Both left and right calculations are empty.</exception>
        public static Calculation Merge(Calculation left, Calculation right, Operator @operator)
        {
            if (left == null && right == null)
            {
                throw new ArgumentNullException("Both left and right calculations are empty.");
            }
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }

            return new CompositeCalculation(left, right, @operator);
        }
        #endregion
    }
}
