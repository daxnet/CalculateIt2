using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public class ConstantCalculation : Calculation
    {
        public ConstantCalculation(int value)
        {
            this.Value = value;
        }

        public override int Value { get; }

        public override string ToString() => this.Value.ToString();
    }
}
