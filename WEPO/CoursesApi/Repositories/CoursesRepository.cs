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
                                Semester = c.Semester,
                                NumberOfStudents = (from a in _db.CourseNStudent 
                                                    where a.CourseName == c.name && a.Semester == c.Semester
                                                    select a).Count()
                            }).ToList();
        
            return courses;
        }
        public CourseDetailsDTO GetCourseDetails(int id)
        {
            var courses = (from c in _db.Courses
                            where c.id == id
                            select new CourseDetailsDTO{
                                name = c.name,
                                StartDate = c.StartDate,
                                EndDate = c.EndDate,
                                Students = (from a in _db.CourseNStudent
                                            join s in _db.Students on a.StudentId equals s.id
                                            where c.name == a.CourseName && a.Semester == c.Semester
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
