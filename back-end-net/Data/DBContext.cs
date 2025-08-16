using Microsoft.EntityFrameworkCore;
using back_end_net.Models;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<order_product>()
        //    .HasKey(sc => new { sc.product_id, sc.order_id }); // Composite PK

        //modelBuilder.Entity<order_product>()
        //    .HasOne(sc => sc.Product)
        //    .WithMany(s => s.order_products)
        //    .HasForeignKey(sc => sc.product_id);

        //modelBuilder.Entity<order_product>()
        //    .HasOne(sc => sc.Order)
        //    .WithMany(c => c.order_products)
        //    .HasForeignKey(sc => sc.order_id);

        //modelBuilder.Entity<order_product>()
        //    .Property(sc => sc.quantity)
        //    .HasDefaultValueSql("0");

        //modelBuilder.Entity<order_product>()
        //    .Property(sc => sc.created_at)
        //    .HasDefaultValueSql("GETDATE()");

        //modelBuilder.Entity<order_product>()
        //    .Property(sc => sc.updated_at)
        //    .HasDefaultValueSql("GETDATE()");
    }

    public DbSet<Product> products { get; set; } = default!;
    public DbSet<Order> orders { get; set; } = default!;
    public DbSet<order_product> order_product { get; set; } = default!;
}
