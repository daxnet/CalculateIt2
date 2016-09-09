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
    internal sealed class RandomizedCalculationValueAdjustment : CalculationVisitor
    {

        private readonly int min;
        private readonly int max;
        private readonly int totalNumberOfConstantCalculations;
        private readonly Func<int, bool> exclusionExpectation;
        private int currentIdx = 0;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        private int hitIndex;

        public RandomizedCalculationValueAdjustment(int min, int max, int totalNumberOfConstantCalculations, Func<int, bool> exclusionExpectation = null)
        {
            this.min = min;
            this.max = max;
            this.exclusionExpectation = exclusionExpectation;
            this.totalNumberOfConstantCalculations = totalNumberOfConstantCalculations;
        }

        private int GetValue()
        {
            int value;
            if (exclusionExpectation != null)
            {
                do
                {
                    if (max == 0)
                    {
                        value = rnd.Next(0, min + 1);
                    }
                    else
                    {
                        value = rnd.Next(min, max + 1);
                    }
                } while (exclusionExpectation(value));
            }
            else
            {
                if (max == 0)
                {
                    value = rnd.Next(0, min + 1);
                }
                else
                {
                    value = rnd.Next(min, max + 1);
                }
            }
            return value;
        }

        internal void Reset()
        {
            currentIdx = 0;
            hitIndex = rnd.Next(totalNumberOfConstantCalculations);
        }

        protected override void VisitConstantCalculation(ConstantCalculation constantCalculation)
        {
            if (currentIdx == hitIndex)
            {
                constantCalculation.SetValue(GetValue());
            }
            currentIdx++;
        }
    }
}
