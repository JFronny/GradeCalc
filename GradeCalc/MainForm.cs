using NCalc2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Style;

namespace GradeCalc
{
    public partial class MainForm : Form
    {
        private List<DataGridViewNumericUpDownColumn> taskColumns = new List<DataGridViewNumericUpDownColumn>();
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn gradeColumn;

        public MainForm()
        {
            InitializeComponent();
            nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.HeaderText = "Name";
            nameColumn.Name = "Name";
            dataGridView.Columns.Add(nameColumn);
            gradeColumn = new DataGridViewTextBoxColumn();
            gradeColumn.HeaderText = "Grade";
            gradeColumn.Name = "Grade";
            gradeColumn.ReadOnly = true;
            dataGridView.Columns.Add(gradeColumn);
            tasksNum_ValueChanged(null, null);
        }

        int round(double val) => (int)Math.Round(val);
        Color getColor(double x, double max) => Color.FromArgb(round(255 * (1 - (x / max))), round(255 * (x / max)), 0);

        private void calcButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Cells[nameColumn.Name].Style.BackColor = Color.White;
                DataGridViewTextBoxCell gradeCell = (DataGridViewTextBoxCell)row.Cells[gradeColumn.Name];
                try
                {
                    Expression ex = new Expression(algorithmBox.Text);
                    decimal maxScore = 0;
                    decimal totalScore = 0;
                    taskColumns.ForEach(s =>
                    {
                        DataGridViewNumericUpDownCell cell = (DataGridViewNumericUpDownCell)row.Cells[s.Name];
                        if (!string.IsNullOrWhiteSpace((string)cell.FormattedValue))
                        {
                            maxScore += cell.Maximum;
                            totalScore += decimal.Parse((string)cell.FormattedValue);
                            ex.Parameters["score"] = decimal.Parse((string)cell.FormattedValue);
                            ex.Parameters["maxScore"] = cell.Maximum;
                            cell.Style.BackColor = getColor((float)NCalcDoubleParser.Parse(ex.Evaluate()), 1);
                        }
                    });
                    ex.Parameters["score"] = (double)totalScore;
                    ex.Parameters["maxScore"] = (double)maxScore;
                    double grade = 6 - (NCalcDoubleParser.Parse(ex.Evaluate()) * 5);
                    gradeCell.Value = (grade.ToString().Length > 13 ? grade.ToString().Remove(13) : grade.ToString()) + " " + texGrade(grade);
                    gradeCell.Style.BackColor = getColor(grade - 1, 5);
                }
                catch (Exception e1)
                {
                    gradeCell.Value = "";
                }
            }
            dataGridView.Sort(nameColumn, ListSortDirection.Ascending);
        }

        private void tasksNum_ValueChanged(object sender, EventArgs e)
        {
            while (tasksNum.Value < taskColumns.Count)
            {
                DataGridViewNumericUpDownColumn col = taskColumns[taskColumns.Count - 1];
                dataGridView.Columns.Remove(col);
                taskColumns.Remove(col);
                col.Dispose();
            }
            while (tasksNum.Value > taskColumns.Count)
            {
                DataGridViewNumericUpDownColumn clm = new DataGridViewNumericUpDownColumn();
                clm.Minimum = 0;
                clm.Maximum = 10;
                clm.Name = "Task " + (taskColumns.Count + 1).ToString();
                dataGridView.Columns.Add(clm);
                taskColumns.Add(clm);
            }
            gradeColumn.DisplayIndex = dataGridView.Columns.Count - 1;
        }

        private string texGrade(double g)
        {
            double gGrade = (new double[] { -0.5, 0, 0.5 }).OrderBy(x => Math.Abs(x - (g - round(g)) + 1)).First();
            return ((char)('A' + round(g) - 1)).ToString() + ((g == 1 || gGrade == -0.5) ? "+" : (g == 6 || gGrade == 0.5) ? "-" : "");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Excel Spreadsheet|*.xlst"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Worksheet1");
                    List<string[]> headerRow = new List<string[]>()
                    {
                        dataGridView.Columns.OfType<DataGridViewColumn>().Select(s => s.HeaderText).ToArray()
                    };
                    string headerRange = "A1:" + char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                    ExcelRangeBase headers = worksheet.Cells[headerRange].LoadFromArrays(headerRow);
                    headers.Style.Font.Bold = true;
                    headers.Style.Font.Size = 14;
                    /*worksheet.Cells[2, 1].LoadFromArrays(dataGridView.Rows.OfType<DataGridViewRow>()
                        .Reverse().Skip(1).Reverse()
                        .Select(s => s.Cells.OfType<DataGridViewCell>()
                            .Select(t => t.Value.ToString())
                            .ToArray()
                        ));*/
                    dataGridView.Rows.OfType<DataGridViewRow>()
                        .Reverse().Skip(1).Reverse().ToList().ForEach(s =>
                        {
                            foreach (DataGridViewCell cell in s.Cells)
                            {
                                worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Value = cell.Value.ToString();
                                worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[cell.RowIndex + 2, cell.ColumnIndex + 1].Style.Fill.BackgroundColor.SetColor(cell.Style.BackColor);
                            }
                        });
                    excel.SaveAs(new FileInfo(dialog.FileName));
                }
            }
        }
    }
}
