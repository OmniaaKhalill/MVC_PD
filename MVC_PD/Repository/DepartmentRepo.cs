using MVC_PD.Models;

namespace MVC_PD.Repository
{
    
    public class DepartmentRepo
    {
        PDContext db = new ();

        public List<Department> GetAall()
        {
            Console.WriteLine("dept list requird");
        
           
            return [.. db.Departments.Where(d=>d.Status==true)];
       



        }

        public Department GetById(int id) 
        {

            return db.Departments.SingleOrDefault(d => d.DeptId == id);
        }

        public void Add(Department dept)        
        {
            db.Departments.Add(dept);
            int added =db.SaveChanges();
            if (added > 0)
            {
                Console.WriteLine("one row effected");
            }
            else
            {
                Console.WriteLine("no rows effected");
            }
        }

        public void Update(Department dept)
        {
            db.Departments.Update(dept);
            db.SaveChanges();

        }

        public void Remove(int id)
        {
            var dept = GetById(id);
            dept.Status = false;

            //db.Departments.Update(dept);
            db.SaveChanges();

        }


    }
}
