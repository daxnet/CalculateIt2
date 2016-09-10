using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    public sealed class AvoidNegativeResultRule : IRule
    {
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (left == null || right == null)
                return;

            var leftValue = left.Value;
            var rightValue = right.Value;

            if (@operator == Operator.Sub &&
                leftValue < rightValue)
            {
                var max = Convert.ToInt32(parameters["max"]);

                var leftConstant = left as ConstantCalculation;
                var rightConstant = right as ConstantCalculation;

                if (leftConstant == null &&
                    rightConstant == null)
                {
                    return;
                }

                if (rightConstant != null)
                {
                    rightConstant.SetValue(rnd.Next(Convert.ToInt32(leftValue) + 1));
                }
                else if (leftConstant != null)
                {
                    leftConstant.SetValue(rnd.Next(Convert.ToInt32(rightValue), max + 1));
                }

            }
        }
    }
}
