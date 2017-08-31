using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class CoursesController : Controller
    {
        private static List<Course> _courses;
        private static List<Student> _students;

        public CoursesController()
        {
            if (_students == null)
            {
                _students = new List<Student>
                {
                    new Student
                    {
                        Name = "Jón Jónsson",
                        SSN = 2902892828
                    },
                    new Student
                    {
                        Name = "lady gaga",
                        SSN = 0230132103
                    }
                };
            }
            if (_courses == null)
            {
                _courses = new List<Course>
                {
                    new Course
                    {
                        ID = 1,
                        Name = "Web services",
                        TemplateID = "T-514-VEFT",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(3)
                    },
                    new Course
                    {
                        ID = 2,
                        Name = "GAGNASKIPAN",
                        TemplateID = "GAG-104",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(2)
                    },
                };
                
            }

        }
        
        // GET api/courses
        [Route("api/courses")]
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Ok(_courses);
        }

        [HttpGet]
        [Route("api/courses/{courseId}")]
        public IActionResult GetCourseByID(int courseId)
        {
            var result = _courses.Where(x => x.ID == courseId).SingleOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("api/courses")]
        public IActionResult CreateCourse([FromBody] Course Item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _courses.Add(Item);
            return Created("create", Item);
        }

        [HttpPut]
        [Route("api/courses/{courseId}")]
        public IActionResult UpdateCourse(int courseId, [FromBody] Course Item)
        {
            var result = _courses.Where(x => x.ID == courseId).SingleOrDefault();
            if (Item == null && Item.ID != courseId)
            {
                return BadRequest();
            }
            if (result == null)
            {
                return NotFound();
            }
            result.Name = Item.Name;
            result.TemplateID = Item.TemplateID;

            return new NoContentResult();
        }


        [HttpDelete]
        [Route("api/courses/{CourseId}")]
        public IActionResult DeleteCourse(int CourseId)
        {
            var remove = _courses.Where(x => x.ID == CourseId).SingleOrDefault();
            if (remove == null)
            {
                return NotFound("Invalid Id");
            }
            _courses.Remove(remove);
            return NoContent();
        }

        
        [HttpPost]
        [Route("api/courses/{CourseId}")]
        public IActionResult AddStudent(int CourseId, [FromBody] Student student)
        {
            var result = _courses.Where(x => x.ID == CourseId).SingleOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            result.Students.Add(student);
            return Created("student", student);
        }

        
        [HttpGet]

        public IActionResult GetStudentsInCourse(int CourseID)
        {
            var result = _courses.Where(x => x.ID == CourseID).SingleOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Students);
        }



    }
}
