using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    public sealed class DivisibilityEnsuranceRule : IRule
    {
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (left == null || right == null)
            {
                return;
            }

            var leftValue = left.Value;
            var rightValue = right.Value;

            if (@operator == Operator.Div &&
                (leftValue % rightValue) != 0)
            {
                var max = Convert.ToInt32(parameters["max"]);

                var leftConstant = left as ConstantCalculation;
                var rightConstant = right as ConstantCalculation;

                if (leftConstant == null &&
                    rightConstant == null)
                {
                    return;
                }

                if (leftConstant != null)
                {
                    var i = 0;
                    do
                    {
                        i++;
                    } while (i * rightValue <= max);
                    var proposedLeftConstantValue = rnd.Next(i) * rightValue;
                    leftConstant.SetValue(proposedLeftConstantValue);
                }
                else if (rightConstant != null)
                {
                    var possibleValues = new List<long>();
                    for (var i = 1; i <= Math.Min(leftValue, max); i++)
                    {
                        if ((leftValue % i) == 0)
                        {
                            possibleValues.Add(i);
                        }
                    }
                    var proposedRightConstantValue = possibleValues[rnd.Next(possibleValues.Count)];
                    rightConstant.SetValue(proposedRightConstantValue);
                }
            }
        }
    }
}
