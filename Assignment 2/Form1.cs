using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Assignment_2
{
    public partial class Form1 : Form
    {
        class row
        {
            public double time;
            public double velocity;
            public double acceleration;
            public double altitude;
        }

        List<row> table = new List<row>();
        public Form1()
        {

            InitializeComponent();

        } //aaa

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enables the data to be opened and used in the programme
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = " csv Files|*.csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        string line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            table.Add(new row());
                            string[] r = sr.ReadLine().Split(',');
                            table.Last().time = double.Parse(r[0]);
                            table.Last().altitude = double.Parse(r[1]);
                        }
                    }
                    derivative();
                    derivative2();
                }
                catch (IOException)
                {
                    MessageBox.Show(openFileDialog1.FileName + "failed to open");
                }
                catch (FormatException)
                {
                    MessageBox.Show(openFileDialog1.FileName + " is not in the required format. ");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show(openFileDialog1.FileName + " is not in the required format. ");
                }
            }
        }
        void derivative()
        {
            //calculated velocity by using altitude and time
            for (int i = 1; i < table.Count; i++)
            {
                double dS = table[i].altitude - table[i - 1].altitude;
                double dt = table[i].time - table[i - 1].time;
                table[i].velocity = dS / dt;
            }
        }

        void derivative2()
        {
            //calculated acceleration using velocity and time
            for (int i = 2; i < table.Count; i++)
            {
                double dV = table[i].velocity - table[i - 1].velocity;
                double dt = table[i].time - table[i - 1].time;
                table[i].acceleration = dV / dt;
            }
        }

        private void velocityTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calculating velocity chart
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Velocity",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart1.Series.Add(series);
            foreach(row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.velocity);
            }
            chart1.ChartAreas[0].AxisX.Title = "time /s";
            chart1.ChartAreas[0].AxisY.Title = "velocity /m/s";
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        private void AccelerationTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calculating acceleration chart
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Acceleration",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart1.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.acceleration);
            }
            chart1.ChartAreas[0].AxisX.Title = "time /s";
            chart1.ChartAreas[0].AxisY.Title = "Acceleration /m/s²";
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        private void AltitudeTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calculating altitude chart
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Altitude",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart1.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.altitude);
            }
            chart1.ChartAreas[0].AxisX.Title = "time /s";
            chart1.ChartAreas[0].AxisY.Title = "Altitude /m";
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enables the programme and the charts to be saved by a CSV file
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "csv Files|*.csv";
            DialogResult results = saveFileDialog1.ShowDialog();
            if (results == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.WriteLine("Time /s, Altitude /A, Velocity /V, Rate of change of Velocity / V/s");
                        foreach (row r in table)
                        {
                            sw.WriteLine(r.time + "," + r.altitude + "," + r.velocity + "," + r.acceleration);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(saveFileDialog1.FileName + " failed to save");
                }
            }
        }

        private void savePNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enables the programme and the charts to be saved by a PNG file
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "png Files|*.png";
            DialogResult results = saveFileDialog1.ShowDialog();
            if (results == DialogResult.OK)
            {
                try
                {
                    chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
                }
                catch
                {
                    MessageBox.Show(saveFileDialog1.FileName + " failed to save");
                }
            }
        }
    }
}
