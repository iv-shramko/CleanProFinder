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
        public virtual DbSet<CleaningServiceServiceProvider> CleaningServiceServiceProviders { get; set; }
        public virtual DbSet<Request> Requests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Request>(
                r => 
                    r.HasMany(r => r.Services)
                    .WithMany()
            );
        }
    }
}
