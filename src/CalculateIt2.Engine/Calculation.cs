using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public abstract class Calculation : IVisitorAcceptor
    {
        public abstract long Value { get; }

        public static Calculation Merge(Calculation left, Calculation right, Operator @operator)
        {
            if (left == null && right == null)
            {
                throw new ArgumentNullException("Both left and right calculations are empty.");
            }
            if (left == null)
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }

            return new CompositeCalculation(left, right, @operator);
        }

        public abstract void Accept(IVisitor visitor);
    }
}
