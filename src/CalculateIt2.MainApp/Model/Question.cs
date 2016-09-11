using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateIt2.MainApp.Model
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Formation { get; set; }

        public override string ToString() => Name;
    }
}
