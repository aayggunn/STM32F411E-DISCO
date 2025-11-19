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

namespace _006.HC_SR04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.DataReceived += SerialPort1_DataReceived;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            { 
            serialPort1.PortName = comboBox2.Text;
            serialPort1.BaudRate = Convert.ToInt32(comboBox1.Text);
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Parity = Parity.None;
            serialPort1.Open();
            label5.Text = "BAĞLANTI KURULDU";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA:");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                label5.Text = "BAĞLANTI KAPATILDI";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HATA:");
            }
        }
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string veri = serialPort1.ReadLine(); // \n ile biten veri olmalı
                Invoke(new Action(() => VeriIsle(veri)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri alınırken hata: " + ex.Message);
            }
        }
        private void VeriIsle(string veri)
        {
            try
            {
                // Örnek veri: MESAFE:120|PWM:85
                string[] bolumler = veri.Split('|');

                foreach (string bolum in bolumler)
                {
                    if (bolum.StartsWith("MESAFE:"))
                    {
                        string mesafeStr = bolum.Replace("MESAFE:", "").Trim();
                        if (int.TryParse(mesafeStr, out int mesafe))
                        {
                            int deger = Math.Min(progressBar1.Maximum, Math.Max(progressBar1.Minimum, mesafe));
                            progressBar1.Value = 20- deger;
                            label3.Text = $"{deger} cm";
                        }
                        else
                        {
                            label3.Text = "Geçersiz";
                        }
                    }
                    else if (bolum.StartsWith("PWM:"))
                    {
                        string pwmStr = bolum.Replace("PWM:", "").Trim();
                        if (int.TryParse(pwmStr, out int pwm))
                        {
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri işlenirken hata: " + ex.Message + "\nGelen veri: " + veri);
            }
        }

    }
}
