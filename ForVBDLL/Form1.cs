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
using System.Drawing;
using System.IO;
using static System.Windows.Forms.ListViewItem;
using System.Data.SqlClient;

namespace ForVBDLL
{
    public partial class Form1 : Form
    {
        public String Info { get; private set; }
        private PriceContext context;


        public Form1(Pubulish pubulish)
        {
            InitializeComponent();
            pubulish.ButtonClick += Pubulish_ButtonClick;
            context = new PriceContext();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;            
        }

        private string Pubulish_ButtonClick()
        {
            return Info;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OriginDataLoad();

        }

        private void OriginDataLoad()
        {
            LoadData(new List<string>());
        }

        private void LoadData(IEnumerable<string> keys)
        {
            Func<PriceModel, bool> func = delegate (PriceModel p) { return Search(p, keys); };
            
            var data = context.Prices
                    .Where(func)
                    .Select(p => new { Name = p.OriMaterialName, Supply = p.SupplyName, Price = p.OriPrice, Img = p.OriBmp,ID=p.ID });
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_DoubleClick(object sender,EventArgs e)
        {
            if(listView1.Focused)
            {
                var ID = Convert.ToInt64( listView1.FocusedItem.SubItems["ID"].Text);
                var cloudMaterialID = context.Prices.Where(p => p.OriEntryID == ID).Select(p => p.OriMaterialID).FirstOrDefault();
                SqlParameter[] param =
                {
                    new SqlParameter("@ID", ID)
                };

                var a= context.Database.SqlQuery<Int64>("Exec proc_SY_InsertMaterial @ID",cloudMaterialID).ToList().FirstOrDefault();
            }
        }

        private void ReturnData(string info)
        {
            MessageBox.Show(info);
        }
    }
}
