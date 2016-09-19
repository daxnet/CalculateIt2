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
using System.Collections.Generic;
using System.Linq;

namespace CalculateIt2.Engine
{
    /// <summary>
    /// Represents the utility class.
    /// </summary>
    internal static class Utils
    {
        private static readonly Dictionary<Operator, string> OperatorSigns = new Dictionary<Operator, string>
        {
            { Operator.Add, "+"},
            { Operator.Sub, "-"},
            { Operator.Mul, "*"},
            { Operator.Div, "/"}
        };

        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Generates the calculation operator randomly.
        /// </summary>
        /// <param name="acceptableOperators">The acceptable operators that can be considered into the operator generating.</param>
        /// <param name="bypass">The operator that should be excluded from the generating operators.</param>
        /// <returns>A randomly generated operator.</returns>
        public static Operator GenerateRandomOperator(string acceptableOperators, Operator bypass = Operator.None)
        {
            // If the proposed bypassing operator is the only one that is allowed to be
            // returned, then return it.
            if (bypass != Operator.None &&
                acceptableOperators.Length == 1 &&
                acceptableOperators.Contains(OperatorSigns[bypass]))
            {
                return bypass;
            }

            while (true)
            {
                var idx = rnd.Next(4);
                var kvp = OperatorSigns.ElementAt(idx);
                if (acceptableOperators.Contains(kvp.Value) && kvp.Key != bypass)
                {
                    return kvp.Key;
                }
            }
        }
    }
}
