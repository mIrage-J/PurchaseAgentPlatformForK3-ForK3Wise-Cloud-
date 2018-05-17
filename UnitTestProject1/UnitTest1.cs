using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForVBDLL;
using System.Windows.Forms;
using System.Data;
using DataToWindowsService;

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
            DataTable table = new DataTable() { TableName="TableName"};            
            table.Columns.Add("ID",typeof(int));
            table.Columns.Add("Name",typeof(string));
            var row = table.NewRow();
            row[0] = 1;
            row[1] = "Adam";
            table.Rows.Add(row);

            TableSender tableReceiver = new TableSender(table);
            tableReceiver.TransportData();
        }
    }
}
