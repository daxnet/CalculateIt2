using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public sealed class ClozeQuestionGenerator : QuestionGenerator<long>
    {
        private const string DigitalPattern = @"\d+";
        private readonly Regex regex = new Regex(DigitalPattern);
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);

        public ClozeQuestionGenerator(string placeHolder = "（ ）", SpacingOption spacingOption = SpacingOption.Thin) : base(placeHolder, spacingOption)
        {
        }

        public override QuestionGenerationResult<long> Generate(Calculation calculation)
        {
            var calculationString = calculation.ToFormattedString(this.SpacingOption);
            var digitalMatches = regex.Matches(calculationString);
            var matchesArray = new Match[digitalMatches.Count];
            digitalMatches.CopyTo(matchesArray, 0);
            var idx = rnd.Next(matchesArray.Length);
            var selectedIndex = matchesArray[idx].Index;
            var selectedValue = Convert.ToInt64(matchesArray[idx].Value);

            var formula = $@"{regex.Replace(calculationString, match =>
            {
                if (match.Index == selectedIndex)
                {
                    return PlaceHolder;
                }
                return match.Value;
            })} = {calculation.Value}";

            return new QuestionGenerationResult<long>(formula, selectedValue);
        }
    }
}
