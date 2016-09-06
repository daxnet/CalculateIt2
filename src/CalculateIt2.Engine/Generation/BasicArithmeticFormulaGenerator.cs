using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public class BasicArithmeticFormulaGenerator : FormulaGenerator
    {
        private int minValue;
        private int maxValue = 0;
        private string acceptableOperators;
        private int numOfFactors;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        private static readonly Dictionary<Operator, string> OperatorSigns = new Dictionary<Operator, string>
        {
            { Operator.Add, "+"},
            { Operator.Sub, "-"},
            { Operator.Mul, "*"},
            { Operator.Div, "/"}
        };

        public BasicArithmeticFormulaGenerator(string formation) : base(formation)
        {
        }

        protected override string FormationPattern => @"^{(?<min>\d+)(~(?<max>\d+))?}(?<operator>(\+)?(\-)?(\*)?(\/)?){1}(\|(?<factors>\d+))?$";

        protected override bool ValidateParameters(IDictionary<string, string> parameters)
        {
            minValue = Convert.ToInt32(parameters["min"]);
            acceptableOperators = parameters["operator"];
            if (!int.TryParse(parameters["factors"], out numOfFactors))
            {
                numOfFactors = 2;
            }

            if (int.TryParse(parameters["max"], out maxValue) && minValue > maxValue)
            {
                errorMessages.Add("Proposed minimal value should be less than or equal to the maximum value.");
            }

            if (string.IsNullOrEmpty(acceptableOperators))
            {
                errorMessages.Add("No acceptable operator has been specified.");
            }

            return errorMessages.Count == 0;
        }

        public override Calculation Generate()
        {
            Calculation result = null;
            for (var idx = 0; idx < this.numOfFactors; idx++)
            {
                long factor = 0;
                if (maxValue == 0)
                {
                    factor = rnd.Next(minValue + 1);
                }
                else
                {
                    factor = rnd.Next(minValue, maxValue + 1);
                }
                var @operator = GenerateOperator();
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
                result = Calculation.Merge(left, right, @operator);
            }
            return result;
        }

        private Operator GenerateOperator()
        {
            while (true)
            {
                var idx = rnd.Next(4);
                var kvp = OperatorSigns.ElementAt(idx);
                if (this.acceptableOperators.Contains(kvp.Value))
                {
                    return kvp.Key;
                }
            }
        }
    }
}
