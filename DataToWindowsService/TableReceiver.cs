using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataToWindowsService
{
    public class TableReceiver
    {
        DataTable table;
        string IPAdress;
        int port;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">要传递到Windows服务的DataTable</param>
        /// <param name="IPAdress">默认为本机，不能用计算机名，格式必须保持与默认值格式一致</param>
        /// <param name="port">Windows服务的端口</param>
        public TableReceiver(DataTable table,string IPAdress= "127.0.0.1",int port= 8999)
        {
            this.table = table;
            this.IPAdress = IPAdress;
            this.port = port;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Windows服务返回的信息</returns>
        [STAThread]
        public string TransportData()
        {
            byte[] data = new byte[1024];
            Socket newClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipPoint;
            try
            {
                ipPoint = new IPEndPoint(IPAddress.Parse(IPAdress), port);
            }
            catch (FormatException)
            {
                throw new Exception("TableReceiver输入的IP地址格式有误");                
            }

            //因为客户端只是用来向特定的服务器发送信息，所以不需要绑定本机的IP和端口。不需要监听。
            try
            {
                newClient.Connect(ipPoint);
            }
            catch (SocketException e)
            {
                throw new Exception($"unable to connect to server, exception infomation:{e.ToString()}");
            }

            int recv;
            MemoryStream ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, table);
            newClient.Send(ms.GetBuffer());
            ms.Dispose();
            data = new byte[1024];
            recv = newClient.Receive(data);
            string stringdata = Encoding.ASCII.GetString(data, 0, recv);           
            newClient.Shutdown(SocketShutdown.Send);
            newClient.Close();
            return stringdata;
        }
    }
}
