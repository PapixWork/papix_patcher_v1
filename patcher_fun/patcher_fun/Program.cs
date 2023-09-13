// Papix Work ~ https://metin2.dev/profile/47045-papix/
using System;
using System.Windows.Forms;

namespace patcher_fun
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
