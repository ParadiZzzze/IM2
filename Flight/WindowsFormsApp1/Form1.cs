using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double dt = 0.01;
        const double g = 9.81;

        double x = 0;
        double y = 0;
        double t = 0;
        double y0; 
        double v0; 
        double angle;

        private void Scale()
        {
            y0 = (double)edHeight.Value;
            v0 = (double)edSpeed.Value;
            angle = (double)edAngle.Value;

            double Ymax = 0;
            double Xmax = 0;
            double Tfull = 0;
            double sinangle = Math.Sin(angle * Math.PI / 180);

            Ymax = y0 + (v0 * v0 * sinangle * sinangle / 2 / g);
            Tfull = (v0 * sinangle + Math.Sqrt(v0 * v0 * sinangle * sinangle + 2 * g * y0)) / g;
            Xmax = v0 * Tfull * Math.Cos(angle * Math.PI / 180);

            chart1.ChartAreas[0].AxisX.Maximum = Math.Ceiling(Xmax) + 1;
            chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Ymax) + 1;

            maxh.Text = "Max.Height: " + Convert.ToString(Ymax);
            maxl.Text = "Max.Length: " + Convert.ToString(Xmax);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            y0 = (double)edHeight.Value;
            v0 = (double)edSpeed.Value;
            angle = (double)edAngle.Value;

            x = 0;
            y = y0;
            t = 0;

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            Scale();

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            x = v0 * Math.Cos(angle * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(angle * Math.PI / 180) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);

            label4.Text = "Time:" + Convert.ToString(t);


            if (y <= 0) timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
