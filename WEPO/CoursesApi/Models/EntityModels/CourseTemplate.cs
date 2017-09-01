using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models.EntityModels
{
    public class CourseTemplate
    {
        [Key]
        public string name { get; set; }
        public string CourseID { get; set; }
    }
}