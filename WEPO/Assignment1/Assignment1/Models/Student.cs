using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class Student
    {
        [Required]
        public long SSN { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
