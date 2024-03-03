using MVC_PD.Models;

namespace MVC_PD.ViewModel
{
    public class  DepartmentCourseViewModel
    {
        public int?  DeptId { get; set; }
        public List<Course> AllCourses { get; set; } 
        public List<Course> CrsInDpt { get; set; } 
        public List<Course> CrsNotInDpt { get; set; } 

    }
}
