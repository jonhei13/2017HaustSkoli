using System.Collections.Generic;
using CoursesApi.Models.DTOModels;

namespace CoursesApi.Repositories
{
    public interface ICoursesRepository
    {
        IEnumerable<CourseDTO> GetCourses();
        CourseDetailsDTO GetCourseDetails(int id);
    }
}