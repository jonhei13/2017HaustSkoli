
using System;
using System.ComponentModel.DataAnnotations;
namespace CoursesApi.ViewModels
{
    public class CourseViewModel
    {
        [Required]
        public int Semester  { get; set; }
        [Required]
        public string CourseID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}