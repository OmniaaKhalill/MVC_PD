using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;
using MVC_PD.Repository;
using MVC_PD.ViewModel;
using System.Runtime.Intrinsics.Arm;

namespace MVC_PD.Controllers
{
    public class StudentController : Controller
    {
        //PDContext db = new PDContext();
        DepartmentRepo deptRepo = new ();
        readonly StudentRepo stdRepo = new ();

        public IActionResult Index()
        {

            var model = stdRepo.GetAall();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
           ViewBag.departments= deptRepo.GetAall();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model) //model binder   create dictionry==> model state check for validation
        {
            if (ModelState.IsValid)
            {
                stdRepo.Add(model);

                return RedirectToAction("Index");
            }
            else
                ViewBag.departments = deptRepo.GetAall();

            return View(model);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = stdRepo.GetById(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }

        //public IActionResult Details2(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var model = db.Students.Include(a => a.Department).SingleOrDefault(a => a.Id == id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(model);

        //}


        public IActionResult Edit(int? id) //get first
        {
//StudentDepartmentsViewModel stdDepts = new StudentDepartmentsViewModel();

            if (id == null)
            {
                return BadRequest();
            }
            var std = stdRepo.GetById(id.Value);
            ViewBag.departments = deptRepo.GetAall();

         
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
                
            stdRepo.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = stdRepo.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            stdRepo.Remove(id.Value);



            return RedirectToAction("Index");
        }



        public IActionResult CheckEmail(string Email, string Name)
        {

            // var eml = db.Students.FirstOrDefault(s => s.Email == Email);
            var std = stdRepo.getByEmail(Email);
            if (std != null)
            {
                //return Json($"{Name}123@iti.com");
                return Json(false);

            }

            return Json(true);

        }
    }
}

