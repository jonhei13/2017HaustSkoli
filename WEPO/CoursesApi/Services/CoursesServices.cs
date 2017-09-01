using System.Collections.Generic;
using CoursesApi.Models.DTOModels;
using CoursesApi.Repositories;

namespace CoursesApi.Services
{
    public class CoursesServices : ICoursesService
    {

        private readonly ICoursesRepository _repo;

        public CoursesServices(ICoursesRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CourseDTO> GetCourses()
        {
            var courses = _repo.GetCourses();
            return courses;
        }
        public CourseDetailsDTO GetCourseDetails(int id)
        {
            var courseDetails = _repo.GetCourseDetails(id);
            return courseDetails;
        }
    }
}