namespace ForVBDLL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PriceContext : DbContext
    {
        public PriceContext()
            : base("name=PriceContext")
        {
            Configuration.UseDatabaseNullSemantics = true;
        }

        public virtual DbSet<PriceModel> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceModel>()
                .Property(e => e.OriMaterialName)
                .IsUnicode(false);

            modelBuilder.Entity<PriceModel>()
                .Property(e => e.OriPrice)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PriceModel>()
                .Property(e => e.SupplierName)
                .IsUnicode(false);

            
        }
    }
}
