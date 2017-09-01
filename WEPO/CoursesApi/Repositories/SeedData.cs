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
                    new StudentTemplate {id = 1, Name = "Jón Jónsson", SSN = 1234567890},
                    new StudentTemplate {id = 2, Name = "Guðrún Jónsdóttir", SSN = 9876543210 },
                    new StudentTemplate {id = 3, Name = "Gunnar Sigurðsson", SSN = 6543219870 },
                    new StudentTemplate {id = 4, Name = "Jóna Halldórsdóttir", SSN = 4567891230}

                };

                foreach (StudentTemplate s in students)
                {
                    
                    context.Students.Add(s);
                }

                context.SaveChanges();

                var courses = new CourseTemplate[]
                {
                    new CourseTemplate{id = 1,CourseID = "T-514-VEFT", name = "Web services", Semester = 20173, StartDate = DateTime.Parse("2017-04-01"), EndDate = DateTime.Parse("2018-02-12")},
                    new CourseTemplate{id = 2,CourseID = "T-514-VEFT", name = "Web services", Semester = 20163, StartDate = DateTime.Parse("2016-04-01"), EndDate = DateTime.Parse("2017-02-12")}, 
                    new CourseTemplate{id = 3,CourseID = "T-111-PROG", name = "Programming", Semester = 20173, StartDate = DateTime.Parse("2017-04-01"), EndDate = DateTime.Parse("2018-02-12")}
                };
                foreach (CourseTemplate c in courses)
                {
                    context.Add(c);
                }
                context.SaveChanges();

                var  studentIncourses = new CourseNStudent[]
                {
                    new CourseNStudent{id = 1, CourseName = "Web services",  StudentId =  1, Semester = 20163},
                    new CourseNStudent{id = 2, CourseName = "Programming",   StudentId =  1, Semester = 20173},
                    new CourseNStudent{id = 3, CourseName = "Web services",  StudentId =  2, Semester = 20163},
                    new CourseNStudent{id = 4, CourseName = "Web services",  StudentId =  1, Semester = 20163},
                    new CourseNStudent{id = 5, CourseName = "Web services",  StudentId =  3, Semester = 20163}

                };
                foreach (CourseNStudent c in studentIncourses)
                {
                    context.Add(c);
                }
                context.SaveChanges();
            }
        }

    }

}
