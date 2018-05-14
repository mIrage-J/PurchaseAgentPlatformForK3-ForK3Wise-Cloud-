namespace ForVBDLL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;

    [Table("T_LMJ_CloudPriceBill", Schema = "dbo")]
    public partial class PriceModel
    {
        public long? OriMaterialID { get; set; }

        public string OriMaterialName { get; set; }

        public decimal? OriPrice { get; set; }

        public long? SupplyID { get; set; }

        public string SupplyName { get; set; }

        public long? CustomerOrgID { get; set; }

        public long? OriEntryID { get; set; }

        public long ID { get; set; }

        [Column("orimatbmp")]
        public byte[] OriBmp { get; set; }
    }
}
