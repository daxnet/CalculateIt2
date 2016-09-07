using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    /// <summary>
    /// Represents the class that can modify the constant calculations
    /// in a given composite calculation instance.
    /// </summary>
    internal sealed class CalculationValueAdjustment : CalculationVisitor
    {

        private readonly long toValue;
        private bool processed = false;
        private int currentIdx = 0;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        private readonly int hitIndex;

        public CalculationValueAdjustment(long toValue, int totalNumberOfConstantCalculations)
        {
            this.toValue = toValue;
            hitIndex = rnd.Next(totalNumberOfConstantCalculations);
        }

        protected override void VisitConstantCalculation(ConstantCalculation constantCalculation)
        {
            if (!processed && currentIdx == hitIndex)
            {
                constantCalculation.SetValue(this.toValue);
                processed = true;
            }
            currentIdx++;
        }
    }
}
