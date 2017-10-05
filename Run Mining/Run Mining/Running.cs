using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Run_Mining
{
    class Running
    {
        public static void runBat()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("fypool.bat");
                startInfo.WorkingDirectory = Path.GetDirectoryName(startInfo.FileName);
                Process.Start(startInfo);
            }
            catch {
                MessageBox.Show("File fypool.bat not found! " +
                "The application must be in the same folder as the files!");
            }
        }

        public static void killBat()
        {
            Process[] local_procs = Process.GetProcesses();
            try
            {
                Process target_proc = local_procs.First(p => p.ProcessName == Form1.target_name);
                target_proc.Kill();

                try
                {
                    ProcessStartInfo powercfg = new ProcessStartInfo("mineoff.bat");
                    powercfg.WorkingDirectory = Path.GetDirectoryName(powercfg.FileName);
                    powercfg.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(powercfg);
                }
                catch {
                    MessageBox.Show("File mineoff.bat not found! " +
                    "The application must be in the same folder as the files!");
                }
            }
            catch (InvalidOperationException)
            {
                Console.Beep(500,500);
            }
        }
    }
}
