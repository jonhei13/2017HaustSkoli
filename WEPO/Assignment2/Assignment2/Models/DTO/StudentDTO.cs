using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.DTO
{
    public class StudentDTO
    {
        [Required]
        public String SSN { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
