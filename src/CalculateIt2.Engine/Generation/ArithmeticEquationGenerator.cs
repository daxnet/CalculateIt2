using CalculateIt2.Engine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public sealed class ArithmeticEquationGenerator : EquationGenerator
    {
        private int max;
        private string acceptableOperators;
        private int numOfFactors_min;
        private int numOfFactors_max;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);


        public ArithmeticEquationGenerator(string formation, params IRule[] rules) 
            : base(formation, rules)
        {
        }

        protected override string FormationPattern => @"^{(?<max>\d+)}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors_min>\d+)(-(?<factors_max>\d+))?)?$";

        protected override bool ValidateParameters(IDictionary<string, string> parameters)
        {
            max = Convert.ToInt32(parameters["max"]);
            if (max <= 0)
            {
                errorMessages.Add("Proposed maximum value should be larger than zero.");
            }

            acceptableOperators = parameters["operator"];
            if (!int.TryParse(parameters["factors_min"], out numOfFactors_min))
            {
                numOfFactors_min = 2;
            }

            if (!int.TryParse(parameters["factors_max"], out numOfFactors_max))
            {
                numOfFactors_max = 0;
            }

            if (numOfFactors_max != 0 && numOfFactors_min > numOfFactors_max)
            {
                errorMessages.Add("Maximum number of factors should be larger than or equal to the minimum value.");
            }

            if (string.IsNullOrEmpty(acceptableOperators))
            {
                errorMessages.Add("No acceptable operator has been specified.");
            }

            return errorMessages.Count == 0;
        }

        public override Calculation Generate()
        {
            if (!this.IsValid)
            {
                throw new InvalidOperationException("Cannot generate the equation: the given equation generation formation is not valid, please see ErrorMessages property for details.");
            }
            Calculation result = null;
            var numOfFactors = numOfFactors_min;
            if (numOfFactors_max != 0 )
            {
                numOfFactors = rnd.Next(numOfFactors_min, numOfFactors_max + 1);
            }

            for (var idx = 0; idx < numOfFactors; idx++)
            {
                long factor = rnd.Next(max + 1);
                var @operator = Utils.GenerateRandomOperator(this.acceptableOperators);
                Calculation left = result, right = new ConstantCalculation(factor);

                if (@operator == Operator.Add || @operator == Operator.Mul)
                {
                    var seed = rnd.Next(DateTime.Now.Millisecond);
                    if ((seed % 2) == 0)
                    {
                        left = new ConstantCalculation(factor);
                        right = result;
                    }
                }

                if (this.rules != null)
                {
                    foreach (var rule in rules)
                    {
                        rule.Apply(left, right, Parameters, ref @operator);
                    }
                }

                result = Calculation.Merge(left, right, @operator);
            }
            return result;
        }
    }
}
