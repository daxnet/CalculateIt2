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

            var minValue = Convert.ToInt32(parameters["min"]);
            var maxValue = 0;
            int.TryParse(parameters["max"], out maxValue);

            long toValue = 0;

            do
            {
                if (maxValue == 0)
                {
                    toValue = rnd.Next(minValue + 1);
                }
                else
                {
                    toValue = rnd.Next(minValue, maxValue + 1);
                }
            } while (toValue > leftValue);

            var counter = new ConstantCalculationCounter();
            right.Accept(counter);

            var adjustment = new RandomizedCalculationValueAdjustment(toValue, counter.NumOfConstantCalculations);
            while (right.Value > leftValue)
            {
                right.Accept(adjustment);
            }
        }
    }
}
