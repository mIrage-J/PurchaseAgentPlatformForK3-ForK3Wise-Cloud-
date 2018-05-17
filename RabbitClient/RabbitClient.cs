using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitClient
{
    class RabbitClient
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(PostData());
            Console.ReadLine();


        }

        public static string PostData()
        {
            bool isError = false;
            string result = "";

            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.150",
                Port = 5672,
                UserName = "test",
                Password = "test"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("table", "fanout");

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queueName, "table", "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        DataTable table = new DataTable();

                        var body = ea.Body;
                        MemoryStream ms = new MemoryStream(body);
                        IFormatter formatter = new BinaryFormatter();

                        try
                        {
                            table = formatter.Deserialize(ms) as DataTable;
                            result = $"Successed!Data:{ table.Rows[0][0]}: { table.Rows[0][1]}";
                        }
                        catch (InvalidOperationException)
                        {
                            isError = true;
                        }
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);


                    };
                    channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);
                }
            }
            return isError ? "尝试接受一个来自TableReceiver的Table，但是由二进制转换为DataTable时失败了" : result;


        }
        
    }
}
