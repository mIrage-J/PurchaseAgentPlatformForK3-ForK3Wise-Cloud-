using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ForVBDLL
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface TestInterface
    {
        string Test();
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class ClassTest : TestInterface
    {
        
    

    public string Test()
        {
            Pubulish pubulish = new Pubulish();
            var window = new Form1(pubulish);
            window.ShowDialog();
            return pubulish.Return();

        }
    }

    public class Pubulish {
        public delegate String ReturnInfo();
        public event ReturnInfo ButtonClick;
        public string Return()
        {
            if (ButtonClick != null)
            {
                return ButtonClick();
            }
            else
                return "";
        }

    }


    
}
