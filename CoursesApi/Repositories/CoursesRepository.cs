using System;
using System.Collections.Generic;
using CoursesApi.Models.DTOModels;
using System.Linq;
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
                                Name = c.name
                            }).ToList();
        
            return courses;
        }
        public CourseDetailsDTO GetCourseDetails(int id)
        {
            return null;
            
        }
    }
}
