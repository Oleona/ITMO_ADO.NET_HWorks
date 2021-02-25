using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace CatCodeFirst
{
    public class CatContext : DbContext
    {
        public CatContext() : base("MyCattery")
        { Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CatContext>()); }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}