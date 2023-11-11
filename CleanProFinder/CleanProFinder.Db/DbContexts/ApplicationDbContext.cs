using CleanProFinder.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Db.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<ServiceUser> ServiceUsers { get; set; }
        public virtual DbSet<CleaningServiceProvider> CleaningServiceProviders { get; set; }
        public virtual DbSet<Premise> Premises { get; set; }
        public virtual DbSet<CleaningService> CleaningServices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CleaningServiceServiceProvider>()
                .HasKey(cssp => new { cssp.CleaningServiceId, cssp.CleaningServiceProviderId });

            modelBuilder.Entity<CleaningServiceServiceProvider>()
                .HasOne(cssp => cssp.CleaningService)
                .WithMany(cs => cs.CleaningServiceServiceProviders)
                .HasForeignKey(cssp => cssp.CleaningServiceId);

            modelBuilder.Entity<CleaningServiceServiceProvider>()
                .HasOne(cssp => cssp.CleaningServiceProvider)
                .WithMany(csp => csp.CleaningServiceServiceProviders)
                .HasForeignKey(cssp => cssp.CleaningServiceProviderId);
        }
    }
}
