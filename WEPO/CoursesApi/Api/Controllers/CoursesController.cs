using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;

        }
        // GET api/courses
        [HttpGet]
        [Route("api/courses")]
        public IActionResult GetCourses()
        {
            var courses = _coursesService.GetCourses();
            return Ok(courses);
        }

        // GET api/courses/id
        [HttpGet]
        [Route("api/courses/{courseId}")]
        public IActionResult GetCourseDetails(int courseId)
        {
            var courses = _coursesService.GetCourseDetails(courseId);
            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }

    }
}
