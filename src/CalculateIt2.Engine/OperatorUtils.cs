using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    internal static class OperatorUtils
    {
        private static readonly Dictionary<Operator, string> OperatorSigns = new Dictionary<Operator, string>
        {
            { Operator.Add, "+"},
            { Operator.Sub, "-"},
            { Operator.Mul, "*"},
            { Operator.Div, "/"}
        };

        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public static Operator GenerateRandomOperator(string acceptableOperators, Operator bypass = Operator.None)
        {
            // TODO: Possible dead loop
            while (true)
            {
                var idx = rnd.Next(4);
                var kvp = OperatorSigns.ElementAt(idx);
                if (acceptableOperators.Contains(kvp.Value) && kvp.Key != bypass)
                {
                    return kvp.Key;
                }
            }
        }
    }
}
