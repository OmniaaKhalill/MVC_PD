using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_PD.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="*")]// effects db
        [StringLength(10 , MinimumLength =3,ErrorMessage ="violate string length")] //max is 10 min=3//only max  effects db
        public string Name { get; set; }

        [Range(20,30)] //min=20 max=30
        public int Age { get; set; }
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]{2,4}")]
        public string Email { get; set; }

        // public Image Image { get; set; }

        [ForeignKey("Department")]
        [Display(Name ="Department")]
        public int DeptNo { get; set; }
        public List<StudentCourse> StudentCourse { get; set; }

        public Department Department { get; set; }
    }
}
