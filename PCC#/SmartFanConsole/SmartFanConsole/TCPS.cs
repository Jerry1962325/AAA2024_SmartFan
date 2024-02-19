using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;



namespace SmartFanConsole
{
    
    internal class TCPS
    {
        
         public Socket TCP_New_Server() {
            byte[] result = new byte[1024];
            int myPort = 8080;
            Socket server;
            IPAddress ip = IPAddress.Parse("192.168.0.103");
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(ip, myPort));
            server.Listen(10);
            return server;
        }

        public int TCP_send(string str, Socket client, Socket myClient) {
            try
            {
                //string sendlen = richTextBox1.Text;
                client.Send(Encoding.Default.GetBytes(str));
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                myClient.Shutdown(SocketShutdown.Both);
                myClient.Close();
                return 1;
            }
        }
    }
}
