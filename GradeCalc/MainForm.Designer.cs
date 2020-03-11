namespace GradeCalc
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.calcButton = new System.Windows.Forms.Button();
            this.configPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.algorithmLabel = new System.Windows.Forms.Label();
            this.tasksLabel = new System.Windows.Forms.Label();
            this.algorithmBox = new System.Windows.Forms.ComboBox();
            this.tasksNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView)).BeginInit();
            this.configPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.tasksNum)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(812, 316);
            this.dataGridView.TabIndex = 0;
            // 
            // calcButton
            // 
            this.calcButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this.calcButton.Location = new System.Drawing.Point(14, 7);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(117, 27);
            this.calcButton.TabIndex = 1;
            this.calcButton.Text = "Calculate Grades";
            this.calcButton.UseVisualStyleBackColor = true;
            this.calcButton.Click += new System.EventHandler(this.calcButton_Click);
            // 
            // configPanel
            // 
            this.configPanel.Controls.Add(this.saveButton);
            this.configPanel.Controls.Add(this.algorithmLabel);
            this.configPanel.Controls.Add(this.tasksLabel);
            this.configPanel.Controls.Add(this.algorithmBox);
            this.configPanel.Controls.Add(this.tasksNum);
            this.configPanel.Controls.Add(this.calcButton);
            this.configPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.configPanel.Location = new System.Drawing.Point(0, 316);
            this.configPanel.Name = "configPanel";
            this.configPanel.Size = new System.Drawing.Size(812, 43);
            this.configPanel.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(782, 7);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(27, 27);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "💾";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // algorithmLabel
            // 
            this.algorithmLabel.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this.algorithmLabel.AutoSize = true;
            this.algorithmLabel.Location = new System.Drawing.Point(348, 13);
            this.algorithmLabel.Name = "algorithmLabel";
            this.algorithmLabel.Size = new System.Drawing.Size(64, 15);
            this.algorithmLabel.TabIndex = 5;
            this.algorithmLabel.Text = "Algorithm:";
            // 
            // tasksLabel
            // 
            this.tasksLabel.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this.tasksLabel.AutoSize = true;
            this.tasksLabel.Location = new System.Drawing.Point(138, 13);
            this.tasksLabel.Name = "tasksLabel";
            this.tasksLabel.Size = new System.Drawing.Size(37, 15);
            this.tasksLabel.TabIndex = 4;
            this.tasksLabel.Text = "Tasks:";
            // 
            // algorithmBox
            // 
            this.algorithmBox.Anchor =
                ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmBox.FormattingEnabled = true;
            this.algorithmBox.Items.AddRange(new object[]
                {"score / maxScore", "Pow(score / maxScore, 0.8)", "Pow(score / maxScore, 1.2)"});
            this.algorithmBox.Location = new System.Drawing.Point(416, 9);
            this.algorithmBox.Name = "algorithmBox";
            this.algorithmBox.Size = new System.Drawing.Size(360, 23);
            this.algorithmBox.TabIndex = 3;
            this.algorithmBox.Text = "score / maxScore";
            // 
            // tasksNum
            // 
            this.tasksNum.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom |
                                                       System.Windows.Forms.AnchorStyles.Left)));
            this.tasksNum.Location = new System.Drawing.Point(190, 10);
            this.tasksNum.Maximum = new decimal(new int[] {50, 0, 0, 0});
            this.tasksNum.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.tasksNum.Name = "tasksNum";
            this.tasksNum.Size = new System.Drawing.Size(140, 23);
            this.tasksNum.TabIndex = 2;
            this.tasksNum.Value = new decimal(new int[] {1, 0, 0, 0});
            this.tasksNum.ValueChanged += new System.EventHandler(this.tasksNum_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 359);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.configPanel);
            this.MinimumSize = new System.Drawing.Size(585, 174);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "GradeCalc";
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView)).EndInit();
            this.configPanel.ResumeLayout(false);
            this.configPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.tasksNum)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.Panel configPanel;
        private System.Windows.Forms.NumericUpDown tasksNum;
        private System.Windows.Forms.ComboBox algorithmBox;
        private System.Windows.Forms.Label tasksLabel;
        private System.Windows.Forms.Label algorithmLabel;
        private System.Windows.Forms.Button saveButton;
    }
}

