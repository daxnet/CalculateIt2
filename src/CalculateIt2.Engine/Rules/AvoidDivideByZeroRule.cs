using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    public sealed class AvoidDivideByZeroRule : IRule
    {
        public void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator)
        {
            if (@operator == Operator.Div &&
                right.Value == 0)
            {
                // This means that the calculation at the left hand side is going to
                // be divided by zero, which is not allowed in the arithmetic calculation.
                // Simply get the workaround by changing the operator to another one other
                // than division.
                var acceptableOperators = parameters["operator"];
                if (right is ConstantCalculation)
                {
                    
                }
                @operator = OperatorUtils.GenerateRandomOperator(acceptableOperators, Operator.Div);
            }
        }
    }
}
