using CalculateIt2.Engine;
using CalculateIt2.Engine.Generation;
using CalculateIt2.Engine.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculateIt2.MainApp
{
    public partial class FrmMain : Form
    {
        // 初始化算式生成器，以产生25以内包含三个因子的四则混合运算计算式，
        // 同时避免产生负数结果，并确保除法运算能够整除。
        private readonly ArithmeticEquationGenerator generator =
            new ArithmeticEquationGenerator("{25}+-*/|3",
                new AvoidNegativeResultRule(), new DivisibilityEnsuranceRule());

        public FrmMain()
        {
            InitializeComponent();
            for(var i=1;i<=5;i++)
            {
                // 随机生成计算式
                var equation = generator.Generate();
                var label = (Label)this.Controls.Find("label" + i, false).First();
                if (label!=null)
                {
                    // 将计算式显示出来
                    label.Text = $"{equation.ToFormattedString(SpacingOption.Thin)} = {equation.Value}";
                }
            }
        }
    }
}

