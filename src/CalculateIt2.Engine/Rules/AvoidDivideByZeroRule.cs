using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    internal sealed class AvoidDivideByZeroRule : IRule
    {
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (right == null)
            {
                return;
            }

            if (@operator == Operator.Div &&
                right.Value == 0)
            {
                var max = Convert.ToInt32(parameters["max"]);

                var counter = new ConstantCalculationCounter();
                right.Accept(counter);

                var adjustment = new RandomizedCalculationValueAdjustment(1, max, counter.NumOfConstantCalculations, x => x == 0);
                while (right.Value == 0)
                {
                    adjustment.Reset();
                    right.Accept(adjustment);
                }
            }
        }
    }
}
