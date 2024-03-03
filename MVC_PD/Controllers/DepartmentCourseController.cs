using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;
using MVC_PD.My_Filters;
using MVC_PD.ViewModel;

namespace MVC_PD.Controllers
{
    public class DepartmentCourseController : Controller
    {
        readonly PDContext db = new ();

        [MyExecptionHandling]
        public IActionResult ShowCourses(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Departments.Include(a => a.Courses).SingleOrDefault(a => a.DeptId == id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }


        [HttpGet]
        public IActionResult manageCourses(int? id)
        {
            DepartmentCourseViewModel deptCrs = new();
            deptCrs.DeptId = id;
            deptCrs.AllCourses = db.Courses.ToList();
            deptCrs.CrsInDpt = [.. db.Departments.Include(a => a.Courses).SingleOrDefault(a => a.DeptId == id).Courses];
            deptCrs.CrsNotInDpt = deptCrs.AllCourses.Except(deptCrs.CrsInDpt).ToList();

            return View(deptCrs);
        }

        [HttpPost]
        public IActionResult manageCourses(int? id, List<int> coursesToRemove, List<int> coursesToAdd)
        {
            var dept = db.Departments.Include(a => a.Courses).SingleOrDefault(a => a.DeptId == id);

            foreach (var crs_id in coursesToRemove)
            {
                var c = db.Courses.SingleOrDefault(c => c.Id == crs_id);
                dept.Courses.Remove(c);

            }

            foreach (var crs_id in coursesToAdd)
            {
                var c = db.Courses.SingleOrDefault(c => c.Id == crs_id);
                dept.Courses.Add(c);

            }
            db.SaveChanges();

            return RedirectToAction("index", "department");
        }


        [HttpGet]
        public IActionResult AddDegree(int CrsId,int DeptId)
        {
            
            List<StudentCourse> std_crs = new();
            var stds = db.Students.Where(s=>s.DeptNo==DeptId).ToList();
           
            foreach (var s in stds)
            {
                StudentCourse sc = new ();
                sc.Student = new Student();

                sc.StudentId = s.Id;
                sc.Student.Name=s.Name;
                sc.Course = db.Courses.FirstOrDefault(c => c.Id == CrsId);
                sc.Degree = db.StudentCourses.FirstOrDefault(c => c.CourseId == CrsId && c.StudentId == s.Id)?.Degree;
             
                std_crs.Add(sc);

            }
         

            return View(std_crs);
        }

        [HttpPost]
        public IActionResult AddDegree(int CrsId, Dictionary<int, int> degree)
        {

            foreach (var item in degree)
            {
                var std = db.StudentCourses.SingleOrDefault(c => c.CourseId == CrsId && c.StudentId == item.Key);
                if (std == null)
                {
                    StudentCourse sc = new () { StudentId = item.Key, CourseId = CrsId, Degree = item.Value };
                    db.StudentCourses.Add(sc);
                }
                else
                {
                    std.Degree = item.Value;
                }

            }
            db.SaveChanges();
            return RedirectToAction("index","department");
        }
    }
}
