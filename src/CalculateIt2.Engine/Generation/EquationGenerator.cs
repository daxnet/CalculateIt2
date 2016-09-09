using System.Linq;
using CalculateIt2.Engine.Rules;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the base class for the equation generators.
    /// </summary>
    public abstract class EquationGenerator
    {
        #region Fields
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

        /// <summary>
        /// The local list for storing the error message texts.
        /// </summary>
        protected readonly List<string> errorMessages = new List<string>();

        /// <summary>
        /// The local list which contains all the registered rules.
        /// </summary>
        protected readonly List<IRule> rules = new List<IRule>();
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>EquationGenerator</c> class.
        /// </summary>
        /// <param name="formation">The formation of the equation that is going to be generated.</param>
        /// <param name="rules">A list of <see cref="Rules.IRule"/> instances that is registered with current equation generator.</param>
        public EquationGenerator(string formation, IEnumerable<IRule> rules = null)
        {
            // Adds the AvoidDivideByZeroRule, as it is a mandatory for a arithmetic calculation.
            this.rules.Add(new AvoidDivideByZeroRule());

            // Load additional rules and register to the current generator instance.
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

            // Firstly use the regular expression to validate the given formation.
            var regex = new Regex(this.FormationPattern);
            var match = regex.Match(this.Formation);
            IsValid = match.Success;
            if (IsValid)
            {
                // If the given formation can pass the regular expression matching, the matching groups
                // will be extracted from the formation, and be stored into the local dictionary as parameters.
                foreach (var groupName in regex.GetGroupNames())
                {
                    parameters.Add(groupName, match.Groups[groupName].Value);
                }

                // Passing the parameters to the ValidateParamters method for additional validation. For example,
                // check if the parameters are valid.
                IsValid = ValidateParameters(parameters);
            }
            else
            {
                // If the given formation cannot pass the regular expression matching, an error
                // message will be added to the local list.
                errorMessages.Add("The formation passed in cannot match the required pattern criteria.");
            }
        }
        #endregion

        #region Public Properties
        public bool IsValid { get; }

        public string Formation { get; }

        public IEnumerable<string> ErrorMessages => errorMessages;
        #endregion

        #region Public Methods
        public abstract Calculation Generate();
        #endregion

        #region Protected Properties
        protected abstract string FormationPattern { get; }

        protected IDictionary<string, string> Parameters => parameters;
        #endregion

        #region Protected Methods
        protected virtual bool ValidateParameters(IDictionary<string, string> parameters) => true;
        #endregion
    }
}
