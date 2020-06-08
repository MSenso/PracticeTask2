using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeTask2
{
    public partial class Form1 : Form
    {
        static string ping_name;
        public Form1()
        {
            InitializeComponent();
        }
        void Run()
        {
            string[] data = File.ReadAllLines("INPUT.TXT");
            string ping_name = data[0].Split(' ')[1];
            string[] packages = new string[data.Length - 1];
            int count_loss = 0;
            int average = 0, min = (int)Math.Pow(10, 4), max = 0;
            for (int i = 0; i < packages.Length; i++)
            {
                if (data[i + 1] == "Time out")
                {
                    count_loss++;
                    packages[i] = data[i + 1];
                }
                else
                {
                    packages[i] = data[i + 1].Split(' ')[3].Split('=')[1];
                    min = Math.Min(min, int.Parse(packages[i]));
                    max = Math.Max(max, int.Parse(packages[i]));
                    average += int.Parse(packages[i]);
                }
            }
            double loss = (double)count_loss / packages.Length;
            loss = Math.Round(loss * 100, 2);
            string output = $"Ping statistics for {ping_name}: \n" +
                $"Packets: Sent = {packages.Length} Received = {packages.Length - count_loss} Lost = {count_loss} ({loss}% loss)";
            if (count_loss != packages.Length)
            {
                average = (int)Math.Round((double)average / (packages.Length - count_loss), MidpointRounding.AwayFromZero);
                output += $"\n" +
                $"Approximate round trip times: \n" +
                $"Minimum = {min} Maximum = {max} Average = {average}";
            }
            output = output.Replace("\n", Environment.NewLine);
            File.WriteAllText("OUTPUT.TXT", output);
            textBox1.Text = output;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Run();
        }
    }
}
