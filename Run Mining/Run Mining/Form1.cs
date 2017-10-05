using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Run_Mining
{
    public partial class Form1 : Form
    {
        public static bool action = false;
        public static string target_name = "miner";

        public Form1()
        {
            TopMost = true;
            InitializeComponent();
            timer.Interval = 2000;
        }

        private void power_Click(object sender, EventArgs e)
        {
            action = !action;            
            if (action)
            {
                power.Text = "Stop!";
                power.ForeColor = Color.Red;
                timer.Start();
            }
            else
            {
                Running.killBat();
                power.Text = "Start!";
                power.ForeColor = Color.Green;
                timer.Stop();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Running.killBat();
            Application.Exit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains(target_name)) > 0)
            { }
            else
            { Running.runBat(); }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.BalloonTipTitle = "Программа была спрятана!";
                notifyIcon.BalloonTipText = "Обратите внимание что программа была спрятана в трей и продолжит свою работу.";
                notifyIcon.ShowBalloonTip(3000);
            }
        }
    }
}
