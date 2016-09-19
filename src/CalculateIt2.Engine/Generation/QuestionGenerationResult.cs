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
    /// Represents the result of a question generation.
    /// </summary>
    /// <typeparam name="TAnswer">The type of the answer to the generated question.</typeparam>
    public sealed class QuestionGenerationResult<TAnswer>
    {
        #region Private Fields
        private readonly string formula;
        private readonly TAnswer answer;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionGenerationResult{TAnswer}"/> class.
        /// </summary>
        /// <param name="formula">The <see cref="System.String"/> presentation of the formula that the question generator has generated.</param>
        /// <param name="answer">The answer to the generated question.</param>
        internal QuestionGenerationResult(string formula, TAnswer answer)
        {
            this.formula = formula;
            this.answer = answer;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the <see cref="System.String"/> presentation of the formula that the question generator has generated.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/> presentation of the formula that the question generator has generated.
        /// </value>
        public string Formula => formula;

        /// <summary>
        /// Gets the answer to the generated question.
        /// </summary>
        /// <value>
        /// The answer to the generated question.
        /// </value>
        public TAnswer Answer => answer;
        #endregion
    }
}
