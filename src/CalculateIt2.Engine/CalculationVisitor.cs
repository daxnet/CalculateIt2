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
    /// Represents the visitor that can visit each node of a calculation.
    /// </summary>
    /// <seealso cref="CalculateIt2.Engine.IVisitor" />
    public abstract class CalculationVisitor : IVisitor
    {
        #region Public Methods        
        /// <summary>
        /// Visits the given object as an acceptor.
        /// </summary>
        /// <param name="acceptor">The object being visited.</param>
        public void Visit(IVisitorAcceptor acceptor)
        {
            if (acceptor is ConstantCalculation)
            {
                this.VisitConstantCalculation(acceptor as ConstantCalculation);
            }
            if (acceptor is CompositeCalculation)
            {
                this.VisitCompositeCalculation(acceptor as CompositeCalculation);
            }
        }
        #endregion

        #region Protected Methods        
        /// <summary>
        /// Visits the constant calculation.
        /// </summary>
        /// <param name="constantCalculation">The constant calculation.</param>
        protected virtual void VisitConstantCalculation(ConstantCalculation constantCalculation) { }

        /// <summary>
        /// Visits the composite calculation.
        /// </summary>
        /// <param name="compositeCalculation">The composite calculation.</param>
        protected virtual void VisitCompositeCalculation(CompositeCalculation compositeCalculation) { }
        #endregion
    }
}
