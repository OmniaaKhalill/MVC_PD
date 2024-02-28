using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;
using System.Runtime.Intrinsics.Arm;

namespace MVC_PD.Controllers
{
    public class StudentController : Controller
    {
        PDContext db = new PDContext();
        public IActionResult Index()
        {
            var model = db.Students.Include(a=>a.Department).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var Deptlst =  db.Departments.ToList();
            ViewBag.Deptlst = Deptlst;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model) //model binder   create dictionry==> model state check for validation
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            ViewBag.Deptlst = db.Departments.ToList();

            return View(model);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Students.Include(a=>a.Department).SingleOrDefault(a => a.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }

        public IActionResult Details2(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Students.Include(a => a.Department).SingleOrDefault(a => a.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return PartialView(model);

        }


        public IActionResult Edit(int? id) //get first
        {

            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Students.Include(a => a.Department).SingleOrDefault(a => a.Id == id);
            var Deptlst = db.Departments.ToList();
            ViewBag.Deptlst = Deptlst;

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Student  std)
        {
            db.Students.Update(std);
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Departments.FirstOrDefault(a => a.DeptId == id);
            if (model == null)
            {
                return NotFound();
            }

            db.Departments.Remove(model);
            db.SaveChanges();


            return RedirectToAction("Index");
        }


    }
}
