using Kingdee.BOS.Core.DynamicForm.PlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.ServiceHelper;
using System.Data;
using DataToWindowsService;
using Kingdee.BOS.Core.Bill.PlugIn;

namespace CloudPlugin
{
    public class UpdatePlan : AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            foreach (var dataEntity in e.DataEntitys)
            {
                var a = Convert.ToString(dataEntity["ID"]);
                var sql = $"SELECT F_SY_PLEntryID AS EntryID,F_SY_MATERIAL AS CloudMatID,sup.FNAME AS [CloudSupplierName],de.F_SY_RAmount AS [Price] ,F_SY_SUPPLIER as SupplierID FROM SY_T_KeyRFQDetail de LEFT JOIN dbo.T_BD_SUPPLIER_L sup ON de.F_SY_SUPPLIER=sup.FSUPPLIERID WHERE FID ={a}";
                using (DataSet ds = DBServiceHelper.ExecuteDataSet(Context, sql))
                {
                    var table = ds.Tables[0];
                    table.TableName = "JPTPlan";
                    TableSender sender = new TableSender(table);
                    sender.TransportData();
                }
            }
        }
    }
   
}
