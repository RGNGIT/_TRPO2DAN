using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace _Dan9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        List<double> List1 = new List<double>();
        List<double> List2 = new List<double>();
        List<double> List3 = new List<double>();
        Random random = new Random();

        double GetNormalValue(int mx, int dx) => (double)(1 / Math.Sqrt(dx) * Math.Sqrt(2 * Math.PI)) * Math.Exp(-1 * (Math.Pow(random.Next(0, 50) - mx, 2)) / 2 * dx);

        List<double> ConcateList()
        {
            List<double> Temp = new List<double>();
            for(int i = 0; i < 30; i++)
            {
                Temp.Add(List1[i]);
                Temp.Add(List2[i]);
                Temp.Add(List3[i]);
            }
            Temp.Sort();
            return Temp;
        }

        void Calculate()
        {
            double Average = 0;
            double Dispersion = 0;
            foreach (var i in ConcateList())
            {
                Average += i;
            }
            foreach (var i in ConcateList())
            {
                Dispersion += Math.Pow(i - (Average / ConcateList().Count), 2);
            }
            Dispersion /= ConcateList().Count - 1;
            label4.Text = $"Среднее значение: {Average / ConcateList().Count} Дисперсия: {Dispersion}\n";
        }

        void FillChart()
        {
            List<int> Counters = new List<int>();
            List<string> Intervals = new List<string>();
            double Min = ConcateList()[0], Max = ConcateList()[ConcateList().Count - 1];
            double Interval = (Max - Min) / 10;
            double Left = Min, Right = Left + Interval;
            int Counter = 0;
            for (int i = 0; i < 10; i++)
            {
                Intervals.Add($"~{Math.Round(Left, 3)}-{Math.Round(Right, 3)}");
                foreach (double j in ConcateList())
                {
                    if (j >= Left && j <= Right)
                    {
                        Counter++;
                    }
                }
                Left += Interval;
                Right += Interval;
                Counters.Add(Counter);
                Counter = 0;
            }
            chart1.Series.Add(new Series("ExpRange")); // Создание нового чарта гистограммы и ее заполнение
            chart1.Series["ExpRange"].Points.DataBindXY(Intervals, Counters);
            for(int i = 0; i < 10; i++)
            {
                label4.Text += $"Столбец {i + 1}: {Counters[i]} ";
            }
            ChartArea CA = chart1.ChartAreas[0];
            CA.AxisY.Maximum = 100;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Visible = false;
            for(int i = 0; i < 30; i++)
            {
                List1.Add(GetNormalValue(2, 4));
                List2.Add(GetNormalValue(3, 3));
                List3.Add(GetNormalValue(4, 4));
                listBox3.Items.Add(List1[i]);
                listBox2.Items.Add(List2[i]);
                listBox1.Items.Add(List3[i]);
            }
            foreach(double i in ConcateList())
            {
                listBox4.Items.Add(i);
            }
            Calculate();
            FillChart();
        }
    }
}
