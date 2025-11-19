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

namespace _003.POTENTIOMETER_CONTROLLED_LED
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
            comboBox1.Items.Add("115200");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = Convert.ToInt32(comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox2.SelectedItem.ToString();
            serialPort1.Open();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                // trackBar1.Value integer değeri seri porta gönderiyoruz.
                // Mesela tek byte olarak gönderilebilir.

                try
                {
                    // Karakter veya byte olarak gönder
                    byte valueToSend = (byte)trackBar1.Value;
                    serialPort1.Write(new byte[] { valueToSend }, 0, 1);

                    // Alternatif olarak string gönder:
                    // serialPort1.Write(valueToSend.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri gönderirken hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seri port açık değil!");
            }
        }
    }
}
