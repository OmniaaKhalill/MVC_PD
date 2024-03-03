using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_PD.Models;
using MVC_PD.Repository;

namespace MVC_PD.Controllers
{
    public class DepartmentController : Controller
    {
       
        DepartmentRepo deptRepo = new();
        public IActionResult Index()
        {
            var model = deptRepo.GetAall();
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
            if (ModelState.IsValid)
            {
                deptRepo.Add(model);
                
                return RedirectToAction("Index");
            }
            else
            {
               
                return View(model);
            }
        }

        public IActionResult Details(int? id) //nullble to ensure i sent a value
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = deptRepo.GetById(id.Value);
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
            var model = deptRepo.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Department dept, int id)
        {
           
            dept.DeptId = id;
            deptRepo.Update(dept);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //var model = deptRepo.GetById(id.Value);
            //if (model == null)
            //{
            //    return NotFound();
            //}

            deptRepo.Remove(id.Value);
      
            return RedirectToAction("Index");
        }



        
    }
}
