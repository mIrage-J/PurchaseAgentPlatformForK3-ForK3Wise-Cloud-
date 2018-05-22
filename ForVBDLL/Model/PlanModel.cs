using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForVBDLL.Model
{
    public class PlanRootModel
    {
        public string Creator { get; set; } 
        public object[] NeedUpDateFields { get; set; }
        public object[] NeedReturnFields { get; set; }
        public string IsDeleteEntry { get; set; } = "True";
        public string SubSystemId { get; set; }
        public string IsVerifyBaseDataField { get; set; } = "false";
        public string IsEntryBatchFill { get; set; }
        public Model Model { get; set; }
    }

    public class Model
    {
        public string FID { get; set; } = "0";
        public string FBillNo { get; set; }
        public Fcreatorid FCreatorId { get; set; } = new Fcreatorid();
        public string FCreateDate { get; set; } = DateTime.Now.ToString();
        public Fmodifierid FModifierId { get; set; }
        public string FModifyDate { get; set; }
        public F_SY_Aduituserid F_SY_AduitUserId { get; set; }
        public string F_SY_AduitDate { get; set; }
        public F_SY_Fbuserid F_SY_FBUserId { get; set; } = new F_SY_Fbuserid();
        public string F_SY_FBDate { get; set; }
        public F_SY_Rfqorgid F_SY_RFQOrgId { get; set; } = new F_SY_Rfqorgid();
        public string F_SY_PLANORDER { get; set; }
        public string F_SY_PLNOFID { get; set; }
        public F_SY_Receiveorgid F_SY_ReceiveOrgId { get; set; } = new F_SY_Receiveorgid();
        public string F_SY_Remarks { get; set; }
        public F_SY_Currency F_SY_Currency { get; set; } = new F_SY_Currency();
        public F_SY_Customer F_SY_Customer { get; set; } = new F_SY_Customer();
        public List<Fentity> FEntity { get; set; }
    }

    public class Fcreatorid
    {
        public string FUserID { get; set; } = "16394";
    }

    public class Fmodifierid
    {
        public string FUserID { get; set; }
    }

    public class F_SY_Aduituserid
    {
        public string FUserID { get; set; }
    }

    public class F_SY_Fbuserid
    {
        public string FUserID { get; set; }
    }

    public class F_SY_Rfqorgid
    {
        public string FNumber { get; set; } = "100";
    }

    public class F_SY_Receiveorgid
    {
        public string FNumber { get; set; } = "100";
    }

    public class F_SY_Currency
    {
        public string FNumber { get; set; } = "PRE001";
    }

    public class F_SY_Customer
    {
        public string FNumber { get; set; } = "CUST0001";
    }

    public class Fentity
    {
        public string FEntryID { get; set; }
        public string F_SY_KeyWord { get; set; }
        public string F_SY_RCOUNT { get; set; }
        public F_SY_Unitid F_SY_UnitID { get; set; }
        public string F_SY_Amount { get; set; }
        public string F_SY_ShipDate { get; set; }
        public F_SY_Material F_SY_Material { get; set; }
        public F_SY_Supplier F_SY_Supplier { get; set; }
        public F_SY_Sunitid F_SY_SUnitID { get; set; }
        public string F_SY_RAmount { get; set; }
        public string F_SY_RAQAmount { get; set; }
        public string F_SY_PLEntryID { get; set; }
        public string F_SY_SUPPAmount { get; set; }
        public string F_SY_ShipCount { get; set; }

        public Fentity(string matID="",string supID = "", string keyWord = "", string actualPrice = "", string demanPrice = "", string qty = "", string actualSum = "", string entryid = "")
        {
            F_SY_Material = new F_SY_Material() { FMaterialID = matID };
            F_SY_Supplier = new F_SY_Supplier() { FSUPPLIERID = supID };
            F_SY_KeyWord = keyWord;
            F_SY_Amount = demanPrice;
            F_SY_RCOUNT = qty;
            F_SY_RAmount = actualPrice;
            F_SY_RAQAmount = actualSum;
            F_SY_PLEntryID = entryid;
        }
    }

    public class F_SY_Unitid
    {
        public string FNumber { get; set; }
    }

    public class F_SY_Material
    {
        public string FMaterialID { get; set; }
    }

    public class F_SY_Supplier
    {
        public string FSUPPLIERID { get; set; }
    }

    public class F_SY_Sunitid
    {
        public string FNumber { get; set; }
    }
}
