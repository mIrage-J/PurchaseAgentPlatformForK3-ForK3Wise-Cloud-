using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace PAServiceSocketTool
{
    public class SocketTool
    {
        int port;
        public SocketTool(int port=8999)
        {
            this.port = port;
        }

        
        public void Run()
        {
            int recv;
            byte[] data = new byte[1024];//缓存客户端发送的信息，socket传递的信息为字节数组
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8999);//这里是指监听所有IP地址，然后8999是本机打开的端口
            EventLog log = new EventLog();
            log.Source = "PurchaseAgentWindowsService";

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(iPEndPoint);//绑定
            socket.Listen(10);//监听


            
            while (true)
            {
                Socket client = socket.Accept();

                IPEndPoint clientIP = (IPEndPoint)client.RemoteEndPoint;

                //Console.WriteLine("connect with client:" + clientIP.Address + "at port:" + clientIP.Port);

                //string welcome = "Welcome!";
                //data = Encoding.ASCII.GetBytes(welcome);
                //client.Send(data, data.Length, SocketFlags.None);
                while (true)
                {
                    data = new byte[client.ReceiveBufferSize];    //almost1GB
                    recv = client.Receive(data);                    
                    Console.WriteLine("recv=" + recv);
                    if (recv == 0)
                    {
                        data = null;
                        break;
                    }

                    MemoryStream ms = new MemoryStream(data);
                    IFormatter formatter = new BinaryFormatter();

                    DataTable table;
                    string excepiton = "";
                    string succeed = "receive succeed";
                    try
                    {
                         table= formatter.Deserialize(ms) as DataTable;
                    }
                    catch(InvalidOperationException)
                    {
                        excepiton = "尝试接受一个来自TableReceiver的Table，但是由二进制转换为DataTable时失败了";
                    }
                    
                    
                    data = Encoding.ASCII.GetBytes(excepiton==""?excepiton:succeed);
                    client.Send(data, data.Length, SocketFlags.None);

                    //接收完成，写入Windows应用程序日志
                    if (excepiton == "")
                        log.WriteEntry(succeed, EventLogEntryType.Information);
                    else
                        log.WriteEntry(excepiton, EventLogEntryType.Error);

                }
                //Console.WriteLine("Disconnected from " + clientIP.Address);
                log.WriteEntry("Disconnected from " + clientIP.Address, EventLogEntryType.Information);


                client.Close();
                //Console.WriteLine("Waiting for a client ......");
                log.WriteEntry("Waiting for a client ......", EventLogEntryType.Information);
            }

            
        }

        
        
    }
}
