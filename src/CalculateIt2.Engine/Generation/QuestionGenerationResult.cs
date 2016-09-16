using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.Engine.Generation
{
    public sealed class QuestionGenerationResult<T>
    {
        private readonly string formula;
        private readonly T answer;

        public QuestionGenerationResult(string formula, T answer)
        {
            this.formula = formula;
            this.answer = answer;
        }

        public string Formula => formula;

        public T Answer => answer;
    }
}
