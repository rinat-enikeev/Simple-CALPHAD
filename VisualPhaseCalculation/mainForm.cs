using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VisualPhaseCalculation
{
    public partial class mainForm : Form
    {
        private double TStep = 1;
        // calculate
        private double xmin = 0.001;
        private double xmax = 0.999;
        private double xstep = 0.001;

        private IList<IBinarySystem> systems;
        private IBinarySystem selectedSystem;
        private BinaryPhaseDiagram phaseDiagram;

        public mainForm()
        {
            InitializeComponent();
            BinarySystemFactory factory = new BinarySystemFactory();
            this.systems = factory.getSystems();
        }


        private void mainForm_Load(object sender, EventArgs e)
        {
            foreach (IBinarySystem system in systems)
            {
                comboBoxBPS.Items.Add(system.leftElement.id + "-" + system.rightElement.id);
            }
            if (comboBoxBPS.Items.Count > 0)
            {
                comboBoxBPS.SelectedItem = comboBoxBPS.Items[0];
            }
        }

        private void comboBoxBPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            IBinarySystem newSelectedSystem = systems[comboBoxBPS.SelectedIndex];
            if (newSelectedSystem != selectedSystem)
            {
                selectedSystem = newSelectedSystem;
                phaseDiagram = new BinaryPhaseDiagram(selectedSystem);
                phaseDiagram.CalculateTX(TStep);

                gibbsTempBar.Maximum = (int)maximalTemperature(selectedSystem);
                gibbsTempBar.Minimum = (int)minimalTemperature(selectedSystem);
                gibbsTempBar.Value = gibbsTempBar.Minimum;
                gibbsTempLabel.Text = "T = " + gibbsTempBar.Value.ToString() + " K";
            }
        }


        private void buttonCalcDSS_Click(object sender, EventArgs e)
        {
            clearChart();

            // in order to scale chart after filling
            double yMin = Double.PositiveInfinity;
            double yMax = Double.NegativeInfinity;
            
            // output
            for (int i = 0; i < phaseDiagram.TArr.Length; i++)
            {
                double y = phaseDiagram.TArr[i];
                if (phaseDiagram.TArr[i] < selectedSystem.rightElement.Ta_b + 1)
                {
                    chart.Series["RightXL"].Points.AddXY(phaseDiagram.xLRightArr[i], y);
                    chart.Series["RightXj"].Points.AddXY(phaseDiagram.xjRightArr[i], y);
                }

                if (phaseDiagram.TArr[i] < selectedSystem.leftElement.Ta_b + 1)
                {
                    chart.Series["LeftXL"].Points.AddXY(phaseDiagram.xLLeftArr[i], y);
                    chart.Series["LeftXj"].Points.AddXY(phaseDiagram.xjLeftArr[i], y);
                }

                if (y < yMin)
                {
                    yMin = y;
                }
                if (y > yMax)
                {
                    yMax = y;
                }
           }

            // scale chart 
            chart.ChartAreas[0].AxisY.ScaleView.Position = yMin;
            chart.ChartAreas[0].AxisY.ScaleView.Size = yMax - yMin;
        }

        private void buttonCalcdG_Click(object sender, EventArgs e)
        {
            clearChart();
            phaseDiagram.CalculateGX((double)gibbsTempBar.Value, xmin, xmax, xstep);

            double yMin = Double.PositiveInfinity;
            double yMax = Double.NegativeInfinity;
            
            // Вывод графиков
            for (int i = 0; i < phaseDiagram.xArr.Length - 1; i++)
            {
                chart.Series["dGjjL"].Points.AddXY(phaseDiagram.xArr[i], phaseDiagram.dGjjLArr[i]);
                chart.Series["dGjjj"].Points.AddXY(phaseDiagram.xArr[i], phaseDiagram.dGjjjArr[i]);

                if (phaseDiagram.dGjjLArr[i] < yMin)
                {
                    yMin = phaseDiagram.dGjjLArr[i];
                }
                if (phaseDiagram.dGjjLArr[i] > yMax)
                {
                    yMax = phaseDiagram.dGjjLArr[i];
                }
                if (phaseDiagram.dGjjjArr[i] < yMin)
                {
                    yMin = phaseDiagram.dGjjjArr[i];
                }
                if (phaseDiagram.dGjjjArr[i] > yMax)
                {
                    yMax = phaseDiagram.dGjjjArr[i];
                }
            }

            chart.ChartAreas[0].AxisY.ScaleView.Position = yMin;
            chart.ChartAreas[0].AxisY.ScaleView.Size = yMax - yMin;
        }

        private void trackBarT_Scroll(object sender, EventArgs e)
        {
            gibbsTempLabel.Text = "T = " + gibbsTempBar.Value.ToString() + " K";
        }

        private void buttonCalcddG_Click(object sender, EventArgs e)
        {
            clearChart();

            phaseDiagram.CalculateGX((double)gibbsTempBar.Value, xmin, xmax, xstep);


            double yMin = Double.PositiveInfinity;
            double yMax = Double.NegativeInfinity;

            // Вывод графиков
            for (int i = 0; i < phaseDiagram.xArr.Length - 1; i++)
            {
                chart.Series["ddGjjL"].Points.AddXY(phaseDiagram.xArr[i], phaseDiagram.ddGjjLArr[i]);
                chart.Series["ddGjjj"].Points.AddXY(phaseDiagram.xArr[i], phaseDiagram.ddGjjjArr[i]);

                if (phaseDiagram.ddGjjLArr[i] < yMin)
                {
                    yMin = phaseDiagram.ddGjjLArr[i];
                }
                if (phaseDiagram.ddGjjLArr[i] > yMax)
                {
                    yMax = phaseDiagram.ddGjjLArr[i];
                }
                if (phaseDiagram.ddGjjjArr[i] < yMin)
                {
                    yMin = phaseDiagram.ddGjjjArr[i];
                }
                if (phaseDiagram.ddGjjjArr[i] > yMax)
                {
                    yMax = phaseDiagram.ddGjjjArr[i];
                }
            }

            chart.ChartAreas[0].AxisY.ScaleView.Position = yMin;
            chart.ChartAreas[0].AxisY.ScaleView.Size = yMax - yMin;
        }

        private void buttonNonDiffDSS_Click(object sender, EventArgs e)
        {
            clearChart();
            // Вывод графиков

            double yMin = Double.PositiveInfinity;
            double yMax = Double.NegativeInfinity;
            double y = 0;

            for (int i = 0; i < phaseDiagram.TArr.Length; i++)
            {
                y = phaseDiagram.TArr[i];
                chart.Series["zjLRight"].Points.AddXY(phaseDiagram.zjLRightArr[i], y);
                chart.Series["zjLLeft"].Points.AddXY(phaseDiagram.zjLLeftArr[i], y);

                if (y < yMin)
                {
                    yMin = y;
                }
                if (y > yMax)
                {
                    yMax = y;
                }
            }

            chart.ChartAreas[0].AxisY.ScaleView.Position = yMin;
            chart.ChartAreas[0].AxisY.ScaleView.Size = yMax - yMin;
            
        }

        private void clearChart()
        {
            //Очищаем график
            chart.Series["RightXL"].Points.Clear();
            chart.Series["RightXj"].Points.Clear();
            chart.Series["LeftXL"].Points.Clear();
            chart.Series["LeftXj"].Points.Clear();
            chart.Series["dGjjL"].Points.Clear();
            chart.Series["dGjjj"].Points.Clear();
            chart.Series["ddGjjL"].Points.Clear();
            chart.Series["ddGjjj"].Points.Clear();
            chart.Series["zjLLeft"].Points.Clear();
            chart.Series["zjLRight"].Points.Clear();
        }

        private double minimalTemperature(IBinarySystem system)
        {
            double tMin = Double.PositiveInfinity;
            if (system.azeotrope.temperature < tMin)
            {
                tMin = system.azeotrope.temperature;
            }
            if (system.leftElement.Ta_b < tMin)
            {
                tMin = system.leftElement.Ta_b;
            }
            if (system.rightElement.Ta_b < tMin)
            {
                tMin = system.rightElement.Ta_b;
            }
            return tMin;
        }

        private double maximalTemperature(IBinarySystem system)
        {
            double tMax = Double.NegativeInfinity;
            if (system.azeotrope.temperature > tMax)
            {
                tMax = system.azeotrope.temperature;
            }
            if (system.leftElement.Ta_b > tMax)
            {
                tMax = system.leftElement.Ta_b;
            }
            if (system.rightElement.Ta_b > tMax)
            {
                tMax = system.rightElement.Ta_b;
            }
            return tMax;
        }

        private void diffLessToExcel_Click(object sender, EventArgs e)
        {
            new ExcelDiagramCreator().drawNonDiffusionDiagramInExcel(phaseDiagram);
        }
    }
}
