// using Microsoft.EntityFrameworkCore;

// namespace OnlineStore.Models
// {
//     public class OnlineStoreContext :DbContext
//     {
//         public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options): base(options)
//         {

//         }

// public DbSet<Product> Products {get;set;}
//     }
// }




using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models
{
    public class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options)
        : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        // Other DbSets...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
