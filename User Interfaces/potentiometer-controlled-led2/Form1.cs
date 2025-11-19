using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace potentiometer_controlled_led2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox2.Items.Add(port);
            }

            comboBox1.Items.Add(115200);
            comboBox1.Items.Add(9600);
            serialPort1.DataReceived += serialPort1_DataReceived;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Convert.ToInt32(comboBox1.SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox2.SelectedItem.ToString();

            try
            {
                if (!serialPort1.IsOpen)
                    serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Port açılamadı: " + ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int pwmValue = trackBar1.Value;
            labelPwm.Text = pwmValue.ToString() + "%";

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine(pwmValue.ToString());
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incoming = serialPort1.ReadLine();
            Invoke(new Action(() =>
            {
                textBox1.AppendText(incoming + Environment.NewLine);
            }));
        }
    }
}
