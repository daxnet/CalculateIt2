using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    [Flags]
    public enum Operator
    {
        None = 0,
        Add = 1,
        Sub = 2,
        Mul = 4,
        Div = 8
    }
}
