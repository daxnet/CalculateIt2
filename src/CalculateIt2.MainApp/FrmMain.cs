using CalculateIt2.Engine.Generation;
using CalculateIt2.Engine.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculateIt2.MainApp
{
    public partial class FrmMain : Form
    {
        private readonly ArithmeticEquationGenerator generator = new ArithmeticEquationGenerator("{30}+-*/|4", new AvoidNegativeResultRule(), new DivisibilityEnsuranceRule());
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var calculation = generator.Generate();
            var equation = calculation.ToFormattedString(Engine.SpacingOption.Thin) + " = " + calculation.Value;
            this.lblEquation.Text = equation;
        }
    }
}
