using Microsoft.AspNetCore.Mvc;
using MVC_PD.Models;
using MVC_PD.My_Filters;

namespace MVC_PD.Controllers
{
    [MyExecptionHandling]
    public class CourseController : Controller
    {
        PDContext db = new();
        public IActionResult Index()
        {
            var model = db.Courses.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course model)
        {
            if (model.Id != 0 && model.Crs_Name?.Length > 2)
            {
                db.Courses.Add(model);
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
            var model = db.Courses.SingleOrDefault(a => a.Id == id);
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
            var model = db.Courses.FirstOrDefault(a => a.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Course crs, int? id)
        {
            var old = db.Courses.FirstOrDefault(a => a.Id == id);
            old.Crs_Name = crs.Crs_Name;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = db.Courses.FirstOrDefault(a => a.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            db.Courses.Remove(model);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}
