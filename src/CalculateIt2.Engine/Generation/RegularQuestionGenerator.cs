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

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the regular question generator.
    /// </summary>
    /// <remarks>
    /// This question generator simply generates a regular arithmetic equation question. For example:
    /// 
    /// 2 + 3 = ____
    /// 
    /// The students should fll in 5 in the place holder.
    /// </remarks>
    /// <seealso cref="CalculateIt2.Engine.Generation.QuestionGenerator{System.Int64}" />
    public sealed class RegularQuestionGenerator : QuestionGenerator<long>
    {
        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="RegularQuestionGenerator"/> class.
        /// </summary>
        /// <param name="placeHolder">The place holder of the question where the students should put the answer in.</param>
        /// <param name="spacingOption">The <see cref="P:CalculateIt2.Engine.Generation.QuestionGenerator`1.SpacingOption" /> value which indicates the spacing options of the generated question.</param>
        public RegularQuestionGenerator(string placeHolder = "____", SpacingOption spacingOption = SpacingOption.Thin) 
            : base(placeHolder, spacingOption)
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
            return new QuestionGenerationResult<long>($"{calculation.ToFormattedString(this.SpacingOption)} = {PlaceHolder}", calculation.Value);
        }
        #endregion
    }
}
