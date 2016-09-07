using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    internal sealed class ConstantCalculationCounter : CalculationVisitor
    {
        private int numOfConstantCalculations;

        protected override void VisitConstantCalculation(ConstantCalculation constantCalculation)
        {
            base.VisitConstantCalculation(constantCalculation);
            this.numOfConstantCalculations++;
        }

        public int NumOfConstantCalculations => this.numOfConstantCalculations;
    }
}
