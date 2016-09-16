using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    /// <summary>
    /// Represents the base class of question generators.
    /// </summary>
    public abstract class QuestionGenerator<T>
    {
        private readonly string placeHolder;
        private readonly SpacingOption spacingOption;

        protected QuestionGenerator(string placeHolder, SpacingOption spacingOption)
        {
            this.placeHolder = placeHolder;
            this.spacingOption = spacingOption;
        }

        protected string PlaceHolder => placeHolder;

        protected SpacingOption SpacingOption => spacingOption;

        public abstract QuestionGenerationResult<T> Generate(Calculation calculation);
    }
}
