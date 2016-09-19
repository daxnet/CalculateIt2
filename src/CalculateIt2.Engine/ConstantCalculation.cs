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


namespace CalculateIt2.Engine
{
    /// <summary>
    /// Represents a constant calculation that has a given <see cref="System.Int64"/> object as its value.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.Calculation" />
    public sealed class ConstantCalculation : Calculation
    {
        #region Private Fields
        private long value;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantCalculation"/> class.
        /// </summary>
        /// <param name="value">The value to be used for initializing this constant calculation.</param>
        public ConstantCalculation(long value)
        {
            this.value = value;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the value of the current calculation.
        /// </summary>
        /// <value>
        /// The value of the calculation.
        /// </value>
        public override long Value
        {
            get { return this.value; }
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor instance to be accepted.</param>
        public override void Accept(IVisitor visitor) => visitor.Visit(this);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => this.Value.ToString();

        /// <summary>
        /// Returns a more human-readable string that represents the current calculation instance.
        /// </summary>
        /// <param name="option">The spacing option.</param>
        /// <returns>A string with better readability.</returns>
        /// <seealso cref="SpacingOption"/>
        public override string ToFormattedString(SpacingOption option = SpacingOption.None) => this.Value.ToString();

        /// <summary>
        /// Operator override for implicitly converts the given <see cref="System.Int64"/> value into a constant calculation.
        /// </summary>
        /// <param name="x">The <see cref="System.Int64"/> value to be converted.</param>
        public static implicit operator ConstantCalculation(long x) => new ConstantCalculation(x);

        /// <summary>
        /// Operator override for implicitly converts the given <see cref="ConstantCalculation"/> into a <see cref="System.Int64"/> value.
        /// </summary>
        /// <param name="c">The <see cref="ConstantCalculation"/> to be converted.</param>
        public static implicit operator long (ConstantCalculation c) => c.Value;
        #endregion

        #region Internal Methods
        /// <summary>
        /// Explicitly sets the value of current <c>ConstantCalculation</c> instance.
        /// </summary>
        /// <param name="value">The value to be set to this instance.</param>
        internal void SetValue(long value)
        {
            this.value = value;
        }
        #endregion

    }
}
