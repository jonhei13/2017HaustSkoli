using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.DTO
{
    public class CourseDTO
    {
        [Required]
        public String CourseID { get; set; }
    }
}
