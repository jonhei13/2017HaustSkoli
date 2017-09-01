using System;
using System.Collections.Generic;
using CoursesApi.Models.DTOModels;
using System.Linq;
using CoursesApi.ViewModels;

namespace CoursesApi.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly AppDataContext _db;
        public CoursesRepository(AppDataContext db)
        {
            _db = db;
        }
        public IEnumerable<CourseDTO> GetCourses()
        {
            var courses = (from c in _db.Courses 
                            select new CourseDTO{
                                Name = c.name,
                                NumberOfStudents = (from a in _db.Students
                                                    join s in _db.CourseNStudent on a.id equals s.StudentId
                                                    where c.name == s.CourseName
                                                    select a).Count()
                            }).ToList();
        
            return courses;
        }
        public CourseDetailsDTO GetCourseDetails(int id)
        {
            var courses = (from c in _db.Courses
                            join b in _db.CourseNStudent on c.CourseID equals b.CourseName
                            select new CourseDetailsDTO{
                                name = c.name,
                                StartDate = b.StartDate,
                                EndDate = b.EndDate,
                                Students = (from a in _db.CourseNStudent
                                            join s in _db.Students on a.StudentId equals s.id
                                            where c.name == a.CourseName
                                            select new StudentViewModel
                                            {
                                                Name = s.Name,
                                                SSN = s.SSN,
                                            }).ToList()
                            }).SingleOrDefault();
            return courses;
        }
    }
}
