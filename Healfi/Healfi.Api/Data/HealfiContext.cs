using System;
using Healfi.Api.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Data
{
    public class HealfiContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        //public virtual DbSet<Origin> Origins { get; set; }
        
        public HealfiContext(DbContextOptions<HealfiContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}