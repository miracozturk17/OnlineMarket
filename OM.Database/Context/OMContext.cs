using System;
using System.Data.Entity;
using OM.Entities.EntityClass;

namespace OM.Database.Context
{
    public class OMContext : DbContext,IDisposable
    {
        public OMContext() : base("OnlineMarket")
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Configration> Configrations{ get; set; }
    }
}
