namespace ForVBDLL
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.seachButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Picture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MaterialName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Supplier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameTextBox.Location = new System.Drawing.Point(22, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(690, 35);
            this.nameTextBox.TabIndex = 0;
            // 
            // seachButton
            // 
            this.seachButton.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.seachButton.Location = new System.Drawing.Point(718, 14);
            this.seachButton.Name = "seachButton";
            this.seachButton.Size = new System.Drawing.Size(450, 33);
            this.seachButton.TabIndex = 1;
            this.seachButton.Text = "搜索";
            this.seachButton.UseVisualStyleBackColor = true;
            this.seachButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Picture,
            this.MaterialName,
            this.Supplier,
            this.Price,
            this.ID});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(22, 53);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1146, 586);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // Picture
            // 
            this.Picture.DisplayIndex = 3;
            this.Picture.Text = "";
            this.Picture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Picture.Width = 160;
            // 
            // MaterialName
            // 
            this.MaterialName.DisplayIndex = 0;
            this.MaterialName.Text = "品名";
            this.MaterialName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaterialName.Width = 503;
            // 
            // Supplier
            // 
            this.Supplier.DisplayIndex = 1;
            this.Supplier.Text = "供应商";
            this.Supplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Supplier.Width = 193;
            // 
            // Price
            // 
            this.Price.DisplayIndex = 2;
            this.Price.Text = "价格";
            this.Price.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Price.Width = 284;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(160, 160);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ID
            // 
            this.ID.Width = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 651);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.seachButton);
            this.Controls.Add(this.nameTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button seachButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader MaterialName;
        private System.Windows.Forms.ColumnHeader Supplier;
        private System.Windows.Forms.ColumnHeader Price;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColumnHeader Picture;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader ID;
    }
}