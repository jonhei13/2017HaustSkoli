using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;

        }
        // GET api/courses
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _coursesService.GetCourses();
            return Ok(courses);
        }

    }
}
