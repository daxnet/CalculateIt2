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

        public override long Value
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
                        return long.MinValue;
                }
            }
        }

        public override void Accept(IVisitor visitor)
        {
            Left.Accept(visitor);
            visitor.Visit(this);
            Right.Accept(visitor);
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

        public override string ToFormattedString(SpacingOption option = SpacingOption.None)
        {
            string operatorSign = null;
            string spacing = "";
            switch (option)
            {
                case SpacingOption.Thin:
                    spacing = " ";
                    break;
                case SpacingOption.Thick:
                    spacing = "   ";
                    break;
            }
            switch (Operator)
            {
                case Operator.Add:
                    operatorSign = $"{spacing}\u002B{spacing}";
                    break;
                case Operator.Sub:
                    operatorSign = $"{spacing}\u2212{spacing}";
                    break;
                case Operator.Mul:
                    operatorSign = $"{spacing}\u00D7{spacing}";
                    break;
                case Operator.Div:
                    operatorSign = $"{spacing}\u00F7{spacing}";
                    break;
            }
            if (Left is CompositeCalculation &&
                !(Right is CompositeCalculation) &&
                OperatorPrecedence((Left as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"\u0028{Left.ToFormattedString(option)}\u0029{operatorSign}{Right.ToFormattedString(option)}";
            }

            if (!(Left is CompositeCalculation) &&
                Right is CompositeCalculation &&
                OperatorPrecedence((Right as CompositeCalculation).Operator) < OperatorPrecedence(this.Operator))
            {
                return $"{Left.ToFormattedString(option)}{operatorSign}\u0028{Right.ToFormattedString(option)}\u0029";
            }

            if (Left is CompositeCalculation && Right is CompositeCalculation)
            {
                return $"\u0028{Left.ToFormattedString(option)}\u0029{operatorSign}\u0028{Right.ToFormattedString(option)}\u0029";
            }

            return $"{Left.ToFormattedString(option)}{operatorSign}{Right.ToFormattedString(option)}";
        }

        private static int OperatorPrecedence(Operator op)
        {
            return op == Operator.Add || op == Operator.Sub ? 1 : 2;
        }
    }
}
