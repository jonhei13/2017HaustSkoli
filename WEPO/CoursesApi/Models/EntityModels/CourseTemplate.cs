using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models.EntityModels
{
    public class CourseTemplate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string CourseID { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}