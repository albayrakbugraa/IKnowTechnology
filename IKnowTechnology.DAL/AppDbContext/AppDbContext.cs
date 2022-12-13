using IKnowTechnology.CORE.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.DAL.DbContext
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<CORE.Entities.TaskList> TaskLists { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // EntityConfigurations klasöründeki EntityTypeConfigurationdan türetilen bütün configleri otomatik alacak
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Server=.;Database=IKnowTechnology;Trusted_Connection=True;MultipleActiveResultSets=True;";
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
