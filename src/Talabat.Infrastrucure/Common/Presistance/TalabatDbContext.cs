using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Domain.identity;
using Talabat.Domain.product;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;
using Talabat.Domain.order;

public class TalabatDbContext : IdentityDbContext<ApplicationUser>
{
    public TalabatDbContext(DbContextOptions<TalabatDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(x => x.Id); 

            entity.HasOne(x => x.ProductBrand)
                  .WithMany(x => x.Products)
                  .HasForeignKey(x => x.ProductBrandId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.ProductType)
                  .WithMany(x => x.products)
                  .HasForeignKey(x => x.ProductTypeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(x => x.Name).IsRequired();
            entity.Property(x => x.Description).IsRequired();
            entity.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(x => x.PictureUrl).IsRequired();
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(X => X.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(x => x.Product)
                  .WithMany() 
                  .HasForeignKey(x => x.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductBrand>(entity =>
        {
            entity.HasMany(x => x.Products)
                  .WithOne(x => x.ProductBrand);
            entity.Property(x => x.Name).IsRequired();
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasMany(x => x.products)
                  .WithOne(x => x.ProductType);
            entity.Property(x => x.Name).IsRequired();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(O => O.Status)
                  .HasConversion(
                      OS => OS.ToString(),
                      OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            entity.Property(X => X.SubTotal).HasColumnType("decimal(18,2)");

            entity.OwnsOne(X => X.ShippingAddress, X => X.WithOwner());

            entity.HasOne(o => o.applicationUser)
                  .WithMany(u => u.Orders)  
                  .HasForeignKey(o => o.UserId)  
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(u => u.Address)
                    .WithOne(a => a.User)
                    .HasForeignKey<Address>(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.Property(X => X.Cost).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<IdentityRole>().ToTable("Roles"); 
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItemes { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
} 