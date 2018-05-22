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
            Task.Run(() => PriceData());            
            PlanData();
        }

        public static void PriceData()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.231",
                Port = 5672,
                UserName = "test",
                Password = "test"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "JPTPrice",
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
                            string connectString = Properties.Settings.Default.databaseString;
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
                    channel.BasicConsume("JPTPrice", false, consumer);

                    while (true)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }
        }
        public static void PlanData()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.1.231",
                Port = 5672,
                UserName = "test",
                Password = "test"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "JPTPlan",
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
                            string connectString = Properties.Settings.Default.databaseString;
                            DataTable table = (DataTable)xs.Deserialize(ms);
                            SqlConnection sqlConnection = new SqlConnection(connectString);
                            sqlConnection.Open();
                            SqlCommand command = sqlConnection.CreateCommand();
                            command.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                var matID = Convert.ToString(table.Rows[i]["CloudMatID"]);
                                command.CommandText = @"" + "proc_SY_InsertMaterial";
                                command.Parameters.Clear();
                                var paramMatID = command.Parameters.AddWithValue("@MatID", matID); //参数必须和存储过程中的参数名一致
                                command.ExecuteNonQuery();                                
                            }
                            command.CommandText = @"" + "UpdatePlan";
                            command.Parameters.Clear();
                            SqlParameter paramTable = command.Parameters.AddWithValue("@Sources", table); //参数必须和存储过程中的参数名一致
                            command.ExecuteNonQuery();
                            sqlConnection.Close();
                        }
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    channel.BasicConsume("JPTPlan", false, consumer);

                    while (true)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }
        }


    }
}
