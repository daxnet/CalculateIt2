using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public sealed class ComparisonQuestionGenerator : QuestionGenerator<string>
    {
        private readonly int threshold;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public ComparisonQuestionGenerator(int threshold, string placeHolder = "\u25CB", SpacingOption spacingOption = SpacingOption.Thin) 
            : base(placeHolder, spacingOption)
        {
            this.threshold = threshold;
        }

        public override QuestionGenerationResult<string> Generate(Calculation calculation)
        {
            var value = calculation.Value;
            var min = value > threshold ? value - threshold : 0;
            var max = value + threshold + 1;

            var v = rnd.Next(Convert.ToInt32(min), Convert.ToInt32(max));

            return new QuestionGenerationResult<string>($"{calculation.ToFormattedString(this.SpacingOption)} {this.PlaceHolder} {v}",
                value == v ? "=" :
                (
                    value > v ? ">" : "<"
                ));
        }
    }
}
