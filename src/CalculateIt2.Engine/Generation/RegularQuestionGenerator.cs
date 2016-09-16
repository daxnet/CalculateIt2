using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public sealed class RegularQuestionGenerator : QuestionGenerator<long>
    {
        public RegularQuestionGenerator(string placeHolder = "____", SpacingOption spacingOption = SpacingOption.Thin) 
            : base(placeHolder, spacingOption)
        {
        }

        public override QuestionGenerationResult<long> Generate(Calculation calculation)
        {
            return new QuestionGenerationResult<long>($"{calculation.ToFormattedString(this.SpacingOption)} = {PlaceHolder}", calculation.Value);
        }
    }
}
