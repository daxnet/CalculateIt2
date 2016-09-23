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
using System.Text.RegularExpressions;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the question generator that generates the cloze questions.
    /// </summary>
    /// <remarks>
    /// Cloze questions are the questions that leaves a place holder in an arithmetic equation, requiring
    /// the student to fill in a number so that the equation can balance. For example, given a calculation:
    /// 
    /// 2 + ( ) = 5
    /// 
    /// The student should fill 3 into the parenthesis.
    /// 
    /// The <c>CloseQuestionGenerator</c> will generate the string '2 + ( ) = 5' as the formula, and number 3 is also
    /// provided in the generated result.
    /// </remarks>
    /// <seealso cref="CalculateIt2.Engine.Generation.QuestionGenerator{System.Int64}" />
    public sealed class ClozeQuestionGenerator : QuestionGenerator<long>
    {
        #region Private Fields
        private const string DigitalPattern = @"\d+";
        private readonly Regex regex = new Regex(DigitalPattern);
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ClozeQuestionGenerator"/> class.
        /// </summary>
        /// <param name="placeHolder">The place holder of the question where the students should put the answer in.</param>
        /// <param name="spacingOption">The <see cref="SpacingOption"/> value which indicates the spacing options of the generated question.</param>
        public ClozeQuestionGenerator(string placeHolder = "（ ）", SpacingOption spacingOption = SpacingOption.Thin) : base(placeHolder, spacingOption)
        {
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Generates the arithmetic question based on the given calculation.
        /// </summary>
        /// <param name="calculation">The calculation equation from which the question is generated.</param>
        /// <returns>A <see cref="QuestionGenerationResult{TAnswer}"/> instance which contains the question formular and the answer.</returns>
        public override QuestionGenerationResult<long> Generate(Calculation calculation)
        {
            var calculationString = calculation.ToFormattedString(this.SpacingOption);
            var digitalMatches = regex.Matches(calculationString);
            var matchesArray = new Match[digitalMatches.Count];
            digitalMatches.CopyTo(matchesArray, 0);
            var idx = rnd.Next(matchesArray.Length);
            var selectedIndex = matchesArray[idx].Index;
            var selectedValue = Convert.ToInt64(matchesArray[idx].Value);

            var formula = $@"{regex.Replace(calculationString, match =>
            {
                if (match.Index == selectedIndex)
                {
                    return PlaceHolder;
                }
                return match.Value;
            })} = {calculation.Value}";

            return new QuestionGenerationResult<long>(formula, selectedValue);
        }
        #endregion
    }
}
