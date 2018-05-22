namespace ForVBDLL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PlanPush : DbContext
    {
        public PlanPush()
            : base("name=PlanPush")
        {
            Configuration.UseDatabaseNullSemantics = true;
        }

        public virtual DbSet<ToCloudPlan> ToCloudPlan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToCloudPlan>()
                .Property(e => e.Price)
                .HasPrecision(23, 10);

            modelBuilder.Entity<ToCloudPlan>()
                .Property(e => e.PlanPrice)
                .HasPrecision(23, 10);
        }
    }
}
