using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AMS
{
    public class AmsDbContext : DbContext
    {
        private string _connectionString;
        private string _assemblyName;

        public AmsDbContext()
        {
            _connectionString = "Server=RAYHAN-PC\\SQLEXPRESS;Database=AMS;User Id=ams;Password=123456;";
            _assemblyName = Assembly.GetExecutingAssembly().FullName;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Course> Course { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_assemblyName));
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>()
            //    .HasOne(r => r.Role)
            //    .WithOne(u => u.Users)
            //    .HasForeignKey<User>(ad => ad.RoleId);
            //base.OnModelCreating(builder);
        }
    }
}
