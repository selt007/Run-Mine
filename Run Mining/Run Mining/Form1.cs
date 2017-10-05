using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Run_Mining
{
    public partial class Form1 : Form
    {
        public static bool action = false;

        public Form1()
        {
            TopMost = true;
            InitializeComponent();
        }

        private void power_Click(object sender, EventArgs e)
        {
            action = !action;
            
            if (action)
            {
                Running.runBat();
                power.Text = "Stop!";
                power.ForeColor = Color.Red;
            }
            else {
                Running.killBat();
                power.Text = "Start!";
                power.ForeColor = Color.Green;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Running.killBat();
            Application.Exit();
        }
    }
}
