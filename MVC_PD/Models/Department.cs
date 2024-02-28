using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PD.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Department id")] //for tag helper
        public int DeptId { get; set; }

        [Display(Name = "Department Name")]
        public string DeptName { get; set; }

        public ICollection<Student> Students { get; set; } =new HashSet<Student>();
        public List<Course> Courses{ get; set; }

    }
}
