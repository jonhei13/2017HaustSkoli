using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.ViewModels
{
    public class CourseViewModel
    {
        public int ID { get; set; }
        public String CourseID { get; set; }

        public IEnumerable<StudentViewModel> Students;
    }
}
