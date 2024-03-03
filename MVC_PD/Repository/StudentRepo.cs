using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVC_PD.Models;

namespace MVC_PD.Repository
{
    public class StudentRepo
    {
        PDContext db = new ();

        public List<Student> GetAall()
        {
            Console.WriteLine("dept list requird");


            return db.Students.Where(s => s.Status == true).Include(a => a.Department).ToList();

        }

        public Student GetById(int id)
        {

            return db.Students.Include(a => a.Department).SingleOrDefault(s => s.Id == id);
        }

        public Student getByEmail(string? Email)
        {

            return db.Students.FirstOrDefault(s => s.Email == Email);

        }

        public void Add(Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
        
        }

        public void Update(Student std)
        {
            db.Students.Update(std);
            db.SaveChanges();

        }

        public void Remove(int id)
        {
            var std = GetById(id);
            std.Status = false;
           
            db.SaveChanges();

        }

       

}
    }


