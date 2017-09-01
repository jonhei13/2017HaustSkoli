using System;
using System.Collections.Generic;
using CoursesApi.ViewModels;

namespace CoursesApi.Models.DTOModels
{
    public class CourseDetailsDTO
    {
        public string name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<StudentViewModel> Students { get; set; }

    }
}