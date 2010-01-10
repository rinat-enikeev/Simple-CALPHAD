namespace VisualPhaseCalculation
{
    partial class mainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series22 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series23 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series24 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series25 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series26 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series27 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series28 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonCalcDSS = new System.Windows.Forms.Button();
            this.buttonCalcdG = new System.Windows.Forms.Button();
            this.buttonCalcddG = new System.Windows.Forms.Button();
            this.gibbsTempBar = new System.Windows.Forms.TrackBar();
            this.gibbsTempLabel = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonNonDiffDSS = new System.Windows.Forms.Button();
            this.comboBoxBPS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.diffLessToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gibbsTempBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCalcDSS
            // 
            this.buttonCalcDSS.Location = new System.Drawing.Point(808, 13);
            this.buttonCalcDSS.Name = "buttonCalcDSS";
            this.buttonCalcDSS.Size = new System.Drawing.Size(61, 23);
            this.buttonCalcDSS.TabIndex = 1;
            this.buttonCalcDSS.Text = "Diagram";
            this.buttonCalcDSS.UseVisualStyleBackColor = true;
            this.buttonCalcDSS.Click += new System.EventHandler(this.buttonCalcDSS_Click);
            // 
            // buttonCalcdG
            // 
            this.buttonCalcdG.Location = new System.Drawing.Point(964, 649);
            this.buttonCalcdG.Name = "buttonCalcdG";
            this.buttonCalcdG.Size = new System.Drawing.Size(48, 23);
            this.buttonCalcdG.TabIndex = 2;
            this.buttonCalcdG.Text = "dG(x)";
            this.buttonCalcdG.UseVisualStyleBackColor = true;
            this.buttonCalcdG.Click += new System.EventHandler(this.buttonCalcdG_Click);
            // 
            // buttonCalcddG
            // 
            this.buttonCalcddG.Location = new System.Drawing.Point(964, 678);
            this.buttonCalcddG.Name = "buttonCalcddG";
            this.buttonCalcddG.Size = new System.Drawing.Size(48, 23);
            this.buttonCalcddG.TabIndex = 3;
            this.buttonCalcddG.Text = "ddG(x)";
            this.buttonCalcddG.UseVisualStyleBackColor = true;
            this.buttonCalcddG.Click += new System.EventHandler(this.buttonCalcddG_Click);
            // 
            // gibbsTempBar
            // 
            this.gibbsTempBar.Location = new System.Drawing.Point(159, 649);
            this.gibbsTempBar.Maximum = 1408;
            this.gibbsTempBar.Minimum = 900;
            this.gibbsTempBar.Name = "gibbsTempBar";
            this.gibbsTempBar.Size = new System.Drawing.Size(799, 45);
            this.gibbsTempBar.TabIndex = 4;
            this.gibbsTempBar.Value = 900;
            this.gibbsTempBar.Scroll += new System.EventHandler(this.trackBarT_Scroll);
            // 
            // gibbsTempLabel
            // 
            this.gibbsTempLabel.AutoSize = true;
            this.gibbsTempLabel.Location = new System.Drawing.Point(494, 688);
            this.gibbsTempLabel.Name = "gibbsTempLabel";
            this.gibbsTempLabel.Size = new System.Drawing.Size(54, 13);
            this.gibbsTempLabel.TabIndex = 5;
            this.gibbsTempLabel.Text = "T = 900 K";
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            this.chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart.BackSecondaryColor = System.Drawing.Color.White;
            this.chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderlineWidth = 2;
            this.chart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea3.Area3DStyle.Inclination = 15;
            chartArea3.Area3DStyle.IsClustered = true;
            chartArea3.Area3DStyle.IsRightAngleAxes = false;
            chartArea3.Area3DStyle.Perspective = 10;
            chartArea3.Area3DStyle.Rotation = 10;
            chartArea3.Area3DStyle.WallWidth = 0;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea3.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisX.Maximum = 1;
            chartArea3.AxisX.Minimum = 0;
            chartArea3.AxisX.ScrollBar.BackColor = System.Drawing.Color.AliceBlue;
            chartArea3.AxisX.ScrollBar.ButtonColor = System.Drawing.SystemColors.Control;
            chartArea3.AxisX.ScrollBar.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea3.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisY.ScrollBar.BackColor = System.Drawing.Color.AliceBlue;
            chartArea3.AxisY.ScrollBar.ButtonColor = System.Drawing.SystemColors.Control;
            chartArea3.AxisY.ScrollBar.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(165)))), ((int)(((byte)(191)))), ((int)(((byte)(228)))));
            chartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea3.BackSecondaryColor = System.Drawing.Color.White;
            chartArea3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.CursorX.Interval = 0.0001;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            chartArea3.CursorY.Interval = 0.2;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.IsUserSelectionEnabled = true;
            chartArea3.CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;
            chartArea3.InnerPlotPosition.Auto = false;
            chartArea3.InnerPlotPosition.Height = 75F;
            chartArea3.InnerPlotPosition.Width = 86.32634F;
            chartArea3.InnerPlotPosition.X = 11.21863F;
            chartArea3.InnerPlotPosition.Y = 3.96004F;
            chartArea3.Name = "Default";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 85F;
            chartArea3.Position.Width = 81.46518F;
            chartArea3.Position.X = 9.267409F;
            chartArea3.Position.Y = 10F;
            chartArea3.ShadowColor = System.Drawing.Color.Transparent;
            this.chart.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Transparent;
            legend3.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold);
            legend3.IsTextAutoFit = false;
            legend3.Name = "Default";
            legend3.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Wide;
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(-3, 71);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series21.ChartArea = "Default";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series21.Legend = "Default";
            series21.Name = "RightXL";
            series22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series22.ChartArea = "Default";
            series22.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series22.Legend = "Default";
            series22.Name = "RightXj";
            series23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series23.ChartArea = "Default";
            series23.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series23.Legend = "Default";
            series23.Name = "dGjjL";
            series24.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series24.ChartArea = "Default";
            series24.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series24.Legend = "Default";
            series24.Name = "dGjjj";
            series25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series25.ChartArea = "Default";
            series25.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series25.Legend = "Default";
            series25.Name = "ddGjjL";
            series26.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series26.ChartArea = "Default";
            series26.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series26.Legend = "Default";
            series26.Name = "ddGjjj";
            series27.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series27.ChartArea = "Default";
            series27.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series27.Legend = "Default";
            series27.Name = "zjLRight";
            series28.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series28.ChartArea = "Default";
            series28.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series28.Legend = "Default";
            series28.Name = "zjLLeft";
            series29.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series29.ChartArea = "Default";
            series29.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series29.Legend = "Default";
            series29.Name = "LeftXL";
            series30.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series30.ChartArea = "Default";
            series30.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series30.Legend = "Default";
            series30.Name = "LeftXj";
            this.chart.Series.Add(series21);
            this.chart.Series.Add(series22);
            this.chart.Series.Add(series23);
            this.chart.Series.Add(series24);
            this.chart.Series.Add(series25);
            this.chart.Series.Add(series26);
            this.chart.Series.Add(series27);
            this.chart.Series.Add(series28);
            this.chart.Series.Add(series29);
            this.chart.Series.Add(series30);
            this.chart.Size = new System.Drawing.Size(1024, 561);
            this.chart.TabIndex = 6;
            // 
            // buttonNonDiffDSS
            // 
            this.buttonNonDiffDSS.Location = new System.Drawing.Point(875, 13);
            this.buttonNonDiffDSS.Name = "buttonNonDiffDSS";
            this.buttonNonDiffDSS.Size = new System.Drawing.Size(137, 23);
            this.buttonNonDiffDSS.TabIndex = 7;
            this.buttonNonDiffDSS.Text = "Diffusionless diagram";
            this.buttonNonDiffDSS.UseVisualStyleBackColor = true;
            this.buttonNonDiffDSS.Click += new System.EventHandler(this.buttonNonDiffDSS_Click);
            // 
            // comboBoxBPS
            // 
            this.comboBoxBPS.FormattingEnabled = true;
            this.comboBoxBPS.Location = new System.Drawing.Point(735, 14);
            this.comboBoxBPS.Name = "comboBoxBPS";
            this.comboBoxBPS.Size = new System.Drawing.Size(67, 21);
            this.comboBoxBPS.TabIndex = 11;
            this.comboBoxBPS.SelectedIndexChanged += new System.EventHandler(this.comboBoxBPS_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(685, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "System:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 654);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Temperature for dG(x), ddG(x):";
            // 
            // diffLessToExcel
            // 
            this.diffLessToExcel.Location = new System.Drawing.Point(937, 42);
            this.diffLessToExcel.Name = "diffLessToExcel";
            this.diffLessToExcel.Size = new System.Drawing.Size(75, 23);
            this.diffLessToExcel.TabIndex = 25;
            this.diffLessToExcel.Text = "( to Excel )";
            this.diffLessToExcel.UseVisualStyleBackColor = true;
            this.diffLessToExcel.Click += new System.EventHandler(this.diffLessToExcel_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 713);
            this.Controls.Add(this.diffLessToExcel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxBPS);
            this.Controls.Add(this.buttonNonDiffDSS);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.gibbsTempLabel);
            this.Controls.Add(this.gibbsTempBar);
            this.Controls.Add(this.buttonCalcddG);
            this.Controls.Add(this.buttonCalcdG);
            this.Controls.Add(this.buttonCalcDSS);
            this.Name = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gibbsTempBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalcDSS;
        private System.Windows.Forms.Button buttonCalcdG;
        private System.Windows.Forms.Button buttonCalcddG;
        private System.Windows.Forms.TrackBar gibbsTempBar;
        private System.Windows.Forms.Label gibbsTempLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button buttonNonDiffDSS;
        private System.Windows.Forms.ComboBox comboBoxBPS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button diffLessToExcel;
    }
}

