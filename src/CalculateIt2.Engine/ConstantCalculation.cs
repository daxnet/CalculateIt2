using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public class ConstantCalculation : Calculation
    {
        private long value;
        public ConstantCalculation(long value)
        {
            this.value = value;
        }

        public override long Value
        {
            get { return this.value; }
        }

        internal void SetValue(long value)
        {
            this.value = value;
        }

        public override void Accept(IVisitor visitor) => visitor.Visit(this);

        public override string ToString() => this.Value.ToString();

        public override string ToFormattedString(SpacingOption option = SpacingOption.None) => this.Value.ToString();

        public static implicit operator ConstantCalculation(long x) => new ConstantCalculation(x);

        public static implicit operator long (ConstantCalculation c) => c.Value;

    }
}
