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
    /// Represents the base class of question generators.
    /// </summary>
    public abstract class QuestionGenerator<T>
    {
        #region Private Fields
        private readonly string placeHolder;
        private readonly SpacingOption spacingOption;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionGenerator{T}"/> class.
        /// </summary>
        /// <param name="placeHolder">The place holder of the question where the students should put the answer in.</param>
        /// <param name="spacingOption">The <see cref="SpacingOption"/> value which indicates the spacing options of the generated question.</param>
        protected QuestionGenerator(string placeHolder, SpacingOption spacingOption)
        {
            this.placeHolder = placeHolder;
            this.spacingOption = spacingOption;
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Generates the arithmetic question based on the given calculation.
        /// </summary>
        /// <param name="calculation">The calculation equation from which the question is generated.</param>
        /// <returns>A <see cref="QuestionGenerationResult{TAnswer}"/> instance which contains the question formular and the answer.</returns>
        public abstract QuestionGenerationResult<T> Generate(Calculation calculation);
        #endregion

        #region Protected Properties        
        /// <summary>
        /// Gets the place holder of the question where the students should put the answer in.
        /// </summary>
        /// <value>
        /// The place holder of the question where the students should put the answer in.
        /// </value>
        protected string PlaceHolder => placeHolder;

        /// <summary>
        /// Gets the <see cref="SpacingOption"/> value which indicates the spacing options of the generated question.
        /// </summary>
        /// <value>
        /// The <see cref="SpacingOption"/> value which indicates the spacing options of the generated question.
        /// </value>
        protected SpacingOption SpacingOption => spacingOption;
        #endregion

        
    }
}
