using System;
using System.Collections.Generic;
using CoursesApi.Models.DTOModels;

namespace CoursesApi.Services
{
    public interface ICoursesService
    {
        IEnumerable<CourseDTO> GetCourses();
        CourseDetailsDTO GetCourseDetails(int id);
    }
    
}
