using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Run_Mining
{
    public partial class Form1 : Form
    {
        public static bool action = false;
        public static string target_name = "miner",json;
        public static double rate;

        public Form1()
        {
            TopMost = true;
            InitializeComponent();
            timer.Interval = 15000;
            timer1.Interval = 60000;
        }

        private void power_Click(object sender, EventArgs e)
        {
            action = !action;            
            if (action)
            {
                Running.runBat();
                power.Text = "Stop!";
                power.ForeColor = Color.Red;
                timer.Start();
                timer1.Start();
            }
            else
            {
                Running.killBat();
                power.Text = "Start!";
                power.ForeColor = Color.Green;
                timer.Stop();
                timer1.Stop();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (StreamReader get = new StreamReader
                    (WebRequest.Create(@"https://api-zcash.flypool.org/miner/t1U7odrjdAwAf4XGeA1eCrjhbKx5sZmVJKz/worker/rig1/currentStats")
                    .GetResponse().GetResponseStream()))
                json = get.ReadToEnd();
            var Result = JsonConvert.DeserializeObject<RootObject>(json);

            rate = Result.data.currentHashrate;
            if (rate > 400) { }
            else { Running.killBat(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.BalloonTipTitle = "Run Mine была спрятана!";
                notifyIcon.BalloonTipText = "Обратите внимание что программа была спрятана в трей и продолжит свою работу.";
                notifyIcon.ShowBalloonTip(1500);
            }
        }
    }
}
