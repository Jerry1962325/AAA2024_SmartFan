using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMAPTest
{
    public partial class Form1 : Form
    {
        Process proc;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 长度
                long t = 100;
                // 创建内存块，test1,其他语言利用这个内存块名字就能找到内存块。
                var mmf = MemoryMappedFile.CreateOrOpen("test1", t, MemoryMappedFileAccess.ReadWrite);
                var viewAccessor = mmf.CreateViewAccessor(0, t);
                string s = richTextBox1.Text;
                viewAccessor.Write(0, s.Length); ;
                viewAccessor.WriteArray<byte>(0, System.Text.Encoding.Default.GetBytes(s), 0, s.Length);
                 MessageBox.Show("write ok");
            }
            catch (System.Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
               var mmf = MemoryMappedFile.OpenExisting("global_share_memory");
            char[] charsInMMf = new char[6];
            var mvstream = mmf.CreateViewStream();
            StreamReader reader = new StreamReader(mvstream);
            string str = reader.ReadLine();
            richTextBox2.Text = str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //启动外部程序
                string appName = @"D:\AAA2024_SmartFan\CVPy\共享writer.py";
                string startApp = @"D:\PF\PY\pythonw.exe";
                proc = Process.Start(startApp,appName);
                if (proc != null)
                {
                    //监视进程退出
                    proc.EnableRaisingEvents = true;
                    //指定退出事件方法
                    //proc.Exited += new EventHandler(proc_Exited);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            proc.Kill();
        }
    }
}
