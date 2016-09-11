using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    /// <summary>
    /// Represents that the implemented classes are the equation generation rules
    /// that are responsible for fixing the calculations in a equation for particular
    /// purposes.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Applies the current rule to the calculation, by fixing the values on either
        /// left or right hand side of the calculation. The operator will also has the chance
        /// to be changed when applying the rule.
        /// </summary>
        /// <param name="left">The left hand side of the calculation.</param>
        /// <param name="right"></param>
        /// <param name="parameters"></param>
        /// <param name="operator"></param>
        void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator);
    }
}
