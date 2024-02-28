using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;

namespace MVC_PD.Controllers
{
    public class DepartmentController : Controller
    {
        PDContext db = new PDContext();
        public IActionResult Index()
        {
            var model = db.Departments.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (model.DeptId != 0 && model.DeptName?.Length > 2)
            {
                db.Departments.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public IActionResult Details(int? id) //nullble to ensure i sent a value
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Departments.SingleOrDefault(a => a.DeptId == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Edit(int? id) //get first
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

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Department dept, int? id)
        {
            var old = db.Departments.FirstOrDefault(a => a.DeptId == id);
            old.DeptName = dept.DeptName;
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
