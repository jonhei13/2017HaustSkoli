using System;
using System.Linq;
using CoursesApi.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesApi.Repositories
{
    public static class SeedData
    {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new AppDataContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDataContext>>()))
                {

                if (context.Courses.Any())
                {
                    return; // DB has been seeded
                }

                var students = new StudentTemplate[]
                {
                    new StudentTemplate {Name = "Jón Jónsson", SSN = 1234567890},
                    new StudentTemplate {Name = "Guðrún Jónsdóttir", SSN = 9876543210 },
                    new StudentTemplate { Name = "Gunnar Sigurðsson", SSN = 6543219870 },
                    new StudentTemplate { Name = "Jóna Halldórsdóttir", SSN = 4567891230}

                };

                foreach (StudentTemplate s in students)
                {
                    
                    context.Students.Add(s);
                }

                context.SaveChanges();

                var courses = new CourseTemplate[]
                {
                    new CourseTemplate{CourseID = "T-514-VEFT", name = "Web services", Semester = 20173, StartDate = DateTime.Parse("2017-04-01"), EndDate = DateTime.Parse("2018-02-12")},
                    new CourseTemplate{CourseID = "T-514-VEFT", name = "Web Services", Semester = 20163, StartDate = DateTime.Parse("2016-04-01"), EndDate = DateTime.Parse("2017-02-12")}, 
                    new CourseTemplate{CourseID = "T-111-PROG", name = "Programming", Semester = 20173, StartDate = DateTime.Parse("2017-04-01"), EndDate = DateTime.Parse("2018-02-12")}
                };
                foreach (CourseTemplate c in courses)
                {
                    context.Add(c);
                }
                context.SaveChanges();
            }
        }

    }

}
