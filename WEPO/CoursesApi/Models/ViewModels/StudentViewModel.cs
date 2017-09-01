using System.ComponentModel.DataAnnotations;

namespace CoursesApi.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long SSN { get; set; }
    }
}