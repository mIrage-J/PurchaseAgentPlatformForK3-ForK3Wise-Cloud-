using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitServer
{
    public class RabbitServerTool
    {
        public static async Task Post(byte[] data)
        {
            await Task.Run(() => Test(data));            
        }

        static void Test(byte[] data)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

           
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare("table", "fanout");

                    channel.BasicPublish(exchange: "table",
                    routingKey: "",                    
                    body: data);
                }
            }
        }
    }
}
