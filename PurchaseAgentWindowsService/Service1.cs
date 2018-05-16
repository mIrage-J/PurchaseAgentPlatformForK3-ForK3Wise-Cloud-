using PurchaseAgentWindowsServiceTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceFramwork
{
    public partial class PurchaseAgentWindowsService : ServiceBase
    {
        public PurchaseAgentWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RunTool();
        }

        protected override void OnStop()
        {
        }

        private async void RunTool()
        {
            PAWindowsServiceTool tool = new PAWindowsServiceTool();
            await Task.Run(() => tool.Run());
        }
    }
}
