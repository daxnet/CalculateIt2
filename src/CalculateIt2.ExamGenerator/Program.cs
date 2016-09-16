using CalculateIt2.Engine.Generation;
using CalculateIt2.Engine.Rules;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculateIt2.ExamGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var numOfColumns = 3;
            var numOfExams = 100;
            var gen = new ArithmeticEquationGenerator("{10}+-|2-3", new AvoidNegativeResultRule());

            var fileName = @"C:\Users\Sunny Chen\Desktop\test.docx";

            var doc = DocX.Create(fileName);

            for (int i = 0; i < numOfExams; i++)
            {
                var titleParagraph = doc.InsertParagraph("10以内加减法测试");
                titleParagraph.FontSize(16).Alignment = Alignment.center;
                titleParagraph.Bold();
                var nameParagraph = doc.InsertParagraph("姓名：__________    时间：__________    成绩：__________").FontSize(10);
                nameParagraph.Alignment = Alignment.center;
                nameParagraph.SetLineSpacing(LineSpacingType.Before, 1);

                var pQ1 = doc.InsertParagraph("1、计算题，请写出算式的计算结果。");
                pQ1.FontSize(14);
                pQ1.SetLineSpacing(LineSpacingType.Before, 1.5F);
                pQ1.Bold();
                var questionGenerator1 = new RegularQuestionGenerator(spacingOption: Engine.SpacingOption.None);
                var testingTable1 = doc.InsertTable(6, numOfColumns);
                testingTable1.Rows[0].Cells[0].Width = doc.PageWidth / 3.0F - 5;
                testingTable1.Rows[0].Cells[1].Width = doc.PageWidth / 3.0F - 5;
                testingTable1.Rows[0].Cells[2].Width = doc.PageWidth / 3.0F - 5;
                testingTable1.Design = TableDesign.TableGrid;
                testingTable1.SetTableCellMargin(TableCellMarginType.top, 5);
                testingTable1.Alignment = Alignment.center;
                testingTable1.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable1.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable1.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable1.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable1.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable1.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                for (var x = 0; x < 6; x++)
                {
                    for (var y = 0; y < numOfColumns; y++)
                    {
                        var calculation = gen.Generate();
                        var result = questionGenerator1.Generate(calculation);
                        testingTable1.Rows[x].Cells[y].Paragraphs.First().Append(result.Formula).FontSize(12).SetLineSpacing(LineSpacingType.Before, 0.5F);
                    }
                }

                var pQ2 = doc.InsertParagraph("2、填空题，请在（）中填入数字，将算式补全。");
                pQ2.FontSize(14);
                pQ2.SetLineSpacing(LineSpacingType.Before, 1.5F);
                pQ2.Bold();
                var questionGenerator2 = new ClozeQuestionGenerator(spacingOption: Engine.SpacingOption.None);
                var testingTable2 = doc.InsertTable(6, numOfColumns);
                testingTable2.Rows[0].Cells[0].Width = doc.PageWidth / 3.0F - 5;
                testingTable2.Rows[0].Cells[1].Width = doc.PageWidth / 3.0F - 5;
                testingTable2.Rows[0].Cells[2].Width = doc.PageWidth / 3.0F - 5;
                testingTable2.Design = TableDesign.TableGrid;
                testingTable2.SetTableCellMargin(TableCellMarginType.top, 5);
                testingTable2.Alignment = Alignment.center;
                testingTable2.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable2.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable2.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable2.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable2.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable2.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                for (var x = 0; x < 6; x++)
                {
                    for (var y = 0; y < numOfColumns; y++)
                    {
                        var calculation = gen.Generate();
                        var result = questionGenerator2.Generate(calculation);
                        testingTable2.Rows[x].Cells[y].Paragraphs.First().Append(result.Formula).FontSize(12).SetLineSpacing(LineSpacingType.Before, 0.5F);
                    }
                }

                var pQ3 = doc.InsertParagraph("3、比大小，在圆圈中填入＞、＜或＝符号。");
                pQ3.SetLineSpacing(LineSpacingType.Before, 1.5F);
                pQ3.FontSize(14);
                pQ3.Bold();
                var questionGenerator3 = new ComparisonQuestionGenerator(5, spacingOption: Engine.SpacingOption.None);
                var testingTable3 = doc.InsertTable(6, numOfColumns);
                testingTable3.Rows[0].Cells[0].Width = doc.PageWidth / 3.0F - 5;
                testingTable3.Rows[0].Cells[1].Width = doc.PageWidth / 3.0F - 5;
                testingTable3.Rows[0].Cells[2].Width = doc.PageWidth / 3.0F - 5;
                testingTable2.Design = TableDesign.TableGrid;
                testingTable3.Alignment = Alignment.center;
                testingTable3.SetBorder(TableBorderType.Left, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable3.SetBorder(TableBorderType.Right, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable3.SetBorder(TableBorderType.Top, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable3.SetBorder(TableBorderType.Bottom, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable3.SetBorder(TableBorderType.InsideH, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                testingTable3.SetBorder(TableBorderType.InsideV, new Border(BorderStyle.Tcbs_none, BorderSize.one, 1, Color.Black));
                for (var x = 0; x < 6; x++)
                {
                    for (var y = 0; y < numOfColumns; y++)
                    {
                        var calculation = gen.Generate();
                        var result = questionGenerator3.Generate(calculation);
                        testingTable3.Rows[x].Cells[y].Paragraphs.First().Append(result.Formula).FontSize(12).SetLineSpacing(LineSpacingType.Before, 0.5F);
                    }
                }

                if (i != numOfExams - 1)
                {
                    doc.InsertSectionPageBreak();
                }
            }
            doc.Save();
        }
    }
}
