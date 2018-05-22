using ForVBDLL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using static System.Windows.Forms.ListViewItem;
using System.Data.SqlClient;

namespace ForVBDLL
{
    public partial class Form1 : Form
    {
        private String[] param;
        private PriceContext context;
        private string keyWord;

        public Form1(Pubulish pubulish,string keyWord)
        {
            InitializeComponent();
            pubulish.ButtonClick += Pubulish_ButtonClick;
            context = new PriceContext();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            this.keyWord = keyWord;
        }

        private string[] Pubulish_ButtonClick()
        {
            return param??new string[4];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nameTextBox.Text = keyWord;
            LoadData(new List<string> { keyWord });
            

        }

        /// <summary>
        /// 窗体加载时无搜索关键字加载商品
        /// </summary>
        private void OriginDataLoad()
        {
            LoadData(new List<string>());
        }

        /// <summary>
        /// 根据关键字搜索商品后展示在ListView中
        /// </summary>
        /// <param name="keys"></param>
        private void LoadData(IEnumerable<string> keys)
        {
            Func<PriceModel, bool> func = delegate (PriceModel p) { return Search(p, keys); };
            
            var data = context.Prices
                    .Where(func)
                    .Select(p => new { Name = p.OriMaterialName, Supply = p.SupplierName, Price = p.OriPrice, Img = p.OriBmp,ID=p.ID });
            listView1.BeginUpdate();
            listView1.Items.Clear();
            imageList1.Images.Clear();
            foreach (var d in data)
            {
                MemoryStream stream = new MemoryStream();
                if (d.Img != null)
                {
                    byte[] bytes = new byte[d.Img.Length];
                    for (int i = 0; i < d.Img.Length; i++)
                    {
                        bytes[i] = (byte)d.Img[i];
                    }
                    stream = new MemoryStream(bytes);
                    imageList1.Images.Add(d.Name, System.Drawing.Image.FromStream(stream));
                }



                var li = new ListViewItem() { Text = "" };
               
                
                li.SubItems.Add(new ListViewSubItem() { Name = "MaterialName", Text = d.Name });
                li.SubItems.Add(new ListViewSubItem() { Name = "Supplier", Text = d.Supply } );
                li.SubItems.Add(new ListViewSubItem() { Name = "Price", Text = d.Price.ToString() });
                li.SubItems.Add(new ListViewSubItem() { Name = "ID", Text = d.ID.ToString() });
                if (imageList1.Images.Keys.Contains(d.Name))
                    li.ImageIndex = imageList1.Images.IndexOfKey(d.Name);

                listView1.Items.Add(li
                    );
            }
            listView1.EndUpdate();
        }

        private bool Search(PriceModel model, IEnumerable<string> keys)
        {
            bool isContain = true;
            if (keys.Count() > 0)
                foreach (string key in keys)
                {
                    if (!model.OriMaterialName.Contains(key))
                        isContain = false;
                }
            return isContain;
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var inputText = nameTextBox.Text;
            if (inputText == "")
                OriginDataLoad();
            else
            {
                var keys = inputText.Split(' ').ToList();
                LoadData(keys);
            }

        }

        

        /// <summary>
        /// 一旦双击，就查找物料库是否有此物料，没有就调用存储过程新增,然后触发事件返回Wise物料ID，供应商名称，价格，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender,EventArgs e)
        {
            if(listView1.Focused)
            {
                var ID = Convert.ToInt64( listView1.FocusedItem.SubItems["ID"].Text);
                var cloudMaterialID = context.Prices.Where(p => p.ID == ID).Select(p => p.OriMaterialID).FirstOrDefault();
                var supplierName = context.Prices.Where(p => p.ID == ID).Select(p => p.SupplierName).FirstOrDefault();
                var price = context.Prices.Where(p => p.ID == ID).Select(p => p.OriPrice).FirstOrDefault();
                SqlParameter[] param =
                {
                    new SqlParameter("@MatID", cloudMaterialID)
                };

                var wiseMaterialID= context.Database.SqlQuery<Int64>("Exec proc_SY_InsertMaterial @MatID",param).ToList().FirstOrDefault();
                this.param = new string[4] { wiseMaterialID.ToString(), supplierName.ToString(), price.ToString(),ID.ToString() };
                Pubulish_ButtonClick();
                this.Close();
            }
        }

        private void ReturnData(string info)
        {
            MessageBox.Show(info);
        }
    }
}
