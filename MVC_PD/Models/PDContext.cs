using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace MVC_PD.Models
{
    public class PDContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; initial catalog=OmniaSdb; Integrated Security=True;Trust Server Certificate=True");
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(c=> new {c.StudentId , c.CourseId }
                
                );
            base.OnModelCreating(modelBuilder); 
        }
    }
}
