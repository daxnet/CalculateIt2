using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    public interface IRule
    {
        void Apply(Calculation left, Calculation right, IDictionary<string, string> parameters, ref Operator @operator);
    }
}
