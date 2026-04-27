using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                builder.OwnsMany(o => o.Items, item =>
                {
                    item.WithOwner().HasForeignKey("OrderId");

                    item.Property(i => i.Name).IsRequired();
                    item.Property(i => i.Price).IsRequired();

                    item.Property(i => i.Type)
                        .HasConversion<int>()
                        .IsRequired();

                    item.Property<int>("Id");
                    item.HasKey("Id");
                });

                builder.Navigation(o => o.Items)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
