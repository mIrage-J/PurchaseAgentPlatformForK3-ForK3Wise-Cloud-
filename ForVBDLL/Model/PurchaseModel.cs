using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForVBDLL.Model
{

    public class PurchaseBillRoot
    {
        public string Creator { get; set; }
        public object[] NeedUpDateFields { get; set; }
        public object[] NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; } = "True";
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; } = "false";
        public string IsEntryBatchFill { get; set; } = "True";
        public ModelPurchaseBill Model { get; set; }
    }

    public class ModelPurchaseBill
    {
        public string FID { get; set; } = "0";
        public string FBillNo { get; set; }
        public Fcreatorid FCreatorId { get; set; } = new Fcreatorid();
        public string FCreateDate { get; set; } = DateTime.Now.ToString();
        public Fmodifierid FModifierId { get; set; }
        public string FModifyDate { get; set; }
        public F_SY_AUDITOR F_SY_AUDITOR { get; set; }
        public string F_SY_AUDITDATE { get; set; }
        public string F_SY_PURDATE { get; set; } = DateTime.Now.ToString();
        public F_SY_CUSTOMER F_SY_CUSTOMER { get; set; }
        public F_SY_Porgid F_SY_POrgId { get; set; } = new F_SY_Porgid();
        public string F_SY_Remarks { get; set; }
        public F_SY_SUPP F_SY_SUPP { get; set; }
        public F_SY_Currency F_SY_Currency { get; set; }
        public string F_SY_PLANORDER { get; set; }
        public string F_SY_PLNOFID { get; set; }
        public List<FentityPurchaseBill> FEntity { get; set; }
    }

    

    public class F_SY_AUDITOR
    {
        public string FUserID { get; set; } = "16394";
    }

    public class F_SY_CUSTOMER
    {
        public string FCUSTID { get; set; }
    }

    public class F_SY_Porgid
    {
        public string FNumber { get; set; } = "100";
    }

    public class F_SY_SUPP
    {
        public string FSUPPLIERID { get; set; }
    }

    

    public class FentityPurchaseBill
    {
        public string FEntryID { get; set; }
        public F_SY_MATERIAL F_SY_MATERIAL { get; set; }
        public string F_SY_QTY { get; set; }
        public F_SY_UNIT F_SY_UNIT { get; set; }
        public string F_SY_UNITPRICE { get; set; }
        public string F_SY_Amount { get; set; }
        public string F_SY_Text { get; set; }
        public string F_SY_SourceBillNo { get; set; }
        public string F_SY_SFentryID { get; set; }
        public string F_SY_PLEntryID { get; set; }
    }

    public class F_SY_MATERIAL
    {
        public string FMaterialID { get; set; }
    }

    public class F_SY_UNIT
    {
        public string FNumber { get; set; }
    }

}
