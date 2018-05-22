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

namespace CloudPlugin
{
    public class UpdatePrice:AbstractOperationServicePlugIn
    {
        public override void AfterExecuteOperationTransaction(AfterExecuteOperationTransaction e)
        {
            base.AfterExecuteOperationTransaction(e);
            var sql = "SELECT EntryID,MaterialID,MateriNumber,MaterialName,SupplierID,SupplierName,Price,Bitmap FROM dbo.SY_V_PRICELIST";
            using (DataSet ds = DBServiceHelper.ExecuteDataSet(Context, sql))
            {
                var table = ds.Tables[0];
                table.TableName = "JPTPrice";
                TableSender sender = new TableSender(table);
                sender.TransportData();
            }
            
        }
    }
}
