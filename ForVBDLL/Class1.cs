using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ForVBDLL.Model;
using SY.WebApiTool.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ForVBDLL
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface TestInterface
    {
        string[] Test(string keyWord);
        void PushToCloud(bool isPlan, string billID,string customerID);
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class ClassTest : TestInterface
    {
        
        

        public string[] Test(string keyWord)
        {
            Pubulish pubulish = new Pubulish();
            var window = new Form1(pubulish,keyWord);
            window.ShowDialog();
            return pubulish.Return();

        }
        

        public void PushToCloud(bool isPlan, string billID, string customerID = "120031")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Url = "http://192.168.1.231/k3cloud/Kingdee.BOS.WebApi.ServicesStub.AuthService.ValidateUser.common.kdsvc";
            List<object> parameters = new List<object>();
            parameters.Add("5af68f80224ac6");//帐套Id
            parameters.Add("Administrator");//用户名
            parameters.Add("888888");//密码
            parameters.Add(2052);
            httpClient.Content = JsonConvert.SerializeObject(parameters);
            var iResult = JObject.Parse(httpClient.AsyncRequest())["LoginResultType"].Value<int>();

            if (isPlan)
            {
                PlanRootModel rootobject = new PlanRootModel();
                rootobject.Model = new Model.Model() ;
                var entity = rootobject.Model.FEntity = new List<Fentity>();
                PlanPush planPush = new PlanPush();
                var plan = planPush.ToCloudPlan.Where(p => p.BillID.ToString() == billID);
                foreach (var entry in plan)
                {
                    entity.Add(new Fentity(
                        matID: Convert.ToString(entry.CloudMatID),
                        supID: Convert.ToString(entry.Supplier),
                        keyWord: Convert.ToString(entry.Deman),
                        actualPrice: Convert.ToString(entry.Price),
                        demanPrice: Convert.ToString(entry.PlanPrice),
                        qty: Convert.ToString(entry.Qty),
                        actualSum: Convert.ToString((entry.Price == 0 ? entry.PlanPrice : entry.Price) * entry.Qty),
                        entryid: Convert.ToString(entry.EntryID)
                        ));
                    rootobject.Model.F_SY_PLANORDER = entry.BillNo;
                    rootobject.Model.F_SY_PLNOFID = Convert.ToString(entry.BillID);
                }

                if (iResult == 1)
                {
                    httpClient = new HttpClient();
                    httpClient.Url = "http://192.168.1.231/K3Cloud/Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save.common.kdsvc";
                    parameters = new List<object>();
                    parameters.Add("SY_KeyRFQ");
                    parameters.Add(JsonConvert.SerializeObject(rootobject));
                    httpClient.Content = JsonConvert.SerializeObject(parameters);
                    var result = httpClient.AsyncRequest();
                }
            }
            else
            {
                PlanPush planPush = new PlanPush();
                var plans = planPush.ToCloudPlan.Where(p => p.BillID.ToString() == billID);
                
                foreach (var plan in plans)
                {
                    PurchaseBillRoot purchaseBillRoot = new PurchaseBillRoot()
                    {
                        Model = new ModelPurchaseBill()
                        {
                            F_SY_PLANORDER = plan.BillNo,
                            F_SY_PLNOFID = Convert.ToString(plan.BillID),
                            F_SY_CUSTOMER = new F_SY_CUSTOMER() { FCUSTID = customerID },
                            F_SY_SUPP = new F_SY_SUPP() { FSUPPLIERID = Convert.ToString( plan.Supplier) }
                        }
                    };
                    var entity = purchaseBillRoot.Model.FEntity = new List<FentityPurchaseBill>()
                    {
                        new FentityPurchaseBill()
                        {
                            F_SY_MATERIAL=new F_SY_MATERIAL(){FMaterialID=Convert.ToString( plan.CloudMatID) },
                            F_SY_QTY=Convert.ToString( plan.Qty),
                            F_SY_UNITPRICE=Convert.ToString(plan.Price),
                            F_SY_Amount=Convert.ToString(plan.Price*plan.Qty),
                            F_SY_PLEntryID=Convert.ToString(plan.EntryID)
                        }
                    };
                    if (iResult == 1)
                    {
                        httpClient = new HttpClient();
                        httpClient.Url = "http://192.168.1.231/K3Cloud/Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save.common.kdsvc";
                        parameters = new List<object>();
                        parameters.Add("SY_PURCHASEBILL");
                        parameters.Add(JsonConvert.SerializeObject(purchaseBillRoot));
                        httpClient.Content = JsonConvert.SerializeObject(parameters);
                        var result = httpClient.AsyncRequest();
                    }
                }
            }

        }
    }





    public class Pubulish
    {
        public delegate String[] ReturnInfo();
        public event ReturnInfo ButtonClick;
        public string[] Return()
        {
            if (ButtonClick != null)
            {
                return ButtonClick();
            }
            else
                return new string[4];
        }

    }





}
