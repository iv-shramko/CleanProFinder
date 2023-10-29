﻿using CleanProFinder.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanProFinder.Db.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<ServiceUser> ServiceUsers { get; set; }
        public virtual DbSet<CleaningServiceProvider> CleaningServiceProviders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
