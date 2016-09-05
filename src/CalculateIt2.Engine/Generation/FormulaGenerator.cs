using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public abstract class FormulaGenerator
    {
        protected FormulaGenerator(string formation)
        {
            this.Formation = formation;
        }

        public string Formation { get; }

        protected abstract string FormationPattern { get; }

        public bool CanHandle => new Regex(this.FormationPattern).IsMatch(this.Formation);

        public abstract Calculation Generate();
    }
}
