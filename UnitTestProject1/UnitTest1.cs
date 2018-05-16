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
        [TestMethod]
        public void TestMethod1()
        {
            ClassTest test = new ClassTest();
            test.Test();
        }

        [TestMethod]
        public void TestTableReceiver()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            var row = table.NewRow();
            row[0] = 1;
            row[1] = "Adam";
            table.Rows.Add(row);

            TableReceiver tableReceiver = new TableReceiver(table);
            Console.WriteLine( tableReceiver.TransportData());
        }
    }
}
