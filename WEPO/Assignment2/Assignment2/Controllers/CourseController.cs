using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        //GET api/courses
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        //GET /api/courses/id
        


        //DELETE /api/courses/id


        //POST api/courses/id/students


    }
}
