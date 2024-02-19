using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFanConsole
{
    public partial class Form1 : Form
    {
        TCPS tCPS = new TCPS();
        Socket server;
        Socket client;
        Socket myClient;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = tCPS.TCP_New_Server();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!tableLayoutPanel1.Enabled)
            {
                tCPS.TCP_send("0+50", client, myClient);
                pictureBox1.Image = SmartFanConsole.Properties.Resources.FAN_RUN;
                tableLayoutPanel1.Enabled = true;
                progressBar2.Value = 50;
            }
            else
            {
                tCPS.TCP_send("0+0", client, myClient);
                pictureBox1.Image = SmartFanConsole.Properties.Resources.FAN_STOP;
                tableLayoutPanel1.Enabled = false;
                progressBar2.Value = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = server.Accept();
            myClient = client;
            textBox1.Text = myClient.RemoteEndPoint.ToString().Split(':')[0];
            textBox2.Text = myClient.RemoteEndPoint.ToString().Split(':')[1];
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = 100;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tCPS.TCP_send(richTextBox1.Text, client, myClient);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tCPS.TCP_send("0+25", client, myClient);
            progressBar2.Value = 25;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tCPS.TCP_send("0+50", client, myClient);
            progressBar2.Value = 50;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            tCPS.TCP_send("0+75", client, myClient);
            progressBar2.Value = 75;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            tCPS.TCP_send("0+100", client, myClient);
            progressBar2.Value = 100;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            tCPS.TCP_send("2+" + numericUpDown1.Value*5, client, myClient);
            Thread.Sleep(100);
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = !numericUpDown1.Enabled;
            trackBar1.Enabled = !trackBar1.Enabled;
        }

        private void radioButton6_MouseDown(object sender, MouseEventArgs e)
        {
            tCPS.TCP_send("1+50", client, myClient); tCPS.TCP_send("1+50", client, myClient);
        }

        private void radioButton8_MouseDown(object sender, MouseEventArgs e)
        {
            tCPS.TCP_send("1+51", client, myClient); tCPS.TCP_send("1+51", client, myClient);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tCPS.TCP_send("4+00", client, myClient);
        }
    }
}
