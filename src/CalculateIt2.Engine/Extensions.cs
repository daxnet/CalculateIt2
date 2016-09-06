using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine
{
    public static class Extensions
    {
        public static Calculation ToCalculation(this long i)
        {
            return new ConstantCalculation(i);
        }
    }
}
