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
    public class RabbitClient
    {
        public static void GetData()
        {            
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

                    consumer.Received +=  (model, ea) =>
                    {
                        var body = ea.Body;
                        XmlSerializer xs = new XmlSerializer(typeof(DataTable));

                        using (MemoryStream ms = new MemoryStream(body))
                        {
                            string connectString= Properties.Settings.Default.databaseString;
                            DataTable table = (DataTable)xs.Deserialize(ms);
                            SqlConnection sqlConnection = new SqlConnection(connectString);
                            sqlConnection.Open();
                            SqlCommand command = sqlConnection.CreateCommand();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = @"" + "MergeIntoPriceCatalogue";
                            SqlParameter paramTable = command.Parameters.AddWithValue("@Sources", table); //参数必须和存储过程中的参数名一致
                            command.ExecuteNonQuery();
                            sqlConnection.Close();
                        }
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume("table_queue", false, consumer);

                    while (true)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }
        }

        

        

    }
}
