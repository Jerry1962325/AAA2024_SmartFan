using System.Net;
using System.Net.Sockets;
using System.Text;

byte[] result = new byte[1024];
int myPort = 8080;
Socket server;
IPAddress ip = IPAddress.Parse("192.168.1.4");
server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
server.Bind(new IPEndPoint(ip, myPort));
server.Listen(10);
//AT + CIPSTART = "TCP","192.168.1.4",8080
Console.WriteLine("Ready");
while (true) { 
    Socket client =server.Accept();
    client.Send(Encoding.Default.GetBytes("I am Here!!!"));
    Socket myClient = client;
    while (true)
    {
        try
        {
            int length = myClient.Receive(result);
            Console.WriteLine("A new person IP:" + myClient.RemoteEndPoint.ToString());
            Console.WriteLine("A person say:" + Encoding.Default.GetString(result, 0, length));

            /*string sendlen = Convert.ToString(Console.ReadLine());
            client.Send(Encoding.Default.GetBytes(sendlen));
            Console.WriteLine("Has sent:" + sendlen);*/
        }
        catch (Exception ex) { 
            Console.WriteLine(ex.Message);
            myClient.Shutdown(SocketShutdown.Both);
            myClient.Close();
            break;
        }
        
    }
}
