using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUBusy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int coreCount = Environment.ProcessorCount;
            Thread[] drawThread = new Thread[(coreCount - 1)];
            for (int i = 0; i < drawThread.Length; i++)
            {
                drawThread[i] = new Thread(DrawCPU);
            }
            for (int i = 0; i < drawThread.Length; i++)
            {
                drawThread[i].Start();
            }
        }

        private void DrawCPU()
        {
            const int SAMPLING_COUNT = 250;
            const double PI = 3.14159;
            const int TOTAL_AMPLITUDE = 5000;
            double[] busySpan = new double[SAMPLING_COUNT];
            int amplitude = (TOTAL_AMPLITUDE) / 2;
            double radian = 0.0;
            double radianIncreament = 2.0 / (double)SAMPLING_COUNT;

            for (int i = 0; i < SAMPLING_COUNT; i++)
            {
                busySpan[i] = (double)(amplitude + Math.Sin(PI * radian) * amplitude);
                radian += radianIncreament;
            }

            int startTick = Environment.TickCount;

            for (int j = 0; ; j = (j + 1) % SAMPLING_COUNT)
            {
                startTick = Environment.TickCount;
                while ((Environment.TickCount - startTick) < busySpan[j])
                {
                    // 
                }
                System.Threading.Thread.Sleep(TOTAL_AMPLITUDE - (int)busySpan[j]);
            }
        }
    }
}
