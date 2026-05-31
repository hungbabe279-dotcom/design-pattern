using System;
using System.Windows.Forms;
using DocumentAnalysisSystem.Forms;

namespace DocumentAnalysisSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
