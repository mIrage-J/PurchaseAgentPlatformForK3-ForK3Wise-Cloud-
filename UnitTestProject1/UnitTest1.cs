using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForVBDLL;
using System.Windows.Forms;

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
    }
}
