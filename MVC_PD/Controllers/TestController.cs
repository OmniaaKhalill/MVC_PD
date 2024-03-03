using Microsoft.AspNetCore.Mvc;
using MVC_PD.My_Filters;

namespace MVC_PD.Controllers
{
    [MyExecptionHandling]
    public class TestController : Controller
    {
       
        public IActionResult Index()
        {
            int l = int.Parse("789sdbbh");
            return Content($"{l}");
        }

        [MyAuthorizeFilter]// can also use [authrize]
        public IActionResult Add()
        { 
            return Content($"add");
        }

        public IActionResult login()
        {
            return Content($"login");
        }

    }
}
