using CoursesApi.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Repositories
{   
     public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {}

        public DbSet<CourseTemplate> Courses { get; set; }
        public DbSet<StudentTemplate> Students { get; set; }
        public DbSet<CourseNStudent> CourseNStudent { get; set; }
    }
}
