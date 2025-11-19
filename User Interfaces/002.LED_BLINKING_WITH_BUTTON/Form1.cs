using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace _002.LED_BLINKING_WITH_BUTTON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Convert.ToInt32(comboBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach(string port in ports)
            {
                comboBox2.Items.Add(port);
            }
            comboBox1.Items.Add("115200");

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox2.SelectedItem.ToString();
            serialPort1.Open();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string veri = serialPort1.ReadExisting();  // Tüm mevcut gelen veriyi al

            this.Invoke(new MethodInvoker(delegate
            {
                if (veri.Contains("1"))
                {
                    label3.Text = "LED ON";
                }
                else if (veri.Contains("2"))
                {
                    label3.Text = "LED OFF";
                }
                else
                {
                    label3.Text = veri; // Diğer gelen mesajları da göster
                }
            }));
        }

    }
}
