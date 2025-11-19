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

namespace PWM
{
    public partial class Form1 : Form
    {
        int position;
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.Open();
                label3.Text = "bağlantı sağlandi.";
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
                label3.Text = "bağlantı kapatildi.";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "hata:");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            position = 500;
            write_Angle(position);
            label5.Text = "Açı: 0°";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            position = 850;
            write_Angle(position);
            label5.Text = "Açı: 45°";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            position = 1200;
            write_Angle(position);
            label5.Text = "Açı: 90°";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            position = 1700;
            write_Angle(position);
            label5.Text = "Açı: 135°";
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int position = trackBar1.Value;
            label5.Text = position.ToString() + "%";

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine(position.ToString());
            }
        }

        private void write_Angle(int value)
        {
            try
            {
                if(serialPort1.IsOpen)
                {
                    serialPort1.WriteLine(value.ToString());
                   // label5.Text = "Açı: " + value.ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "hata:");
            }
        }

        
    }
}
