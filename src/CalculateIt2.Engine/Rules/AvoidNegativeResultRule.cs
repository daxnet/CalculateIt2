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
                var minValue = Convert.ToInt32(parameters["min"]);
                var maxValue = 0;
                int.TryParse(parameters["max"], out maxValue);

                var leftConstant = left as ConstantCalculation;
                var rightConstant = right as ConstantCalculation;

                if (leftConstant == null &&
                    rightConstant == null)
                {
                    return;
                }

                if (rightConstant != null)
                {
                    rightConstant.SetValue(rnd.Next(Convert.ToInt32(leftValue + 1)));
                }
                else if (leftConstant != null)
                {
                    var upperValue = maxValue == 0 ? minValue : maxValue;
                    leftConstant.SetValue(rnd.Next(Convert.ToInt32(rightValue), upperValue + 1));
                }

            }
        }
    }
}
