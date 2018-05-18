using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForVBDLL;
using System.Windows.Forms;
using System.Data;
using DataToWindowsService;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public void TestMethod1()
        {
            ClassTest test = new ClassTest();
            test.Test();
        }

        [TestMethod]
        public void TestTableReceiver()
        {
            //DataTable table = new DataTable() { TableName="TableName"};            
            //table.Columns.Add("ID",typeof(int));
            //table.Columns.Add("Name",typeof(string));
            //var row = table.NewRow();
            //row[0] = 1;
            //row[1] = "Adam";
            //table.Rows.Add(row);
            DataTable table = new DataTable() { TableName = "TableName" };
            SqlConnection conn = new SqlConnection("data source=192.168.1.150;initial catalog=MW;persist security info=True;user id=sa;password=123;MultipleActiveResultSets=True;");
            SqlCommand comm = new SqlCommand( "SELECT * FROM dbo.SY_V_PRICELIST",conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(table);


            TableSender tableReceiver = new TableSender(table);
            tableReceiver.TransportData();
        }

        [TestMethod]
        public void TransportMemory()
        {
            DataTable table = new DataTable() { TableName = "TableName" };
            SqlConnection conn = new SqlConnection("data source=192.168.1.150;initial catalog=MW;persist security info=True;user id=sa;password=123;MultipleActiveResultSets=True;");
            SqlCommand comm = new SqlCommand("SELECT * FROM dbo.SY_V_PRICELIST", conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            adapter.Fill(table);
            byte[] bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                serializer.Serialize(ms, table);
                bytes = ms.ToArray();

            }

            XmlSerializer xs = new XmlSerializer(typeof(DataTable));
            using (MemoryStream ms = new MemoryStream(bytes))
            {

                DataTable table2 = (DataTable)xs.Deserialize(ms);
            }
        }
    }
}
