using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PD.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int StudentId { get; set;}

        [ForeignKey("Course")]
        public int CourseId { get; set;}
        public int? Degree { get; set;}  
        public Student Student { get; set;}
        public Course Course { get; set;}
        

    }
}
