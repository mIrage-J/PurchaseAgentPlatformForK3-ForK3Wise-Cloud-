using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RabbitClient
{
    class RabbitClient
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
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
                    channel.QueueDeclare(queue: "table_queue",
                           durable: false,
                           exclusive: false, 
                           autoDelete: false, 
                           arguments: null);



                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        XmlSerializer xs = new XmlSerializer(typeof(DataTable));

                        using (MemoryStream ms = new MemoryStream(body))
                        {
                            DataTable table = (DataTable)xs.Deserialize(ms);
                            SqlConnection sqlConnection = new SqlConnection("data source=192.168.1.150;initial catalog=Test;persist security info=True;user id=sa;password=123;MultipleActiveResultSets=True;");
                            sqlConnection.Open();
                            SqlCommand command = new SqlCommand($"insert into mqtest values({table.Rows[0][0]},'{table.Rows[0][1]}')",sqlConnection);
                            command.ExecuteNonQuery();

                            Console.WriteLine($"{table.Rows[0][0]},{table.Rows[0][1]}");
                        }
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume("table_queue", false, consumer);
                    
                    Console.ReadKey();
                }
            }
            

        }

        

    }
}
