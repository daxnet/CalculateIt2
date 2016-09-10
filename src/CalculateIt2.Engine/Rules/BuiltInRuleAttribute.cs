using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Rules
{
    /// <summary>
    /// Represents that the decorated class is a built-in rule that cannot be removed or
    /// modified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class BuiltInRuleAttribute : Attribute
    {
    }
}
