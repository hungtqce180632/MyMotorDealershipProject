namespace MyMotorDealership.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Models;

    public class MotorDealershipDbContext : IdentityDbContext<ApplicationUser>
    {
        public MotorDealershipDbContext(DbContextOptions<MotorDealershipDbContext> options)
            : base(options)
        {
        }

        public DbSet<Motor> Motors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ExtraType> ExtraTypes { get; set; }

        public DbSet<MotorExtra> MotorExtras { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Motor>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Motors)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Motor>()
                .HasOne(c => c.FuelType)
                .WithMany(ft => ft.Motors)
                .HasForeignKey(c => c.FuelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Motor>()
                .HasOne(c => c.TransmissionType)
                .WithMany(tt => tt.Motors)
                .HasForeignKey(c => c.TransmissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Motor>()
                .HasOne(c => c.Post)
                .WithOne(p => p.Motor)
                .HasForeignKey<Post>(p => p.MotorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<Motor>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            builder
                .Entity<Post>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Extra>()
                .HasOne(e => e.Type)
                .WithMany(et => et.Extras)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MotorExtra>()
                .HasKey(ce => new { ce.MotorId, ce.ExtraId });

            builder
                .Entity<MotorExtra>()
                .HasOne(ce => ce.Motor)
                .WithMany(c => c.MotorExtras)
                .HasForeignKey(ce => ce.MotorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MotorExtra>()
                .HasOne(ce => ce.Extra)
                .WithMany(e => e.MotorExtras)
                .HasForeignKey(ce => ce.ExtraId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
