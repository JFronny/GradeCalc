using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CC_Functions.W32.Forms;
using NCalc2;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace GradeCalc
{
    public partial class MainForm : Form
    {
        private readonly DataGridViewTextBoxColumn _gradeColumn;
        private readonly DataGridViewTextBoxColumn _nameColumn;

        private readonly List<DataGridViewNumericUpDownColumn>
            _taskColumns = new List<DataGridViewNumericUpDownColumn>();

        public MainForm()
        {
            InitializeComponent();
            _nameColumn = new DataGridViewTextBoxColumn {HeaderText = "Name", Name = "Name"};
            dataGridView.Columns.Add(_nameColumn);
            _gradeColumn = new DataGridViewTextBoxColumn {HeaderText = "Grade", Name = "Grade", ReadOnly = true};
            dataGridView.Columns.Add(_gradeColumn);
            tasksNum_ValueChanged(null, null);
        }

        private static int Round(double val) => (int) Math.Round(val);

        private static Color GetColor(double x, double max) =>
            Color.FromArgb(Round(255 * (x / max)), Round(255 * (1 - (x / max))), 0);

        private void calcButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Cells[_nameColumn.Name].Style.BackColor = Color.White;
                DataGridViewTextBoxCell gradeCell = (DataGridViewTextBoxCell) row.Cells[_gradeColumn.Name];
                try
                {
                    Expression ex = new Expression(algorithmBox.Text);
                    decimal maxScore = 0;
                    decimal totalScore = 0;
                    _taskColumns.ForEach(s =>
                    {
                        DataGridViewNumericUpDownCell cell = (DataGridViewNumericUpDownCell) row.Cells[s.Name];
                        if (string.IsNullOrWhiteSpace((string) cell.FormattedValue)) return;
                        maxScore += cell.Maximum;
                        totalScore += decimal.Parse((string) cell.FormattedValue);
                        ex.Parameters["score"] = decimal.Parse((string) cell.FormattedValue);
                        ex.Parameters["maxScore"] = cell.Maximum;
                        cell.Style.BackColor = GetColor((float) NCalcDoubleParser.Parse(ex.Evaluate()), 1);
                    });
                    ex.Parameters["score"] = (double) totalScore;
                    ex.Parameters["maxScore"] = (double) maxScore;
                    double grade = 6 - (NCalcDoubleParser.Parse(ex.Evaluate()) * 5);
                    gradeCell.Value = (grade.ToString(CultureInfo.InvariantCulture).Length > 13
                                          ? grade.ToString(CultureInfo.InvariantCulture).Remove(13)
                                          : grade.ToString(CultureInfo.InvariantCulture)) +
                                      " " + TexGrade(grade);
                    gradeCell.Style.BackColor = GetColor(grade - 1, 5);
                }
                catch (Exception)
                {
                    gradeCell.Value = "";
                }
            }
            dataGridView.Sort(_nameColumn, ListSortDirection.Ascending);
        }

        private void tasksNum_ValueChanged(object sender, EventArgs e)
        {
            while (tasksNum.Value < _taskColumns.Count)
            {
                DataGridViewNumericUpDownColumn col = _taskColumns[_taskColumns.Count - 1];
                dataGridView.Columns.Remove(col);
                _taskColumns.Remove(col);
                col.Dispose();
            }
            while (tasksNum.Value > _taskColumns.Count)
            {
                DataGridViewNumericUpDownColumn clm = new DataGridViewNumericUpDownColumn
                {
                    Minimum = 0, Maximum = 10, Name = "Task " + (_taskColumns.Count + 1)
                };
                dataGridView.Columns.Add(clm);
                _taskColumns.Add(clm);
            }
            _gradeColumn.DisplayIndex = dataGridView.Columns.Count - 1;
        }

        private static string TexGrade(double g)
        {
            double gGrade = new[] {-0.5, 0, 0.5}.OrderBy(x => Math.Abs((x - (g - Round(g))) + 1)).First();
            return (char) (('A' + Round(g)) - 1) +
                   (g == 1 || gGrade == -0.5 ? "+" : g == 6 || gGrade == 0.5 ? "-" : "");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Excel Spreadsheet|*.xlst"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Worksheet1");
            List<string[]> headerRow = new List<string[]>
            {
                dataGridView.Columns.OfType<DataGridViewColumn>().Select(s => s.HeaderText).ToArray()
            };
            string headerRange = "A1:" + char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
            worksheet.Cells[headerRange].LoadFromArrays(headerRow);
            dataGridView.Rows.OfType<DataGridViewRow>()
                .Reverse().Skip(1).Reverse().ToList().ForEach(s =>
                {
                    foreach (DataGridViewCell cell in s.Cells)
                    {
                        worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Value = cell.Value.ToString();
                        worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Style.Fill.PatternType =
                            ExcelFillStyle.Solid;
                        worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Style.Fill.BackgroundColor
                            .SetColor(cell.Style.BackColor);
                    }
                });
            excel.SaveAs(new FileInfo(dialog.FileName));
        }
    }
}