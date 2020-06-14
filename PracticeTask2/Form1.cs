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
            string[] data = File.ReadAllLines("INPUT.TXT"); // Считывание данных
            string ping_name = data[0].Split(' ')[1]; // Выделение имени из первой строки
            string[] packages = new string[data.Length - 1];
            int count_loss = 0; // Подсчет потерянных пакетов
            int sum = 0, min = (int)Math.Pow(10, 4), max = 0;
            for (int i = 0; i < packages.Length; i++)
            {
                if (data[i + 1] == "Time out") // Пакет утерян
                {
                    count_loss++; // Счетчик утерянных пакетов увеличился
                    packages[i] = data[i + 1]; // Запись данных в массив данных о пакетах
                }
                else
                {
                    packages[i] = data[i + 1].Split(' ')[3].Split('=')[1]; // Выделение числа (времени) из строки
                    min = Math.Min(min, int.Parse(packages[i])); // Определение нового минимального времени
                    max = Math.Max(max, int.Parse(packages[i])); // Определение нового максимального времени
                    sum += int.Parse(packages[i]); // К сумме прибавляется текущее число
                }
            }
            double loss = (double)count_loss / packages.Length; // Подсчет доли утерянных пакетов
            loss = Math.Round(loss * 100, 2); // Представление в виде процентов с округлением до 2 знаков после запятой
            string output = $"Ping statistics for {ping_name}: \n" +
                $"Packets: Sent = {packages.Length} Received = {packages.Length - count_loss} Lost = {count_loss} ({loss}% loss)";
            if (count_loss != packages.Length) // Если утеряны не все пакеты
            {
                int average = (int)Math.Round((double)sum / (packages.Length - count_loss), MidpointRounding.AwayFromZero); // Подсчет среднего значения
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
