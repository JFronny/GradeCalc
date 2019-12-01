using NCalc2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

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

        private void calcButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                decimal maxScore = 0;
                decimal totalScore = 0;
                taskColumns.ForEach(s =>
                {
                    DataGridViewNumericUpDownCell cell = (DataGridViewNumericUpDownCell)row.Cells[s.Name];
                    if (!string.IsNullOrWhiteSpace((string)cell.FormattedValue))
                    {
                        maxScore += cell.Maximum;
                        totalScore += decimal.Parse((string)cell.FormattedValue);
                    }
                });
                Expression ex = new Expression(algorithmBox.Text);
                ex.Parameters["score"] = (double)totalScore;
                ex.Parameters["maxScore"] = (double)maxScore;
                //double gradeVal = (double)totalScore / (double)maxScore;
                double grade = 6 - (NCalcDoubleParser.Parse(ex.Evaluate()) * 5);
                ((DataGridViewTextBoxCell)row.Cells[gradeColumn.Name]).Value = (grade.ToString().Length > 13 ? grade.ToString().Remove(13) : grade.ToString()) + " " + texGrade(grade);
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
            double gGrade = (new double[] { -0.5, 0, 0.5 }).OrderBy(x => Math.Abs(x - (g - (int)Math.Round(g)))).First();
            return ((char)('A' + (int)Math.Round(g) - 1)).ToString() + ((g == 1 || gGrade == -0.5) ? "+" : (g == 6 || gGrade == 0.5) ? "-" : "");
        }
    }
}
