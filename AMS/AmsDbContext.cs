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
        public DbSet<CourseTeacher> CourseTeacher { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ClassTimeTable> ClassTimeTable { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

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
            builder.Entity<CourseStudent>().ToTable("CourseStudents");
            builder.Entity<CourseTeacher>().ToTable("CourseTeachers");
            builder.Entity<CourseStudent>().HasKey(cs => new { cs.UserId, cs.CourseId });

            builder.Entity<CourseStudent>()
                .HasOne(c => c.Course)
                .WithMany(s => s.Users)
                .HasForeignKey(c => c.CourseId);

            builder.Entity<CourseStudent>()
                .HasOne(s => s.User)
                .WithMany(c => c.Courses)
                .HasForeignKey(s => s.UserId);


            //builder.Entity<Course>()
            //    .HasOne(c => c.User)
            //    .WithMany(u => u.Courses)
            //    .HasForeignKey(u => u.UserId);

            //builder.Entity<User>()
            //    .HasOne(r => r.Role)
            //    .WithOne(u => u.Users)
            //    .HasForeignKey<User>(ad => ad.RoleId);
            base.OnModelCreating(builder);
        }
    }
}
