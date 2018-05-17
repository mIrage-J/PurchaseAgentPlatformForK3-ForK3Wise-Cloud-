using RabbitClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Purchase_Agent_Customer_Service
{
    /// <summary>
    /// 客户端
    /// </summary>
    public partial class PACustService : ServiceBase
    {
        public PACustService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Function();
        }

        private async void Function()
        {
            await  Task.Run(()=> RabbitClient.RabbitClient.GetData());
            
        }

        protected override void OnStop()
        {
        }


    }
}
