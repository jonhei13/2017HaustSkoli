using System;

namespace CoursesApi.Models.EntityModels
{
    public class CourseNStudent
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}