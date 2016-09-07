using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    [BuiltIn]
    internal sealed class AvoidDivideByZeroRule : IRule
    {
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (@operator == Operator.Div &&
                right.Value == 0)
            {
                var minValue = Convert.ToInt32(parameters["min"]);
                var maxValue = 0;
                int.TryParse(parameters["max"], out maxValue);

                long toValue = 0;
                
                //Ensure that the value for adjustment is not zero.
                while (toValue == 0)
                {
                    if (maxValue == 0)
                    {
                        toValue = rnd.Next(minValue + 1);
                    }
                    else
                    {
                        toValue = rnd.Next(minValue, maxValue + 1);
                    }
                }

                var counter = new ConstantCalculationCounter();
                right.Accept(counter);

                var adjustment = new RandomizedCalculationValueAdjustment(toValue, counter.NumOfConstantCalculations);
                while (right.Value == 0)
                {
                    right.Accept(adjustment);
                }
            }
        }
    }
}
