using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public class CompositeCalculation : Calculation
    {
        public CompositeCalculation(Calculation left, Calculation right, Operator @operator)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = @operator;
        }

        public Calculation Left { get; }
        public Calculation Right { get; }
        public Operator Operator { get; }

        public override int Value
        {
            get
            {
                switch(Operator)
                {
                    case Operator.Add:
                        return Left.Value + Right.Value;
                    case Operator.Sub:
                        return Left.Value - Right.Value;
                    case Operator.Mul:
                        return Left.Value * Right.Value;
                    case Operator.Div:
                        return Left.Value / Right.Value;
                    default:
                        return int.MinValue;
                }
            }
        }

        public override string ToString()
        {
            string operatorSign = null;
            switch(Operator)
            {
                case Operator.Add:
                    operatorSign = "+";
                    break;
                case Operator.Sub:
                    operatorSign = "-";
                    break;
                case Operator.Mul:
                    operatorSign = "*";
                    break;
                case Operator.Div:
                    operatorSign = "/";
                    break;
            }
            if (Left is CompositeCalculation && 
                !(Right is CompositeCalculation) && 
                OperatorPrecedence((Left as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"({Left}){operatorSign}{Right}";
            }

            if (!(Left is CompositeCalculation) &&
                Right is CompositeCalculation &&
                OperatorPrecedence((Right as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"{Left}{operatorSign}({Right})";
            }

            if (Left is CompositeCalculation && Right is CompositeCalculation)
            {
                return $"({Left}){operatorSign}({Right})";
            }

            return $"{Left}{operatorSign}{Right}";
        }

        private static int OperatorPrecedence(Operator op)
        {
            return op == Operator.Add || op == Operator.Sub ? 1 : 2;
        }
    }
}
