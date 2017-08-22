using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class Course
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String TemplateID { get; set; }
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public List<Student> Students = new List<Student>();
    }
}
