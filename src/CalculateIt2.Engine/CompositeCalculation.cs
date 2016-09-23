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
    public class CompositeCalculation : Calculation
    {
        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeCalculation"/> class.
        /// </summary>
        /// <param name="left">The left hand side of the calculation.</param>
        /// <param name="right">The right hand side of the calculation.</param>
        /// <param name="operator">The arithmetic operator to be used.</param>
        public CompositeCalculation(Calculation left, Calculation right, Operator @operator)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = @operator;
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the left hand side of the calculation.
        /// </summary>
        /// <value>
        /// The left part of the calculation.
        /// </value>
        public Calculation Left { get; }
        /// <summary>
        /// Gets the right hand side of the calculation.
        /// </summary>
        /// <value>
        /// The right part of the calculation.
        /// </value>
        public Calculation Right { get; }
        /// <summary>
        /// Gets the arithmetic operator.
        /// </summary>
        /// <value>
        /// The arithmetic operator.
        /// </value>
        public Operator Operator { get; }
        /// <summary>
        /// Gets the value of the current calculation.
        /// </summary>
        /// <value>
        /// The value of the calculation.
        /// </value>
        public override long Value
        {
            get
            {
                switch(Operator)
                {
                    case Operator.Add:
                        return Left.Value + Right.Value;
                    case Operator.Sub:
                        return Left.Value - Right.Value;
                    case Operator.Mul:
                        return Left.Value * Right.Value;
                    case Operator.Div:
                        return Left.Value / Right.Value;
                    default:
                        return long.MinValue;
                }
            }
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor instance to be accepted.</param>
        public override void Accept(IVisitor visitor)
        {
            Left.Accept(visitor);
            visitor.Visit(this);
            Right.Accept(visitor);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string operatorSign = null;
            switch(Operator)
            {
                case Operator.Add:
                    operatorSign = "+";
                    break;
                case Operator.Sub:
                    operatorSign = "-";
                    break;
                case Operator.Mul:
                    operatorSign = "*";
                    break;
                case Operator.Div:
                    operatorSign = "/";
                    break;
            }
            if (Left is CompositeCalculation && 
                !(Right is CompositeCalculation) && 
                OperatorPrecedence((Left as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"({Left}){operatorSign}{Right}";
            }

            if (!(Left is CompositeCalculation) &&
                Right is CompositeCalculation &&
                OperatorPrecedence((Right as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"{Left}{operatorSign}({Right})";
            }

            if (Left is CompositeCalculation && Right is CompositeCalculation)
            {
                return $"({Left}){operatorSign}({Right})";
            }

            return $"{Left}{operatorSign}{Right}";
        }

        /// <summary>
        /// Returns a more human-readable string that represents the current calculation instance.
        /// </summary>
        /// <param name="option">The spacing option.</param>
        /// <returns>
        /// A string with better readability.
        /// </returns>
        /// <seealso cref="SpacingOption" />
        public override string ToFormattedString(SpacingOption option = SpacingOption.None)
        {
            string operatorSign = null;
            string spacing = "";
            switch (option)
            {
                case SpacingOption.Thin:
                    spacing = " ";
                    break;
                case SpacingOption.Thick:
                    spacing = "   ";
                    break;
            }
            switch (Operator)
            {
                case Operator.Add:
                    operatorSign = $"{spacing}+{spacing}";
                    break;
                case Operator.Sub:
                    operatorSign = $"{spacing}-{spacing}";
                    break;
                case Operator.Mul:
                    operatorSign = $"{spacing}×{spacing}";
                    break;
                case Operator.Div:
                    operatorSign = $"{spacing}÷{spacing}";
                    break;
            }
            if (Left is CompositeCalculation &&
                !(Right is CompositeCalculation) &&
                OperatorPrecedence((Left as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"({Left.ToFormattedString(option)}){operatorSign}{Right.ToFormattedString(option)}";
            }

            if (!(Left is CompositeCalculation) &&
                Right is CompositeCalculation &&
                OperatorPrecedence((Right as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"{Left.ToFormattedString(option)}{operatorSign}({Right.ToFormattedString(option)})";
            }

            if (Left is CompositeCalculation && Right is CompositeCalculation)
            {
                return $"({Left.ToFormattedString(option)}){operatorSign}({Right.ToFormattedString(option)})";
            }

            return $"{Left.ToFormattedString(option)}{operatorSign}{Right.ToFormattedString(option)}";
        }
        #endregion

        #region Private Methods
        private static int OperatorPrecedence(Operator op)
        {
            return op == Operator.Add || op == Operator.Sub ? 1 : 2;
        }
        #endregion
    }
}
