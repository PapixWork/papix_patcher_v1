// Papix Work ~ https://metin2.dev/profile/47045-papix/
using patcher_fun.Properties;
using patcher_fun.Settings;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace patcher_fun
{
    public partial class Form1 : Form
    {
        // Instance
        public static Form1 Instance { get; private set; }

        public Form1()
        {
            // Component
            InitializeComponent();
            // Instance
            Instance = this;
            // Load icon
            this.Icon = Properties.Resources.Icon;
            // Set App Name
            this.Text = Settings.Config.Name;
            // Clean label1
            label1.Text = "";
            // Load text to Play Button
            button1.Text = Languagues.Text.Play;
            // Load text to Settings Button
            button2.Text = Languagues.Text.Settings;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ProgressBarAnimation()
        {
            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Value = i;

                System.Threading.Thread.Sleep(1);
            }
        }

        public void SetLabelText(string text)
        {
            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(Config.BinaryProcessName).Length > 0)
            {
                MessageBox.Show(Languagues.Text.CloseGame, Config.Name);
                MessageBox.Show(Languagues.Text.CloseProcess + " " + Config.BinaryProcessName + ".", Config.Name);
                Application.Exit();
            }
            else
            {
                ProgressBarAnimation();
                button1.Enabled = false;
                FileDownloader.UpdateFilesFromRemote();
                label1.Text = Languagues.Text.Completed;
                button1.Enabled = true;
            }

            if (File.Exists(Config.Binary))
            {
                Process.Start(Config.Binary);
            }
            else
            {
                MessageBox.Show(Config.Binary + " " + Languagues.Text.FileNotFound, Config.Name);

            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Config.Configuration))
            {
                System.Diagnostics.Process.Start(Config.Configuration);
            }
            else
            {
                MessageBox.Show(Config.Configuration + " " + Languagues.Text.FileNotFound, Config.Name);

            }
        }
    }
}
