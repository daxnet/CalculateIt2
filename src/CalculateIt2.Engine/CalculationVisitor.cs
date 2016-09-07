using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public abstract class CalculationVisitor : IVisitor
    {
        public void Visit(IVisitorAcceptor acceptor)
        {
            if (acceptor is ConstantCalculation)
            {
                this.VisitConstantCalculation(acceptor as ConstantCalculation);
            }
            if (acceptor is CompositeCalculation)
            {
                this.VisitCompositeCalculation(acceptor as CompositeCalculation);
            }
        }

        protected virtual void VisitConstantCalculation(ConstantCalculation constantCalculation) { }

        protected virtual void VisitCompositeCalculation(CompositeCalculation compositeCalculation) { }
    }
}
