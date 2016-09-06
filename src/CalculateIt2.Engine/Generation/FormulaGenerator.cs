using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculateIt2.Engine.Generation
{
    public abstract class FormulaGenerator
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();
        protected readonly List<string> errorMessages = new List<string>();

        public FormulaGenerator(string formation)
        {
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
