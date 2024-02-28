using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_PD.Models
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Crs_Name { get; set; }
        public int Duration { get; set; }
        public List<StudentCourse> StudentCourse { get; set; }
        public List<Department> Departments { get; set; }

    }
}

