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
        /// <summary>
        /// 异步方法，无需使用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task Post(byte[] data)
        {
            await Task.Run(() => Test(data));            
        }

        /// <summary>
        /// 异步方法，无需使用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
        public static void PostQueue(byte[] data)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "table_queue",
                        durable:false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                        routingKey: "table_queue",
                        basicProperties:properties,
                        body: data);
                }
            }
        }
        
    }
}
