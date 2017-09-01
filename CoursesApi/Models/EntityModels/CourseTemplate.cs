using System;

namespace CoursesApi.Models.EntityModels
{
    public class CourseTemplate
    {
        public string name { get; set; }
        public string CourseID { get; set; }
        public long Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}