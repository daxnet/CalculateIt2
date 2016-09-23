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

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the question generator that generates the comparison questions.
    /// </summary>
    /// <remarks>
    /// A comparison question is the one that, it will give the student a calculation, as the left
    /// part of the equation, and then propose a value which might be less than, greater than or
    /// equal to the value of the calculation. The student should use &lt; &gt and = signs to make
    /// the equation reasonable.
    /// 
    /// For example:
    /// 
    /// 2 + 3 ○ 6
    /// 
    /// The students should fill &lt; sign in the circle.
    /// </remarks>
    /// <seealso cref="CalculateIt2.Engine.Generation.QuestionGenerator{System.String}" />
    public sealed class ComparisonQuestionGenerator : QuestionGenerator<string>
    {
        #region Private Fields
        private readonly int threshold;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonQuestionGenerator"/> class.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <param name="placeHolder">The place holder.</param>
        /// <param name="spacingOption">The spacing option.</param>
        public ComparisonQuestionGenerator(int threshold, string placeHolder = "\u25CB", SpacingOption spacingOption = SpacingOption.Thin) 
            : base(placeHolder, spacingOption)
        {
            this.threshold = threshold;
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Generates the arithmetic question based on the given calculation.
        /// </summary>
        /// <param name="calculation">The calculation equation from which the question is generated.</param>
        /// <returns>A <see cref="QuestionGenerationResult{TAnswer}"/> instance which contains the question formular and the answer.</returns>
        public override QuestionGenerationResult<string> Generate(Calculation calculation)
        {
            var value = calculation.Value;
            var min = value > threshold ? value - threshold : 0;
            var max = value + threshold + 1;

            var v = rnd.Next(Convert.ToInt32(min), Convert.ToInt32(max));

            return new QuestionGenerationResult<string>($"{calculation.ToFormattedString(this.SpacingOption)} {this.PlaceHolder} {v}",
                value == v ? "=" :
                (
                    value > v ? ">" : "<"
                ));
        }
        #endregion
    }
}
