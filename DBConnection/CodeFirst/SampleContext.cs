using System.Data.Entity;

namespace CodeFirst
{
    public class SampleContext : DbContext
    {
        public SampleContext() : base("MyShop")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SampleContext>());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .Property(c =>
           c.LastName).IsRequired().HasMaxLength(30);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<VipOrder> VipOrders { get; set; }
    }
}
