namespace ForVBDLL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("V_LMJ_PushToCloudPlan", Schema = "dbo")]
    public partial class ToCloudPlan
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EntryID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Deman { get; set; }

        public long? CloudMatID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Qty { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal Price { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal PlanPrice { get; set; }

        public long? Supplier { get; set; }

        public string BillNo { get; set; }
    }
}
