using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public class ConstantCalculation : Calculation
    {
        public ConstantCalculation(long value)
        {
            this.Value = value;
        }

        public override long Value { get; }

        public override string ToString() => this.Value.ToString();

        public static implicit operator ConstantCalculation(long x) => new ConstantCalculation(x);

        public static implicit operator long (ConstantCalculation c) => c.Value;

    }
}
