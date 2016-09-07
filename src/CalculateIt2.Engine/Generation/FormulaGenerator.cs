using System.Linq;
using CalculateIt2.Engine.Rules;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the base class for the formula generators.
    /// </summary>
    public abstract class FormulaGenerator
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();
        protected readonly List<string> errorMessages = new List<string>();
        protected readonly List<IRule> rules = new List<IRule>();

        public FormulaGenerator(string formation, IEnumerable<IRule> rules = null)
        {
            this.rules.Add(new AvoidDivideByZeroRule());

            if (rules != null)
            {
                foreach(var rule in rules)
                {
                    if (this.rules.Any(r => r.GetType() == rule.GetType())) continue;
                    this.rules.Add(rule);
                }
            }

            this.Formation = formation;
            errorMessages.Clear();
            var regex = new Regex(this.FormationPattern);
            var match = regex.Match(this.Formation);
            IsValid = match.Success;
            if (IsValid)
            {
                foreach (var groupName in regex.GetGroupNames())
                {
                    parameters.Add(groupName, match.Groups[groupName].Value);
                }
                IsValid = ValidateParameters(parameters);
            }
            else
            {
                errorMessages.Add("The formation passed in cannot match the required pattern criteria.");
            }
        }

        public bool IsValid { get; }

        public string Formation { get; }

        public IEnumerable<string> ErrorMessages => errorMessages;

        public abstract Calculation Generate();

        protected abstract string FormationPattern { get; }

        protected IDictionary<string, string> Parameters => parameters;

        protected virtual bool ValidateParameters(IDictionary<string, string> parameters) => true;
    }
}
