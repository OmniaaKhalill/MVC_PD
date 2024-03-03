using MVC_PD.Models;

namespace MVC_PD.ViewModel
{
    public class StudentDepartmentsViewModel
    {
        public Student Student { get; set; }
        
        public IEnumerable<Department> Departments {  get; set; }  
    }
}
