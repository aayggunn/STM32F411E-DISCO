using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ADC
{
    public partial class Form1 : Form
    {
        private string data;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("115200");

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    string incoming = serialPort1.ReadLine();
                    data = incoming;  // global değişken
                    this.BeginInvoke(new EventHandler(displayData_event)); // BeginInvoke daha uygundur
                }
            }
            catch (IOException ioex)
            {
                // Seri port kapanırken okuma denemesi yapılmış olabilir
                Console.WriteLine("IO Hatası: " + ioex.Message);
            }
            catch (InvalidOperationException)
            {
                // Port zaten kapalı olabilir
                Console.WriteLine("Port açık değil.");
            }
            catch (Exception ex)
            {
                // Genel hata yakalama
                MessageBox.Show("DataReceived Hatası: " + ex.Message);
            }
        }


        private void displayData_event(object sender, EventArgs e)
        {
            if (int.TryParse(data, out int pwm))
            {
                pwm = Math.Max(progressBar1.Minimum, Math.Min(progressBar1.Maximum, pwm));
                progressBar1.Value = pwm;
                label6.Text = pwm.ToString();
            }
            else
            {
                label6.Text = "Hatalı veri: " + data;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();
                label3.Text = "bağlantı sağlandı...";
                label3.BackColor = Color.Green;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "hata:");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                label3.Text = "bağlanti kapatildi...";
                label3.BackColor = Color.Red;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "hata:");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }
    }
}
