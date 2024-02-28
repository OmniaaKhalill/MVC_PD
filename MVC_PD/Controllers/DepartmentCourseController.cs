using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;
using MVC_PD.ViewModel;

namespace MVC_PD.Controllers
{
    public class DepartmentCourseController : Controller
    {
        PDContext db = new PDContext();


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
            DepartmentCourse deptCrs = new DepartmentCourse();
            deptCrs.DeptId = id;
            deptCrs.AllCourses = db.Courses.ToList();
            deptCrs.CrsInDpt = (db.Departments.Include(a => a.Courses).SingleOrDefault(a => a.DeptId == id)).Courses.ToList();
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
            
            List<StudentCourse> std_crs = new List<StudentCourse>();
            var stds = db.Students.Where(s=>s.DeptNo==DeptId).ToList();
           
            foreach (var s in stds)
            {
                StudentCourse sc = new StudentCourse();
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
                    StudentCourse sc = new StudentCourse() { StudentId = item.Key, CourseId = CrsId, Degree = item.Value };
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
